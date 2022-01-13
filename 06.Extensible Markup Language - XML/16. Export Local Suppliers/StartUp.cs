using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using CarDealer.Data;
using CarDealer.DTO.Input;
using CarDealer.Models;
using System;
using System.Xml.Linq;
using CarDealer.DTO.Output;
using CarDealer.Converter;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new CarDealerContext();
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            //string supplierXml = File.ReadAllText("../../../Datasets/suppliers.xml");
            //string partXml = File.ReadAllText("../../../Datasets/parts.xml");
            //string carXml = File.ReadAllText("../../../Datasets/cars.xml");
            //string customerXml = File.ReadAllText("../../../Datasets/customers.xml");
            //string saleXml = File.ReadAllText("../../../Datasets/sales.xml");

            //ImportSuppliers(context, supplierXml);
            //ImportParts(context, partXml);
            //ImportCars(context, carXml);
            //ImportCustomers(context, customerXml);
            //ImportSales(context, saleXml);

            var result = GetLocalSuppliers(context);
            
            Console.WriteLine(result);
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(x => !x.IsImporter)
                .Select(x => new SupplierOutputModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    PartsCount = x.Parts.Count
                })
                .ToList();

            var result = XmlConverter.Serialize(suppliers, "suppliers");
            return result;
        }

        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(x => x.Make == "BMW")
                .Select(x => new BMWOutputModel
                {
                    Id = x.Id,
                    Model = x.Model,
                    TravelledDistance = x.TravelledDistance
                })
                .OrderBy(x => x.Model)
                .ThenByDescending(x => x.TravelledDistance)
                .ToList();

            var result = XmlConverter.Serialize(cars, "cars");
            return result;
        }

        public static string GetCarsWithDistance(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(x => x.TravelledDistance > 2_000_000)
                .Select(x => new CarOutputModel
                {
                    Make = x.Make,
                    Model = x.Model,
                    TravelledDistance = x.TravelledDistance
                })
                .OrderBy(x => x.Make)
                .ThenBy(x => x.Model)
                .Take(10)
                .ToArray();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CarOutputModel[]), new XmlRootAttribute("cars"));

            var textWriter = new StringWriter();

            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            xmlSerializer.Serialize(textWriter, cars, ns);

            var result = textWriter.ToString();
            return result;
        }

        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(SaleInputModel[]), new XmlRootAttribute("Sales"));

            var saleDtos = xmlSerializer.Deserialize(new StringReader(inputXml)) as SaleInputModel[];

            var saleIds = context.Cars.Select(x => x.Id).ToList();

            var sales = saleDtos.Where(x => saleIds.Contains(x.CarId))
                .Select(x => new Sale
                {
                    CarId = x.CarId,
                    CustomerId = x.CustomerId,
                    Discount = x.Discount
                })
                .ToList();

            context.Sales.AddRange(sales);
            context.SaveChanges();


            return $"Successfully imported {sales.Count()}";
        }

        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(CustomerInputModel[]), new XmlRootAttribute("Customers"));

            var customerDtos = xmlSerializer.Deserialize(new StringReader(inputXml)) as CustomerInputModel[];

            var customers = customerDtos.Select(x => new Customer
            {
                Name = x.Name,
                BirthDate = x.BirthDate,
                IsYoungDriver = x.IsYoungDriver
            })
            .ToList();

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Count()}";
        }

        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(CarInputModel[]), new XmlRootAttribute("Cars"));

            var carDtos = xmlSerializer.Deserialize(new StringReader(inputXml)) as CarInputModel[];


            var cars = new List<Car>();
            var partCars = new List<PartCar>();

            foreach(var carDto in carDtos)
            {
                var car = new Car()
                {
                    Make = carDto.Make,
                    Model = carDto.Model,
                    TravelledDistance = carDto.TravelledDistance
                };

                var parts = carDto
                    .CarPartsInputModel
                    .Where(cp => context.Parts.Any(p => p.Id == cp.Id))
                    .Select(p => p.Id)
                    .Distinct();

                foreach(var part in parts)
                {
                    PartCar partCar = new PartCar()
                    {
                        PartId = part,
                        Car = car
                    };

                    partCars.Add(partCar);
                }

                cars.Add(car);
            }

            context.PartCars.AddRange(partCars);

            context.Cars.AddRange(cars);

            context.SaveChanges();

            return $"Successfully imported {cars.Count()}";
        }

        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(PartInputModel[]), new XmlRootAttribute("Parts"));

            var partInputModels = xmlSerializer.Deserialize(new StringReader(inputXml)) as PartInputModel[];

            var supplierIds = context.Suppliers.Select(x => x.Id).ToList();

            var parts = partInputModels
                .Where(x => supplierIds.Contains(x.SupplierId))
                .Select(x => new Part
                {
                    Name = x.Name,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    SupplierId = x.SupplierId
                })
                .ToList();

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Count()}";
        }

        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(SupplierInputModel[]), new XmlRootAttribute("Suppliers"));

            var suppliersDto = (SupplierInputModel[])xmlSerializer.Deserialize(new StringReader(inputXml));

            var suppliers = suppliersDto.Select(x => new Supplier
            {
                Name = x.Name,
                IsImporter = x.IsImporter
            })
            .ToList();

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Count()}";
        }
    }
}
