using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigurationLibrary.Classes;
using NorthWindLibrary.Projections;

namespace BaseUnitTestProject.Base
{
    public class SqlServerOperations
    {
        protected static string ConnectionString = ConfigurationHelper.ConnectionString();

        public static CustomerData CustomerData(int customerId)
        {
            CustomerData customer = new CustomerData();
            using var cn = new SqlConnection(ConnectionString);
            using var cmd = new SqlCommand { Connection = cn, CommandText = SqlStatements.Main};

            cmd.Parameters.Add("@CustomerIdentifier", SqlDbType.Int).Value = customerId;
            cn.Open();

            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();

                customer.CustomerIdentifier = customerId;
                customer.CompanyName = reader.GetString(1);
                customer.City = reader.GetString(2);
                customer.PostalCode = reader.GetString(3);
                customer.ContactId = reader.GetInt32(4);
                customer.FirstName = reader.GetString(5);
                customer.LastName = reader.GetString(6);
                customer.ContactTypeIdentifier = reader.GetInt32(7);
                customer.ContactTitle = reader.GetString(8);
                customer.CountryIdentifier = reader.GetInt32(9);
                customer.Country = reader.GetString(10);
                customer.PhoneTypeIdentifier = reader.GetInt32(11);
                customer.PhoneNumber = reader.GetString(12);

            }


            return customer;
        }
    }

    public class CustomerData
    {
        public int CustomerIdentifier { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ContactTypeIdentifier { get; set; }
        public string ContactTitle { get; set; }
        public int CountryIdentifier { get; set; }
        public string Country { get; set; }
        public int PhoneTypeIdentifier { get; set; }
        public string PhoneNumber { get; set; }
        public override string ToString() => CustomerIdentifier.ToString();
    }

    public class SqlStatements
    {
        public static string Main => @"
SELECT        
	Cust.CustomerIdentifier, 
	Cust.CompanyName, 
	Cust.City, 
	Cust.PostalCode, 
	Contacts.ContactId, 
	Contacts.FirstName, 
	Contacts.LastName, 
	Cust.ContactTypeIdentifier, 
	CT.ContactTitle,
	Countries.CountryIdentifier, 
	Countries.Name AS Country, 
	Devices.PhoneTypeIdentifier, 
	Devices.PhoneNumber
FROM            Customers AS Cust INNER JOIN
                         ContactType AS CT ON Cust.ContactTypeIdentifier = CT.ContactTypeIdentifier INNER JOIN
                         Countries ON Cust.CountryIdentifier = Countries.CountryIdentifier INNER JOIN
                         Contacts ON Cust.ContactId = Contacts.ContactId INNER JOIN
                         ContactDevices AS Devices ON Contacts.ContactId = Devices.ContactId
WHERE         (Cust.CustomerIdentifier = @CustomerIdentifier)";
    }
}
