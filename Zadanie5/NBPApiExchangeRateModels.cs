public class Rootobject
{
    public string table { get; set; }
    public string currency { get; set; }
    public string code { get; set; }
    public Rate[] rates { get; set; }
}

public class Rate
{
    public string no { get; set; }
    public string effectiveDate { get; set; }
    public Decimal mid { get; set; }
}