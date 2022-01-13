using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using CarDealer.Data;
using CarDealer.DTO;
using CarDealer.Models;
using Newtonsoft.Json;

namespace CarDealer
{
    public class StartUp
    {
        static IMapper mapper;

        public static void Main(string[] args)
        {
            var context = new CarDealerContext();
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            //string supplierJson = File.ReadAllText("../../../Datasets/suppliers.json");
            //string partJson = File.ReadAllText("../../../Datasets/parts.json");
            //string carJson = File.ReadAllText("../../../Datasets/cars.json");
            //string customerJson = File.ReadAllText("../../../Datasets/customers.json");
            //string saleJson = File.ReadAllText("../../../Datasets/sales.json");

            //ImportSuppliers(context, supplierJson);
            //ImportParts(context, partJson);
            //ImportCars(context, carJson);
            //ImportCustomers(context, customerJson);
            //ImportSales(context, saleJson);

            var result = GetTotalSalesByCustomer(context);
            Console.WriteLine(result);
        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                .Where(x => x.Sales.Count > 0)
                .Select(x => new
                {
                    fullName = x.Name,
                    boughtCars = x.Sales.Count(),
                    spentMoney = x.Sales.Sum(s => s.Car.PartCars.Sum(p => p.Part.Price))
                })
                .OrderByDescending(x => x.spentMoney)
                .ThenByDescending(x => x.boughtCars)
                .ToList();

            return JsonConvert.SerializeObject(customers, Formatting.Indented);
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars
                .Select(x => new
                {
                    car = new
                    {
                        Make = x.Make,
                        Model = x.Model,
                        TravelledDistance = x.TravelledDistance
                    },
                    parts = x.PartCars.Select(p => new
                    {
                        Name = p.Part.Name,
                        Price = p.Part.Price.ToString("F2")
                    })
                })
                .ToList();

            return JsonConvert.SerializeObject(cars, Formatting.Indented);
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(x => x.IsImporter == false)
                .Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name,
                    PartsCount = x.Parts.Count()
                })
                .ToList();

            return JsonConvert.SerializeObject(suppliers, Formatting.Indented);
        }

        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(x => x.Make == "Toyota")
                .Select(x => new
                {
                    Id = x.Id,
                    Make = x.Make,
                    Model = x.Model,
                    TravelledDistance = x.TravelledDistance
                })
                .OrderBy(x => x.Model)
                .ThenByDescending(x => x.TravelledDistance)
                .ToList();

            return JsonConvert.SerializeObject(cars, Formatting.Indented);
        }

        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var customers = context.Customers
                .OrderBy(x => x.BirthDate)
                .ThenBy(x => x.IsYoungDriver)
                .Select(x => new
                {
                    Name = x.Name,
                    BirthDate = x.BirthDate.ToString("dd/MM/yyyy"),
                    IsYoungDriver = x.IsYoungDriver
                })
                .ToList();

            return JsonConvert.SerializeObject(customers, Formatting.Indented);
        }

        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            InitializeAutoMapper();

            var dtoSales = JsonConvert.DeserializeObject<IEnumerable<SaleInputModel>>(inputJson);

            var sales = mapper.Map<IEnumerable<Sale>>(dtoSales);

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Count()}.";
        }

        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            InitializeAutoMapper();

            var dtoCustomers = JsonConvert.DeserializeObject<IEnumerable<CustomerInputModel>>(inputJson);

            var customers = mapper.Map<IEnumerable<Customer>>(dtoCustomers);

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Count()}.";
        }

        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            InitializeAutoMapper();

            //var dtoCars = JsonConvert.DeserializeObject<IEnumerable<CarInputModel>>(inputJson);

            //var cars = mapper.Map<IEnumerable<Car>>(dtoCars);

            //context.Cars.AddRange(cars);
            //context.SaveChanges();

            //return $"Successfully imported {cars.Count()}.";



            var dtoCars = JsonConvert.DeserializeObject<IEnumerable<CarInputModel>>(inputJson);

            var cars = new List<Car>();

            //with mapper
            foreach (var dtoCar in dtoCars)
            {
                var car = mapper.Map<Car>(dtoCar);

                foreach (var partId in dtoCar.PartsId)
                {
                    car.PartCars.Add(new PartCar
                    {
                        PartId = partId
                    });
                }

                cars.Add(car);
            }

            //without mapper
            //foreach (var dtoCar in dtoCars)
            //{
            //    var car = new Car
            //    {
            //        Model = dtoCar.Model,
            //        Make = dtoCar.Make,
            //        TravelledDistance = dtoCar.TravelledDistance,
            //    };

            //    foreach (var partId in dtoCar.PartsId)
            //    {
            //        car.PartCars.Add(new PartCar() { PartId = partId });
            //    }

            //    cars.Add(car);
            //}

            context.Cars.AddRange(cars);

            context.SaveChanges();

            return $"Successfully imported {cars.Count()}.";
        }

        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            InitializeAutoMapper();

            var supplierIds = context.Suppliers.Select(x => x.Id).ToList();

            var dtoParts = JsonConvert.DeserializeObject<IEnumerable<PartInputModel>>(inputJson)
                .Where(x => supplierIds.Contains(x.SupplierId)).ToList();

            var parts = mapper.Map<IEnumerable<Part>>(dtoParts);

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Count()}.";
        }

        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            InitializeAutoMapper();

            var dtoSuppliers = JsonConvert.DeserializeObject<IEnumerable<SupplierInputModel>>(inputJson);

            var suppliers = mapper.Map<IEnumerable<Supplier>>(dtoSuppliers);

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Count()}.";
        }

        private static void InitializeAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CarDealerProfile>();
            });

            mapper = config.CreateMapper();
        }
    }
}
