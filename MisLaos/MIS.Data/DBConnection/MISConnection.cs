using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Data.Model
{
    class MISConnection
    {
        public MISConnection()
        {

        }

        public static MySqlConnection CreateInstance()
        {
            string host = "185.224.137.128";
            int port = 3306;
            string database = "u445720649_misdatabase";
            string username = "u445720649_misuser";
            string password = "misDB@2020";

            MySqlConnection instance = null;
            string connectionString = @"Server=" + host + ";Database=" + database
             + ";port=" + port + ";User Id=" + username + ";password=" + password;
            instance = new MySqlConnection(connectionString);
            instance.Open();
            return instance;

        }
    }
}
