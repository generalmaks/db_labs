using Microsoft.AspNetCore.Mvc;
using RestApiLab6.Models;
using RestApiLab6.Repositories;

namespace RestApiLab6.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private UserRepository _userRepository;

    public UserController(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    [Route("users")]
    public async Task<ActionResult<List<IdentifiedUser>>> GetUsers()
    {
        try
        {
            var users = await _userRepository.GetAll();
            return Ok(users);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("users/{id}")]
    public async Task<ActionResult<IdentifiedUser>> GetUser(int id)
    {
        try
        {
            var user = await _userRepository.Get(id);
            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("users")]
    public async Task<ActionResult<IdentifiedUser>> CreateUser(User user)
    {
        try
        {
            var created = await _userRepository.Create(user);
            return Created($"/users/{created.Id}", created);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("users/{id}")]
    public async Task<ActionResult> UpdatePhoneNumber(int id, string phoneNumber)
    {
        try
        {
            await _userRepository.UpdatePhoneNumber(id, phoneNumber);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("users/update/{id}")]
    public async Task<ActionResult> UpdatePassword(int id, string password)
    {
        try
        {
            await _userRepository.UpdatePassword(id, password);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("users/{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        try
        {
            await _userRepository.Delete(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}