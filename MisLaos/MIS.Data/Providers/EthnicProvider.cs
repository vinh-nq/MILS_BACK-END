using MIS.Data.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Data.Providers
{
    public interface IEthnicProvider
    {
        void GetAllEthnic();
    }
    public class EthnicProvider : IEthnicProvider
    {
        public void GetAllEthnic()
        {
            using(MySqlConnection conn = MISConnection.CreateInstance())
            {
                string sqlString = @"SELECT * FROM u445720649_misdatabase.tbl_ethnic";

                MySqlCommand sqlCommand = new MySqlCommand(sqlString, conn);
                MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    string a = Convert.ToString(sqlDataReader["ethnic_lao"]);
                }
            }
        }
    }
}
