/**********************************************************************************
 *  Purpose: This code will try to mock the query implementation using the ado.net.
 *
 *
 *  @author  Chetan Rote
 *  @version 1.0
 *  @since   24-11-2020
 **********************************************************************************/
using System;

namespace AddressBookDB
{
    class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            int choice;
            Console.WriteLine("Welcome to Address Book Database.");
            AddressBookRepository repository = new AddressBookRepository();
            do
            {
                Console.WriteLine("1.Retrieve all data.\n2.Update Contact.\n3.Get Contacts by date range.\n4.Exit.");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        repository.RetrieveFromDatabase();
                        break;
                    case 2:
                        Console.WriteLine("Enter name");
                        string[] name = Console.ReadLine().Split(" ");
                        Console.WriteLine("Enter phone no");
                        string phoneNo = Console.ReadLine();
                        repository.UpdateContact(name, phoneNo);
                        break;
                    case 3:
                        Console.WriteLine("Enter start date");
                        string startDate = Console.ReadLine();
                        Console.WriteLine("Enter end date");
                        string endDate = Console.ReadLine();
                        repository.GetContactsByDateRange(startDate, endDate);
                        break;
                    case 4:
                        Console.WriteLine("Thank you");
                        break;
                    default:
                        Console.WriteLine("Invalid Option");
                        break;
                }
            }
            while (choice != 4);
            Console.ReadKey();
        }
    }
}
