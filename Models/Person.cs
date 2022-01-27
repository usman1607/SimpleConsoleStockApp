using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMSFile.Models
{
    public abstract class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        public Person(string firstName, string lastName, string phone, string address, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phone;
            Address = address;
            Email = email;
        }
    }
}
