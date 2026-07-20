using System;

namespace RentACarApi.Models
{
    public class Rental
    {
        public int Kiralama_ID { get; set; }
        public int Arac_ID { get; set; }
        public string Musteri_Adi { get; set; } = string.Empty;
        public DateTime Baslangic_Tarihi { get; set; }
        public DateTime Bitis_Tarihi { get; set; }
        public decimal Toplam_Tutar { get; set; }
    }
}
