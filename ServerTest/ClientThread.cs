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
            SocketUtilities.SocketUtilityClass su = 
                new SocketUtilities.SocketUtilityClass(client);

            su.Open();

            bool jatka = true;

            while (jatka)
            {
                // Viimeisimmän komennon vastaanottamisen ajankohta:
                DateTime timeLastCommandReceived = DateTime.Now;

                // Tarkistetaan, onko dataa syötettäväksi
                if (su.DataAvailable())
                {
                    // Luetaan ja käsitellään komennot
                    string command = su.ReadMessage();
                    string answer = "";

                    switch (command)
                    {
                        case SocketUtilities.Commands.TIME:
                            answer = DateTime.Now.ToString();
                            break;
                        case SocketUtilities.Commands.NUMBER_OF_CLIENTS:
                            answer = "1"; // 1000
                            break;
                        case SocketUtilities.Commands.QUIT:
                            answer = "lopetus";
                            jatka = false;
                            break;
                    }
                    su.WriteMessage(answer);
                    Console.WriteLine(answer);
                }
                else // if (su.DataAvailable())
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
            su.Close();
            
        }
    }
}
