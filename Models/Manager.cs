using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMSFile.Models
{
    public class Manager : Person
    {
        public string ManagerId { get; set; }

        public Manager(string firstName, string lastName, string phone, string address, string email) : base(firstName, lastName, phone, address, email)
        {
            ManagerId = $"STF-{Guid.NewGuid().ToString().Replace("-", "").Substring(0, 7).ToUpper()}";
        }

        public Manager(string firstName, string lastName, string phone, string address, string email, string id) : this(firstName, lastName, phone, address, email)
        {
            ManagerId = id;
        }

        public override string ToString()
        {
            return $"{FirstName}\t{LastName}\t{PhoneNumber}\t{Address}\t{Email}\t{ManagerId}";
        }

        public static Manager ToManager(string str)
        {
            var managerStr = str.Split("\t");
            var firstName = managerStr[0];
            var lastName = managerStr[1];
            var phoneNo = managerStr[2];
            var address = managerStr[3];
            var email = managerStr[4];
            var id = managerStr[5];

            return new Manager(firstName, lastName, phoneNo, address, email, id);
        }
    }
}
