using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD__Core.Models
{
    public class License
    {
        public int LicenseID { get; set; }
        public int Driver_ID { get; set; }
        public DateTime IssueDate { get; set; }        
        public string IssueReasen { get; set; }
        public string Note { get; set; }
        public bool isActive { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int LicenseClass_ID { get; set; }
        public int CreateByUser_ID { get; set; }
        public int Application_ID { get; set; }
    }
}
