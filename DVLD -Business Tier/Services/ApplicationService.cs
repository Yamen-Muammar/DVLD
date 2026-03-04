using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD__Core.Models;
using DVLD__Data_Tier.Repositories;

namespace DVLD__Business_Tier.Services
{
    public class ApplicationService
    {
        public static bool SaveApplication(Application application)
        {
            int newId = -1;

            if (!_ValidApplication(application))
            {
                return false;
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
        public static bool SaveLocalDrivingLicenseApplication(Application application,int licenseClassID)
        {
            int newId = -1;

            if (!_ValidApplication(application, licenseClassID))
            {
                return false;
            }

            // --- ROUTING LOGIC ---
            // If the ID is -1, it means this object hasn't been saved to the database yet.
            if (application.ApplicationID == -1)
            {
                try
                {
                    newId = ApplicationRepository.AddNewLocalDrivingLicesneApplication(application, licenseClassID);

                    if (newId == -1)
                    {                        
                        throw new Exception("Error While Createing New Application");
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("**** Error SaveApplication >>" + ex + "****");
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

        public static List<Application> GetAllApplications()
        {
           
            return ApplicationRepository.GetAllApplications();
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

            //int FounedID = ApplicationRepository.doesHasAnActiveLocalDrivingLicenseApplication(application.Person_ID);
            //if (FounedID != -1)
            //{
            //    throw new Exception($"User Already Has an Active Application , Id = {FounedID}");                
            //} 
            
            return true;
        }
        private static bool _ValidApplication(Application application, int licenseClassID)
        {
            if (application.PaidFees < 0)
            {

                return false;
            }

            if (application.ApplicationDate > DateTime.Now)
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
