using System.Data.SqlClient;

namespace EmarketApp.DAL
{
    public class Db
    {
        
        public SqlConnection conn = new SqlConnection(
            "Server=CANAVAR\\HASANSQL;Database=EmarketDB;User Id=sa;Password=Ankara_06.;");
    }
}
