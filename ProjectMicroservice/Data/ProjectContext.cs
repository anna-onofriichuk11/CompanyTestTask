using MongoDB.Driver;
using ProjectMicroservice.Entities;

namespace ProjectMicroservice.Data;

public class ProjectContext
{
    private readonly IMongoDatabase _database;
    public ProjectContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetConnectionString("MongoDB"));
        _database = client.GetDatabase("MyDatabase");
    }

    public IMongoCollection<Project> Projects =>
        _database.GetCollection<Project>("Projects");
}