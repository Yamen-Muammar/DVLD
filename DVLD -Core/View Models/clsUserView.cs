using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD__Core.View_Models
{
    public class clsUserView
    {
        public int UserID { get; set; }
        public string UserName { get; set; }        
        public string FullName { get; set; }
        public bool isActive { get; set; }
    }
}
