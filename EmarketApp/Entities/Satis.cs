using System;
namespace EmarketApp.Entities
{
    public class Satis
    {
        public int SatisID { get; set; }
        public int MusteriID { get; set; }
        public int PersonelID { get; set; }
        public DateTime Tarih { get; set; }
        public decimal ToplamTutar { get; set; }
    }
}
