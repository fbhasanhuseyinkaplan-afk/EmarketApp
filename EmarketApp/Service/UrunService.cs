using System.Data;
using EmarketApp.DAL;

namespace EmarketApp.Service
{
    public class UrunService
    {
        private readonly UrunDAL _urunDal = new UrunDAL();

        public DataTable Liste() { return _urunDal.Liste(); }
        public void Ekle(string urunAd, int kategoriID) { _urunDal.Ekle(urunAd, kategoriID); }
        public void Guncelle(int urunID, string urunAd, int kategoriID) { _urunDal.Guncelle(urunID, urunAd, kategoriID); }
        public void Sil(int urunID) { _urunDal.Sil(urunID); }
        public decimal SonFiyatGetir(int urunID) { return _urunDal.SonFiyatGetir(urunID); }
    }
}
