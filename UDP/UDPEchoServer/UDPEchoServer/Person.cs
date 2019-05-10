using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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

                connection.ConnectionString =
                    @"Server=tcp:lula.database.windows.net,1433;Initial Catalog=3_semester_projekt;Persist Security Info=False;User ID=luca1291;Password=Simpel123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
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
                    //Vi skal have id til at auto generere i dben, så skulle det virke :D okay ty :D 
                    cmd.CommandText = $@"insert into Person (Fornavn, Efternavn, Email) VALUES ('{model.Fornavn}', '{model.Efternavn}', '{model.Email}');SELECT SCOPE_IDENTITY()";
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
                    var hastighedCalculator = model.Hastighed.ToString().Replace(",", ".");
                    var AccelCalculator = model.Acceleration.ToString().Replace(",", ".");


                    cmd.CommandText = $@"insert into PersonData 
                        (Hastighed, Acceleration, Tid, FK_Person) VALUES 
                ({hastighedCalculator}, {AccelCalculator},'{model.Tid}', {model.FK_Person})";
                    
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
            public decimal Hastighed { get; set; }
            public decimal Acceleration { get; set; }
            public string Tid { get; set; }
            public int FK_Person { get; set; }
        }
    }
}
