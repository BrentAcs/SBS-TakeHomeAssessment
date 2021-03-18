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
      using (var client = new RequestSocket())
      {
        client.Connect("tcp://localhost:5555");

        foreach (var person in records)
        {
          var json = person.ToJson();
          client.SendFrame(json, false);
          var message = client.ReceiveFrameString();
          Console.WriteLine($"Receiver responded with: {message}");
        }
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
