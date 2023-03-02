using UserMicroservice.Entities;

namespace UserMicroservice.Services.Interfaces;

public interface ISubscriptionService
{
    Task<Subscription> GetSubscriptionAsync(int id, CancellationToken cancellationToken);
    Task AddSubscriptionAsync(Subscription subscription, CancellationToken cancellationToken);
}