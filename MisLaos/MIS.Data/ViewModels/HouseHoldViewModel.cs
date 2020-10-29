using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Data.ViewModels
{
    public class HouseHoldViewModel
    {
        public float Id { get; set; }

        public string Village { get; set; }

        public string Unit { get; set; }

        public string HHLevel { get; set; }

        public string HeadOfHHName { get; set; }

        public int TotalHHMembers { get; set; }

        public string NumberPlots { get; set; }

        public int NumberPregnant { get; set; }

        public int NumberChild { get; set; }
    }
}
