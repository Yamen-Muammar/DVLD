using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD__Core.View_Models
{
    public class clsLocalDrivingLicesnseApplicationView
    {
        public int LDLApplicationID { get; set; }
        public string DrivingClassTitle { get; set; }
        public string NationalNO { get; set; }
        public string FullName { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int PassedTests { get; set; } = 0;
        public string Status { get; set; }
    }
}
