using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD__Core.Models
{
    public class User
    {
        public int UserID { get; set; }
        public int Person_ID { get; set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public bool isActive { get; set; }
    }
}
