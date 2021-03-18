using System;
using System.Threading;
using NetMQ;
using NetMQ.Sockets;
using SBS.Common;

namespace Receiver
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("SBS Assessment receiver");

      using var server = new ResponseSocket();
      server.Bind("tcp://*:5555");
      while (true)
      {
        var message = server.ReceiveFrameString();
        Console.WriteLine("Received {0}", message);

        var person = message.FromJson<Person>();
        foreach (var propertyInfo in person.GetType().GetProperties())
        {
          Console.Write($"{propertyInfo.Name,-10} ");
        }
        Console.WriteLine();


        
        Thread.Sleep(100);
        //Console.WriteLine("Sending World");
        server.SendFrame("Received.");
      }
    }
  }
}
