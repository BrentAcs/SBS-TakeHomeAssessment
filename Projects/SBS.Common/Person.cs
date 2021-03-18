using System;

namespace SBS.Common
{
  public class Person
  {
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
  }

  public class DisplayWidthAttribute
  {
    public DisplayWidthAttribute(int width)
    {
      Width = width;
    }

    public int Width { get; }
  }
}
