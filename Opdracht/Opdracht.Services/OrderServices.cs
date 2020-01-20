using Opdracht.Core.Models;
using Opdracht.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Opdracht.Services
{

    public class OrderServices
    {
        private static bool Status;
        public static bool StopGenerating;
        private static readonly string FolderPath = @"C:\Users\aman\Desktop\Orders";

        private static DataContext context = new DataContext();


        //This Method generate's an order every 5 seconds
        public static void StartOders()
        {
            while (StopGenerating != true)
            {
                SerializeOrders();
                Thread.Sleep(5000);
            }

        }
        // This Method Generate an Order
        public static void SerializeOrders()
        {
            if (Status == true)
            {
                Status = false;
            }
            else
            {
                Status = true;
            }

            Order order = new Order(Status);
            XmlSerializer serializer = new XmlSerializer(typeof(Order));
            var Unique = Guid.NewGuid().ToString();

            using (TextWriter tw = new StreamWriter(FolderPath + "\\" + Unique + ".xml"))
            {
                serializer.Serialize(tw, order);
            }
        }

        // This Method Puts all the orders into the Database
        public static void DeSerializeAllOrders()
        {
            Console.WriteLine("Shifting Orders To Database");
            List<string> files = Directory.GetFiles(FolderPath).ToList();
            foreach (var filePath in files)
            {
                FileInfo file = new FileInfo(filePath);
                var orderInDataBase = context.Orders.FirstOrDefault(o => o.Filename == file.Name);
                if (file.Extension == ".xml")
                {
                    if (orderInDataBase == null)
                    {
                        var order = DeSerializeOrders(file, filePath);
                        order.Id = Guid.NewGuid().ToString();
                        context.Orders.Add(order);
                        context.SaveChanges();
                    }
                }
            }
        }
        // This Method Deserialized an Order and Returns it.
        public static Order DeSerializeOrders(FileInfo file, string filePath)
        {

            XmlSerializer deserializer = new XmlSerializer(typeof(Order));
            TextReader reader = new StreamReader(filePath);
            var order = (Order)deserializer.Deserialize(reader);
            order.Filename = file.Name;
            order.FileContent = System.IO.File.ReadAllText(filePath);
            order.CreatedAt = DateTime.Now;
            reader.Close();
            return order;
        }

    }
}
