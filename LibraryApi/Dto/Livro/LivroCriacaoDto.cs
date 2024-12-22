using LibraryApi.Dto.Vinculo;
using LibraryApi.Models;

namespace LibraryApi.Dto.Livro;

public class LivroCriacaoDto
{
    public string Titulo { get; set; }
    public AutorVinculoDto Autor { get; set; }
}