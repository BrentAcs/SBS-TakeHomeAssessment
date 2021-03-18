using System;
using System.Globalization;
using System.IO;
using CsvHelper;
using SBS.Common;

namespace Sender
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Hello World, I'm Sender!");

      // TODO: add support for accepting command line file(s)
      string csvPath = @"./people.csv";
      if (!File.Exists(csvPath))
      {
        Console.WriteLine($"Input file doesn't exist.");
      }

      using var reader = new StreamReader(csvPath);
      using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
      var records = csv.GetRecords<Person>();

      Console.ReadKey();
    }
  }
}
