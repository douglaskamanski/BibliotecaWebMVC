using BibliotecaWebMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BibliotecaWebMVC.Data.Persistence;

public class AutorPersistence
{
    private BibliotecaContext _context;

    public AutorPersistence(BibliotecaContext context)
    {
        _context = context;
    }

    public IEnumerable<Autor> BuscaTodosAutores()
    {
        return _context.Autores.ToList();
    }

    public Autor BuscarAutorPorId(int id)
    {
        Autor autorDb = _context.Autores.FirstOrDefault(a => a.Id == id);

        return autorDb;
    }

    public Autor Adicionar(Autor autor)
    {
        _context.Autores.Add(autor);
        _context.SaveChanges();

        return autor;
    }

    public Autor Editar(Autor autorEditado)
    {
        Autor autorDb = _context.Autores.FirstOrDefault(a => a.Id == autorEditado.Id);

        if (autorDb == null) throw new System.Exception("Autor não encontrado!");

        autorDb.Nome = autorEditado.Nome;
        autorDb.DataNascimento = autorEditado.DataNascimento;
        autorDb.Nacionalidade = autorEditado.Nacionalidade;

        _context.Autores.Update(autorDb);
        _context.SaveChanges();

        return autorDb;
    }

    public bool Excluir(int id)
    {
        Autor autorDb = BuscarAutorPorId(id);

        if (autorDb == null) throw new System.Exception("Autor não encontrado!");

        _context.Autores.Remove(autorDb);
        _context.SaveChanges();

        return true;
    }

    public IEnumerable<SelectListItem> BuscaTodosAutoresSelectList()
    {
        return _context.Autores.Select(a => new SelectListItem
                                {
                                    Value = a.Id.ToString(),
                                    Text = a.Nome
                                }).ToList();
    }
}
