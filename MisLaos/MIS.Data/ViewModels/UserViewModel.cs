using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Data.ViewModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Mobilephone { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string Type { get; set; }
        public Nullable<int> Admin { get; set; }
        public Nullable<int> Enabled { get; set; }
        public Nullable<int> RoleId { get; set; }
        public Nullable<int> Active { get; set; }
        public string CreatedBy { get; set; }

        public UserViewModel(DataRow row)
        {
            this.UserId = (int)row["UserId"];
            this.Password = row["Password"].ToString();
            this.FullName = row["FullName"].ToString();
            this.UserName = row["UserName"].ToString();
            this.Mobilephone = row["Mobilephone"].ToString();
            this.Email = row["Email"].ToString();
            this.Department = row["Department"].ToString();
            this.Type = (string)row["Type"];
            this.Admin = int.Parse(row["Admin"].ToString());
            this.Enabled = (int)row["Enabled"];
            this.RoleId = (int)row["fk_role_id"];
            this.Active = (int)row["Active"];
            this.CreatedBy = row["CreatedBy"].ToString();
        }

        public UserViewModel()
        {
        }
    }
    
}
