using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RESTfullAPIRoComputer.Model
{
    public class UDPReceiver
    {
        public UdpClient connectionSocket { get; set; }



        public UDPReceiver(UdpClient connection)
        {
            connectionSocket = connection;
        }

        public static async void doit(Person person)
        {
            UdpClient udpServer = new UdpClient(1111); // UDP Port
            IPAddress ip = IPAddress.Parse("192.168.24.142"); // sætter en ip
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(ip, 1111); // laver et remote End point
            //Console.WriteLine("Ready to gather data"); // Giver en besked, så vi ved vi er connected
            var list = new List<decimal>(); // laver en liste til vores data
            var listTid = new List<DateTime>(); // laver en liste til vores tid
            try
            {
                while (true)
                {

                    Byte[] receiveBytes = udpServer.Receive(ref RemoteIpEndPoint); // modtager nogle bytes fra UDP Senderen
                    string receivedData = Encoding.ASCII.GetString(receiveBytes); // Laver bytes om til string
                    if (receivedData.Contains("stop")) // metode til at gemme data
                    {
                        var success = saveData(list, listTid, person); // Kalder saveData Metoden
                        //Console.WriteLine("Saved data: " + success); //Udskriver om data'en er gemt ordentligt
                        list.Clear(); // Tømmer listen til vi skal gemme noget nyt
                        listTid.Clear(); // tømmer listen til vi skal gemme noget nyt
                    }
                    else
                    {
                        string[] data = receivedData.Split(' '); //Laver et string array af vores modtaget data, og deler den ved space
                        decimal force = 0; // variable til at beregne force fra UDP Senderen
                        if (!string.IsNullOrEmpty(data[8])) // tjekker om en string ikke er null, eller tom på datafelt 8.
                            force = Convert.ToDecimal(data[8], CultureInfo.InvariantCulture); // Convertere vores string over til decimal.

                        DateTime tid = new DateTime(); // Laver en variable datetime
                        if (!string.IsNullOrEmpty(data[3])) // tjekker om en string ikke er null, eller tom på datafelt 3.
                            tid = Convert.ToDateTime(data[3]); // Convertere string over til datetime
                        string text = "Tid: " + tid.ToString() + " Force: " + force; // en variable med vores modtaget data
                        //Console.WriteLine(text); // udskriver vores modtaget data
                        list.Add(force); // tilføjer vores modtaget data til en liste af decimal
                        listTid.Add(tid); // tilføjere vores tid til en liste af tid.
                        //string sendData = "Server: " + text.ToUpper(); // serveren sender
                        //Byte[] sendBytes = Encoding.ASCII.GetBytes(sendData); // 
                        //udpServer.Send(sendBytes, sendBytes.Length, RemoteIpEndPoint); //

                        //Console.WriteLine(text);
                    }
                }
            }
            catch (Exception ex)
            {
                // reconnector til UDP Sender
                doit(person);
            }

        }

        public static bool saveData(List<decimal> list, List<DateTime> listTid, Person person)
        {
            decimal tryngdekraft = 9.82M; // Tyngdekraft udregning
            var avg = list.Average(); // finder gennemsnittet af vores acceleration
            var lastTime = listTid.Last(); // finder det sidste element i vores liste af tid
            var firstTime = listTid.First();// finder det første element i vores liste af tid
            var time = lastTime - firstTime; // finder differentien af vores tid
            decimal accel = avg * tryngdekraft; // udregning af vores acceleration
            decimal hastighed = accel * Convert.ToInt16(time.Seconds); //udregning af hastighed i m/s
            person.Data.Acceleration = accel; // tilføjer vores data til et objekt
            person.Data.Hastighed = hastighed; // tilføjer vores data til et objekt

            person.Data.Tid = Convert.ToString(time); // tilføjer vores data til et objekt
            person.Data.FkEmail = person.Email; //tilføjer vores data til et objekt
            return Person.SetData(person.Data); //Den gemmer person data og returernerer en true, eller false, om det går godt, eller skidt.
        }
    }

    }

