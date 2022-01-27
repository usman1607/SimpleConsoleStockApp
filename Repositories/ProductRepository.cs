using StockMSFile.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMSFile.Repositories
{
    public class ProductRepository
    {
        List<Product> products = new List<Product>();
        string file = "C:\\Users\\Utman\\Desktop\\StockMSFile\\Files\\products.txt";
        
        public ProductRepository()
        {
            ReadProductsFromFile();
        }

        private void ReadProductsFromFile()
        {
            try
            {
                if (File.Exists(file))
                {
                    var allProducts = File.ReadAllLines(file);
                    foreach(var product in allProducts)
                    {
                        products.Add(Product.ToProduct(product));
                    }
                }
                else
                {
                    string path = "C:\\Users\\Utman\\Desktop\\StockMSFile\\Files";
                    Directory.CreateDirectory(path);
                    string fileName = "products.txt";
                    string fullPath = Path.Combine(path, fileName);
                    using (File.Create(fullPath)) { }
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void RefreshFile()
        {
            try
            {
                using (StreamWriter sr = new StreamWriter(file))
                {
                    foreach(var product in products)
                    {
                        sr.WriteLine(product.ToString());
                    }
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void AddProductToFile(Product product)
        {
            try
            {
                using (StreamWriter sr = new StreamWriter(file, true))
                {
                    sr.WriteLine(product.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void AddNewProduct()
        {
            Console.Write("Enter product name: ");
            var name = Console.ReadLine();
            Console.Write("Enter product quantity: ");
            var quantity = int.Parse(Console.ReadLine());

            var product = ExistByName(name);
            if (product != null)
            {
                UpdateProductQuantity(product, quantity, true);
                Console.WriteLine("Product is an existing product and the quantity updated successfully.");
            }
            else
            {
                Console.Write("Enter product cost price: ");
                var costPrice = decimal.Parse(Console.ReadLine());
                Console.Write("Enter product selling price: ");
                var sellingPrice = decimal.Parse(Console.ReadLine());
                Console.Write("Enter product category: ");
                var category = Console.ReadLine();
                product = new Product(name, costPrice, sellingPrice, quantity, category);
                products.Add(product);
                AddProductToFile(product);
                Console.WriteLine("New Product created successfully.");
            }
        }

        private Product ExistByName(string name)
        {
            foreach(var product in products)
            {
                if (product.Name.ToUpper().Equals(name.ToUpper()))
                {
                    return product;
                }
            }
            return null;
        }

        private void PrintProduct(Product product)
        {
            Console.WriteLine(product.ToString());
        }

        public void GetAll()
        {
            if (products.Count == 0)
            {
                Console.WriteLine("No Manager added yet.");
            }

            int i = 1;
            foreach(var product in products)
            {
                Console.Write(i+".\t");
                PrintProduct(product);
                i++;
            }
        }

        public void FindProductBySKU()
        {
            Console.Write("Enter the SKU of product to find: ");
            string sku = Console.ReadLine();
            var product = GetProductById(sku);
            if(product != null)
            {
                PrintProduct(product);
            }
            else
            {
                Console.WriteLine("Invalid SKU.");
            }
        }

        public void GetAllProductForSale()
        {
            int i = 0;
            foreach (var product in products)
            {
                if(product.Quantity > 0)
                {
                    Console.Write((i+1) + ".\t");
                    Console.WriteLine($"{product.SKU}\t{product.Name}\t{product.Quantity}\t{product.SellingPrice}");
                    i++;
                }
                
            }
            if(i == 0)
            {
                Console.WriteLine("No Product to sell.");
            }
        }

        public void UpdateProduct()
        {
            Console.Write("Enter the id of product to update: ");
            string id = Console.ReadLine();
            var product = GetProductById(id);
            if(product != null)
            {
                Console.Write("Enter product new cost price: ");
                product.CostPrice = decimal.Parse(Console.ReadLine());
                Console.Write("Enter product new selling price: ");
                product.SellingPrice = decimal.Parse(Console.ReadLine());
                Console.Write("Enter product new quantity: ");
                product.Quantity = int.Parse(Console.ReadLine());
                Console.Write("Enter product new category: ");
                product.Category = Console.ReadLine();
                RefreshFile();
                Console.WriteLine("Product updated successfully.");
            }
        }

        public void UpdateProductQuantity(Product product, int quantity, bool add=false)
        {
            if (add)
            {
                product.Quantity += quantity;
            }
            else
            {
                product.Quantity -= quantity;
            }
            RefreshFile();
        }

        public Product GetProductById(string id)
        {
            foreach(var product in products)
            {
                if (product.SKU.Equals(id))
                {
                    return product;
                }
            }
            return null;
        }
    }
}
