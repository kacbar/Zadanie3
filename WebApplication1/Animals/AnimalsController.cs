using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Animals;

[ApiController]
[Route("/api/animals")]
public class AnimalsController : ControllerBase
{
    private readonly IAnimalService _animalService;
    
    public AnimalsController(IAnimalService animalService)
    {
        _animalService = animalService;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAllAnimals([FromQuery] string orderBy = "Name")
    {
        if (!IsValidOrderBy(orderBy))
        {
            return BadRequest("Invalid orderBy parameter.");
        }
        
        var animals = _animalService.GetAllAnimals(orderBy);
        return Ok(animals);
    }
    
    private bool IsValidOrderBy(string orderBy)
    {
        return orderBy == "Name" || orderBy == "Description" || orderBy == "Category" || orderBy == "Area";
    }

    [HttpPost]
    public IActionResult CreateAnimal([FromBody] CreateAnimal animal)
    {
        var success = _animalService.CreateAnimal(animal);
        return success ? StatusCode(StatusCodes.Status201Created) : Conflict();
    }

    [HttpPut("{idAnimal}")]
    public IActionResult UpdateAnimal(int idAnimal, [FromBody] UpdateAnimal updateRequest)
    {
        var existingAnimal = _animalService.GetAnimalById(idAnimal);
        if (existingAnimal == null)
        {
            return NotFound();
        }

        existingAnimal.Name = updateRequest.Name ?? existingAnimal.Name;
        existingAnimal.Description = updateRequest.Description ?? existingAnimal.Description;
        existingAnimal.Category = updateRequest.Category ?? existingAnimal.Category;
        existingAnimal.Area = updateRequest.Area ?? existingAnimal.Area;

        var success = _animalService.UpdateAnimal(existingAnimal);
        return success ? NoContent() : StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpDelete("{idAnimal}")]
    public IActionResult DeleteAnimal(int idAnimal)
    {
        var existingAnimal = _animalService.GetAnimalById(idAnimal);
        if (existingAnimal == null)
        {
            return NotFound();
        }

        var success = _animalService.DeleteAnimal(idAnimal);
        return success ? NoContent() : StatusCode(StatusCodes.Status500InternalServerError);
    }
}
