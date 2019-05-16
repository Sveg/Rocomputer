using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Remotion.Linq.Parsing;

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

        public virtual ICollection<PersonData> PersonData { get; set; }


        private static SqlConnection connection = new SqlConnection();

        

        public static bool Login(Person person)
        {
            string connectionString =
                "Server=tcp:lula.database.windows.net,1433;Initial Catalog=3_semester_projekt;Persist Security Info=False;User ID=luca1291;Password=Simpel123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
            try
            {
                //using (SqlConnection con = new SqlConnection(connectionString))
                //{
                //    con.Open();
                //    SqlCommand command = new SqlCommand("INSERT INTO Person VALUES(@Email, @Fornavn, @Efternavn)", con);
                //    command.Parameters.AddWithValue("@Email", person.Email);
                //    command.Parameters.AddWithValue("@Fornavn", person.Fornavn);
                //    command.Parameters.AddWithValue("@Efternavn", person.Efternavn);
                //    command.ExecuteNonQuery();
                //}

                //return true;

                using (var cmd = connection.CreateCommand())
                {
                     

                     cmd.CommandText = $@"if not exists(select top 1 * from Person where email = '{person.Email}') insert into Person VALUES ('{person.Email}','{person.Fornavn}', '{person.Efternavn}')";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

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
    }
}