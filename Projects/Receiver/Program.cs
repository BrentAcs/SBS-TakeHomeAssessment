using System;
using System.Linq;
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

      bool hasHeader = false;

      using var server = new ResponseSocket();
      server.Bind("tcp://*:5555");
      while (true)
      {
        var message = server.ReceiveFrameString();
        var person = message.FromJson<Person>();

        if (!hasHeader)
        {
          WriteHeader(person);
          hasHeader = true;
        }

        WritePerson(person);

        server.SendFrame($"Received person with Id: {person.Id}.");
      }
    }

    public static void WritePerson(Person person)
    {
      foreach (var propertyInfo in person.GetType().GetProperties())
      {
        DisplayWidthAttribute displayWidth = (DisplayWidthAttribute)propertyInfo
          .GetCustomAttributes(typeof(DisplayWidthAttribute), false)
          .FirstOrDefault();

        // NOTE: For brevity, assume no nulls will be read.
        string value = propertyInfo.GetValue(person).ToString();
        // NOTE: Padding was creating pre-mature balding, give the 3 hour "deadline", so this works.
        Console.Write($"{value}{new string(' ', displayWidth.Width - value.Length)}");
      }

      Console.WriteLine();
    }

    public static void WriteHeader(Person person)
    {
      int col = 0;
      foreach (var propertyInfo in person.GetType().GetProperties())
      {
        DisplayWidthAttribute displayWidth = (DisplayWidthAttribute)propertyInfo
          .GetCustomAttributes(typeof(DisplayWidthAttribute), false)
          .FirstOrDefault();

        // NOTE: Padding was creating pre-mature balding, give the 3 hour "deadline", so this works.
        Console.Write($"{propertyInfo.Name}{new string(' ', displayWidth.Width - propertyInfo.Name.Length)}");
      }

      Console.WriteLine();
    }
  }
}
