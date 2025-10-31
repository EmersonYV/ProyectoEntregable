using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Controllers;

[ApiController]
[Route("api/[controller]")] // /api/PersonaJuridicas
public class PersonaJuridicasController : ControllerBase
{
    private readonly IPersonaService _service;
    public PersonaJuridicasController(IPersonaService service) => _service = service;

    // GET /api/PersonaJuridicas?filtro=...
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonaJuridica>>> Get([FromQuery] string? filtro)
    {
        var data = await _service.ListPersonaJuridicasAsync(filtro ?? string.Empty);
        return Ok(data);
    }

    // GET /api/PersonaJuridicas/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PersonaJuridica>> GetById(int id)
    {
        var item = await _service.GetPersonaJuridicaByIdAsync(id);
        if (item is null) return NotFound();
        return Ok(item);
    }

    // POST /api/PersonaJuridicas
    [HttpPost]
    public async Task<ActionResult<int>> Post([FromBody] PersonaJuridica model)
    {
        var id = await _service.CreatePersonaJuridicaAsync(model);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    // PUT /api/PersonaJuridicas/5
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, [FromBody] PersonaJuridica model)
    {
        var rows = await _service.UpdatePersonaJuridicaAsync(id, model);
        if (rows == 0) return NotFound();
        return NoContent();
    }

    // DELETE /api/PersonaJuridicas/5
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var rows = await _service.DeletePersonaJuridicaAsync(id);
        if (rows == 0) return NotFound();
        return NoContent();
    }
}