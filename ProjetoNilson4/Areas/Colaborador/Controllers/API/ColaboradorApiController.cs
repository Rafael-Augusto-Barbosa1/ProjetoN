using Microsoft.AspNetCore.Mvc;
using ProjetoNilson4.Models;
using ProjetoNilson4.Repository;
using ProjetoNilson4.Repository.Contract;

namespace ProjetoNilson4.Areas.Colaborador.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColaboradorApiController : Controller
    {
        private IColaboradorRepository _colaboradorRepository;
        public ColaboradorApiController(IColaboradorRepository colaboradorRepository)
        {
            _colaboradorRepository = colaboradorRepository;
        }
        [HttpGet]
        public IActionResult ListarColaboradores()
        {
            var colaboradores = _colaboradorRepository.ObterTodosColaboradores();
            return Ok(colaboradores);
        }


        [HttpPost]
        public IActionResult Post(Models.Colaborador colaborador)
        {
            if (colaborador is null)
            {
                return BadRequest(new
                {
                    mensagem = "os dados do colaborador são obrigatórios."
                });
            }
            _colaboradorRepository.Cadastrar(colaborador);
            return Ok(new
            {
                mensagem = "cadastro realizado com sucesso.",
                colaborador = colaborador
            });
        }

        //pegar por id
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var colaborador = _colaboradorRepository.ObterColaborador(id);

            if (colaborador == null)
            {
                return NotFound(new
                {
                    mensagem = "colaborador não encontrado"
                });
            }
            return Ok(colaborador);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Models.Colaborador colaborador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var colaboradorExistente = _colaboradorRepository.ObterColaborador(id);
            if (colaboradorExistente == null)
            {
                return NotFound(new
                {
                    mensagem = "Colaborador não encontrado."
                });
            }
            colaborador.Id = id;

            _colaboradorRepository.Atualizar(colaborador);

            return Ok(new
            {
                mensagem = "Colaborador atualizado com sucesso."
            });
        }

        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            var colaborador = _colaboradorRepository.ObterColaborador(id);

            if (colaborador == null)
            {
                return NotFound(new
                {
                    mensagem = "Colaborador não encontrado."
                });
            }

            _colaboradorRepository.Excluir(id);

            return Ok(new
            {
                mensagem = "Colaborador excluído com sucesso."
            });
        }
    }
