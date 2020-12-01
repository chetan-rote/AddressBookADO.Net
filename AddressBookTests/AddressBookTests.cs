using Microsoft.VisualStudio.TestTools.UnitTesting;
using AddressBookDB;
using System;
using System.Collections.Generic;
using RestSharp;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        /// <summary>
        /// UC23
        /// Given the multiple data on post should return total count.
        /// </summary>
        [TestMethod]
        public void GivenMultipleContacts_OnPost_ShouldReturnTotalCount()
        {
            List<Contact> contacts = new List<Contact>();
            contacts.Add(new Contact { FirstName = "Shubham", LastName = "Dubey", ZipCode = "402511", Address = "Pitampura", City = "Indore", State = "Madhya Pradesh", PhoneNo = "8745960125", Email = "shubham@gmail.com", Type = "Profession" });
            contacts.Add(new Contact { FirstName = "Rugved", LastName = "Jage", ZipCode = "600125", Address = "SakiNaka", City = "Heydrabad", State = "Telangana", PhoneNo = "9452660125", Email = "rugved@gmail.com", Type = "Friends" });
            /// Iterating over the addressbook list to get each instance.
            foreach (Contact contact in contacts)
            {
                //Arrange
                /// Adding the request to post data to the rest API.
                RestRequest request = new RestRequest("/addressBook", Method.POST);
                JObject jObject = new JObject();
                /// Adding the data attribute with data elements.
                jObject.Add("FirstName", contact.FirstName);
                jObject.Add("LastName", contact.LastName);
                jObject.Add("ZipCode", contact.ZipCode);
                jObject.Add("Address", contact.Address);
                jObject.Add("City", contact.City);
                jObject.Add("State", contact.State);
                jObject.Add("PhoneNo", contact.PhoneNo);
                jObject.Add("Email", contact.Email);
                jObject.Add("Type", contact.Type);
                request.AddParameter("application/json", jObject, ParameterType.RequestBody);
                //Act
                IRestResponse response = restClient.Execute(request);
                //Assert
                Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            }
            IRestResponse restResponse = GetAddressBook();
            List<Contact> dataResponse = JsonConvert.DeserializeObject<List<Contact>>(restResponse.Content);
            /// Checks the count is correct.
            Assert.AreEqual(6, dataResponse.Count);
        }
        /// <summary>
        /// UC 24
        /// Given contact record on update should return updated contact. 
        /// </summary>
        [TestMethod]
        public void GivenContacttOnUpdate_ShoulReturnUpdatedContact()
        {
            Contact contact = new Contact();
            contact.FirstName = "Shubham";
            contact.LastName = "Dubey";
            contact.ZipCode = "402511";
            contact.Address = "Pitampura";
            contact.City = "Bhopal";
            contact.State = "Madhya Pradesh";
            contact.PhoneNo = "7748562152";
            contact.Email = "shubham@gmail.com";
            contact.Type = "Profession";
            //Arrange
            RestRequest restRequest = new RestRequest("/addressBook/5", Method.PUT);
            JObject jObject = new JObject();
            jObject.Add("FirstName", contact.FirstName);
            jObject.Add("LastName", contact.LastName);
            jObject.Add("ZipCode", contact.ZipCode);            
            jObject.Add("Address", contact.Address);
            jObject.Add("City", contact.City);
            jObject.Add("State", contact.State);
            jObject.Add("PhoneNo", contact.PhoneNo);
            jObject.Add("Email", contact.Email);
            jObject.Add("Type", contact.Type);
            restRequest.AddParameter("application/json", jObject, ParameterType.RequestBody);
            //Act
            IRestResponse restResponse = restClient.Execute(restRequest);
            //Assert
            Assert.AreEqual(restResponse.StatusCode, HttpStatusCode.OK);
            Contact dataResponse = JsonConvert.DeserializeObject<Contact>(restResponse.Content);
            Assert.AreEqual(contact.FirstName, dataResponse.FirstName);
            Assert.AreEqual(contact.City, dataResponse.City);
            Console.WriteLine(restResponse.Content);
        }
        /// <summary>
        /// UC 25
        /// Given contact Id deletes the record with id and validates whether deleted or not.
        /// </summary>
        [TestMethod]
        public void DeleteRecordOnAddressBook_ValidateRecordDeleted()
        {
            //Arrange
            RestRequest restRequest = new RestRequest("/addressBook/5", Method.DELETE);
            //Act
            IRestResponse restResponse = restClient.Execute(restRequest);
            //Assert
            Assert.AreEqual(restResponse.StatusCode, HttpStatusCode.OK);
            Console.WriteLine(restResponse.Content);
        }
    }
}
