using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMSFile.Models
{
    public class Customer: Person
    {
        public string CustomerId { get; set; }
        public decimal TotalPuchase { get; set; }

        public Customer(string firstName, string lastName, string phone, string address, string email):base(firstName, lastName, phone, address, email)
        {
            CustomerId = $"CUS-{Guid.NewGuid().ToString().Replace("-", "").Substring(0, 7).ToUpper()}";
        }

        public Customer(string firstName, string lastName, string phone, string address, string email, string id, decimal totalPuchase) : this(firstName, lastName, phone, address, email)
        {
            CustomerId = id;
            TotalPuchase = totalPuchase;
        }

        public override string ToString()
        {
            return $"{FirstName}\t{LastName}\t{PhoneNumber}\t{Address}\t{Email}\t{CustomerId}\t{TotalPuchase}";
        }

        public static Customer ToCustomer(string str)
        {
            var customerStr = str.Split("\t");
            var firstName = customerStr[0];
            var lastName = customerStr[1];
            var phoneNo = customerStr[2];
            var address = customerStr[3];
            var email = customerStr[4];
            var id = customerStr[5];
            var totalPuchase = decimal.Parse(customerStr[6]);

            return new Customer(firstName, lastName, phoneNo, address, email, id, totalPuchase);
        }
    }
}
