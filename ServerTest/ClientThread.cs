using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Threading;
namespace ServerTest
{
    // Tässä luokassa on asiakkaan palveleminen
    // Omassa säikeessään (sivu 34, Sockets.pptx)
    class ClientThread
    {
        private TcpClient client;
        public ClientThread(TcpClient client)
        {
            this.client = client;
        }

        // Ajetaan omassa säikeessään
        public void ServeClient()
        {
            // Avataan yhteydet
            // Avataan streamit
            NetworkStream ns = client.GetStream();
            StreamWriter sw = new StreamWriter(ns);
            StreamReader sr = new StreamReader(ns);
            sw.AutoFlush = true;

            bool jatka = true;

            while (jatka)
            {
                // Viimeisimmän komennon vastaanottamisen ajankohta:
                DateTime timeLastCommandReceived = DateTime.Now;

                // Tarkistetaan, onko dataa syötettäväksi
                if (ns.DataAvailable)
                {
                    // Luetaan ja käsitellään komennot
                    string command = sr.ReadLine();
                    string answer = "";

                    switch (command)
                    {
                        case "TIME":
                            answer = DateTime.Now.ToString();
                            break;
                        case "NUMBER_OF_CLIENTS":
                            answer = "1"; // 1000
                            break;
                        case "QUIT":
                            answer = "lopetus";
                            jatka = false;
                            break;
                    }
                    sw.WriteLine(answer);
                    Console.WriteLine(answer);
                }
                else // if (ns.DataAvailable)
                {
                    // Lopetetaan, jos viimeisestä komennosta on kulunut yli minuutti:
                    DateTime timeNow = DateTime.Now;
                    TimeSpan erotus = timeNow - timeLastCommandReceived;

                    if (erotus.TotalSeconds > 60)
                        jatka = false;
                    else
                        Thread.Sleep(500);
                }
            }
            // Suljetaan yhteydet
            sw.Close();
            sr.Close();
            ns.Close();
            client.Close();
            
        }
    }
}
