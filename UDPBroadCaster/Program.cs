using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace UDPBroadCaster
{
    class Program
    {
        static void Main(string[] args)
        {
            var b = new Broadcaster();
            b.Send();
            Console.ReadKey();
        }
    }

    public class Broadcaster
    {
        public async Task Send()
        {
            var sender = new Sender();
            Console.WriteLine("Press ESC to stop");
            ConsoleKeyInfo cki;
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    cki = Console.ReadKey(true);
                    if (cki.Key == ConsoleKey.Escape)
                        break;
                }
                Task wait = Task.Delay(3600);
                Console.WriteLine("sending... " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss"));
                sender.Send(string.Format("this is UDPBroacaster calling" ));
                await wait;
            }
            
        }
    }

    public class Sender
    {
        public void Send(string message)
        {

            using (var client = new UdpClient())
            {
                
                IPEndPoint ip = new IPEndPoint(IPAddress.Parse("172.16.10.255"), 1900);
                byte[] bytes = Encoding.ASCII.GetBytes(message);
                client.Send(bytes, bytes.Length, ip);
                client.Close();
            }

        }
    }
}
