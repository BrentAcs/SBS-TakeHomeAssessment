using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using CsvHelper;
using NetMQ;
using NetMQ.Sockets;
using SBS.Common;

namespace Sender
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("SBS Assessment sender!");

      // TODO: add support for accepting command line file(s)
      string csvPath = @"./people.csv";
      if (!File.Exists(csvPath))
      {
        Console.WriteLine($"Input file doesn't exist.");
      }

      var records = ReadCsv(csvPath);

      var record = records.First();
      var json = record.ToJson();


      using (var client = new RequestSocket())
      {
        client.Connect("tcp://localhost:5555");
        client.SendFrame(json);


        var message = client.ReceiveFrameString();
        //Console.WriteLine("Received {0}", message);
      }

      Console.WriteLine("Press any key to quit.");
      Console.ReadKey();
    }

    private static IEnumerable<Person> ReadCsv(string csvPath)
    {
      using var reader = new StreamReader(csvPath);
      using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
      var records = csv.GetRecords<Person>();
      return records.ToList();
    }
  }
}
