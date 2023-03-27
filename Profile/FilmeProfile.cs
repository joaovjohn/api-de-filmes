
using AutoMapper;
using FilmesAPI.Context.DTOs;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles;

public class FilmeProfile : Profile
{
    public FilmeProfile()
    {
        CreateMap<CriarFilmeDTO, Filme>();
        CreateMap<AtualizarFilmeDTO, Filme>();
        CreateMap<Filme, AtualizarFilmeDTO>();
        CreateMap<Filme, ReadFilmeDTO>();
    }
}