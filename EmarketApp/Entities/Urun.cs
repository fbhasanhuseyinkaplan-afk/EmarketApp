using System;
namespace EmarketApp.Entities
{
    public class Urun
    {
        public int UrunID { get; set; }
        public string UrunAd { get; set; }
        public int KategoriID { get; set; }
        public int? KampanyaID { get; set; }
        public DateTime EklenmeTarihi { get; set; }
        public bool AktifMi { get; set; }
    }
}
