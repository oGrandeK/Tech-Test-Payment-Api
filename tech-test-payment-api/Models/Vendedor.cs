using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace tech_test_payment_api.Models
{
    public class Vendedor
    {
        public Vendedor()
        {
            Vendas = new Collection<Venda>();
        }

        public int VendedorId { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        [JsonIgnore]
        public ICollection<Venda> Vendas { get; set; }
    }
}
