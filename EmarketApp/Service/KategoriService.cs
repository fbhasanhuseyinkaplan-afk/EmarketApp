using System.Data;
using EmarketApp.DAL;

namespace EmarketApp.Service
{
    public class KategoriService
    {
        private readonly KategoriDAL _kategoriDal = new KategoriDAL();

        public DataTable Liste() { return _kategoriDal.Liste(); }
    }
}
