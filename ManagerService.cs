using StockMSFile.Models;
using StockMSFile.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMSFile
{
    public class ManagerService
    {
        List<Sale> sales = new List<Sale>();
        ManagerRepository managerRepository;
        ProductRepository productRepository;
        CustomerRepository customerRepository;
        string file = "C:\\Users\\Utman\\Desktop\\StockMSFile\\Files\\sales.txt";

        public ManagerService()
        {
            ReadSalesFromFile();
            managerRepository = new ManagerRepository();
            productRepository = new ProductRepository();
            customerRepository = new CustomerRepository();
        }

        private void ReadSalesFromFile()
        {
            try
            {
                if (File.Exists(file))
                {
                    var allSales = File.ReadAllLines(file);
                    foreach (var sale in allSales)
                    {
                        sales.Add(Sale.ToSale(sale));
                    }
                }
                else
                {
                    string path = "C:\\Users\\Utman\\Desktop\\StockMSFile\\Files";
                    Directory.CreateDirectory(path);
                    string fileName = "sales.txt";
                    string fullPath = Path.Combine(path, fileName);
                    using (File.Create(fullPath)) { }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void FindProduct()
        {
            productRepository.FindProductBySKU();
        }

        public void FindCustomer()
        {
            customerRepository.FindCustomer();
        }

        private void AddToFile(Sale sale)
        {
            try
            {
                using (StreamWriter sr = new StreamWriter(file, true))
                {
                    sr.WriteLine(sale.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void FindStaffById(Manager admin)
        {
            if (IsAdmin(admin))
            {
                managerRepository.FindStaff();
            }
            else
            {
                Console.WriteLine("Sorry, its only admin that can find staff.");
            }
        }

        internal void ListAllStaff(Manager admin)
        {
            if (IsAdmin(admin))
            {
                managerRepository.GetAll();
            }
            else
            {
                Console.WriteLine("Sorry, its only admin that can view staff list.");
            }
        }

        public void ListAllProducts()
        {
            productRepository.GetAll();
        }

        public void ListAllCustomers()
        {
            customerRepository.ListAllCustomers();
        }

        private void RefreshFile()
        {
            try
            {
                using (StreamWriter sr = new StreamWriter(file))
                {
                    foreach (var sale in sales)
                    {
                        sr.WriteLine(sale.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void AddManager(Manager admin)
        {
            if(IsAdmin(admin))
            {
                managerRepository.AddNewManager();
            }
            else
            {
                Console.WriteLine("Sorry, its only admin that can add manager");
            }
        }

        private bool IsAdmin(Manager admin)
        {
            return admin.ManagerId[0] == 'A';
        }

        public Manager Login()
        {
            return managerRepository.Login();
        }

        public void AddProduct(Manager admin)
        {
            if (IsAdmin(admin))
            {
                productRepository.AddNewProduct();
            }
            else
            {
                Console.WriteLine("Sorry, its only admin that can add new product.");
            }
        }

        public void MakeSale(Manager manager)
        {
            Console.Write("A new customer press 1\nAn old customer press 2: ");
            int val;
            if(int.TryParse(Console.ReadLine(), out val))
            {
                switch (val)
                {
                    case 1:
                        MakeNewSale(manager);
                        break;
                    case 2:
                        MakeNewSale(manager, false);
                        break;
                }
            }
        }

        private void MakeNewSale(Manager manager, bool newCustomer = true)
        {
            Customer customer;
            if (newCustomer)
            {
                customer = customerRepository.AddNewCustomer();
            }
            else
            {
                Console.Write("Enter your customerId: ");
                string id = Console.ReadLine();
                customer = customerRepository.GetCustomerById(id);
            }
                
            if(customer != null)
            {
                bool error = true;
                while (error)
                {
                    var buyMore = true;
                    do
                    {
                        Console.WriteLine();
                        productRepository.GetAllProductForSale();
                        Console.WriteLine();
                        Console.Write("Enter the SKU of the product you want to buy: ");
                        var sku = Console.ReadLine();
                        Console.Write("How many? ");
                        var quantity = int.Parse(Console.ReadLine());
                        var product = productRepository.GetProductById(sku);
                        if (product != null && product.Quantity >= quantity)
                        {
                            error = false;

                            Console.WriteLine($"Total price for {quantity} - {product.Name} is: {product.SellingPrice * quantity}.");
                            Console.Write("Press 1 to proceed or 0 to cancil: ");
                            int op = int.Parse(Console.ReadLine());
                            if (op == 1)
                            {
                                productRepository.UpdateProductQuantity(product, quantity);
                                customerRepository.UpdateTotalPuchase(customer, quantity * product.SellingPrice);
                                var sale = new Sale(product.Name, product.SKU, quantity, product.SellingPrice, product.SellingPrice * quantity, customer.FirstName + " " + customer.LastName, manager.FirstName + " " + manager.LastName);
                                sales.Add(sale);
                                AddToFile(sale);
                                Console.WriteLine("Sales successful:");
                                //Console.WriteLine(sale.ToString());
                                PrintReceipt(sale);
                                Console.Write("Do you want to buy more goods? y for Yes and n for No: ");
                                var ans = Console.ReadLine().ToUpper();
                                if (!ans.Equals("Y"))
                                {
                                    Console.WriteLine("Thanks for your patronage, please come next time...");
                                    buyMore = false;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Thanks for your patronage, please come next time...");
                                return;
                            }

                        }
                        else
                        {
                            Console.WriteLine($"SKU: {sku} is incorrect OR the number of the product in the store is less than {quantity}");
                        }
                    } while (buyMore);
                }
            }
            else
            {
                Console.WriteLine("Can not locate or register customer, try again");
            }
        }
        
        private void PrintReceipt(Sale sale)
        {
            Console.WriteLine("======================================================");
            Console.WriteLine($"|Reference:\t\t|\t{sale.Reference}");

            Console.WriteLine($"|Customer Name:\t\t|\t{sale.CustomerName}");

            Console.WriteLine("|=====================================================");

            Console.WriteLine($"|Product Name:\t\t|\t{sale.Name}");

            Console.WriteLine("|=====================================================");

            Console.WriteLine($"|SKU:\t\t\t|\t{sale.SKU}");

            Console.WriteLine("|=====================================================");

            Console.WriteLine($"|Quantity:\t\t|\t{sale.Quantity}");

            Console.WriteLine("|=====================================================");

            Console.WriteLine($"|Unit Price:\t\t|\t{sale.UnitPrice}");

            Console.WriteLine("|=====================================================");

            Console.WriteLine($"|Total Price:\t\t|\t{sale.TotalPrice}");

            Console.WriteLine("|=====================================================");

            Console.WriteLine($"|Sold By:\t\t|\t{sale.SoldBy}");

            Console.WriteLine("|=====================================================");

            Console.WriteLine($"|Time Sold:\t\t|\t{sale.DateSold}");

            Console.WriteLine("======================================================");
        }
    }
}
