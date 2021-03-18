using System.IO;
using Newtonsoft.Json;

namespace SBS.Common
{
  public static class JsonCovertExtensions
  {
    public static string ToJson(this object obj, Formatting formatting = Formatting.None)
    {
      return JsonConvert.SerializeObject(obj, formatting);
    }

    public static void ToJsonFile(this object obj, string filename, Formatting formatting = Formatting.None)
    {
      File.WriteAllText(filename, obj.ToJson(Formatting.Indented));
    }

    public static T FromJson<T>(this string json)
    {
      return JsonConvert.DeserializeObject<T>(json);
    }

    public static T FromJsonFile<T>(this string filename)
    {
      return File.ReadAllText(filename).FromJson<T>();
    }
  }
}