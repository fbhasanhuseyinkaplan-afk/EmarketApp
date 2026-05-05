using System.Data;
using EmarketApp.DAL;

namespace EmarketApp.Service
{
    public class SatisService
    {
        private readonly SatisDAL _satisDal = new SatisDAL();

        public int SatisEkle(int musteriID, int personelID, decimal toplamTutar) { return _satisDal.SatisEkle(musteriID, personelID, toplamTutar); }
        public void SatisDetayEkle(int satisID, int urunID, int adet, decimal birimFiyat) { _satisDal.SatisDetayEkle(satisID, urunID, adet, birimFiyat); }
        public DataTable SatisListe() { return _satisDal.SatisListe(); }
    }
}
