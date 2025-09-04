using ASPNETChallenge.Models;

namespace ASPNETChallenge.Repositories
{
    public interface IMusteriRepository : IRepository<Musteri>
    {
        Task<List<Musteri>> GetMusterilerWithFaturalarAsync();
    }
}
