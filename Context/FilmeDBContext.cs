using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Data;

public class FilmeDBContext : DbContext
{
    public FilmeDBContext(DbContextOptions<FilmeDBContext> opts)
        : base(opts) 
    { 
    }
    public DbSet<Filme> Filmes { get; set; }
}
