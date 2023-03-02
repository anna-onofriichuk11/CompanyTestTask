using Microsoft.AspNetCore.Mvc;
using ProjectMicroservice.Data;
using ProjectMicroservice.Entities;
using ProjectMicroservice.Services.Interfaces;

namespace ProjectMicroservice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Project project, CancellationToken cancellationToken)
    {
        await _projectService.AddProjectAsync(project, cancellationToken);
        return Ok(new {project.Id});
    }

    [HttpGet("byUserId/{userId:int}")]
    public async Task<IActionResult> GetProjectByUserId(int userId, CancellationToken cancellationToken)
    {
        var result = await _projectService.GetProjectsByUserIdAsync(userId, cancellationToken);

        if (result == null || result.Count == 0)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
