using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDPEchoServer
{
    public static class Person
    {
        private static SqlConnection connection = new SqlConnection();
        public static SqlConnection GetConnection()
        {
            if (connection.State == ConnectionState.Closed)
            {
                
                connection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                connection.Open();
            }
            return connection;
        }

        public static bool createPerson(PersonDTO model)
        {
            try
            {
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $@"insert into Person (Fornavn, Efternavn, Email) VALUES ('{model.Fornavn}', '{model.Efternavn}', '{model.Email}');SCOPE_IDENTITY()";
                    model.Id = Convert.ToInt32(cmd.ExecuteScalar());
                    model.Data.FK_Person = model.Id;
                }

            }
            catch (Exception e)
            {
                return false;
            }

            return setPersonData(model.Data);
        }
        public static bool setPersonData(PersonDataDTO model)
        {
            try
            {
                using (var cmd = connection.CreateCommand())
                {
                    //UPDATE table_name
                    //SET column1 = value1, column2 = value2, ...
                    //WHERE id = 0;
                    cmd.CommandText = $@"insert into PersonData (FK_Person, Hastighed, Acceleration) VALUES ({model.FK_Person}, {model.Hastighed}, {model.Acceleration}')";
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public class PersonDTO
        {
            
            public int Id { get; set; }
            public string Fornavn { get; set; }
            public string Efternavn { get; set; }
            public string Email { get; set; }
            public PersonDataDTO Data { get; set; } = new PersonDataDTO();
        

        }
        public class PersonDataDTO
        {
            public int Id { get; set; }
            public int FK_Person { get; set; }
            public int Hastighed { get; set; }
            public double Acceleration { get; set; }
        }
    }
}
