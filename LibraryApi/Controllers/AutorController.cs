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
    
    [HttpGet("BusacaAutorPorLivroId/{idLivro}")]
    public async Task<ActionResult<ResponseModel<AutorModel>>> BusacaAutorPorLivroId(int idLivro)
    {
        var autor = await _autorInterface.BuscarAutorPorId(idLivro);
        return Ok(autor);
    }
}