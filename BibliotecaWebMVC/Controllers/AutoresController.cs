using BibliotecaWebMVC.Data.Persistence;
using BibliotecaWebMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaWebMVC.Controllers;

public class AutoresController : Controller
{
    private readonly AutorPersistence _autorPersistence;

    public AutoresController(AutorPersistence autorPersistence)
    {
        _autorPersistence = autorPersistence;
    }

    public IActionResult Listar()
    {
        var autores = _autorPersistence.BuscaTodosAutores();

        return View(autores);
    }

    public IActionResult Adicionar()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Adicionar(Autor autor)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _autorPersistence.Adicionar(autor);
                TempData["MensagemSucesso"] = "Autor cadastrado com sucesso";
                return RedirectToAction("Listar");
            }

            return View(autor);
        }
        catch (Exception ex)
        {
            TempData["MensagemErro"] = $"Erro ao cadastrar autor. Erro: {ex.Message}";
            return RedirectToAction("Listar");
        }
    }

    public IActionResult Editar(int id)
    {
        Autor autor = _autorPersistence.BuscarAutorPorId(id);

        return View(autor);
    }

    [HttpPost]
    public IActionResult Editar(Autor autor)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _autorPersistence.Editar(autor);
                TempData["MensagemSucesso"] = "Autor alterado com sucesso";
                return RedirectToAction("Listar");
            }

            return View(autor);

        }
        catch (Exception ex)
        {
            TempData["MensagemErro"] = $"Erro ao alterar autor. Erro: {ex.Message}";
            return View(autor);
        }
    }

    public IActionResult ExcluirConfirmacao(int id)
    {
        Autor autor = _autorPersistence.BuscarAutorPorId(id);

        return View(autor);
    }

    public IActionResult Excluir(int id)
    {
        try
        {
            bool excluidoSucesso = _autorPersistence.Excluir(id);

            if (excluidoSucesso)
            {
                TempData["MensagemSucesso"] = "Autor excluído com sucesso";
            }
            else
            {
                TempData["MensagemErro"] = $"Erro ao excluir autor";
            }

            return RedirectToAction("Listar");
        }
        catch (Exception ex)
        {
            TempData["MensagemErro"] = $"Erro ao excluir autor. Erro: {ex.Message}";
            return RedirectToAction("Listar");
        }
    }
}
