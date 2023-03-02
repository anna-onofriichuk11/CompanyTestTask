using Microsoft.EntityFrameworkCore;
using UserMicroservice.Data;
using UserMicroservice.Entities;
using UserMicroservice.Services.Interfaces;

namespace UserMicroservice.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly UserContext _dbContext;

    public SubscriptionService(UserContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    
    public Task<Subscription> GetSubscriptionAsync(int id, CancellationToken cancellationToken)
    {
        return _dbContext.Subscriptions.Where(s => s.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task  AddSubscriptionAsync(Subscription subscription, CancellationToken cancellationToken)
    {
        _dbContext.Subscriptions.Add(subscription);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}