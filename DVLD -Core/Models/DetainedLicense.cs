using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD__Core.Models
{
    public class DetainedLicense
    {
        public int DetainID { get; set; }
        public int License_ID { get; set; }
        public DateTime DetainDate { get; set; }
        public decimal FineFees { get; set; }
        public int CreatedByUser_ID { get; set; }
        public bool isReleased { get; set; }       
        public DateTime? ReleaseDate { get; set; }
        public int? ReleaseByUser_ID { get; set; }
        public int? ReleaseApplication_ID { get; set; }
    }
}
