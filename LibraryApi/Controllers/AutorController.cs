using LibraryApi.Dto.Autor;
using LibraryApi.Models;
using LibraryApi.Services.Autor;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AutorController : ControllerBase
{
    private readonly IAutorInterface _autorInterface;
    
    public AutorController(IAutorInterface autorInterface)
    {
        _autorInterface = autorInterface;
    }


    [HttpGet("ListarAutores")]
    public async Task<ActionResult<ResponseModel<List<AutorModel>>>> ListarAutores()
    {
        var autores = await _autorInterface.ListarAutores();   
        return Ok(autores);
    }

    [HttpGet("BuscarAutorPorId/{idAutor}")]
    public async Task<ActionResult<ResponseModel<AutorModel>>> BuscarAutorPorId(int idAutor)
    {
        var autor = await _autorInterface.BuscarAutorPorId(idAutor);
        return Ok(autor);
    }
    
    [HttpGet("BuscarAutorPorIdLivro/{idLivro}")]
    public async Task<ActionResult<ResponseModel<AutorModel>>> BuscarAutorPorIdLivro(int idLivro)
    {
        var autor = await _autorInterface.BuscarAutorPorIdLivro(idLivro);
        return Ok(autor);
    }

    [HttpPost("CriarAutor")]
    public async Task<ActionResult<ResponseModel<List<AutorModel>>>> CriarAutor(AutorCriacaoDto autorCriacaoDto)
    {
        var autores = await _autorInterface.CriarAutor(autorCriacaoDto);
        return Ok(autores);
    }
    
    [HttpPut("EditarAutor")]
    public async Task<ActionResult<ResponseModel<List<AutorModel>>>> EditarAutor(AutorEdicaoDto autorEdicaoDto)
    {
        var autores = await _autorInterface.EditarAutor(autorEdicaoDto);
        return Ok(autores);
    }
    
    [HttpDelete("ExcluirAutor/{idAutor}")]
    public async Task<ActionResult<ResponseModel<List<AutorModel>>>> ExcluirAutor(int idLivro)
    {
        var autor = await _autorInterface.ExcluirAutor(idLivro);
        return Ok(autor);
    }
}