using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ProjectMicroservice.Entities;
using UserMicroservice.Entities;
using Xunit;

// here is a simple example of integration tests
//todo: end writing this tests
// public class IntegrationTests
// {
//     private readonly TestServer _server;
//     private readonly HttpClient _client;
//
//     public IntegrationTests()
//     {
//         // Arrange
//         _server = new TestServer(new WebHostBuilder()
//             .UseEnvironment("Development")
//             .UseStartup<Startup>());
//
//         _client = _server.CreateClient();
//     }
//
//     [Fact]
//     public async Task CreateProjectAndUser()
//     {
//         // Arrange
//         var project = new Project{Id= "1",  Name = "Test Project", UserId = "1", Charts = null};
//         var user = new User{Id=1, Name = "Test User", Email = "anna@gmail.com"};
//
//         // Act
//         var projectResponse = await _client.PostAsync("/api/project",
//             new StringContent(JsonConvert.SerializeObject(project), Encoding.UTF8, "application/json"));
//
//         var userResponse = await _client.PostAsync("/api/user",
//             new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));
//
//         // Assert
//         projectResponse.EnsureSuccessStatusCode();
//         userResponse.EnsureSuccessStatusCode();
//     }
//
//     [Fact]
//     public async Task GetProjectsByUserId()
//     {
//         // Arrange
//         var userId = 1;
//
//         // Act
//         var response = await _client.GetAsync($"/api/project/byUserId/{userId}");
//
//         // Assert
//         Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
//     }
//
//     [Fact]
//     public async Task GetUser()
//     {
//         // Arrange
//         var userId = 1;
//
//         // Act
//         var response = await _client.GetAsync($"/api/user/{userId}");
//
//         // Assert
//         Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
//     }
// }