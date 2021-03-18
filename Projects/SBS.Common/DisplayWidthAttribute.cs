using System;

namespace SBS.Common
{
  [AttributeUsage(AttributeTargets.Property)]
  public class DisplayWidthAttribute : Attribute
  {
    public DisplayWidthAttribute(int width)
    {
      Width = width;
    }

    public int Width { get; }
  }
}