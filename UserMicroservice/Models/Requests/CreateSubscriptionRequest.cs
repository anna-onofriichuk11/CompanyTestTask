using System.ComponentModel.DataAnnotations;
using UserMicroservice.ExtraAttributes;

namespace UserMicroservice.Models.Requests;

public class CreateSubscriptionRequest
{
    [Required]
    [OneOf("Free", "Trial", "Super")]
    public string Type { get; set; }
    
    [Required]
    public DateTime? StartDate { get; set; }
    
    [Required]
    public DateTime? EndDate { get; set; }
}