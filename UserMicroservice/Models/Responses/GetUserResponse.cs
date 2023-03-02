namespace UserMicroservice.Models.Responses;

public class GetUserResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int SubscriptionId { get; set; }
}