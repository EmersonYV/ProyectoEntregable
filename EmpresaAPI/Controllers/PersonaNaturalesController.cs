using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Controllers;

[ApiController]
[Route("api/[controller]")] // /api/PersonaNaturales
public class PersonaNaturalesController : ControllerBase
{
    private readonly IPersonaService _service;
    public PersonaNaturalesController(IPersonaService service) => _service = service;

    // GET /api/PersonaNaturales?filtro=...
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonaNatural>>> Get([FromQuery] string? filtro)
    {
        var data = await _service.ListPersonaNaturalesAsync(filtro ?? string.Empty);
        return Ok(data);
    }

    // GET /api/PersonaNaturales/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PersonaNatural>> GetById(int id)
    {
        var item = await _service.GetPersonaNaturalByIdAsync(id);
        if (item is null) return NotFound();
        return Ok(item);
    }

    // POST /api/PersonaNaturales
    [HttpPost]
    public async Task<ActionResult<int>> Post([FromBody] PersonaNatural model)
    {
        var id = await _service.CreatePersonaNaturalAsync(model);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    // PUT /api/PersonaNaturales/5
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, [FromBody] PersonaNatural model)
    {
        var rows = await _service.UpdatePersonaNaturalAsync(id, model);
        if (rows == 0) return NotFound();
        return NoContent();
    }

    // DELETE /api/PersonaNaturales/5
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var rows = await _service.DeletePersonaNaturalAsync(id);
        if (rows == 0) return NotFound();
        return NoContent();
    }
}