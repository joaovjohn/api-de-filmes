
using AutoMapper;
using FilmesAPI.Context.DTOs;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles;

public class FilmeProfile : Profile
{
    public FilmeProfile()
    {
        CreateMap<CriarFilmeDTO, Filme>();
    }
}