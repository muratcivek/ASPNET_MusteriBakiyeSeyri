using System.Text.Json.Serialization;

namespace ASPNETChallenge.Models
{
    public class Fatura
    {
        public int Id { get; set; }
        public int MusteriId { get; set; }
        public DateTime FaturaTarihi { get; set; }
        public decimal FaturaTutari { get; set; }
        public DateTime? OdemeTarihi { get; set; }

       
        [JsonIgnore]
        public Musteri Musteri { get; set; } = null!;
    }
}
