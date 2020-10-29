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
    public class PermissionProvider
    {
        public async Task<List<PermissionViewModel>> GetByRoleId(int RoleId)
        {
            try
            {
                var lst = new List<PermissionViewModel>();
                using (MySqlConnection conn = MISConnection.CreateInstance())
                {
                    string sqlString = @"SELECT * FROM tbl_roles_functions as rf inner join tbl_functions as f on rf.FunctionCode = f.FunctionCode where rf.fk_role_id = " + RoleId + ";";

                    MySqlCommand sqlCommand = new MySqlCommand(sqlString, conn);
                    MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (await sqlDataReader.ReadAsync())
                    {
                        var per = new PermissionViewModel();
                        per.PermissionId = await sqlDataReader.GetFieldValueAsync<int>(0);
                        per.fk_role_id = await sqlDataReader.GetFieldValueAsync<int>(0);
                        per.FunctionCode = await sqlDataReader.GetFieldValueAsync<string>(0);
                        per.FunctionName = await sqlDataReader.GetFieldValueAsync<string>(0);
                        per.Allowed = await sqlDataReader.GetFieldValueAsync<int>(0);
                        lst.Add(per);
                    }
                }
                return lst;
            }
            catch (Exception e)
            {
                return new List<PermissionViewModel>();
            }
        }
    }
}
