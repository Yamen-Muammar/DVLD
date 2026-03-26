using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD__Core.View_Models
{
    public class clsAppointmentsView
    {
        public int TestAppointmentID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal ClassFees { get; set; }
        public bool isLocked { get; set; }      
    }
}
