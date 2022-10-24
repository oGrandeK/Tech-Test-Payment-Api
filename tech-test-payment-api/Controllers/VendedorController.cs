using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tech_test_payment_api.Data;
using tech_test_payment_api.Models;

namespace tech_test_payment_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendedorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VendedorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Vendedor>> Get()
        {
            var vendedores = _context.Vendedor.Take(10).ToList();

            if (vendedores is null) return NotFound("Não foi possível encontrar os vendedores.");

            return vendedores;
        }

        [HttpGet("{id:int}", Name ="ObterVendedor")]
        public ActionResult<Vendedor> Get(int id)
        {
            var vendedor = _context.Vendedor.Find(id);

            if (vendedor is null) return NotFound("Não foi possível encontrar o vendedor.");

            return Ok(vendedor);
        }

        [HttpPost]
        public ActionResult<Vendedor> Post(Vendedor vendedor)
        {
            if (vendedor is null) return BadRequest("Dados do vendedor inválido");

            _context.Vendedor.Add(vendedor);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterVendedor", new {id = vendedor.VendedorId}, vendedor);
        }

        [HttpPut("{id:int}")]
        public ActionResult<Vendedor> Put(int id, Vendedor vendedor)
        {
            if (id != vendedor.VendedorId) return BadRequest("Não foi possível encontrar o vendedor.");

            _context.Entry(vendedor).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(vendedor);
        }

        [HttpDelete("{id}")]
        public ActionResult<Vendedor> Delete(int id)
        {
            var vendedor = _context.Vendedor.Find(id);

            if (vendedor is null) return BadRequest("Não foi possível encontrar o vendedor.");

            _context.Vendedor.Remove(vendedor);
            _context.SaveChanges();

            return Ok(vendedor);
        }
    }
}
