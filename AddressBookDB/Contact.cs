using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBookDB
{
    public class Contact
    {
        public Contact() { }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Contact"/> class.
        /// </summary>
        /// <param name="FirstName">The first name.</param>
        /// <param name="LastName">The last name.</param>
        /// <param name="Address">The address.</param>
        /// <param name="ZipCode">The zip code.</param>
        /// <param name="City">The city.</param>
        /// <param name="State">The state.</param>
        /// <param name="PhoneNo">The phone no.</param>
        /// <param name="Email">The email.</param>
        /// <param name="Type">The type.</param>
        public Contact(string FirstName, string LastName, string Address, string ZipCode, string City, string State, string PhoneNo, string Email, string Type)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Address = Address;
            this.ZipCode = ZipCode;
            this.City = City;
            this.State = State;
            this.PhoneNo = PhoneNo;
            this.Email = Email;
            this.Type = Type;
        }
    }
}
