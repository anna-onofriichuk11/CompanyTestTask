using MongoDB.Driver;
using MongoDB.Driver.Linq;
using ProjectMicroservice.Entities;
using ProjectMicroservice.Services.Interfaces;

namespace ProjectMicroservice.Services;

public class ProjectService : IProjectService
{
    private readonly IMongoCollection<Project> _projectsCollection;

    public ProjectService(IMongoCollection<Project> projectsCollection)
    {
        _projectsCollection = projectsCollection ?? throw new ArgumentNullException(nameof(projectsCollection));
    }

    public async Task AddProjectAsync(Project project, CancellationToken cancellationToken)
    {
        await _projectsCollection.InsertOneAsync(project, cancellationToken: cancellationToken);
    }

    public async Task<IList<Project>> GetProjectsByUserIdAsync(int userId, CancellationToken cancellationToken)
    {
        return await _projectsCollection.AsQueryable().Where(p => p.UserId.Equals(userId.ToString())).ToListAsync(cancellationToken);
    }
}