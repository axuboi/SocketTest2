using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace SocketUtilities
{
    public class Commands
    {
        public const string TIME = "TIME";
        public const string NUMBER_OF_CLIENTS = "NUMBER_OF_CLIENTS";
        public const string QUIT = "QUIT";
    }

    public class SocketUtilityClass
    {
        // Jäsenmuuttujat
        private TcpClient client;
        private NetworkStream ns;
        private StreamWriter sw;
        private StreamReader sr;


        public void WriteMessage(string s)
        {
            sw.WriteLine(s);
        }

        public string ReadMessage()
        {
            return sr.ReadLine();
        }

        public SocketUtilityClass(TcpClient client)
        {
            this.client = client;
        }

        public bool DataAvailable()
        {
            return ns.DataAvailable;
        }

        public void Open()
        {
            ns = client.GetStream();
            sw = new StreamWriter(ns);
            sr = new StreamReader(ns);

            sw.AutoFlush = true;
        }

        public void Write(string msg)
        {
            sw.WriteLine(msg);
        }

        public string Read()
        {
            return sr.ReadLine();
        }

        public void Close()
        {
            sr.Close();
            sw.Close();
            ns.Close();
            client.Close();
        }
    }
}
