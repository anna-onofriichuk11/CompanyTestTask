namespace UserMicroservice.Models.Responses;

public class GetSubscriptionResponse
{
    public int Id { get; set; }
    public string Type { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}