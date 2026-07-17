namespace RentACarApi.Models
{
    public class Car
    {
        public int ID { get; set; }
        public string Marka_Model { get; set; } = string.Empty;
        public string Plaka { get; set; } = string.Empty;
        public decimal Gunluk_Fiyat { get; set; }
        public string Durum { get; set; } = "Mevcut"; 
    }
}
