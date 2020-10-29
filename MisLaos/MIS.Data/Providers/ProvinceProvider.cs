using MIS.Data.Model;
using MIS.Data.ViewModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Data.Providers
{
    public interface IProvinceProvider
    {
        List<ProvinceViewModel> GetAllProvince();
    }
    public class ProvinceProvider : IProvinceProvider
    {
        public List<ProvinceViewModel> GetAllProvince()
        {
            List<ProvinceViewModel> provinceViewModels = new List<ProvinceViewModel>();
            using (MySqlConnection conn = MISConnection.CreateInstance())
            {
                string sqlString = @"SELECT * FROM u445720649_misdatabase.tbl_province";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlString, conn);
                mySqlCommand.CommandType = System.Data.CommandType.Text;
                using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
                {
                    while (mySqlDataReader.Read())
                    {
                        ProvinceViewModel provinceViewModel = new ProvinceViewModel();
                        provinceViewModel.Id = Convert.ToString(mySqlDataReader["provid"]);
                        provinceViewModel.Name = Convert.ToString(mySqlDataReader["provname"]);
                        provinceViewModel.NameEng = Convert.ToString(mySqlDataReader["provnameeng"]);

                        provinceViewModels.Add(provinceViewModel);
                    }
                }
            }

            return provinceViewModels;
        }
    }
}
