namespace SBS.Common
{
  public class Person
  {
    [DisplayWidth(4)]
    public int Id { get; set; }
    [DisplayWidth(20)]
    public string FirstName { get; set; }
    [DisplayWidth(20)]
    public string LastName { get; set; }
    [DisplayWidth(15)] 
    public string City { get; set; }
    [DisplayWidth(5)] 
    public string State { get; set; }
    [DisplayWidth(7)] 
    public string Country { get; set; }
  }
}
