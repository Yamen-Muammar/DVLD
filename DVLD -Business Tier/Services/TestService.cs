using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD__Core.Models;
using DVLD__Data_Tier.Repositories;

namespace DVLD__Business_Tier.Services
{
    public class TestService
    {
        private TestRepository _testRepo;
        private ApplicationRepository _applicationRepository;
        public TestService()
        {
            _testRepo = new TestRepository();
        }

        public async Task<TestRepository> GetTestByIDAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> PassedTestCount(string personNationalNo)
        {
            
            int returnedPassedCount = await _testRepo.GetPassedTestsAsync(personNationalNo);

            if (returnedPassedCount > 4 || returnedPassedCount < 0)
            {
                throw new Exception("invalid returned Value");
            }
            return returnedPassedCount;
        }

        public async Task<bool> isAppointmentHasFailTestResultAsync(int appointmentId)
        {
            return await _testRepo.isAppointmentHasFailTestResultAsync(appointmentId);
        }


    }
}
