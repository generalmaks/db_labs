using Microsoft.AspNetCore.Mvc;
using RestApiLab6.Models;
using RestApiLab6.Repositories;

namespace RestApiLab6.Controllers;

[ApiController]
public class CategoryController : ControllerBase
{
    private CategoryRepository _categoryRepo;

    public CategoryController(CategoryRepository categoryRepo)
    {
        _categoryRepo = categoryRepo;
    }

    [HttpGet]
    [Route("categories")]
    public async Task<ActionResult<List<IdentifiedCategory>>> GetCategories()
    {
        try
        {
            var categories = await _categoryRepo.GetAll();
            return Ok(categories);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("categories/{id}")]
    public async Task<ActionResult<IdentifiedCategory>> GetCategory(int id)
    {
        try
        {
            var category = await _categoryRepo.Get(id);
            return Ok(category);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("categories")]
    public async Task<ActionResult<List<IdentifiedCategory>>> CreateCategory([FromBody] Category category)
    {
        try
        {
            var created = await _categoryRepo.Add(category);
            return Created($"/categories/{created.Id}", created);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("categories/{id}")]
    public async Task<ActionResult> UpdateCategoryName(int id, string name)
    {
        try
        {
            await _categoryRepo.UpdateName(id, name);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("categories/{id}")]
    public async Task<ActionResult> DeleteCategory(int id)
    {
        try
        {
            await _categoryRepo.Delete(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}