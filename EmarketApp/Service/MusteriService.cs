using System.Data;
using EmarketApp.DAL;

namespace EmarketApp.Service
{
    public class MusteriService
    {
        private readonly MusteriDAL _musteriDal = new MusteriDAL();

        public DataTable Liste() { return _musteriDal.Liste(); }
    }
}
