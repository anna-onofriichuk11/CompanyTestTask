using System.ComponentModel.DataAnnotations;

namespace UserMicroservice.Models.Requests;

public class UpdateUserRequest
{
    public string UserName { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
}