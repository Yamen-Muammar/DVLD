using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD__Core.View_Models
{
    public class clsLicenseHistoryView
    {
        public int LicenseID { get; set; }
        public int Application_ID { get; set; }
        public string ClassName { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool isActive { get; set; }
    }
}
