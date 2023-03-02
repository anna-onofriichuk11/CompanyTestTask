namespace UserMicroservice.Models.Requests;

public class GetSubscriptionTypesRequest
{
    public IList<int> UserIds { get; set; }
}