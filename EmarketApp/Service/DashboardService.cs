using System.Data;
using EmarketApp.DAL;

namespace EmarketApp.Service
{
    public class DashboardService
    {
        private readonly DashboardDAL _dashboardDal = new DashboardDAL();

        public decimal ToplamCiro() { return _dashboardDal.ToplamCiro(); }
        public int ToplamSiparis() { return _dashboardDal.ToplamSiparis(); }
        public int ToplamUrun() { return _dashboardDal.ToplamUrun(); }
        public int ToplamMusteri() { return _dashboardDal.ToplamMusteri(); }
        public DataTable SonSiparisler() { return _dashboardDal.SonSiparisler(); }
        public DataTable EnCokSatanUrunler() { return _dashboardDal.EnCokSatanUrunler(); }
        public int ToplamSatisDetayAdet() { return _dashboardDal.ToplamSatisDetayAdet(); }
        public DataTable KategoriSatisOranlari() { return _dashboardDal.KategoriSatisOranlari(); }
        public DataTable Son7GunSatis() { return _dashboardDal.Son7GunSatis(); }
    }
}
