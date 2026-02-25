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
        public static bool UpdateApplicationType(ApplicationType applicationType)
        {
            if (!_validateApplicationType(applicationType))
            {
                return false;
            }

            if (!ApplicationsTypesRepository.UpdateApplicationType(applicationType))
            {
                throw new Exception("An error occurred while updating the application type. Please try again later.");
            }
            return true;
        }
        public static List<ApplicationType> GetAllApplicationTypes()
        {
            List<ApplicationType> applicationTypes;
            try
            {
                applicationTypes = ApplicationsTypesRepository.GetAllApplicationTypes();
                if (applicationTypes == null || applicationTypes.Count == 0)
                {
                    throw new Exception("No application types found.");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex+" ,Please try again later.");
            }
                      
           return applicationTypes;
        }
        private static bool _validateApplicationType(ApplicationType applicationType)
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

        public static ApplicationType GetApplicationTypeByID(int applicationTypeID)
        {
            ApplicationType applicationType = null;
            try
            {
                applicationType = ApplicationsTypesRepository.GetApplicationTypeByID(applicationTypeID);
                if (applicationType == null)
                {
                    throw new Exception("Application type not found.");
                }

            }
            catch (Exception ex)
            {
                throw;
            }

            return applicationType;
        } 
    }
}

