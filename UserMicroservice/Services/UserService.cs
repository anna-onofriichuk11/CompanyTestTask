using Microsoft.EntityFrameworkCore;
using UserMicroservice.Data;
using UserMicroservice.Entities;
using UserMicroservice.Services.Interfaces;

namespace UserMicroservice.Services;

public class UserService : IUserService
{
    private readonly UserContext _dbContext;

    public UserService(UserContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    
    public async Task<User> GetUserAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.Where(user => user.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task AddUserAsync(User user, CancellationToken cancellationToken)
    {
        _dbContext.Users.Add(user);
        
        await _dbContext.SaveChangesAsync(cancellationToken);   
    }
    
    public async Task UpdateUserAsync(User user, CancellationToken cancellationToken)
    {
        var existingUser = await _dbContext.Users.FindAsync(user.Id);
        if (existingUser == null)
        {
            throw new ArgumentException($"User with id {user.Id} not found.", nameof(user.Id));
        }

        existingUser.Name = user.Name;
        existingUser.Email = user.Email;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task DeleteUserAsync(int id, CancellationToken cancellationToken)
    {
        var existingUser = await _dbContext.Users.FindAsync(id);
        if (existingUser == null)
        {
            throw new ArgumentException($"User with id {id} not found.", nameof(id));
        }

        _dbContext.Users.Remove(existingUser);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task SetSubscriptionId(int userId, int subscriptionId, CancellationToken cancellationToken)
    {
        var user = new User {Id = userId};
        _dbContext.Users.Attach(user);
        user.SubscriptionId = subscriptionId;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IList<string>> GetSubscriptionTypesAsync(IList<int> userIds, CancellationToken cancellationToken)
    {
        var query = _dbContext.Users
            .Where(u => userIds.Contains(u.Id))
            .OrderBy(u => u.Id)
            .Select(u => u.Subscription.Type);

        var subscriptionTypes = await query.ToListAsync(cancellationToken);

        return subscriptionTypes;
    }

}