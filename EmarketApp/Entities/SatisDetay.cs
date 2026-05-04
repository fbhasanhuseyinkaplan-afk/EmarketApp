namespace EmarketApp.Entities
{
    public class SatisDetay
    {
        public int SatisDetayID { get; set; }
        public int SatisID { get; set; }
        public int UrunID { get; set; }
        public int Adet { get; set; }
        public decimal BirimFiyat { get; set; }
        public decimal AraToplam { get; set; }
    }
}
