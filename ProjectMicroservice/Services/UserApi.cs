using System.Text.Json;
using ProjectMicroservice.Services.Interfaces;

namespace ProjectMicroservice.Services;

public class UserApi : IUserApi
{
    private readonly HttpClient _httpClient;

    public UserApi(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<IEnumerable<string>> GetSubscriptionTypes(IList<int> userIds, CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, "/user/subscriptionTypes")
        {
            Content = JsonContent.Create(new {userIds}),
        };

        using var response = await _httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();

        var responseObj = JsonSerializer.Deserialize<GetSubscriptionTypesResponse>(
            await response.Content.ReadAsStreamAsync(cancellationToken));

        return responseObj.SubscriptionTypes;
    }

    class GetSubscriptionTypesResponse
    {
        public IList<string> SubscriptionTypes { get; set; }
    }
}
