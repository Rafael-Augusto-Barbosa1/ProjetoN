using Microsoft.AspNetCore.Mvc;
using ColaboradorClass = ProjetoNilson4.Models.Colaborador;
using ProjetoNilson4.Repository.Contract;
using ProjetoNilson4.Libraries.Filtro;
using ProjetoNilson4.Models.Constant;

namespace ProjetoNilson4.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    [ColaboradorAutorizacao(ColaboradorTipoConstant.Gerente)]
    public class ColaboradorController : Controller
    {
        private IColaboradorRepository _colaboradorRepository;
        public ColaboradorController(IColaboradorRepository colaboradorRepository)
        {
            _colaboradorRepository = colaboradorRepository;
        }
        public IActionResult Index()
        {
            return View(_colaboradorRepository.ObterTodosColaboradores());
        }
        [HttpGet]
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(Models.Colaborador colaborador)
        {
            colaborador.Tipo = ColaboradorTipoConstant.Comum;
            if (ModelState.IsValid)
            {
                _colaboradorRepository.Cadastrar(colaborador);
                TempData["MSG_S"] = "Registro salvo com sucesso!";
                return RedirectToAction("LoginColaborador", "Home");
            }
            else
            {
                return View(colaborador);
            }
        }

        [HttpGet]
        [ValidateHttpReferer]
        public IActionResult Atualizar(int id)
        {
            Models.Colaborador colaborador = _colaboradorRepository.ObterColaborador(id);
            return View(colaborador);
        }
        [HttpPost]
        public IActionResult Atualizar([FromForm] Models.Colaborador colaborador) 
        { 
            if(ModelState.IsValid)
            {
                _colaboradorRepository.Atualizar(colaborador);
                TempData["MSG_S"] = "Registro salvo com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [ValidateHttpReferer]
        public IActionResult Excluir(int id)
        {
            _colaboradorRepository.Excluir(id);
            return RedirectToAction(nameof(Index));
        }
    }
    
}

