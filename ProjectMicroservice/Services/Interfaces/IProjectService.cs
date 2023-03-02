using ProjectMicroservice.Entities;

namespace ProjectMicroservice.Services.Interfaces;

public interface IProjectService
{
    Task AddProjectAsync(Project project, CancellationToken cancellationToken);
    Task<IList<Project>> GetProjectsByUserIdAsync(int userId, CancellationToken cancellationToken);
}