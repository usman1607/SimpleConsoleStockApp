using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMSFile.Models
{
    public class Product
    {
        public string Name { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int Quantity { get; set; }
        public string SKU { get; set; }
        public string Category { get; set; }

        public Product(string name, decimal costPrice, decimal sellPrice, int quantity, string category)
        {
            Name = name;
            CostPrice = costPrice;
            SellingPrice = sellPrice;
            Quantity = quantity;
            Category = category;
            SKU = $"{Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10).ToUpper()}";
        }

        public Product(string name, decimal cost, decimal sellPrice, int quantity, string category, string sku)
        {
            Name = name;
            CostPrice = cost;
            SellingPrice = sellPrice;
            Quantity = quantity;
            Category = category;
            SKU = sku;
        }

        public override string ToString()
        {
            return $"{Name}\t{CostPrice}\t{SellingPrice}\t{Quantity}\t{Category}\t{SKU}";
        }

        public static Product ToProduct(string productStr)
        {
            var product = productStr.Split("\t");
            var name = product[0];
            var cost = decimal.Parse(product[1]);
            var sellPrice = decimal.Parse(product[2]);
            var quantity = int.Parse(product[3]);
            var category = product[4];
            var sku = product[5];

            return new Product(name, cost, sellPrice, quantity, category, sku);
        }
    }
}
