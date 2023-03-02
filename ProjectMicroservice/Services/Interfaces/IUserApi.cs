namespace ProjectMicroservice.Services.Interfaces;

public interface IUserApi
{
     Task<IEnumerable<string>> GetSubscriptionTypes(IList<int> userIds, CancellationToken cancellationToken);
}