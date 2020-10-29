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
    public interface IDistrictProvider
    {
        List<DistrictViewModel> GetAllDistrict();
    }
    public class DistrictProvider : IDistrictProvider
    {
        public List<DistrictViewModel> GetAllDistrict()
        {
            List<DistrictViewModel> districtViewModels = new List<DistrictViewModel>();
            using (MySqlConnection conn = MISConnection.CreateInstance())
            {
                string sqlString = @"SELECT * FROM u445720649_misdatabase.tbl_district;";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlString, conn);
                mySqlCommand.CommandType = System.Data.CommandType.Text;
                using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
                {
                    while (mySqlDataReader.Read())
                    {
                        DistrictViewModel districtViewModel = new DistrictViewModel();
                        districtViewModel.No = Convert.ToString(mySqlDataReader["no"]);
                        districtViewModel.ProvinceId = Convert.ToString(mySqlDataReader["proid"]);
                        districtViewModel.DistrictId = Convert.ToString(mySqlDataReader["distid"]);
                        districtViewModel.DistrictName = Convert.ToString(mySqlDataReader["distnamelao"]);
                        districtViewModel.DistrictNameEng = Convert.ToString(mySqlDataReader["distnameeng"]);

                        districtViewModels.Add(districtViewModel);
                    }
                }
            }
            return districtViewModels;
        }
    }
}
