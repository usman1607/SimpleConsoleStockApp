using StockMSFile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMSFile.Menus
{
    public class MainMenu
    {
        SalesMenu salesMenu;
        public MainMenu()
        {
            salesMenu = new SalesMenu();
        }

        public void Menu()
        {
            var exit = false;

            while (!exit)
            {
                Console.Clear();
                PrintMenu();
                int value;
                if (int.TryParse(Console.ReadLine(), out value))
                {
                    int op = value;
                    switch (op)
                    {
                        case 1:
                            var manager = salesMenu.Login();
                            if(manager != null)
                            {
                                salesMenu.Menu(manager);
                            }
                            else
                            {
                                Console.WriteLine("Invalid email or id...\nPress any key to try again...");
                                Console.ReadKey();
                            }
                            break;
                        case 0:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid inpute");
                            break;
                    }
                }

            }
        }

        private void PrintMenu()
        {
            Console.WriteLine("==================================");
            Console.WriteLine("====== Welcome to CLH Store ======");
            Console.WriteLine("==================================");
            Console.WriteLine();
            Console.WriteLine("1.\tLogin.");
            Console.WriteLine("0.\tExit.");
        }
    }
}
