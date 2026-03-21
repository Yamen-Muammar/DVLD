using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD__Core.Models;
using DVLD__Data_Tier.Repositories;

namespace DVLD__Business_Tier.Services
{
    public class ApplicationsTypeService
    {
        private ApplicationsTypesRepository _repository;

        public ApplicationsTypeService()
        {
            _repository = new ApplicationsTypesRepository();
        }
        public async Task<bool> UpdateApplicationType(ApplicationType applicationType)
        {
            if (!_validateApplicationType(applicationType))
            {
                return false;
            }

            if (!await _repository.UpdateApplicationType(applicationType))
            {
                throw new Exception("Can not update!. Please try again later.");
            }
            return true;
        }
        public async Task<List<ApplicationType>> GetAllApplicationTypes()
        {
            List<ApplicationType> applicationTypes = null;
            
            applicationTypes = await _repository.GetAllApplicationTypes();

            if (applicationTypes == null)
            {
                throw new Exception("Error While Storeing Data.");
            }
         
           return applicationTypes;
        }
        private bool _validateApplicationType(ApplicationType applicationType)
        {
            if (applicationType == null)
            {
                throw new ArgumentNullException(nameof(applicationType), "Application type cannot be null.");
            }
            if (string.IsNullOrWhiteSpace(applicationType.ApplicationTypeTitle))
            {
                throw new ArgumentException("Application type title cannot be empty.", nameof(applicationType));
            }
            if (applicationType.ApplicationTypeFees < 0)
            {
                throw new ArgumentException("Application type fees cannot be negative.", nameof(applicationType));
            }
            return true;
        }
        public async Task<ApplicationType> GetApplicationTypeByID(int applicationTypeID)
        {
            ApplicationType applicationType = null;
            
            applicationType = await _repository.GetApplicationTypeByID(applicationTypeID);

            if (applicationType == null)
            {
                throw new Exception("Application type not found !");
            }

            return applicationType;
        } 
    }
}

