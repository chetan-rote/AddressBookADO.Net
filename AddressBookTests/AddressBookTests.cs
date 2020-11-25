using Microsoft.VisualStudio.TestTools.UnitTesting;
using AddressBookDB;

namespace AddressBookTests
{
    [TestClass]
    public class AddressBookTests
    {
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
            string[] name = "Kunal Patil".Split(" ");
            string phoneNo = "9858647525";
            AddressBookRepository repository = new AddressBookRepository();
            /// Act
            bool result = repository.UpdateContact(name, phoneNo);
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
            string[] name = "Kunal P".Split(" ");
            string phoneNo = "9858647525";
            AddressBookRepository repository = new AddressBookRepository();
            /// Act
            bool result = repository.UpdateContact(name, phoneNo);
            /// Assert
            Assert.AreEqual(result, false);
        }
    }
}
