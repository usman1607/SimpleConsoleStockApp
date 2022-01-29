using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMSFile.Models
{
    public static class LogBook
    {
        public static decimal TotalPuchaseCost { get; set; }
        public static int TotalGoodsPuchased { get; set; }


        public static decimal TotalSales { get; set; }
        public static decimal TotalCostOfSales { get; set; }
        public static decimal TotalProfit { get; set; }


        public static int TotalGoodsSold { get; set; }
        public static int TotalGoodsLeft { get; set; }

        static string file = "C:\\Users\\Utman\\Desktop\\StockMSFile\\Files\\logBook.txt";

        private static void FormLogBook(decimal totalPuchaseCost, decimal totalSales, decimal totalCostOfSales, decimal totalProfit, int totalGoodsPuchased, int totalGoodsSold, int totalGoodsLeft)
        {
            TotalPuchaseCost = totalPuchaseCost;
            TotalSales = totalSales;
            TotalCostOfSales = totalCostOfSales;
            TotalProfit = totalProfit;
            TotalGoodsPuchased = totalGoodsPuchased;
            TotalGoodsSold = totalGoodsSold;
            TotalGoodsLeft = totalGoodsLeft;
        }

        public static void UpdateLogBook(Product product, int quantity, bool newPuchase = false)
        {
            if (newPuchase)
            {
                TotalGoodsPuchased += quantity;
                TotalPuchaseCost += (product.CostPrice * quantity);
            }
            else
            {
                var soldPrice = quantity * product.SellingPrice;
                var soldCost = quantity * product.CostPrice;

                TotalSales += soldPrice;
                TotalCostOfSales += soldCost;
                TotalProfit = TotalCostOfSales - TotalSales;

                TotalGoodsSold += quantity;
                TotalGoodsLeft = TotalGoodsPuchased - TotalGoodsSold;
            }
            RefreshFile();
        }

        public static void ReadLogFromFile()
        {
            try
            {
                if (File.Exists(file))
                {
                    var allLogs = File.ReadAllLines(file);
                    foreach (var log in allLogs)
                    {
                        FormLogBookFromStr(log);
                    }
                }
                else
                {
                    string path = "C:\\Users\\Utman\\Desktop\\StockMSFile\\Files";
                    Directory.CreateDirectory(path);
                    string fileName = "logBook.txt";
                    string fullPath = Path.Combine(path, fileName);
                    using (File.Create(fullPath)) { }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void RefreshFile()
        {
            try
            {
                using (StreamWriter sr = new StreamWriter(file))
                {
                    sr.WriteLine(FormAString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static string FormAString()
        {
            return $"{TotalPuchaseCost}\t{TotalSales}\t{TotalCostOfSales}\t{TotalProfit}\t{TotalGoodsPuchased}\t{TotalGoodsSold}\t{TotalGoodsLeft}";
        }

        private static void FormLogBookFromStr(string str)
        {
            var logBook = str.Split("\t");

            var totalPuchaseCost = decimal.Parse(logBook[0]);
            var totalSales = decimal.Parse(logBook[1]);
            var totalCostOfSales = decimal.Parse(logBook[2]);
            var totalProfit = decimal.Parse(logBook[3]);
            var totalGoodsPuchased = int.Parse(logBook[4]);
            var totalGoodsSold = int.Parse(logBook[5]);
            var totalGoodsLeft = int.Parse(logBook[6]);
            FormLogBook(totalPuchaseCost, totalSales, totalCostOfSales, totalProfit, totalGoodsPuchased, totalGoodsSold, totalGoodsLeft);
        }
    }
}
