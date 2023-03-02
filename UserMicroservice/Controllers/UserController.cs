using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserMicroservice.Entities;
using UserMicroservice.Models.Requests;
using UserMicroservice.Models.Responses;
using UserMicroservice.Services.Interfaces;

namespace UserMicroservice.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public UserController(IMapper mapper, IUserService userService)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);
        
        await _userService.AddUserAsync(user, cancellationToken);
        
        var response = _mapper.Map<CreateUserResponse>(user);

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserAsync(id, cancellationToken);

        if (user is null)
        {
            return NotFound();
        }

        var response = _mapper.Map<GetUserResponse>(user);
        
        return Ok(response);
    }

    [HttpPatch("{userId:int}/subscriptionId/{subscriptionId:int}")]
    public async Task<IActionResult> SetSubscriptionId(
        [FromRoute] int userId, [FromRoute] int subscriptionId, CancellationToken cancellationToken)
    {
        await _userService.SetSubscriptionId(userId, subscriptionId, cancellationToken);
        
        return Ok();
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutUser(int id, [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserAsync(id, cancellationToken);

        if (user is null)
        {
            return NotFound();
        }

        _mapper.Map(request, user);

        await _userService.UpdateUserAsync(user, cancellationToken);

        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUser(int id, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserAsync(id, cancellationToken);

        if (user is null)
        {
            return NotFound();
        }

        await _userService.DeleteUserAsync(user.Id, cancellationToken);

        return Ok();
    }

}