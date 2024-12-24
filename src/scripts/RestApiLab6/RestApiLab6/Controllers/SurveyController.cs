using Microsoft.AspNetCore.Mvc;
using RestApiLab6.Models;
using RestApiLab6.Repositories;

namespace RestApiLab6.Controllers;
[ApiController]
public class SurveyController : ControllerBase
{
    private SurveyRepository _surveyRepository;

    public SurveyController(SurveyRepository surveyRepository)
    {
        _surveyRepository = surveyRepository;
    }

    [HttpGet]
    [Route("surveys/owner/{ownerId}")]
    public async Task<ActionResult<List<IdentifiedSurvey>>> GetSurveysByOwner(int ownerId)
    {
        try
        {
            var surveys = await _surveyRepository.GetAllByOwner(ownerId);
            return Ok(surveys);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("surveys/{titlePart:alpha}")]
    public async Task<ActionResult<List<IdentifiedSurvey>>> GetSurveysByTitlePart(string titlePart)
    {
        try
        {
            var surveys = await _surveyRepository.GetAllWithTitlePart(titlePart);
            return Ok(surveys);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("surveys/{id:int}")]
    public async Task<ActionResult<IdentifiedSurvey>> GetSurveyById(int id)
    {
        try
        {
            var survey = await _surveyRepository.GetById(id);
            return Ok(survey);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("surveys")]
    public async Task<ActionResult<IdentifiedSurvey>> CreateSurvey(Survey survey)
    {
        try
        {
            var created = await _surveyRepository.Add(survey);
            return Created($"/surveys/{created.Id}", created);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("surveys/update/title")]
    public async Task<ActionResult> UpdateSurveyTitle(int id, string title)
    {
        try
        {
            await _surveyRepository.UpdateTitle(id, title);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("surveys/update/description")]
    public async Task<ActionResult> UpdateSurveyDescription(int id, string description)
    {
        try
        {
            await _surveyRepository.UpdateDescription(id, description);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("surveys/update/activity")]
    public async Task<ActionResult> UpdateSurveyActivity(int id, bool isActive)
    {
        try
        {
            await _surveyRepository.UpdateActivity(id, isActive);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("surveys/delete/{id:int}")]
    public async Task<ActionResult> DeleteSurvey(int id)
    {
        try
        {
            await _surveyRepository.Delete(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
