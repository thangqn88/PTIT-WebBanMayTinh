using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.Models
{
    class SetupConnection
    {
        public static SqlConnection connection =
            new SqlConnection("server=.\\SQLEXPRESS; Database=DBWebSite;integrated security=SSPI ");
    }
}
