using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CykelLib;
using Newtonsoft.Json;

namespace TCPServerCykel
{
    internal class CykelServerWorker
    {
        private static List<Cykel> cykels = new List<Cykel>()
        {
            new Cykel(1, "rød", 200, 16),
            new Cykel(2, "blå", 500, 32)
        };

        internal void Start()
        {
            TcpListener server = new TcpListener(IPAddress.Loopback, 4646);
            server.Start();
            while (true)
            {
                TcpClient socket = server.AcceptTcpClient();
                Task.Run(() =>
                {
                    TcpClient tempSocket = socket;
                    DoClient(tempSocket);
                });
            }
        }

        private void DoClient(TcpClient socket)
        {

            NetworkStream ns = socket.GetStream();

            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);



            //Cykel cykel = JsonConvert.DeserializeObject<Cykel>();
            //String cykelString = sr.ReadLine();


            sw.AutoFlush = true;

            String str1 = sr.ReadLine();
            String str2;// = sr.ReadLine();
            //String[] vs = str1.Split(" ");

            String strRetur = "Resultat = ";
            //String command = vs[0];
            String test = "virker det?";
            String json = JsonConvert.SerializeObject(cykels);


            if (str1 == "HentAlle")
            {

                sw.WriteLine(strRetur + json);
            }
            else if (str1 == "Hent")
            {
                str2 = sr.ReadLine();
                var Id = Convert.ToInt32(str2);
                sw.WriteLine(strRetur + JsonConvert.SerializeObject(cykels.Find(c => c.Id == Id)));

            }
            else if (str1 == "Gem")
            {

                sw.WriteLine("{\"Id\":1,\"Farve\":\"rÃ¸d\",\"Pris\":200,\"Gear\":16}");
                str2 = sr.ReadLine();
                Cykel nycykel = JsonConvert.DeserializeObject<Cykel>(str2);
                cykels.Add(nycykel);

            }

        }
    }
}
