using StockMSFile.Menus;
using StockMSFile.Models;
using StockMSFile.Repositories;
using System;

namespace StockMSFile
{
    class Program
    {
        static void Main(string[] args)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Menu();
        }
    }
}
