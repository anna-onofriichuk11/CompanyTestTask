using Microsoft.AspNetCore.Mvc;
using ProjectMicroservice.Data;
using ProjectMicroservice.Entities;

namespace ProjectMicroservice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly ProjectContext _context;

    public ProjectController(ProjectContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
    {
        return await _context.Projects.ToListAsync();
    }

    [HttpGet("{id:length(24)}", Name = "GetProject")]
    public async Task<ActionResult<Project>> GetProject(string id)
    {
        var project = await _context.Projects.Find(p => p.Id == id).FirstOrDefaultAsync();

        if (project == null)
        {
            return NotFound();
        }

        return project;
    }

    [HttpPost]
    public async Task<ActionResult<Project>> PostProject(Project project)
    {
        await _context.Projects.InsertOneAsync(project);

        return CreatedAtRoute("GetProject", new {id = project.Id.ToString()}, project);
    }

}
