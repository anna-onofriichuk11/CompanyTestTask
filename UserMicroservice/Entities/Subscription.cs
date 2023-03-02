namespace UserMicroservice.Entities;

public class Subscription : TEntity
{
    public string Type { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}