using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCCorp.IG.Core.Helper
{
    public class MySqlDbContext
    {
        internal static string _cons = @"Server=192.168.23.22;Database=social_index_v2;User ID=thuyetnd;Password=AIV3k6H0uzWmcoaf2RQ9;charset=utf8";
        internal MySqlConnection _connect;

        public void OpenMySql()
        {
            _connect = new MySqlConnection(_cons);

            try
            {
                if (_connect.State == System.Data.ConnectionState.Closed || _connect.State == System.Data.ConnectionState.Broken)
                {
                    _connect.Open();
                }
            }
            catch (Exception)
            {
                
            }
        }

        //Đóng kết nối
        public void Dispose()
        {
            if (_connect.State == System.Data.ConnectionState.Open)
            {
                _connect.Close();
                _connect.Dispose();
            }
            else
            {
                _connect.Dispose();
            }
        }
    }
}
