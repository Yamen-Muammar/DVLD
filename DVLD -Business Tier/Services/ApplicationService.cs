using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD__Core.Models;
using DVLD__Core.View_Models;
using DVLD__Data_Tier.Repositories;

namespace DVLD__Business_Tier.Services
{
    public class ApplicationService
    {
        public enum enStatus
        {
            New = 1 , Completed = 2, Canceled = 3
        }
        public static Application GetApplicationByID(int applicationID)
        {
            Application application = null;

            application = ApplicationRepository.GetApplicationByID(applicationID);

            return application;
        }     
        public static bool SaveApplication(Application application)
        {
            int newId = -1;

            if (!_ValidApplication(application))
            {
                throw new Exception("Application Information not Valid");
            }

            // --- ROUTING LOGIC ---
            // If the ID is -1, it means this object hasn't been saved to the database yet.
            if (application.ApplicationID == -1)
            {
                try
                {
                    newId = ApplicationRepository.AddNewApplication(application);

                    if (newId == -1)
                    {
                        return false;
                        throw new Exception("Can't Create New Application");
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("**** Error SaveApplication >>"+ex + "****");
                    throw;
                }
                finally
                {
                    application.ApplicationID = newId; 
                }
                return true;
            }

            // If the ID is greater than 0, it already exists. We are Updating it!
            if (application.ApplicationID > 0)
            {
                
                try
                {
                    if (!ApplicationRepository.UpdateApplication(application))
                    {
                        throw new Exception("Error While Updateing");
                    }
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
             
            }
            return false;
        }
        private static bool _ValidApplication(Application application)
        {
            if (application.PaidFees < 0)
            {

                return false;
            }

            if (application.ApplicationDate > DateTime.Now)
            {

                return false;
            }

            return true;
        }

        private static string _getSelectedStatus(enStatus status)
        {
            switch (status)
            {
                case enStatus.New:
                    return "New";
                case enStatus.Completed:
                    return "Completed";
                case enStatus.Canceled:
                    return "Canceled";
                default:
                    throw new ArgumentException();
            }
        }



        // For Local driving license applications only!!
        public static bool SaveLocalDrivingLicenseApplication(Application application,int licenseClassID)
        {
            int newId = -1;

            if (!_ValidApplication(application, licenseClassID))
            {
                return false;
            }

            if (application.ApplicationID == -1)
            {
                
                newId = ApplicationRepository.AddNewLocalDrivingLicesneApplication(application, licenseClassID);

                if (newId == -1)
                {                        
                    throw new Exception("Error While Createing New Application");
                }
                
                application.ApplicationID = newId;
                
                return true;
            }

            return false;
        }
        public static bool UpdateLDLApplicationStatus(int localDrivingLicenseApplicationID , enStatus status)
        {
            DVLD__Core.Models.Application selectedApplication = _getApplicationOnLDLA_ID(localDrivingLicenseApplicationID);

            if (selectedApplication.ApplicationStatus == _getSelectedStatus(status))
            {
                throw new Exception($"Status Already {_getSelectedStatus(status)}");
            }

            if (selectedApplication == null)
            {
                throw new Exception("Application Not Found!");
            }

            selectedApplication.ApplicationStatus = _getSelectedStatus(status);

            ApplicationRepository.UpdateApplication(selectedApplication);
            return true;
        }
        private static DVLD__Core.Models.Application _getApplicationOnLDLA_ID(int localDrivingLicenseApplicationID)
        {
            DVLD__Core.Models.Application application = ApplicationRepository.GetApplicationByLDL_ID(localDrivingLicenseApplicationID);
            return application;
        }      
        public static List<clsLocalDrivingLicesnseApplicationView> GetAllLDLApplications()
        {       
              return ApplicationRepository.GetAll_L_D_L_Applications();          
        }
        private static bool _ValidApplication(Application application, int licenseClassID)
        {
            if (!_ValidApplication(application))
            {
                return false;
            }

            int FounedID = ApplicationRepository.doesHasAnActiveLocalDrivingLicenseApplication(application.Person_ID, licenseClassID);
            if (FounedID != -1)
            {
                throw new Exception($"User Already Has an Active Application , Id = {FounedID}");
            }

            return true;
        }
    }
}
