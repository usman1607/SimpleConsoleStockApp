using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMSFile.Models
{
    public class Sale
    {
        public string Reference { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime DateSold { get; set; }
        public string CustomerName { get; set; }
        public string SoldBy { get; set; }

        public Sale(string name, string sku, int quantity, decimal unitPrice, decimal totalPrice, string customerName, string soldBy)
        {
            Reference = $"SD-{Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToUpper()}";
            Name = name;
            SKU = sku;
            Quantity = quantity;
            UnitPrice = unitPrice;
            TotalPrice = totalPrice;
            DateSold = DateTime.Now;
            CustomerName = customerName;
            SoldBy = soldBy;
        }

        public Sale(string reference, string name, string sku, int quantity, decimal unitPrice, decimal totalPrice, DateTime dateSold, string customerName, string soldBy)
        {
            Reference = reference;
            Name = name;
            SKU = sku;
            Quantity = quantity;
            UnitPrice = unitPrice;
            TotalPrice = totalPrice;
            DateSold = dateSold;
            CustomerName = customerName;
            SoldBy = soldBy;
        }

        public override string ToString()
        {
            return $"{Reference}\t{Name}\t{SKU}\t{Quantity}\t{UnitPrice}\t{TotalPrice}\t{DateSold}\t{CustomerName}\t{SoldBy}";
        }

        public static Sale ToSale(string str)
        {
            var sale = str.Split("\t");
            var reference = sale[0];
            var name = sale[1];
            var sku = sale[2];
            var quantity = int.Parse(sale[3]);
            var unitPrice = decimal.Parse(sale[4]);
            var totalPrice = decimal.Parse(sale[5]);
            var dateSold = DateTime.Parse(sale[6]);
            var customerName = sale[7];
            var soldBy = sale[8];

            return new Sale(reference, name, sku, quantity, unitPrice, totalPrice, dateSold, customerName, soldBy);
        }
    }
}
