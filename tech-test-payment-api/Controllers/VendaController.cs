
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tech_test_payment_api.Data;
using tech_test_payment_api.Models;

namespace tech_test_payment_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VendaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VendaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Venda>> Get()
        {
            var vendas = _context.Venda.Include(p => p.Vendedor).Include(x => x.Produto).OrderBy(v => v.VendaId).ToList();

            return vendas;
        }

        [HttpGet("{id}", Name = "BuscarVenda")]
        public ActionResult Get(int id)
        {
            var venda = _context.Venda.Include(v => v.Vendedor).Include(x => x.Produto).FirstOrDefault(v => v.VendaId == id);

            if (venda is null) return NotFound("Venda não encontrada.");

            return Ok(venda);
        }

        [HttpPost]
        public IActionResult RegistrarVenda(Venda venda)
        {
            if (venda is null) return BadRequest();

            venda.StatusVenda = EnumVenda.Aguardando_Pagamento;

            _context.Venda.Add(venda);
            _context.SaveChanges();

            return Ok(venda);
        }

        [HttpPut("{id:int}")]
        public ActionResult AtualizaVenda(int id, EnumVenda status)
        {
            var result = false;
            var mensagem = "";

            var venda = _context.Venda.Find(id);

            if (venda is null)
            {
                return NotFound("Não foi possível encontrar a venda");
            } else
            {
                switch (venda.StatusVenda)
                {
                    case EnumVenda.Aguardando_Pagamento:
                        {
                            if ((status == EnumVenda.Pagamento_Aprovado) || (status == EnumVenda.Cancelada))
                            {
                                result = true;
                                venda.StatusVenda = status;
                                break;
                            }
                            else
                            {
                                mensagem = "Operação negada. Por favor selecione 'Pagamento Aprovado' ou 'Cancelada'";
                                break;
                            }
                        }
                    case EnumVenda.Pagamento_Aprovado:
                        {
                            if ((status == EnumVenda.Enviado_Transportadora) || (status == EnumVenda.Cancelada))
                            {
                                result = true;
                                venda.StatusVenda = status;
                                break;
                            }
                            else
                            {
                                mensagem = "Operação negada. Por favor selecione 'Enviado para Transportadora' ou 'Cancelada'";
                                break;
                            }
                        }
                    case EnumVenda.Enviado_Transportadora:
                        {
                            if (status == EnumVenda.Entregue)
                            {
                                result = true;
                                venda.StatusVenda = status;
                                break;
                            }
                            else
                            {
                                mensagem = "Operação negada. Por favor selecione 'Entregue'";
                                break;
                            }
                        }
                    case EnumVenda.Entregue:
                        {
                            mensagem = "Operação negada. A venda já foi entregue e não pode mais ser alterada";
                            break;
                        }
                    default:
                        {
                            mensagem = "Operação negada. O status da venda não pode ser vazio";
                            break;
                        }
                }
            }
            if (result)
            {
                _context.SaveChanges();
                return Ok($"Status da venda {id} alterado com sucesso");
            } else
            {
                return BadRequest(mensagem);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var venda = _context.Venda.Find(id);

            if (venda is null) return NotFound("Não foi possível encontrar a venda");

            _context.Venda.Remove(venda);
            _context.SaveChanges();

            return Ok(venda);
        }
    }
}
