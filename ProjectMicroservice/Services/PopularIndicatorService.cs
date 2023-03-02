using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using ProjectMicroservice.Entities;
using ProjectMicroservice.Services.Interfaces;

namespace ProjectMicroservice.Services;

public class PopularIndicatorService : IPopularIndicatorService
{
    private readonly IMongoCollection<Project> _projectsCollection;
    private readonly IUserApi _userApi;

    public PopularIndicatorService(IMongoCollection<Project> projectsCollection, IUserApi userApi)
    {
        _projectsCollection = projectsCollection ?? throw new ArgumentNullException(nameof(projectsCollection));
        _userApi = userApi ?? throw new ArgumentNullException(nameof(userApi));
    }

    public async Task<IList<Indicator>> GetIndicatorAsync(string subscriptionType, int top, CancellationToken cancellationToken)
    {
        using var cursor = await _projectsCollection.AsQueryable(new AggregateOptions {BatchSize = 1000})
            .SelectMany(project => project.Charts.Select(chart => new {project.UserId, chart}))
            .SelectMany(x => x.chart.Indicators.Select(indicator => new {x.UserId, IndicatorName = indicator.Name}))
            .ToCursorAsync(cancellationToken);

        var buckets = new Dictionary<string, int>();
        while (await cursor.MoveNextAsync(cancellationToken))
        {
            var batch = cursor.Current.OrderBy(b => b.UserId).ToArray();
            var groupedBatch = batch.GroupBy(b => Convert.ToInt32(b.UserId), b => b.IndicatorName).ToArray();

            var userIds = groupedBatch.Select(x => x.Key).ToArray();
            var subscriptionTypes = await _userApi.GetSubscriptionTypes(userIds, cancellationToken);

            var filteredBatch = groupedBatch.Zip(subscriptionTypes).Where(zip => zip.Second == subscriptionType).Select(zip => zip.First);
            foreach (var grouping in filteredBatch)
            {
                foreach (var indicatorName in grouping)
                {
                    buckets[indicatorName] = buckets.GetValueOrDefault(indicatorName) + 1;
                }
            }
        }

        return buckets.OrderByDescending(pair => pair.Value).Take(top)
            .Select(pair => new Indicator
            {
                Name = pair.Key,
                Used = pair.Value,
            }).ToArray();
    }
}
