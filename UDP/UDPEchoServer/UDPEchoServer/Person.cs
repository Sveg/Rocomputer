using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
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
            //help
            try
            {
                using (var cmd = connection.CreateCommand())
                {
                    //Vi skal have id til at auto generere i dben, så skulle det virke :D okay ty :D 
                    cmd.CommandText = $@"insert into Person (Email, Fornavn, Efternavn) VALUES ('{model.Email}','{model.Fornavn}', '{model.Efternavn}');SELECT SCOPE_IDENTITY()";
                    model.Email = Convert.ToString(cmd.ExecuteScalar());
                    model.Data.FK_Email = model.Email;
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
                        (Hastighed, Acceleration, Tid, FK_Email) VALUES 
                ({hastighedCalculator}, {AccelCalculator},'{model.Tid}', '{model.FK_Email}')";
                    
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
            public string Fornavn { get; set; }
            public string Efternavn { get; set; }
            public string Email { get; set; }
            public PersonDataDTO Data { get; set; } = new PersonDataDTO();

            
        }
        public class PersonDataDTO
        {
            private static int nextId;
            public int Id { get; set; }
            public decimal Hastighed { get; set; }
            public decimal Acceleration { get; set; }
            public string Tid { get; set; }
            public string FK_Email { get; set; }

            public PersonDataDTO()
            {
                if (Id == 0 || Id == null)
                {
                    Id = Id + 1;
                }
                else if (Id == 1)
                {
                    Id = nextId++;
                }
            }
        }
    }
}
