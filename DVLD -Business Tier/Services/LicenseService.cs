using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD__Core.Models;
using DVLD__Core.View_Models;
using DVLD__Data_Tier.Repositories;

namespace DVLD__Business_Tier.Services
{
    public class LicenseService
    {
        LicenseRepository _licenseRepo;
        public LicenseService()
        {
            _licenseRepo = new LicenseRepository();
        }

        // add
        public async Task<int> AddLicenseAsync(DVLD__Core.Models.License license)
        {
            if (!_isValid(license))
            {
                return -1;
            }

            if (await DoesApplicationHasLicense((int)license.LocalDrivingLicenseApplication_ID))
            {
                throw new Exception("Applicant Already Has A License");
            }


            ApplicationRepository applicationRepo = new ApplicationRepository();

            DVLD__Core.Models.Application applicationData = await applicationRepo.GetApplicationByLDL_ID((int)license.LocalDrivingLicenseApplication_ID);
            if (applicationData == null)
            {
                throw new Exception("Error: No application found for the given LocalDrivingLicenseApplication_ID");
            }

            
            license.ExpirationDate =await _getExpirationDate(license.IssueDate,(int)license.LocalDrivingLicenseApplication_ID);

            return await _insertNewLicense(license, applicationData);
        }

        public async Task<int> AddInternationalLicenseAsync(DVLD__Core.Models.Application application, InternationalLicense internationalLicense)
        {
            if (!_isValid(application,internationalLicense))
            {
                return -1;
            }


            if (await _doesApplicationHasActiveInternationalLicense(internationalLicense.LocalLicense_ID))
            {
                throw new Exception("Applicant Already Has an International License");
            }

            if (!await _doesApplicationHasActiveLicense(internationalLicense.LocalLicense_ID))
            {
                throw new Exception("Applicant Local License IS Inactive");
            }


            return await _licenseRepo.InsertNewInternationalLicense(application,internationalLicense);
        }

        public async Task<int> RenewLicenseAsync(DVLD__Core.Models.Application application, DVLD__Core.Models.License PreviousLicense , DVLD__Core.Models.License newlicense)
        {
            int founderLicenseID = await _licenseRepo.ActiveLicense(newlicense.Driver_ID, (int)newlicense.LicenseClass_ID);
            if (founderLicenseID != -1)
            {
                 throw new Exception($"An active license already exists for this driver\nLicense ID : {founderLicenseID}.");
            }


            DVLD__Core.Models.License existingLicense = await _licenseRepo.GetLicenseByIDAsync(PreviousLicense.LicenseID);
            if (existingLicense == null)
            {
                throw new Exception("License not found.");
            }
            if (!_isLicenseExpire(existingLicense.ExpirationDate))
            {
                throw new Exception("Cannot renew an active license.");
            }


            return await _licenseRepo.RenewLocalLicense(existingLicense.LicenseID, application, newlicense);
        }

        public async Task<int> ReplaceLicenseAsync(DVLD__Core.Models.Application application, DVLD__Core.Models.License PreviousLicense, DVLD__Core.Models.License newlicense)
        {
           
            DVLD__Core.Models.License existingLicense = await _licenseRepo.GetLicenseByIDAsync(PreviousLicense.LicenseID);
            if (existingLicense == null)
            {
                throw new Exception("License not found.");
            }
            if (_isLicenseExpire(existingLicense.ExpirationDate))
            {
                throw new Exception("Cannot Replace an Expire license.");
            }


            return await _licenseRepo.ReplaceLocalLicense(existingLicense.LicenseID, application, newlicense);
        }

        // get 
        public async Task<DVLD__Core.Models.License > GetLicenseByLDLAppID(int ldlAppid)
        {
            return await _licenseRepo.GetLicenseByLocalDrivingLicenseAppIDAsync(ldlAppid);
        }

        public async Task<DVLD__Core.Models.License> GetLicenseByID(int id)
        {
            if (id <= 0 )
            {
                throw new ArgumentException("Invalid License ID.");
            }
            return await _licenseRepo.GetLicenseByIDAsync(id);
        }

        public async Task<bool> DoesApplicationHasLicense(int ldlAppID)
        {
            DVLD__Core.Models.License license = await _licenseRepo.GetLicenseByLocalDrivingLicenseAppIDAsync(ldlAppID);
            return license != null;
        }
        private async Task<bool>_doesApplicationHasActiveLicense(int licID)
        {
            DVLD__Core.Models.License license = await _licenseRepo.GetLicenseByIDAsync(licID);
            return license != null && !_isLicenseExpire(license.ExpirationDate);
        }

        public async Task<bool> DoesApplicationHasInternationalLicense(int LocalLicenseID)
        {
            DVLD__Core.Models.InternationalLicense license = await _licenseRepo.GetInternationalLicenseByLocalLicenseIDAsync(LocalLicenseID);
            return (license != null);
        }

        private async Task<bool> _doesApplicationHasActiveInternationalLicense(int LocalLicenseID)
        {
            DVLD__Core.Models.InternationalLicense license = await _licenseRepo.GetInternationalLicenseByLocalLicenseIDAsync(LocalLicenseID);
            return (license != null) && !_isLicenseExpire(license.ExpirationDate);
        }

        public async Task<List<clsLicenseHistoryView>> GetAllLocalLicensesForPerson(int personID)
        {
            return await _licenseRepo.GetAllLocalLicensesForPersonAsync(personID);
        }

        public async Task<List<clsInternationalLicenseHistory>> GetAllInternationalLicenseForPerson(int personID)
        {
            return await _licenseRepo.GetAllInternationalLicensesForPersonAsync(personID);
        }

        public async Task<List<clsInternationalLicenseHistory>> GetAllIntarnationalLicense()
        {
            return await _licenseRepo.GetAllInternationalLicensesAsync();
        }

        //helper methods

        private bool _isLicenseExpire(DateTime expirationDate)
        {
            return (DateTime.Compare(DateTime.Now, expirationDate) >= 0);
        }
        private async Task<int> _insertNewLicense(DVLD__Core.Models.License license , DVLD__Core.Models.Application applicationData)
        {
            DriverRepository driverRepository = new DriverRepository();
            Driver driver = await driverRepository.GetDriverByPersonIDAsync(applicationData.Person_ID);
            if (driver == null)
            {
                return await _licenseRepo.InsertNewLicenseForFirstTimeAsync(applicationData, license);
            }
            else
            {
                license.Driver_ID = driver.DriverID;
                return await _licenseRepo.InsertNewLicenseOnlyAsync(applicationData, license);
            }
        }
        private bool _isValid(DVLD__Core.Models.License license)
        {
            if (license == null)
            {
                throw new ArgumentNullException(nameof(license));
            }

            if (license.LocalDrivingLicenseApplication_ID <= 0)
            {
                throw new ArgumentException("Invalid LocalDrivingLicenseApplication_ID.");
            }
            return true;
        }
        private bool _isValid(DVLD__Core.Models.Application application, DVLD__Core.Models.InternationalLicense license)
        {
            if (license == null)
            {
                throw new ArgumentNullException(nameof(license));
            }

            if (application == null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            if (license.LocalLicense_ID <= 0)
            {
                throw new ArgumentException("Invalid LocalLicense_ID.");
            }

            if (application.PaidFees <= 0)
            {
                throw new ArgumentException("Invalid Application Paid Fees.");
            }

            if (application.ApplicationDate.Day != DateTime.Today.Day)
            {
                throw new ArgumentException("Invalid Application Date");
            }

            return true;
        }
        private async Task<DateTime> _getExpirationDate(DateTime issueDate,int ldlAppID)
        {
            LicenseClassRepository _licenseClassRepo = new LicenseClassRepository();
            LicenseClass licenseClass = await _licenseClassRepo.GetLicenseClassByLDLApp_IdAsync(ldlAppID);
            if (licenseClass == null)
            {
                throw new Exception("Error: No License Class found for the given LocalDrivingLicenseApplication_ID");
            }
            
            int addYears = licenseClass.DefaultValidityLength;
            return issueDate.AddYears(addYears);
        }
    }
}
