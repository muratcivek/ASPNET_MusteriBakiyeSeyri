document.addEventListener("DOMContentLoaded", () => {
    const customerSelect = document.getElementById("musteriSelect");
    const maxDebtInfo = document.getElementById("maxDebtInfo");
    const ctx = document.getElementById('bakiyeChart').getContext('2d');
    let myChart;

    fetch("/api/musteri/all")
        .then(res => res.json())
        .then(data => {
            data.forEach(m => {
                const option = document.createElement("option");
                option.value = m.id;
                option.textContent = m.unvan;
                customerSelect.appendChild(option);
            });
        })
        .catch(err => console.error("Müşteri listesi alınamadı:", err));

    customerSelect.addEventListener("change", () => {
        const customerId = parseInt(customerSelect.value);
        if (!customerId) {
            maxDebtInfo.innerHTML = "📌 Henüz müşteri seçilmedi.";
            if (myChart) myChart.destroy();
            return;
        }

        fetch(`/api/musteri/${customerId}/bakiye`)
            .then(res => res.json())
            .then(data => {
                if (myChart) myChart.destroy();

                const labels = data.history.map(h => h.date);
                const amounts = data.history.map(h => h.amount);
                const maxDebt = data.maxDebt;
                const maxIndex = amounts.indexOf(maxDebt);
                const pointColors = amounts.map((a, i) => i === maxIndex ? 'red' : '#003366');

                myChart = new Chart(ctx, {
                    type: 'line',
                    data: {
                        labels: labels,
                        datasets: [{
                            label: 'Borç Seyri',
                            data: amounts,
                            borderColor: '#003366',
                            backgroundColor: 'rgba(0,51,102,0.2)',
                            fill: true,
                            tension: 0.3,
                            pointBackgroundColor: pointColors,
                            pointRadius: 6
                        }]
                    },
                    options: {
                        responsive: true,
                        plugins: {
                            legend: { display: true },
                            tooltip: {
                                callbacks: {
                                    label: function (context) {
                                        const val = context.raw;
                                        return val === maxDebt ? `🔴 Maksimum Borç: ${val} TL` : `Borç: ${val} TL`;
                                    }
                                }
                            }
                        },
                        scales: {
                            x: { title: { display: true, text: 'Tarih' } },
                            y: { title: { display: true, text: 'Borç (TL)' } }
                        }
                    }
                });

                maxDebtInfo.innerHTML = `📌 En yüksek borç: <b>${maxDebt} TL</b> <br> Tarih: <b>${data.maxDebtDate}</b>`;
            })
            .catch(err => {
                console.error("Borç verisi alınamadı:", err);
                maxDebtInfo.innerHTML = "❌ Veriler yüklenemedi.";
            });
    });
});
