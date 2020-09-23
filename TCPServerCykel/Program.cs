using System;

namespace TCPServerCykel
{
    class Program
    {
        static void Main(string[] args)
        {
            CykelServerWorker worker = new CykelServerWorker();
            worker.Start();

            Console.ReadLine();
        }
    }
}
