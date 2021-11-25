using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogoJogos.InputModel;
using ApiCatalogoJogos.Services;
using ApiCatalogoJogos.ViewModel;
using Microsoft.AspNetCore.Mvc;


namespace ApiCatalogoJogos.Controllers.V1
{


    [Route("api/V1/[Controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {

        private readonly IJogoService _jogoService;

        public JogosController(IJogoService jogoService)
        {
            _jogoService = jogoService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<JogoViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var jogos = await _jogoService.Obter(pagina, quantidade);
            if (jogos.Count() == 0)
            {
                return NoContent();
            }

            return Ok(jogos);

        }
        [HttpGet("{idjogo:guid}")]
        public async Task<ActionResult<JogoViewModel>> Obter([FromRoute] Guid idjogo)
        {
            var jogo = await _jogoService.Obter(idjogo);
            if (jogo == null)
            {
                return NoContent(jogo);

            }
            return Ok();

        }

        [HttpPost]
        public async Task<ActionResult<JogoViewModel>> InserirJogo([FromBody] JogoInputModel jogoInputModel)
        {
            try
            {
                var jogo = await _jogoService.Inserir(jogoInputModel);
                return Ok(jogo);
            }
            //catch (JogoJaCadastadoException ex)
            catch (Exception ex)
            {
                return UnprocessableEntity("Já existe um jogo com este nome para esta produtora");
            }


        }

        [HttpPut("{idjogo:guid}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idJogo, [FromBody] JogoInputModel jogoInputModel)
        {
            try
            {
                await _jogoService.Atualizar(idJogo, jogoInputModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Não existe este jogo");
            }


        }

        [HttpPatch("{idjogo:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idJogo, [FromRoute] double preco)
        {
            try
            {
                await _jogoService.Atualizar(idJogo, preco);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Não existe este jogo");
            }

        }


        [HttpDelete("{idjogo:guid}")]
        public async Task<ActionResult> ApagarJogo([FromRoute] Guid idJogo)
        {
            try
            {
                await _jogoService.Atualizar(idJogo);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Não existe este jogo");
            }

        }
    }
}