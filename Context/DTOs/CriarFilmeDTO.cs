using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Context.DTOs;

public class CriarFilmeDTO
{

    [Required(ErrorMessage = "O título do filme é obrigatório")]
    public string Titulo { get; set; }

    [Required(ErrorMessage = "O Gênero do filme é obrigatório")]
    [StringLength(50, ErrorMessage = "O tamanho do gênero não pode exceder 50 caracteres")]
    public string Genero { get; set; }

    [Required(ErrorMessage = "A duração do filme é obrigatória")]
    [Range(50, 360, ErrorMessage = "A duração deve ter entre 50 e 360 minutos")]
    public int Duracao { get; set; }

}
