using AutoMapper;
using FilmesAPI.Context.DTOs;
using FilmesAPI.Data;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Collections.Generic;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class FilmeController : ControllerBase
{
    private FilmeDBContext _context;
    private IMapper _mapper;

    public FilmeController(FilmeDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionaFilme([FromBody] CriarFilmeDTO filmeDTO)
    {
        Filme filme = _mapper.Map<Filme>(filmeDTO);
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaFilmesPorId),
            new { id = filme.Id },
            filme);
    }
    [HttpGet("{id}")] // pesquisa o filme com filme/{id}
    public IActionResult RecuperaFilmesPorId(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();
        var filmeDTO = _mapper.Map<ReadFilmeDTO>(filme);
        return Ok(filmeDTO);
    }
    [HttpGet] // se quiser fazer pesquisa personalizada
    // no postman utilizar localhost.../filme?skip=10&take=5  (skipar 10 e mostrar 5)
    public IEnumerable<ReadFilmeDTO> RecuperaFilmesPaginado(
        [FromQuery] int skip = 0,
        [FromQuery] int take = 10)
    {
        return _mapper.Map<List<ReadFilmeDTO>>(_context.Filmes.Skip(skip).Take(take));
    }

    [HttpPut("{id}")] // faz a alteração através do id
    public IActionResult AtualizaFilme(int id,
        [FromBody] AtualizarFilmeDTO filmeDTO)
    {
        var filme = _context.Filmes.FirstOrDefault(
            filme => filme.Id == id);
        if (filme == null) return NotFound();
        _mapper.Map(filmeDTO, filme);
        _context.SaveChanges();
        return NoContent();
    }
    [HttpPatch("{id}")] // verificar no swagger (não esquecer [ { } ] - 
    // exp - "op": "replace", "path": "/titulo", "value": "Avatar"
    public IActionResult AtualizaFilmeParcial(int id,
        JsonPatchDocument<AtualizarFilmeDTO> patch)
    {
        var filme = _context.Filmes.FirstOrDefault(
            filme => filme.Id == id);
        if (filme == null) return NotFound();

        var filmeParaAtualizar = _mapper.Map<AtualizarFilmeDTO>(filme);

        patch.ApplyTo(filmeParaAtualizar, ModelState);
        if (!TryValidateModel(filmeParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(filmeParaAtualizar, filme);
        _context.SaveChanges();
        return NoContent();
    }
    [HttpDelete("{id}")]
    public IActionResult DeletarFilme(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(
            filme => filme.Id == id);
        if (filme == null) return NotFound();
        _context.Remove(filme);
        _context.SaveChanges();
        return NoContent();
    }
}
