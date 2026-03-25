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
        private ApplicationRepository _appRepo;
        public enum enStatus
        {
            New = 1 , Completed = 2, Canceled = 3
        }

        public ApplicationService()
        {
            _appRepo = new ApplicationRepository();
        }
        public async Task<Application> GetApplicationByID(int applicationID)
        {
            Application application = null;

            application = await _appRepo.GetApplicationByID(applicationID);

            return application;
        }     
        public async Task<bool> SaveApplication(Application application)
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
                newId = await _appRepo.AddNewApplication(application);
               
                if (newId == -1)
                {
                    return false;
                    throw new Exception("Can't Create New Application");
                }
                else
                {
                    application.ApplicationID = newId;
                }
                return true;
            }

            // If the ID is greater than 0, it already exists. We are Updating it!
            if (application.ApplicationID > 0)
            {

                    if (!await _appRepo.UpdateApplication(application))
                    {
                        throw new Exception("Error While Updateing");
                    }
                    return true;
            }
            return false;
        }
        private  bool _ValidApplication(Application application)
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
        private  string _getSelectedStatus(enStatus status)
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
        public async Task<DVLD__Core.Models.Application> GetApplicationOnLDLA_ID(int localDrivingLicenseApplicationID)
        {
            DVLD__Core.Models.Application application = await _appRepo.GetApplicationByLDL_ID(localDrivingLicenseApplicationID);
            return application;
        }


        // For Local driving license applications only!!
        public async Task<bool> SaveLocalDrivingLicenseApplication(Application application,int licenseClassID)
        {
            int newId = -1;

            if (!await _ValidApplication(application, licenseClassID))
            {
                return false;
            }

            if (application.ApplicationID == -1)
            {
                
                newId = await _appRepo.AddNewLocalDrivingLicesneApplicationAsync(application, licenseClassID);

                if (newId == -1)
                {                        
                    throw new Exception("Error While Createing New Application");
                }
                
                application.ApplicationID = newId;
                
                return true;
            }

            return false;
        }
        public async Task<bool> UpdateLDLApplicationStatus(int localDrivingLicenseApplicationID , enStatus status)
        {
            DVLD__Core.Models.Application selectedApplication = await GetApplicationOnLDLA_ID(localDrivingLicenseApplicationID);

            if (selectedApplication == null)
            {
                throw new Exception("Application Not Found!");
            }

            if (selectedApplication.ApplicationStatus == _getSelectedStatus(status))
            {
                throw new Exception($"Status Already {_getSelectedStatus(status)}");
            }

            selectedApplication.ApplicationStatus = _getSelectedStatus(status);
            selectedApplication.LastStatusDate = DateTime.Now;

            await _appRepo.UpdateApplication(selectedApplication);

            return true;
        }     
        public async  Task<List<clsLocalDrivingLicesnseApplicationView>> GetAllLDLApplications()
        {       
              return await _appRepo.GetAll_L_D_L_Applications();          
        }
        private async Task<bool> _ValidApplication(Application application, int licenseClassID)
        {
            if (!_ValidApplication(application))
            {
                return false;
            }

            int FounedID = await _appRepo.doesHasAnActiveLocalDrivingLicenseApplication(application.Person_ID, licenseClassID);
            if (FounedID != -1)
            {
                throw new Exception($"User Already Has an Active Application , Id = {FounedID}");
            }

            return true;
        }
        public async Task<bool> DeleteLDLApplicationAsync(int lDLappID) 
        {
            if (lDLappID < 0)
            {
                throw new ArgumentException("Value Error");               
            }

            await _appRepo.DeleteLDLApplication(lDLappID);
            return true;
                
        } 
    }
}
