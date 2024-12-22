using System.Text.Json.Serialization;
using LibraryApi.Models;

namespace LibraryApi.Dto.Vinculo;

public class AutorVinculoDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
}