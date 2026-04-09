using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD__Core.Models;
using DVLD__Data_Tier.Repositories;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD__Business_Tier.Services
{
    public class TestService
    {
        private TestRepository _testRepo;
        private ApplicationRepository _applicationRepository;
        private AppointmentRepository _appointmentRepository;
        public TestService()
        {
            _testRepo = new TestRepository();
            _appointmentRepository = new AppointmentRepository();
        }

        public async Task<TestRepository> GetTestByIDAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<int> PassedTestCount(int ldlAppID)
        {
            
            int returnedPassedCount = await _testRepo.GetPassedTestsAsync(ldlAppID);

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
        public async Task<bool> isAppointmentTakedTestAsync(int appointmentID)
        {
            return await _testRepo.isAppointmentTakedTestAsync(appointmentID);
        }
        private async Task<bool> _ValidateTestAsync(Test test)
        {
           
            if (test.TestAppointment_ID <= 0)
            {
                return false;
            }

            if (test.CreatedByUser_ID <= 0)
            {
                return false;
            }

            if (await _testRepo.isAppointmentTakedTestAsync(test.TestAppointment_ID))
            {
                throw new Exception("This appointment already has a test result.");
            }

            return true;
        }

        public async Task<Test> GetTestByIDAsync(int testID)
        {
            return await _testRepo.GetTestByIDAsync(testID);
        }

        // ==========================================
        // THE TRAFFIC COP (Public Router)
        // ==========================================
        public async Task<bool> SaveTestAsync(Test test)
        {
            // 1. Validate first
            if (!await _ValidateTestAsync(test))
            {
                throw new Exception("Test Data is not valid.");
            }

            // 2. Direct Traffic
            if (test.TestID == -1)
            {
                return await _AddNewTestAsync(test);
            }
            else if (test.TestID > 0)
            {
                return await _UpdateTestAsync(test);
            }

            return false;
        }

        // ==========================================
        // PRIVATE HELPERS (Single Responsibility)
        // ==========================================
        private async Task<bool> _AddNewTestAsync(Test test)
        {
            int newId = await _testRepo.AddNewTestAsync(test);

            if (newId != -1)
            {
                //update lock status for appointment
                // TODO : Create A Transactional sql for this  
                bool isLockedStatusUpdated = await _appointmentRepository.UpdateAppointmentLockStatusAsync(test.TestAppointment_ID);
                if (! isLockedStatusUpdated)
                {
                    return false;
                }

                // We update the ID in memory here so the UI gets it back
                test.TestID = newId;
                return true;
            }

            return false;
        }

        private async Task<bool> _UpdateTestAsync(Test test)
        {
            return await _testRepo.UpdateTestAsync(test);
        }
    }
}
