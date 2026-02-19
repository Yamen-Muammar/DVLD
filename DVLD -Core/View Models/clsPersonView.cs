using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD__Core.View_Models
{
    public class clsPersonView
    {
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string MiddelName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string NationalNO { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CountryName { get; set; }
        public string Address { get; set; }        
        public DateTime DateOfBirth { get; set; }
    }
}
