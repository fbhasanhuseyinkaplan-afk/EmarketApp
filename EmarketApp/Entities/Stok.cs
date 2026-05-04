using System;
namespace EmarketApp.Entities
{
    public class Stok
    {
        public int StokID { get; set; }
        public int UrunID { get; set; }
        public int Miktar { get; set; }
        public DateTime GuncellemeTarihi { get; set; }
    }
}
