using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM_C_3.Connect
{
    public class Connectstring
    {
        private static string con = @"Data Source=ptpm\sqlexpress;Initial Catalog=C#3_ASM;Integrated Security=True";
        public static SqlConnection getConnection()
        {
            return new SqlConnection(con);
        }

    }
}
