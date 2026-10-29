using Microsoft.AspNetCore.Mvc;
using ProjetoNilson4.Models;
using ProjetoNilson4.Repository.Contract;

namespace ProjetoNilson4.Areas.Colaborador.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteApiController : Controller
    {
        private IClienteRepository _clienteRepository;
        public ClienteApiController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        public IActionResult ListarClientes()
        {
            var clientes = _clienteRepository.ObterTodosClientes();
            return Ok(clientes);
        }

        [HttpPost]
        public IActionResult Post(Cliente cliente)
        {
            if (cliente is null)
            {
                return BadRequest(new
                {
                    mensagem = "os dados do cliente são obrigatórios."
                });
            }
            _clienteRepository.Cadastrar(cliente);
            return Ok(new
            {
                mensagem = "cadastro realizado com sucesso.",
                cliente = cliente
            });
        }

        //pegar por id
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var cliente = _clienteRepository.ObterCliente(id);

            if (cliente == null)
            {
                return NotFound(new
                {
                    mensagem = "cliente não encontrado"
                });
            }
            return Ok(cliente);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var clienteExistente = _clienteRepository.ObterCliente(id);
            if (clienteExistente == null)
            {
                return NotFound(new
                {
                    mensagem = "Cliente não encontrado."
                });
            }
            cliente.Id = id;

            _clienteRepository.Atualizar(cliente);

            return Ok(new
            {
                mensagem = "Cliente atualizado com sucesso."
            });
        }

        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            var cliente = _clienteRepository.ObterCliente(id);

            if(cliente == null)
            {
                return NotFound(new
                {
                    mensagem = "Cliente não encontrado."
                });
            }

            _clienteRepository.Excluir(id);

            return Ok(new
            {
                mensagem = "Cliente excluído com sucesso."
            });
        }
        
    }
}
