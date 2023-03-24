using FilmesAPI.Data;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class FilmeController : ControllerBase
{
    private FilmeDBContext _context;

    public FilmeController(FilmeDBContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult AdicionaFilme([FromBody]Filme filme)
    {
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaFilmesPorId),
            new { id = filme.Id },
            filme);
    }
    //[HttpGet]
    //public List<Filme> RecuperaFilmes()
    //{
    //    return _context.Filmes;
    //}
    [HttpGet("{id}")]
    public IActionResult RecuperaFilmesPorId(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if(filme == null) return NotFound();
        return Ok(filme);
    }
    [HttpGet("paginado")] // se quiser fazer pesquisa personalizada
    // no postman utilizar localhost.../filme?skip=10&take=5  (skipar 10 e mostrar 5)
    public IEnumerable<Filme> RecuperaFilmesPaginado([FromQuery] int skip = 0,
        [FromQuery] int take = 10)
    {
        return _context.Filmes.Skip(skip).Take(take);
    } 
}
