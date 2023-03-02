using Microsoft.AspNetCore.Mvc;
using ProjectMicroservice.Services.Interfaces;

namespace ProjectMicroservice.Controllers;

[ApiController]
[Route("[controller]")]
public class IndicatorController : ControllerBase
{
    private readonly IPopularIndicatorService _popularIndicatorService;

    public IndicatorController(IPopularIndicatorService popularIndicatorService)
    {
        _popularIndicatorService = popularIndicatorService ?? throw new ArgumentNullException(nameof(popularIndicatorService));
    }

    [HttpGet("{subscriptionType}")]
    public async Task<IActionResult> Get([FromRoute] string subscriptionType, CancellationToken cancellationToken)
    {
        return Ok(await _popularIndicatorService.GetIndicatorAsync(subscriptionType, take: 3, cancellationToken));
    }
}