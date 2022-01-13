using System.IO;
using CarDealer.Data;
using System;
using System.Xml.Serialization;
using CarDealer.IO.Input;
using System.Linq;
using CarDealer.Models;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new CarDealerContext();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            string supplierXml = File.ReadAllText("../../../Datasets/suppliers.xml");

            var result = ImportSuppliers(context, supplierXml);
            Console.WriteLine(result);
        }

        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(SupplierInputModel[]), new XmlRootAttribute("Suppliers"));

            var suplierDtos = xmlSerializer.Deserialize(new StringReader(inputXml)) as SupplierInputModel[];

            var suppliers = suplierDtos
                .Select(x => new Supplier
                {
                    Name = x.Name,
                    IsImporter = x.IsImporter
                })
                .ToArray();

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Count()}";
        }
    }
}
