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
    public class UserProvider
    {
        public async Task<UserViewModel> GetByUserName(string userName)
        {
            var user = new UserViewModel();
            try
            {
                using (MySqlConnection conn = MISConnection.CreateInstance())
                {
                    string sqlString = @"SELECT * FROM tbl_users as u left join tbl_roles as r on u.fk_role_id = r.RoleId where u.UserName = '" + userName + "';";

                    MySqlCommand sqlCommand = new MySqlCommand(sqlString, conn);
                    MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (await sqlDataReader.ReadAsync())
                    {
                        user.UserId = await sqlDataReader.GetFieldValueAsync<int>(0);
                        user.Password = await sqlDataReader.GetFieldValueAsync<string>(1);
                        user.FullName = await sqlDataReader.GetFieldValueAsync<string>(2);
                        user.UserName = await sqlDataReader.GetFieldValueAsync<string>(3);
                        user.Mobilephone = await sqlDataReader.GetFieldValueAsync<string>(4);
                        user.Email = await sqlDataReader.GetFieldValueAsync<string>(5);
                        user.Department = await sqlDataReader.GetFieldValueAsync<string>(6);
                        user.Type = await sqlDataReader.GetFieldValueAsync<char>(7);
                        user.Admin = await sqlDataReader.GetFieldValueAsync<int>(8);
                        user.Enabled = await sqlDataReader.GetFieldValueAsync<int>(9);
                        user.RoleId = await sqlDataReader.GetFieldValueAsync<int>(10);
                        user.Active = await sqlDataReader.GetFieldValueAsync<int>(11);
                        user.CreatedBy = await sqlDataReader.GetFieldValueAsync<string>(12);
                    }
                }
                return user;
            }
            catch
            {
                return null;
            }
        }

        public async Task<UserViewModel> GetByUserNameAndPassword(string userName, string passWord)
        {
            var user = new UserViewModel();
            try
            {
                using (MySqlConnection conn = MISConnection.CreateInstance())
                {
                    string sqlString = @"SELECT * FROM tbl_users as u left join tbl_roles as r on u.fk_role_id = r.RoleId where u.UserName = '" + userName + "' and u.Password ='" + passWord + "';";

                    MySqlCommand sqlCommand = new MySqlCommand(sqlString, conn);
                    MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (await sqlDataReader.ReadAsync())
                    {
                        user.UserId = await sqlDataReader.GetFieldValueAsync<int>(0);
                        user.Password = await sqlDataReader.GetFieldValueAsync<string>(1);
                        user.FullName = await sqlDataReader.GetFieldValueAsync<string>(2);
                        user.UserName = await sqlDataReader.GetFieldValueAsync<string>(3);
                        user.Mobilephone = await sqlDataReader.GetFieldValueAsync<string>(4);
                        user.Email = await sqlDataReader.GetFieldValueAsync<string>(5);
                        user.Department = await sqlDataReader.GetFieldValueAsync<string>(6);
                        user.Type = await sqlDataReader.GetFieldValueAsync<char>(7);
                        user.Admin = await sqlDataReader.GetFieldValueAsync<int>(8);
                        user.Enabled = await sqlDataReader.GetFieldValueAsync<int>(9);
                        user.RoleId = await sqlDataReader.GetFieldValueAsync<int>(10);
                        user.Active = await sqlDataReader.GetFieldValueAsync<int>(11);
                        user.CreatedBy = await sqlDataReader.GetFieldValueAsync<string>(12);
                    }
                }
                return user;
            }
            catch
            {
                return null;
            }
        }
    }
}
