using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD__Core.Models;
using DVLD__Data_Tier.Repositories;

namespace DVLD__Business_Tier.Services
{
    public class DetainedLicenseService
    {
        private DetainedLicenseRepository _repo;
        public DetainedLicenseService()
        {
            _repo = new DetainedLicenseRepository();
        }

        public async Task<bool> IsLicenseDetained(int licenseID)
        {
            return await _repo.IsLicenseDetained(licenseID);
        }

        public async Task<int> AddDetainLicense(DetainedLicense detainedLicense)
        {
            if (!_isDetainedValid(detainedLicense))
            {
                return -1;
            }

            if (await IsLicenseDetained(detainedLicense.License_ID))
            {
                throw new Exception("License already Detained");
            }

            return await _repo.AddNewDetainedLicenseAsync(detainedLicense);
        }

        private bool _isDetainedValid(DetainedLicense detainedLicense)
        {
            if (detainedLicense.FineFees <= 0)
            {
                throw new ArgumentException("Fine Fees must valid");
            }
            return true;
        }

        public async Task<List<DetainedLicense>> GetAllDetainedLicenses()
        {
            return await _repo.GetAllDetainedLicensesAsync();
        }
    }
}
