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
    public class SocketUtilityClass
    {
        // Jäsenmuuttujat
        private TcpClient client;
        private NetworkStream ns;
        private StreamWriter sw;
        private StreamReader sr;

        public SocketUtilityClass(TcpClient client)
        {
            this.client = client;
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
