using System.ComponentModel.DataAnnotations;

namespace UserMicroservice.Models.Requests;

public class CreateUserRequest
{
    [Required]
    public string UserName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
}