using StockMSFile.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMSFile.Repositories
{
    public class ManagerRepository
    {
        List<Manager> managers = new List<Manager>();
        string file = "C:\\Users\\Utman\\Desktop\\StockMSFile\\Files\\managers.txt";

        public ManagerRepository()
        {
            ReadManagersFromFile();
        }

        private void ReadManagersFromFile()
        {
            try
            {
                if (File.Exists(file))
                {
                    var allManagers = File.ReadAllLines(file);
                    foreach (var manager in allManagers)
                    {
                        managers.Add(Manager.ToManager(manager));
                    }
                }
                else
                {
                    string path = "C:\\Users\\Utman\\Desktop\\StockMSFile\\Files";
                    Directory.CreateDirectory(path);
                    string fileName = "managers.txt";
                    string fullPath = Path.Combine(path, fileName);
                    using (File.Create(fullPath)) { }
                }
            }
            catch (Exception e)
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
                    foreach (var manager in managers)
                    {
                        sr.WriteLine(manager.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void AddToFile(Manager manager)
        {
            try
            {
                using (StreamWriter sr = new StreamWriter(file, true))
                {
                    sr.WriteLine(manager.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void AddNewManager()
        {
            Console.Write("Enter manager first name: ");
            var firstName = Console.ReadLine();
            Console.Write("Enter manager last name: ");
            var lastName = Console.ReadLine();
            Console.Write("Enter manager phone number: ");
            var phoneNo = Console.ReadLine();
            Console.Write("Enter manager address: ");
            var address = Console.ReadLine();
            Console.Write("Enter manager email address: ");
            var email = Console.ReadLine();
            var manager = new Manager(firstName, lastName, phoneNo, address, email);
            managers.Add(manager);
            AddToFile(manager);
            Console.WriteLine($"Manager created successfully, the manager ID is: {manager.ManagerId}");
        }

        internal void GetAll()
        {
            if(managers.Count == 0)
            {
                Console.WriteLine("No Manager added yet.");
            }
            int i = 1;
            foreach (var manager in managers)
            {
                Console.Write(i + ".\t");
                PrintProduct(manager);
                i++;
            }
        }

        internal void FindStaff()
        {
            Console.Write("Enter the Id of staff to find: ");
            string id = Console.ReadLine();
            var manager = GetManagerById(id);
            if (manager != null)
            {
                PrintProduct(manager);
            }
            else
            {
                Console.WriteLine("Invalid Id.");
            }
        }

        private void PrintProduct(Manager manager)
        {
            Console.WriteLine(manager.ToString());
        }

        public void UpdateManager()
        {
            Console.Write("Enter the id of manager to update: ");
            string id = Console.ReadLine();
            var manager = GetManagerById(id);
            if (manager != null)
            {
                Console.Write("Enter manager first name: ");
                manager.FirstName = Console.ReadLine();
                Console.Write("Enter manager last name: ");
                manager.LastName = Console.ReadLine();
                Console.Write("Enter manager phone number: ");
                manager.PhoneNumber = Console.ReadLine();
                Console.Write("Enter manager address: ");
                manager.Address = Console.ReadLine();
                Console.Write("Enter manager email address: ");
                manager.Email = Console.ReadLine();

                RefreshFile();
                Console.WriteLine("Manager updated successfully.");
            }
        }

        public Manager Login()
        {
            Console.Write("Enter your email: ");
            var email = Console.ReadLine();
            Console.Write("Enter your id: ");
            var id = Console.ReadLine();

            return LoginManager(email, id);
        }

        private Manager LoginManager(string email, string id)
        {
            foreach (var manager in managers)
            {
                if (manager.Email.Equals(email) && manager.ManagerId.Equals(id))
                {
                    return manager;
                }
            }
            return null;
        }

        public Manager GetManagerById(string id)
        {
            foreach (var manager in managers)
            {
                if (manager.ManagerId.Equals(id))
                {
                    return manager;
                }
            }
            return null;
        }
    }
}
