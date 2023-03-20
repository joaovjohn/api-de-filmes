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
    [HttpPost]
    public void AdicionaFilme([FromBody]Filme filme)
    {
        filmes.Add(filme);
    }
    [HttpGet]
    public List<Filme> RecuperaFilmes()
    {
        return filmes;
    }
}
