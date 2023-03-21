using FilmesAPI.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class FilmeController : ControllerBase
{
    private static List<Filme> filmes = new List<Filme>();
    private static int id = 1;
    [HttpPost]
    IActionResult AdicionaFilme([FromBody]Filme filme)
    {
        filme.Id = id++;
        filmes.Add(filme);
        return CreatedAtAction(nameof(RecuperaFilmesPorId),
            new { id = filme.Id },
            filme);
    }
    [HttpGet]
    public List<Filme> RecuperaFilmes()
    {
        return filmes;
    }
    [HttpGet("{id}")]
    public IActionResult RecuperaFilmesPorId(int id)
    {
        var filme = filmes.FirstOrDefault(filme => filme.Id == id);
        if(filme == null) return NotFound();
        return Ok(filme);
    }
    [HttpGet("paginado")] // se quiser fazer pesquisa personalidade
    // no postman utilizar localhost.../filme?skip=10&take=5  (skipar 10 e mostrar 5)
    public IEnumerable<Filme> RecuperaFilmesPaginado([FromQuery] int skip = 0,
        [FromQuery] int take = 10)
    {
        return filmes.Skip(skip).Take(take);
    } 
}
