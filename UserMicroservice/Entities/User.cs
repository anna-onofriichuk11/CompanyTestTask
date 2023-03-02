namespace UserMicroservice.Entities;

public class User : TEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int? SubscriptionId { get; set; }
    public virtual Subscription? Subscription { get; set; }
}