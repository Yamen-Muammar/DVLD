using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD__Core.Models
{
    public class TestAppointment
    {
        public int TestAppointmentID { get; set; }
        public int TestType_ID { get; set; }
        public int LocalDrivingLicenseApplication_ID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUser_ID { get; set; }
        public bool isLocked { get; set; }        
        public int? RetakeTestApplication_ID { get; set; }
    }
}
