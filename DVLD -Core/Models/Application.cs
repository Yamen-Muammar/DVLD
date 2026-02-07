using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD__Core.Models
{
    public class Application
    {
        public int ApplicationID { get; set; }
        public int CreatedByUser_ID { get; set; }
        public int ApplicationType_ID { get; set; }
        public int Person_ID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public decimal PaidFees { get; set; }        
        public DateTime? LastStatusDate { get; set; }
        public int ApplicationStatus { get; set; }
    }
}
