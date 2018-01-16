﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace ClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 8221;
            TcpClient client = new TcpClient("localhost", port);

            NetworkStream ns = client.GetStream();
            StreamWriter sw = new StreamWriter(ns);
            StreamReader sr = new StreamReader(ns);

            sw.AutoFlush = true;

            string vastaus = "";
            while (true)
            {
                Console.WriteLine("Anna komento ( TIME / NUMBER_OF_CLIENTS / QUIT )");
                string komento = Console.ReadLine();

                // lähetetään komento
                sw.WriteLine(komento);

                // odotetaan vastausta
                vastaus = sr.ReadLine();
                Console.WriteLine(vastaus);
                if (komento == "QUIT")
                    break;
            }

            /*
            sw.WriteLine("TIME");
            vastaus = sr.ReadLine();
            Console.WriteLine(vastaus);
            */
            sw.Close();
            sr.Close();
            ns.Close();
            client.Close();

            Console.ReadKey();
        }
    }
}
