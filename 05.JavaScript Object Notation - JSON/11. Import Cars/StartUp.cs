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
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            string supplierJson = File.ReadAllText("../../../Datasets/suppliers.json");
            string partJson = File.ReadAllText("../../../Datasets/parts.json");
            string carJson = File.ReadAllText("../../../Datasets/cars.json");

            ImportSuppliers(context, supplierJson);
            ImportParts(context, partJson);

            var result = ImportCars(context, carJson);
            Console.WriteLine(result);
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
