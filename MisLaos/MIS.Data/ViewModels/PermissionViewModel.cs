using Org.BouncyCastle.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Data.ViewModels
{
    public class PermissionViewModel
    {
        public int PermissionId { get; set; }
        public Nullable<int> fk_role_id { get; set; }
        public string FunctionCode { get; set; }
        public string FunctionName { get; set; }
        public Nullable<int> Allowed { get; set; }
    }
}
