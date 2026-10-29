using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using ProjetoNilson4.Libraries.Filtro;
using ProjetoNilson4.Models;
using ProjetoNilson4.Repository.Contract;

namespace ProjetoNilson4.Controllers
{
    public class ClienteController : Controller
    {
        private IClienteRepository _clienteRepository;

        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        public IActionResult Cadastro()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cadastro([FromForm]Cliente cliente)
        {
            var CPFExit = _clienteRepository.BuscaCpfCliente(cliente.CPF.Replace("-", "").Replace(".", "")).CPF;
            var EmailExit = _clienteRepository.BuscaEmailCliente(cliente.Email).Email;
            if(!string.IsNullOrWhiteSpace(CPFExit))
            {
                //CPF Cadastrado
                ViewData["MSG_CPF"] = "CPF já cadastrado, por favor verifique os dados digitados";
                return View();
            }
            else if (!string.IsNullOrWhiteSpace(EmailExit))
            {
                //Email Cadastrado
                ViewData["MSG_Email"] = "E-mail já cadastrado, por favor verifique os dados digitados";
                return View();
            }
            else if(ModelState.IsValid)
            {
                _clienteRepository.Cadastrar(cliente);
                return RedirectToAction("LoginCliente", "Home");
            }
            return View();
        }

        
    }
}
