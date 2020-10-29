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
    public interface IHouseHoldProvider
    {
        List<HouseHoldViewModel> SearchHouseHold(string provinceId, string districtId, string villageId, string unitId, int child, int pregnant, string headName);
    }
    public class HouseHoldProvider : IHouseHoldProvider
    {
        public List<HouseHoldViewModel> SearchHouseHold(string provinceId, string districtId, string villageId, string unitId, int child, int pregnant, string headName)
        {
            List<HouseHoldViewModel> houseHoldViewModels = new List<HouseHoldViewModel>();
            using (MySqlConnection conn = MISConnection.CreateInstance())
            {
                string sqlString = @"Select * From (SELECT hh_id, hh_level, unit, hh_head_name, total_hh_members, number_plots,
					                                    (SELECT villname FROM u445720649_misdatabase.tbl_village where villid Like substring(a.unit, 1, 7)) as 'village',
					                                    (Select sum(hh_pregnant_woman = 1) From u445720649_misdatabase.tbl_hh_member Where hh_id Like a.hh_code Group by hh_id) as 'hh_pregnant_woman',
					                                    (Select sum(hh_total_age_cal < 2) From u445720649_misdatabase.tbl_hh_member where hh_id Like a.hh_code Group by hh_id) as 'hh_child'
				                                    FROM u445720649_misdatabase.tbl_information as a) as b ";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlString, conn);
                mySqlCommand.CommandType = System.Data.CommandType.Text;
                mySqlCommand.Parameters.AddWithValue("@ProvinceId", provinceId);
                using (MySqlDataReader sqlDataReader = mySqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        HouseHoldViewModel houseHoldViewModel = new HouseHoldViewModel();
                        houseHoldViewModel.Id = Convert.ToInt64(sqlDataReader["hh_id"]);
                        houseHoldViewModel.HHLevel = Convert.ToString(sqlDataReader["hh_level"]);
                        houseHoldViewModel.Unit = Convert.ToString(sqlDataReader["unit"]);
                        houseHoldViewModel.HeadOfHHName = Convert.ToString(sqlDataReader["hh_head_name"]);
                        houseHoldViewModel.TotalHHMembers = Convert.ToInt32(sqlDataReader["total_hh_members"]);
                        houseHoldViewModel.NumberPlots = Convert.ToString(sqlDataReader["number_plots"]);
                        houseHoldViewModel.Village = Convert.ToString(sqlDataReader["village"]);
                        houseHoldViewModel.NumberPregnant = Convert.ToInt32(sqlDataReader["hh_pregnant_woman"]);
                        houseHoldViewModel.NumberChild = Convert.ToInt32(sqlDataReader["hh_child"]);

                        houseHoldViewModels.Add(houseHoldViewModel);
                    }

                }
            }


            return houseHoldViewModels;
        }
    }
}

