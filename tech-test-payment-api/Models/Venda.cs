using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace tech_test_payment_api.Models
{
    public class Venda
    {
        public int VendaId { get; set; }
        public DateTime Data { get; set; }
        public EnumVenda StatusVenda { get; set; }

        public int VendedorId { get; set; }
        public Vendedor Vendedor { get; set; }

        public Produto Produto { get; set; }
    }
}
