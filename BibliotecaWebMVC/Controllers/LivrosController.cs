using BibliotecaWebMVC.Data.Persistence;
using BibliotecaWebMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaWebMVC.Controllers;

public class LivrosController : Controller
{
    private readonly LivroPersistence _livroPersistence;

    public LivrosController(LivroPersistence livroPersistence)
    {
        _livroPersistence = livroPersistence;
    }

    public IActionResult Listar()
    {
        var livros = _livroPersistence.BuscaTodosLivros();

        return View(livros);
    }

    public IActionResult Adicionar()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Adicionar(Livro livro)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _livroPersistence.Adicionar(livro);
                TempData["MensagemSucesso"] = "Livro cadastrado com sucesso";
                return RedirectToAction("Listar");
            }

            return View(livro);
        }
        catch (Exception ex)
        {
            TempData["MensagemErro"] = $"Erro ao cadastrar livro. Erro: {ex.Message}";
            return RedirectToAction("Listar");
        }

    }

    public IActionResult Editar(int id)
    {
        Livro livro = _livroPersistence.BuscarLivroPorId(id);

        return View(livro);
    }

    [HttpPost]
    public IActionResult Editar(Livro livro)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _livroPersistence.Editar(livro);
                TempData["MensagemSucesso"] = "Livro alterado com sucesso";
                return RedirectToAction("Listar");
            }

            return View(livro);
        }
        catch (Exception ex)
        {
            TempData["MensagemErro"] = $"Erro ao alterar livro. Erro: {ex.Message}";
            return View(livro);
        }
    }

    public IActionResult ExcluirConfirmacao(int id)
    {
        Livro livro = _livroPersistence.BuscarLivroPorId(id);

        return View(livro);
    }

    public IActionResult Excluir(int id)
    {
        try
        {
            bool excluidoSucesso = _livroPersistence.Excluir(id);

            if (excluidoSucesso)
            {
                TempData["MensagemSucesso"] = "Livro excluído com sucesso";
            }
            else
            {
                TempData["MensagemErro"] = $"Erro ao excluir livro";
            }

            return RedirectToAction("Listar");
        }
        catch (Exception ex)
        {
            TempData["MensagemErro"] = $"Erro ao excluir livro. Erro: {ex.Message}";
            return RedirectToAction("Listar");
        }
    }

}
