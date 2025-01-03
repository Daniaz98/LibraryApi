using LibraryApi.Data;
using LibraryApi.Dto.Livro;
using LibraryApi.Models;
using Microsoft.EntityFrameworkCore;
using Exception = System.Exception;

namespace LibraryApi.Services.Livro;

public class LivroService : ILivroInterface
{
    private readonly AppDbContext _context;
    
    public  LivroService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<ResponseModel<List<LivroModel>>> ListarLivros()
    {
        ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
        try
        {
            var livros = await _context.Livros.Include(a => a.Autor).ToListAsync();
            
            resposta.Dados = livros;
            resposta.Mensagem = "Livros listados com sucesso!";
            
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<LivroModel>> BuscarLivroPorId(int idLivro)
    {
        ResponseModel<LivroModel> resposta = new ResponseModel<LivroModel>();
        try
        {
            var livro = await _context.Livros
                .Include(a => a.Autor)
                .FirstOrDefaultAsync(x => x.Id == idLivro);

            if (livro == null)
            {
                resposta.Mensagem = "Nenhum registro foi encontrado!";
                resposta.Status = false;
                return resposta;
            }

            resposta.Dados = livro;
            resposta.Mensagem = "Livro encontrado com sucesso!";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<LivroModel>>> BuscarLivroPorIdAutor(int idAutor)
    {
        ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
        try
        {
            var livro = await _context.Livros
                .Include(a => a.Autor)
                .Where(LivroBanco => LivroBanco.Autor.Id == idAutor)
                .ToListAsync(); 
            
            
            if (livro == null )
            {
                resposta.Mensagem = "Nenhum registro foi encontrado!";
                resposta.Status = false;
                return resposta;
            }
            
            resposta.Dados = livro;
            resposta.Mensagem = "Livros encontrados com sucesso!";
            return resposta;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ResponseModel<List<LivroModel>>> CriarLivro(LivroCriacaoDto livroCriacaoDto)
    {
        ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();

        try
        {
            var autor = await _context.Autores
                .FirstOrDefaultAsync(autorBanco => autorBanco.Id == livroCriacaoDto.Autor.Id);
            
            if (autor == null)
            {
              resposta.Mensagem = "Nenhum registro foi encontrado!";
              return resposta;
            }

            var livro = new LivroModel()
            {
                Titulo = livroCriacaoDto.Titulo,
                Autor = autor,
            };
            
            _context.Livros.Add(livro);
            await _context.SaveChangesAsync();
            
            resposta.Dados = await _context.Livros.Include(a => a.Autor).ToListAsync();

            return resposta;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<ResponseModel<List<LivroModel>>> EditarLivro(LivroEdicaoDto livroEdicaoDto)
    {
        ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
        
        try
        {
            var livro = await _context.Livros
                .Include(a => a.Autor)
                .FirstOrDefaultAsync(livroBanco => livroBanco.Id == livroEdicaoDto.Id);
            
            var autor = await _context.Autores.FirstOrDefaultAsync(autor => autor.Id == livroEdicaoDto.Autor.Id);
            
            if (autor == null)
            {
                resposta.Mensagem = "Nenhum registro foi encontrado!";
                return resposta;
            }
            
            if (livro == null)
            {
                resposta.Mensagem = "Nenhum livro foi encontrado!";
                return resposta;
            }
            
            livro.Titulo = livroEdicaoDto.Titulo;  
            livro.Autor = autor;
            
            _context.Update(livro);
            await _context.SaveChangesAsync();
            
            resposta.Dados = await _context.Livros.ToListAsync();

            return resposta;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ResponseModel<List<LivroModel>>> ExcluirLivro(int idLivro)
    {
        ResponseModel<List<LivroModel>> resposta =  new ResponseModel<List<LivroModel>>();

        try
        {
            var livro = await _context.Livros
                .Include(a=> a.Autor)
                .FirstOrDefaultAsync(x => x.Id == idLivro);

            if (livro == null)
            {
                resposta.Mensagem = "Nenhum registro foi encontrado!";
                return resposta;
            }
            
            _context.Remove(livro);
            await _context.SaveChangesAsync();
            
            resposta.Dados = await _context.Livros.ToListAsync();
            
            return resposta;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
