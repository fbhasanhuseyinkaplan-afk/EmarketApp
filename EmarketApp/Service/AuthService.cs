using EmarketApp.DAL;

namespace EmarketApp.Service
{
    public class AuthService
    {
        private readonly PersonelDAL _personelDal = new PersonelDAL();

        public bool GirisBasariliMi(string kullanici, string sifre)
        {
            return _personelDal.GirisDogrula(kullanici, sifre);
        }
    }
}
