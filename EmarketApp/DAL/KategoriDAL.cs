using System.Data;
using System.Data.SqlClient;

namespace EmarketApp.DAL
{
    public class KategoriDAL
    {
        Db db = new Db();

        public DataTable Liste()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM vw_KategoriListe", db.conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
