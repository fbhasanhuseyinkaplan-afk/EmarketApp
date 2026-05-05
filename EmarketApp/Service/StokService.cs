using System.Data;
using EmarketApp.DAL;

namespace EmarketApp.Service
{
    public class StokService
    {
        private readonly StokDAL _stokDal = new StokDAL();

        public DataTable Liste() { return _stokDal.Liste(); }
    }
}
