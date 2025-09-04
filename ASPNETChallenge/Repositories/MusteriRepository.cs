using ASPNETChallenge.Data;
using ASPNETChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNETChallenge.Repositories
{
    public class MusteriRepository : Repository<Musteri>, IMusteriRepository
    {
        public MusteriRepository(AppDbContext context) : base(context) { }

        public async Task<List<Musteri>> GetMusterilerWithFaturalarAsync()
        {
            return await _context.Musteriler
                                 .Include(m => m.Faturalar)
                                 .ToListAsync();
        }
    }
}
