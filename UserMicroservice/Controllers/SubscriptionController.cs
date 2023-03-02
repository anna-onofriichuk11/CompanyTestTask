using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserMicroservice.Entities;
using UserMicroservice.Models.Requests;
using UserMicroservice.Models.Responses;
using UserMicroservice.Services.Interfaces;

namespace UserMicroservice.Controllers;

[ApiController]
[Route("[controller]")]
public class SubscriptionController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ISubscriptionService _subscriptionService;

    public SubscriptionController(IMapper mapper, ISubscriptionService subscriptionService)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSubscriptionRequest request, CancellationToken cancellationToken)
    {
        var subscription = _mapper.Map<Subscription>(request);
        
        await _subscriptionService.AddSubscriptionAsync(subscription, cancellationToken);
        
        var response = _mapper.Map<CreateSubscriptionResponse>(subscription);

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionService.GetSubscriptionAsync(id, cancellationToken);

        if (subscription is null)
        {
            return NotFound();
        }

        var response = _mapper.Map<GetSubscriptionResponse>(subscription);
        
        return Ok(response);
    }
}