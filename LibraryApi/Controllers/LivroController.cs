using LibraryApi.Dto.Livro;
using LibraryApi.Models;
using LibraryApi.Services.Livro;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers;


[Route("api/[controller]")]
[ApiController]
public class LivroController : ControllerBase
{
    private readonly ILivroInterface _livroInterface;
    
    public LivroController(ILivroInterface livroInterface)
    {
        _livroInterface = livroInterface;
    }

    [HttpGet("ListarLivros")]
    public async Task<ActionResult<List<ResponseModel<LivroModel>>>> GetLivros()
    {
        var livros = await _livroInterface.ListarLivros();
        return Ok(livros);
    }
    
    [HttpGet("BuscarLivroPorId/{idLivro}")]
    public async Task<ActionResult<ResponseModel<AutorModel>>> BuscarLivroPorId(int idLivro)
    {
        var livro = await _livroInterface.BuscarLivroPorId(idLivro);
        return Ok(livro);
    }
    
    [HttpGet("BuscarLivroPorIdAutor/{idAutor}")]
    public async Task<ActionResult<ResponseModel<AutorModel>>> BuscarLivroPorIdAutor(int idAutor)
    {
        var livro = await _livroInterface.BuscarLivroPorIdAutor(idAutor);
        return Ok(livro);
    }
    
    [HttpPost("CriarLivro")]
    public async Task<ActionResult<ResponseModel<List<AutorModel>>>> CriarLivro(LivroCriacaoDto livroCriacaoDto)
    {
        var livro = await _livroInterface.CriarLivro(livroCriacaoDto);
        return Ok(livro);
    }
    
    [HttpPut("EditarLivro")]
    public async Task<ActionResult<ResponseModel<List<AutorModel>>>> EditarLivro(LivroEdicaoDto livroEdicaoDto)
    {
        var livro = await _livroInterface.EditarLivro(livroEdicaoDto);
        return Ok(livro);
    }
    
    [HttpDelete("ExcluirLivro/{idLivro}")]
    public async Task<ActionResult<ResponseModel<List<AutorModel>>>> ExcluirLivro(int idLivro)
    {
        var livro = await _livroInterface.ExcluirLivro(idLivro);
        return Ok(livro);
    }
}