using Microsoft.AspNetCore.Mvc;
using RestApiLab6.Models;
using RestApiLab6.Repositories;

namespace RestApiLab6.Controllers;

[ApiController]
public class ExpertiseController : ControllerBase
{
    private ExpertiseRepository _expertiseRepository;

    public ExpertiseController(ExpertiseRepository expertiseRepository)
    {
        _expertiseRepository = expertiseRepository;
    }

    [HttpGet]
    [Route("expertises/user/{userId}")]
    public async Task<ActionResult<List<IdentifiedExpertise>>> GetExpertisesByUser(int userId)
    {
        try
        {
            var expertises = await _expertiseRepository.GetAllByUser(userId);
            return Ok(expertises);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("expertises/{id}")]
    public async Task<ActionResult<IdentifiedExpertise>> GetExpertiseById(int id)
    {
        try
        {
            var expertise = await _expertiseRepository.Get(id);
            return Ok(expertise);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("expertises")]
    public async Task<ActionResult<IdentifiedExpertise>> CreateExpertise(Expertise expertise)
    {
        try
        {
            var created = await _expertiseRepository.Add(expertise);
            return Created($"/expertises/{created.Id}", created);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("expertises/{id}")]
    public async Task<ActionResult> UpdateExpertiseRate(int id, double rate)
    {
        try
        {
            await _expertiseRepository.UpdateRate(id, rate);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("expertises/{id}")]
    public async Task<ActionResult> DeleteExpertise(int id)
    {
        try
        {
            await _expertiseRepository.Delete(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}