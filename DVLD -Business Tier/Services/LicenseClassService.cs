using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD__Core.Models;
using DVLD__Data_Tier.Repositories;

namespace DVLD__Business_Tier.Services
{
    public class LicenseClassService
    {
        public static List<LicenseClass> GetAlllicenseClasses()
        {
			var list = new List<LicenseClass>();
			try
			{
				list = LicenseClassRepository.GetAllLicenseClasses();
			}
			catch (Exception)
			{

				throw new Exception("Error while Get All license classes");
			}
			return list;
        }


    }
}
