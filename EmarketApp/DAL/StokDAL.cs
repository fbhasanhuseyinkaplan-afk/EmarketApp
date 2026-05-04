using System.Data;
using System.Data.SqlClient;

namespace EmarketApp.DAL
{
    public class StokDAL
    {
        Db db = new Db();

        public DataTable Liste()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM vw_StokListe", db.conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
