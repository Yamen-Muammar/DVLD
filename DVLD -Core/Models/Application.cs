using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD__Core.Models
{
    public class Application
    {
        private string _applicationStatus;
        public int ApplicationID { get; set; }
        public int CreatedByUser_ID { get; set; }
        public int ApplicationType_ID { get; set; }
        public int Person_ID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public decimal PaidFees { get; set; }        
        public DateTime? LastStatusDate { get; set; }
        public string ApplicationStatus 
        {
            get => _applicationStatus;
            set
            {
                if (!_isStatusValid(value))
                {
                    throw new ArgumentException("Status Must be one of these (New , Canceled,Completed)");
                }
                _applicationStatus = value;
            } 
        }
        

        private bool _isStatusValid(string enteredstatus)
        {
            if (string.IsNullOrEmpty(enteredstatus)) { return false; }

            if(enteredstatus == "New")
            {
                return true; 
            }

            if (enteredstatus == "Canceled")
            {
                return true;
            }

            if (enteredstatus == "Completed")
            {
                return true;
            }
            return false;

        }
    }
}
