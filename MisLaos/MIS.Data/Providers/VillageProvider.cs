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
    public interface IVillageProvider
    {
        List<VillageViewModel> GetAllVillage();
    }
    public class VillageProvider : IVillageProvider
    {
        public List<VillageViewModel> GetAllVillage()
        {
            List<VillageViewModel> villageViewModels = new List<VillageViewModel>();
            using (MySqlConnection conn = MISConnection.CreateInstance())
            {
                string sqlString = @"SELECT * FROM u445720649_misdatabase.tbl_village";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlString, conn);
                mySqlCommand.CommandType = System.Data.CommandType.Text;
                using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
                {
                    while (mySqlDataReader.Read())
                    {
                        VillageViewModel villageViewModel = new VillageViewModel();
                        villageViewModel.VillageId = Convert.ToString(mySqlDataReader["villid"]);
                        villageViewModel.DistrictId = Convert.ToString(mySqlDataReader["distid"]);
                        villageViewModel.VillageName = Convert.ToString(mySqlDataReader["villname"]);
                        villageViewModel.VillageNameEng = Convert.ToString(mySqlDataReader["villnameeng"]);

                        villageViewModels.Add(villageViewModel);
                    }
                }
            }
            return villageViewModels;
        }
    }
}
