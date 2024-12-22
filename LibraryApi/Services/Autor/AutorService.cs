using LibraryApi.Data;
using LibraryApi.Dto.Autor;
using LibraryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Services.Autor;

public class AutorService : IAutorInterface
{
    private readonly AppDbContext _context;
    private IAutorInterface _autorInterfaceImplementation;

    public AutorService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<ResponseModel<List<AutorModel>>> ListarAutores()
    {
        ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();
        try
        {
            var autores = await _context.Autores.ToListAsync();
            
            resposta.Dados = autores;
            resposta.Mensagem = "Autores listados com sucesso!";
            
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<AutorModel>> BuscarAutorPorId(int idAutor)
    {
        ResponseModel<AutorModel> resposta = new ResponseModel<AutorModel>();
        try
        {
            var autor = await _context.Autores.FirstOrDefaultAsync(x => x.Id == idAutor);

            if (autor == null)
            {
                resposta.Mensagem = "Nenhum registro foi encontrado!";
                resposta.Status = false;
                return resposta;
            }

            resposta.Dados = autor;
            resposta.Mensagem = "Autor encontrado com sucesso!";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro)
    {
        ResponseModel<AutorModel> resposta = new ResponseModel<AutorModel>();
        try
        {
            var livro = await _context.Livros
                .Include(x => x.Autor)
                .FirstOrDefaultAsync(a => a.Id == idLivro);

            if (livro == null )
            {
                resposta.Mensagem = "Nenhum registro foi encontrado!";
                resposta.Status = false;
                return resposta;
            }
            
            resposta.Dados = livro.Autor;
            resposta.Mensagem = "Autor encontrado com sucesso!";
            return resposta;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ResponseModel<List<AutorModel>>> CriarAutor(AutorCriacaoDto autorCriacaoDto)
    {
        ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();

        try
        {
            var autor = new AutorModel()
            {
                Nome = autorCriacaoDto.Nome,
                Sobrenome = autorCriacaoDto.Sobrenome,
            };
            
            _context.Add(autor);
            await _context.SaveChangesAsync();
            
            resposta.Dados = await _context.Autores.ToListAsync();
            resposta.Mensagem = "Autor criado com sucesso!";

            return resposta;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<ResponseModel<List<AutorModel>>> EditarAutor(AutorEdicaoDto autorEdicaoDto)
    {
        ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();
        
        try
        {
            var autor = await _context.Autores
                .FirstOrDefaultAsync(autorBanco => autorBanco.Id == autorEdicaoDto.Id);

            if (autor == null)
            {
                resposta.Mensagem = "Nenhum registro foi encontrado!";
                return resposta;
            }
            
            autor.Nome = autorEdicaoDto.Nome;  
            autor.Sobrenome = autorEdicaoDto.Sobrenome;
            
            _context.Update(autor);
            await _context.SaveChangesAsync();
            
            resposta.Dados = await _context.Autores.ToListAsync();

            return resposta;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ResponseModel<List<AutorModel>>> ExcluirAutor(int idAutor)
    {
        ResponseModel<List<AutorModel>> resposta =  new ResponseModel<List<AutorModel>>();

        try
        {
            var autor = await _context.Autores.FirstOrDefaultAsync(x => x.Id == idAutor);

            if (autor == null)
            {
                resposta.Mensagem = "Nenhum registro foi encontrado!";
                return resposta;
            }
            
            _context.Remove(autor);
            await _context.SaveChangesAsync();
            
            resposta.Dados = await _context.Autores.ToListAsync();
            
            return resposta;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}