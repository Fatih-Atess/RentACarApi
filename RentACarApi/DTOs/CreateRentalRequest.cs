using System;

namespace RentACarApi.DTOs
{
    public class CreateRentalRequest
    {
        public int ID { get; set; }
        public string Musteri_Adi { get; set; } = string.Empty;
        public DateTime Baslangic_Tarihi { get; set; }
        public DateTime Bitis_Tarihi { get; set; }
        public decimal Toplam_Tutar { get; set; }
    }
}
