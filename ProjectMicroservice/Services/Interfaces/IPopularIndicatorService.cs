using ProjectMicroservice.Entities;

namespace ProjectMicroservice.Services.Interfaces;

public interface IPopularIndicatorService
{
    Task<IList<Indicator>> GetIndicatorAsync(string subscriptionType, int take, CancellationToken cancellationToken);
}