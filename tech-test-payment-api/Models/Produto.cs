using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace tech_test_payment_api.Models
{
    public class Produto
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }

        [JsonIgnore]
        public int VendaId { get; set; }
    }
}
