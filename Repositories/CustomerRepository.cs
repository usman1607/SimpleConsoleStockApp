using StockMSFile.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMSFile.Repositories
{
    public class CustomerRepository
    {
        List<Customer> customers = new List<Customer>();
        string file = "C:\\Users\\Utman\\Desktop\\StockMSFile\\Files\\customers.txt";

        public CustomerRepository()
        {
            ReadCustomersFromFile();
        }

        private void ReadCustomersFromFile()
        {
            try
            {
                if (File.Exists(file))
                {
                    var allCustomers = File.ReadAllLines(file);
                    foreach (var customer in allCustomers)
                    {
                        customers.Add(Customer.ToCustomer(customer));
                    }
                }
                else
                {
                    string path = "C:\\Users\\Utman\\Desktop\\StockMSFile\\Files";
                    Directory.CreateDirectory(path);
                    string fileName = "customers.txt";
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
                    foreach (var customer in customers)
                    {
                        sr.WriteLine(customer.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void AddToFile(Customer customer)
        {
            try
            {
                using (StreamWriter sr = new StreamWriter(file, true))
                {
                    sr.WriteLine(customer.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public Customer AddNewCustomer()
        {
            Console.Write("Enter customer first name: ");
            var firstName = Console.ReadLine();
            Console.Write("Enter customer last name: ");
            var lastName = Console.ReadLine();
            Console.Write("Enter customer phone number: ");
            var phoneNo = Console.ReadLine();
            Console.Write("Enter customer address: ");
            var address = Console.ReadLine();
            Console.Write("Enter customer email address: ");
            var email = Console.ReadLine();
            var customer = new Customer(firstName, lastName, phoneNo, address, email);
            customers.Add(customer);
            AddToFile(customer);
            Console.WriteLine($"Customer added successfully, the customer Id is: {customer.CustomerId}");
            return customer;
        }

        private void PrintCustomer(Customer customer)
        {
            Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}\tID: {customer.CustomerId}\tEmail: {customer.Email}\tTotalPuchase: {customer.TotalPuchase}");
        }

        public void FindCustomer()
        {
            Console.Write("Enter the id of customer to find: ");
            string id = Console.ReadLine();
            var customer = GetCustomerById(id);
            if (customer != null)
            {
                PrintCustomer(customer);
            }
            else
            {
                Console.WriteLine("Invalid customer ID.");
            }
        }

        public void ListAllCustomers()
        {
            if (customers.Count == 0)
            {
                Console.WriteLine("No Customer added yet.");
            }
            else
            {
                SortCustomers(customers);

                int i = 1;
                foreach (var customer in customers)
                {
                    Console.Write(i + ".\t");
                    PrintCustomer(customer);
                    i++;
                }
            }
            
        }

        private void SortCustomers(List<Customer> all)
        {
            Customer temp;
            bool swapped;
            for(int i = 0; i < all.Count-1; i++)
            {
                swapped = false;
                for (int j = 0; j < all.Count-i-1; j++)
                {
                    if (all[j].TotalPuchase < all[j + 1].TotalPuchase)
                    {
                        temp = all[j];
                        all[j] = all[j + 1];
                        all[j + 1] = temp;
                        swapped = true;
                    }
                }

                if (swapped == false)
                    break;
            }
        }

        public void UpdateCustomer()
        {
            Console.Write("Enter the id of customer to update: ");
            string id = Console.ReadLine();
            var customer = GetCustomerById(id);
            if (customer != null)
            {
                Console.Write("Enter customer first name: ");
                customer.FirstName = Console.ReadLine();
                Console.Write("Enter customer last name: ");
                customer.LastName = Console.ReadLine();
                Console.Write("Enter customer phone number: ");
                customer.PhoneNumber = Console.ReadLine();
                Console.Write("Enter customer address: ");
                customer.Address = Console.ReadLine();
                Console.Write("Enter customer email address: ");
                customer.Email = Console.ReadLine();

                RefreshFile();
                Console.WriteLine("Customer updated successfully.");
            }
            else
            {
                Console.WriteLine("Invalid customer ID.");
            }
        }

        public Customer GetCustomerById(string id)
        {
            foreach (var customer in customers)
            {
                if (customer.CustomerId.Equals(id))
                {
                    return customer;
                }
            }
            return null;
        }

        public void UpdateTotalPuchase(Customer customer, decimal value)
        {
            customer.TotalPuchase += value;
            RefreshFile();
        }
    }
}
