using BibliotecaWebMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaWebMVC.Data.Persistence;

public class LivroPersistence
{
    private BibliotecaContext _context;

    public LivroPersistence(BibliotecaContext context)
    {
        _context = context;
    }

    public IEnumerable<Livro> BuscaTodosLivros()
    {
        return _context.Livros.Include(l => l.Autor).ToList();
    }

    public Livro BuscarLivroPorId(int id)
    {
        Livro livroDb = _context.Livros.FirstOrDefault(a => a.Id == id);

        return livroDb;
    }

    public Livro Adicionar(Livro livro)
    {
        _context.Livros.Add(livro);
        _context.SaveChanges();

        return livro;
    }

    public Livro Editar(Livro livroEditado)
    {
        Livro livroDb = _context.Livros.FirstOrDefault(a => a.Id == livroEditado.Id);

        if (livroDb == null) throw new System.Exception("Livro não encontrado!");

        livroDb.Nome = livroEditado.Nome;
        livroDb.AutorId = livroEditado.AutorId;
        livroDb.DataPublicacao = livroEditado.DataPublicacao;
        livroDb.Edicao = livroEditado.Edicao;
        livroDb.Editora = livroEditado.Editora;
        livroDb.Isbn = livroEditado.Isbn;
        livroDb.Idioma = livroEditado.Idioma;

        _context.Livros.Update(livroDb);
        _context.SaveChanges();

        return livroDb;
    }

    public bool Excluir(int id)
    {
        Livro livroDb = BuscarLivroPorId(id);

        if (livroDb == null) throw new System.Exception("Livro não encontrado!");

        _context.Livros.Remove(livroDb);
        _context.SaveChanges();

        return true;
    }
}
