using StockMSFile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMSFile.Menus
{
    public class SalesMenu
    {
        public ManagerService managerService;

        public SalesMenu()
        {
            managerService = new ManagerService();
        }

        public Manager Login()
        {
            return managerService.Login();
        }

        public void Menu(Manager manager)
        {
            var check = true;
            while (check)
            {
                Console.Clear();
                PrintMenu(manager);
                int op;
                if (int.TryParse(Console.ReadLine(), out op))
                {
                    switch (op)
                    {
                        case 1:
                            managerService.MakeSale(manager);
                            Console.WriteLine("Press any key to continue....");
                            Console.ReadKey();
                            break;
                        case 2:
                            managerService.AddManager(manager);
                            Console.WriteLine("Press any key to continue....");
                            Console.ReadKey();
                            break;
                        case 3:
                            managerService.AddProduct(manager);
                            Console.WriteLine("Press any key to continue....");
                            Console.ReadKey();
                            break;
                        case 4:
                            managerService.FindProduct();
                            Console.WriteLine("Press any key to continue....");
                            Console.ReadKey();
                            break;
                        case 5:
                            managerService.ListAllProducts();
                            Console.WriteLine("Press any key to continue....");
                            Console.ReadKey();
                            break;
                        case 6:
                            managerService.FindStaffById(manager);
                            Console.WriteLine("Press any key to continue....");
                            Console.ReadKey();
                            break;
                        case 7:
                            managerService.ListAllStaff(manager);
                            Console.WriteLine("Press any key to continue....");
                            Console.ReadKey();
                            break;
                        case 8:
                            managerService.ListAllCustomers();
                            Console.WriteLine("Press any key to continue....");
                            Console.ReadKey();
                            break;
                        case 9:
                            managerService.FindCustomer();
                            Console.WriteLine("Press any key to continue....");
                            Console.ReadKey();
                            break;
                        case 10:
                            managerService.ViewLogBook(manager);
                            Console.WriteLine("Press any key to continue....");
                            Console.ReadKey();
                            break;
                        case 0:
                            check = false;
                            break;

                        default:
                            Console.WriteLine("Invalid inpute");
                            break;
                    }
                }
            }
        }

        private void PrintMenu(Manager manager)
        {
            Console.WriteLine("=======================================================================");
            Console.WriteLine($"                  Welcome: {manager.FirstName} {manager.LastName} ");
            Console.WriteLine("=======================================================================");
            Console.WriteLine();
            Console.WriteLine("Enter 1 to make sales");
            Console.WriteLine("Enter 2 to add new manager");
            Console.WriteLine("Enter 3 to add product");
            Console.WriteLine("Enter 4 to find product by sku");
            Console.WriteLine("Enter 5 to list all products");
            Console.WriteLine("Enter 6 to find staff by Id");
            Console.WriteLine("Enter 7 to list all staff");
            Console.WriteLine("Enter 8 to list all customers");
            Console.WriteLine("Enter 9 to find customer by Id");
            Console.WriteLine("Enter 10 to view log book.");
            Console.WriteLine("Enter 0 to logout.");
            Console.Write("\nEnter your input: ");
        }
    }
}
