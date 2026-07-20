namespace RentACarApi.DTOs
{
    public class AddCarDTO
    {
        public string Marka_Model { get; set; } = string.Empty;
        public string Plaka { get; set; } = string.Empty;
        public decimal Gunluk_Fiyat { get; set; }
    }
}
