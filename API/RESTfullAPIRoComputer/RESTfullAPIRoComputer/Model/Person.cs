using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Remotion.Linq.Parsing;
using RESTfullAPIRoComputer.Model;

namespace RESTfullAPIRoComputer
{
    public partial class Person
    {
        public Person()
        {
            PersonData = new HashSet<PersonData>();
        }

        public string Email { get; set; }
        public string Fornavn { get; set; }
        public string Efternavn { get; set; }
        public PersonData Data { get; set; } = new PersonData();

        public virtual ICollection<PersonData> PersonData { get; set; }


        private static SqlConnection connection = new SqlConnection();

        

        public static bool Login(Person person) // Metode til login med data fra google
        {
            try
            {
                
                // når den kaldes køres dette
                using (var cmd = connection.CreateCommand())
                {
                     
                    // insert statement, for at indsætte den  i databasen, med emailen fra google sign in.
                    // hvor den spørger om mailen eksiterer. Hvis mail eksistere i databasen, kører den videre til næste metode.
                     cmd.CommandText = $@"if not exists(select top 1 * from Person where email = '{person.Email}') insert into Person VALUES ('{person.Email}','{person.Fornavn}', '{person.Efternavn}')";
                    cmd.ExecuteNonQuery();
                    // samtidig kører vores doit metode, som skaber connection, til vores UDPSender
                    new Task(() => { UDPReceiver.doit(person); }).Start();
                    
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        public static bool SetData(PersonData model)
        {
            try
            {
                using (var cmd = connection.CreateCommand())
                {

                    var hastighedCalculator = model.Hastighed.ToString().Replace(",", "."); //Metode til at erstatte , med .
                    var AccelCalculator = model.Acceleration.ToString().Replace(",", "."); //Metode til at erstatte , med .

                    // sql query til at indsætte data på det dataobjekt, som er logget ind via google.
                    cmd.CommandText = $@"insert into PersonData 
                        (Hastighed, Acceleration, Tid, FK_Email) VALUES 
                ({hastighedCalculator}, {AccelCalculator},'{model.Tid}', '{model.FkEmail}')";

                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static SqlConnection GetConnection() // metode til at skabe til databasen. Bliver kaldt i program.cs
        {
            if (connection.State == ConnectionState.Closed) // tjekker om connection er lukket. Hvis den er, bruger vi vores connection string, og åbner op for connection
            {

                connection.ConnectionString =
                    @"Server=tcp:lula.database.windows.net,1433;Initial Catalog=3_semester_projekt;Persist Security Info=False;User ID=luca1291;Password=Simpel123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                connection.Open();
            }
            // ellers returnerer den bare connection
            return connection;
        }
    }
}