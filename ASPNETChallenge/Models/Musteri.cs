namespace ASPNETChallenge.Models
{
    public class Musteri
    {
        public int Id { get; set; }
        public string Unvan { get; set; } = string.Empty;

       
        public List<Fatura> Faturalar { get; set; } = new();
    }
}
