using Microsoft.VisualStudio.TestTools.UnitTesting;
using AddressBookDB;
using System;
using System.Collections.Generic;
using RestSharp;
using System.Net;
using Newtonsoft.Json;

namespace AddressBookTests
{
    [TestClass]
    public class AddressBookTests
    {
        RestClient restClient = new RestClient("http://localhost:4000");
        /// <summary>
        /// Gets the address book.
        /// </summary>
        /// <returns></returns>
        private IRestResponse GetAddressBook()
        {
            RestRequest request = new RestRequest("/addressBook", Method.GET);

            IRestResponse response = restClient.Execute(request);
            return response;
        }
        /// <summary>
        /// Given the retrieve contacts from database should return true.
        /// </summary>
        [TestMethod]
        public void Given_RetrieveContactsFromDatabase_ShouldReturnTrue()
        {
            /// Arrange
            AddressBookRepository repository = new AddressBookRepository();
            /// Act
            bool result = repository.RetrieveFromDatabase();
            /// Assert
            Assert.AreEqual(result, true);
        }
        /// <summary>
        /// Givens the first and last name should return true.
        /// </summary>
        [TestMethod]
        public void Given_FirstAndLastName_Should_ReturnTrue()
        {
            /// Arrange
            string firstName = "Kunal";
            string lastName = "Patil";
            string phoneNo = "9858647525";
            AddressBookRepository repository = new AddressBookRepository();
            /// Act
            bool result = repository.UpdateContact(firstName, lastName, phoneNo);
            /// Assert
            Assert.AreEqual(result, true);
        }
        /// <summary>
        /// Givens the wrong first and last name should return false.
        /// </summary>
        [TestMethod]
        public void Given_WrongFirstAndLastName_Should_ReturnFalse()
        {
            /// Arrange
            string firstName = "Kunal";
            string lastName = "P";
            string phoneNo = "9858647525";
            AddressBookRepository repository = new AddressBookRepository();
            /// Act
            bool result = repository.UpdateContact(firstName,  lastName, phoneNo);
            /// Assert
            Assert.AreEqual(result, false);
        }
        /// <summary>
        /// Adds the multiple contacts using thread.
        /// </summary>
        [TestMethod]
        public void AddMultipleContacts_UsingThread()
        {
            List<Contact> contacts = new List<Contact>();
            contacts.Add(new Contact { FirstName = "Mukesh", LastName = "Maurya", Address = "Vasai", ZipCode = "400125", City = "Thane", State = "Maharashtra", PhoneNo = "8547861020", Email = "mukesh@gmail.com", Type = "Profession", Date = new System.DateTime(2018 - 08 - 12) });
            contacts.Add(new Contact { FirstName = "Sagar", LastName = "Verma", Address = "RK naagr", ZipCode = "210320", City = "Jaipur", State = "Rajasthan", PhoneNo = "7452015302", Email = "sagar@gmail.com", Type = "Friend", Date = new System.DateTime(2019 - 10 - 11) });

            AddressBookRepository addressBook = new AddressBookRepository();
            DateTime startTime = DateTime.Now;
            addressBook.AddMultipleContactsWithThread(contacts);
            DateTime endTime = DateTime.Now;
            Console.WriteLine("Total time for execution of thread :" + (endTime - startTime));
        }
        /// <summary>
        /// UC22
        /// On calling addressbook to rest api should return records.
        /// </summary>
        [TestMethod]
        public void OnCallingAddressBook_ReturnAddressBook()
        {
            //Act
            IRestResponse restResponse = GetAddressBook();
            //Assert
            Assert.AreEqual(restResponse.StatusCode, HttpStatusCode.OK);
            List<Contact> addressBookResponse = JsonConvert.DeserializeObject<List<Contact>>(restResponse.Content);
            Assert.AreEqual(4, addressBookResponse.Count);
            foreach (Contact contact in addressBookResponse)
            {
                Console.WriteLine($"First name: {contact.FirstName}\nLast name: {contact.LastName}\nZipCode: {contact.ZipCode}" +
                    $"\nPhoneNo: {contact.PhoneNo}\nEmail: {contact.Email}\nAddress: {contact.Address}\nCity: {contact.City}" +
                    $"\nState: {contact.State}\nType: {contact.Type}\n");
            }
        }
    }
}
