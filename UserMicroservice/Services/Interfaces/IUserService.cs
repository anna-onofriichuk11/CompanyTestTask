using UserMicroservice.Entities;

namespace UserMicroservice.Services.Interfaces;

public interface IUserService
{
    Task<User> GetUserAsync(int id, CancellationToken cancellationToken);
    Task AddUserAsync(User user, CancellationToken cancellationToken);
    Task UpdateUserAsync(User user, CancellationToken cancellationToken);
    Task DeleteUserAsync(int id, CancellationToken cancellationToken);
    Task SetSubscriptionId(int userId, int subscriptionId, CancellationToken cancellationToken);
    Task<IList<string>> GetSubscriptionTypesAsync(IList<int> userIds, CancellationToken cancellationToken);
}