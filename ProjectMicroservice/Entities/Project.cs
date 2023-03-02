namespace ProjectMicroservice.Entities;

public class Project
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string Name { get; set; }
    public List<Chart> Charts { get; set; }
}