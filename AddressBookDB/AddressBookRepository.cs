﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AddressBookDB
{
    public class AddressBookRepository
    {
        /// Specifying the connection string from the sql server connection.
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AddressBook_DB;Integrated Security=True";
        /// Establishing the connection using the Sql Connection.
        SqlConnection connection;
        /// <summary>
        /// The contact list to store contacts.
        /// </summary>
        List<Contact> contacList = new List<Contact>();
        int count;
        /// <summary>
        /// UC1 Retrieves all data from database.
        /// </summary>
        /// <returns></returns>
        public bool RetrieveFromDatabase()
        {
            connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    ///Query to get all data from DB.
                    string query = "Select Type,FirstName,LastName,Address,Contact.ZipCode,PhoneNo,Email,City,State" +
                                    " from AddressBook Inner JOIN  AddressBookContact On AddressBook.BookId = AddressBookContact.BookId" +
                                    " Inner Join Contact ON AddressBookContact.CId = Contact.CId" +
                                    " Inner Join Zip ON Contact.ZipCode = Zip.ZipCode;";
                    /// Impementing the command on the connection fetched database table.
                    SqlCommand cmd = new SqlCommand(query, connection);
                    /// Opening the connection to start mapping.
                    this.connection.Open();
                    /// executing the sql data reader to fetch the records.
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        /// Moving to the next record from the table
                        while (reader.Read())
                        {
                            Contact c = new Contact();
                            c.Type = reader.GetString(0);
                            c.FirstName = reader.GetString(1);
                            c.LastName = reader.GetString(2);
                            c.Address = reader.GetString(3);
                            c.ZipCode = reader.GetString(4);
                            c.PhoneNo = reader.GetString(5);
                            c.Email = reader.GetString(6);
                            c.City = reader.GetString(7);
                            c.State = reader.GetString(8);

                            contacList.Add(c);

                            Console.WriteLine(c.Type + "  " + c.FirstName + "  " + c.LastName + "  " + c.Address + "  " + c.ZipCode + "  " +
                                c.City + "  " + c.State + "  " + c.PhoneNo + "  " + c.Email);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Table is empty");
                    }
                    return true;
                }

            }
            /// Catching the exception.
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            /// Always ensuring the closing of the connection.
            finally
            {
                connection.Close();
            }
            return false;
        }
        /// <summary>
        /// UC2 Updates the contact.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phoneNo"></param>
        /// <returns></returns>
        public bool UpdateContact(string[] name, string phoneNo)
        {
            connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    /// Updating details using the stored procedure.
                    SqlCommand command = new SqlCommand("SpUpdateDetails", this.connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FirstName", name[0]);
                    command.Parameters.AddWithValue("@LastName", name[1]);
                    command.Parameters.AddWithValue("@PhoneNo", phoneNo);
                    /// Opening the connection to start mapping.
                    this.connection.Open();
                    /// Storing the result of the executed query.
                    var result = command.ExecuteNonQuery();
                    /// Checks for the rows.
                    if (result != 0)
                    {
                        Console.WriteLine("Contact updated successfully");
                        return true;
                    }                    
                    Console.WriteLine("No such contact");
                    return false;
                }
            }
            /// Catching the exception.
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            /// Alway ensuring the closing of the connection.
            finally
            {
                connection.Close();
            }
            return false;
        }
    }
}
