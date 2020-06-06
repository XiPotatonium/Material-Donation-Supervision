using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace MDS.Server
{
    static class Connect
    {
        public static SqlConnection Connection;
        public static void ConnectDatabase()
        {
            string connString = "Server=121.36.8.111,1433;DataBase=Material;uid=rdsuser;pwd=20200202bwD";      
            Connection = new SqlConnection(connString);
            Connection.Open();
        }
    }
}
