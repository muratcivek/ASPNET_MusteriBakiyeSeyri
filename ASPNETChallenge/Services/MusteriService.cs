using ASPNETChallenge.Data;
using ASPNETChallenge.Models;
using ASPNETChallenge.Repositories;

namespace ASPNETChallenge.Services
{
    public class MusteriService
    {
        private readonly IMusteriRepository _musteriRepository;

        public MusteriService(IMusteriRepository musteriRepository)
        {
            _musteriRepository = musteriRepository;
        }

        public async Task<List<Musteri>> GetMusterilerAsync()
        {
            return await _musteriRepository.GetMusterilerWithFaturalarAsync();
        }

        public async Task<object> GetBakiyeSeyriAsync(int musteriId)
        {
            var musteri = (await _musteriRepository.GetMusterilerWithFaturalarAsync())
                          .FirstOrDefault(m => m.Id == musteriId);

            if (musteri == null)
                return new { history = new List<BakiyeEntry>(), maxDebt = 0m, maxDebtDate = string.Empty };

            decimal bakiye = 0;
            var history = new List<BakiyeEntry>();

            // Faturaları ve ödemeleri sırayla işle
            foreach (var f in musteri.Faturalar.OrderBy(f => f.FaturaTarihi))
            {
                // Fatura eklendiğinde bakiye artar
                bakiye += f.FaturaTutari;
                history.Add(new BakiyeEntry(f.FaturaTarihi, bakiye));

                // Ödeme varsa bakiye düşer
                if (f.OdemeTarihi.HasValue)
                {
                    bakiye -= f.FaturaTutari;
                    history.Add(new BakiyeEntry(f.OdemeTarihi.Value, bakiye));
                }
            }

            // Maksimum bakiye ve tarihi
            var maxEntry = history.OrderByDescending(h => h.Amount).FirstOrDefault();
            decimal maxDebt = maxEntry?.Amount ?? 0m;
            string maxDebtDate = maxEntry?.Date.ToString("yyyy-MM-dd") ?? string.Empty;

            return new { history, maxDebt, maxDebtDate };
        }

       
        public record BakiyeEntry(DateTime Date, decimal Amount);

    }
}
