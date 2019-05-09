using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace UDPEchoServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Person.GetConnection();

            //Creates a UdpClient for reading incoming data.
            UdpClient udpServer = new UdpClient(1111);

            //Creates an IPEndPoint to record the IP Address and port number of the sender.  
            IPAddress ip = IPAddress.Parse("192.168.1.33");
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(ip, 1111);
           
            try
            {
                // Blocks until a message is received on this socket from a remote host (a client).
                Console.WriteLine("Server is blocked");
                var list = new List<double>();
                var listTid = new List<DateTime>();
                while (true)
                {
                    
                    Byte[] receiveBytes = udpServer.Receive(ref RemoteIpEndPoint);
                    //Server is now activated");

                    string receivedData = Encoding.ASCII.GetString(receiveBytes);
                    string[] data = receivedData.Split(' ');

                    double force = 0;
                    if (!string.IsNullOrEmpty(data[8]))
                        force = Convert.ToDouble(data[8]);

                    DateTime tid = new DateTime();
                    if(!string.IsNullOrEmpty(data[3]))
                        tid = Convert.ToDateTime(data[3]);

                    string text = "Tid: " + tid.ToString() + " Force: " + force;
                    Console.WriteLine(text);

                    list.Add(force);
                    listTid.Add(tid);

                    if (receivedData.Contains("stop"))
                        break;

                    string sendData = "Server: " + text.ToUpper();
                    Byte[] sendBytes = Encoding.ASCII.GetBytes(sendData);

                    udpServer.Send(sendBytes, sendBytes.Length, RemoteIpEndPoint);

                   
                }
                var avg = list.Average();
                var lastTime = listTid.Last();
                var firstTime = listTid.First();
                var time = lastTime - firstTime;
                

                Person.PersonDTO person = new Person.PersonDTO();
                person.Fornavn = "Frank";
                person.Data.Acceleration = avg;
                person.Data.Hastighed = 0;

                var success = Person.createPerson(person);

                Console.WriteLine($"Average: {avg} - Time: {lastTime}");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        
    }
}
