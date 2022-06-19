using AutoMapper;
using easyCloud.Security.Domain.Services.Communication;
using easyCloud.Shared.Extensions;
using easyCloud.User.Domain.Services;
using easyCloud.User.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace easyCloud.User.Controllers;
[Produces("application/json")]
[ApiController]
[Route("/api/v1/[controller]")]
    
public class UsersController: ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost("/auth/sign-in")]
    public async Task<IActionResult> Authenticate(AuthenticateRequest request)
    {
        var response = await _userService.Authenticate(request);
        return Ok(response);
    }
    
    [AllowAnonymous]
    [HttpPost("/auth/sign-up")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        await _userService.RegisterAsync(request);
        return Ok(new {message = "Registration successful."});
    }
    
    [HttpGet]
    public async Task<IEnumerable<UserResource>> GetAllAsync()
    {
        var users = await _userService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Domain.Models.User>, IEnumerable<UserResource>>(users);

        return resources;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        var resource = _mapper.Map<Domain.Models.User, UserResource>(user);
        return Ok(resource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateRequest request)
    {
        await _userService.UpdateAsync(id, request);
        return Ok(new {message = "User Updated Successfully."});
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _userService.DeleteAsync(id);
        return Ok(new {message = "User Deleted successfully."});
    }
}