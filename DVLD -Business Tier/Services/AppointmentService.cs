using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD__Core.Models;
using DVLD__Core.View_Models;
using DVLD__Data_Tier.Repositories;

namespace DVLD__Business_Tier.Services
{
    public class AppointmentService
    {
        private AppointmentRepository _repository;
        private ApplicationService _applicationService;
        public AppointmentService()
        {
            _repository = new AppointmentRepository();
            _applicationService = new ApplicationService();
        }

        public async Task<int> AddTestAppointmentAsync(TestAppointment testAppointment)
        {
            if (!_validateAppointment(testAppointment))
            {
                return -1;
            }

            int doesHasAppointment = await DoesApplicantHasAnActiveAppointmentAsync(testAppointment.LocalDrivingLicenseApplication_ID,testAppointment.TestType_ID);

            if (doesHasAppointment != -1)
            {
                throw new Exception($"Applicant already has an appointment : {doesHasAppointment}");
            }


            return await _repository.AddNewTestAppoitmentAsync(testAppointment);
        }

        private bool _validateAppointment(TestAppointment appointment)
        {
            if (appointment == null)
            {
                throw new ArgumentNullException("appointment has no data");
            }
            if (appointment.TestAppointmentID <= 0)
            {
                throw new ArgumentException("invalid ID");
            }
            if (DateTime.Compare(appointment.AppointmentDate, DateTime.Now) <= 0)
            {
                throw new Exception("Select Futuer Date");
            }

            if (appointment.PaidFees < 0)
            {
                throw new ArgumentException("inValid PaidFess");
            }

            return true;
        }

        public async Task<int> DoesApplicantHasAnActiveAppointmentAsync(int LDLAppID, int testType)
        {
            return await _repository.DoesApplicationHasActiveAppointmentAsync(LDLAppID,testType);
        }

        public async Task<List<clsAppointmentsView>> GetAllAppointmentsAsync(int lDLAppID , int testType)
        {
            return await _repository.GetAllAppointmentsAsync(lDLAppID, testType);
        }

        public async Task<bool> UpdateAppointmentDateTimeAsync(int testAppointmentID, DateTime date)
        {
            if (!_validateAppointment(new TestAppointment { PaidFees = 1 , AppointmentDate = date , TestAppointmentID = testAppointmentID}))
            {
                return false;
            }
            return await _repository.UpdateAppointmentDateAsync(testAppointmentID, date);
        }

        public async Task<TestAppointment> GetAppointmentAsync(int testAppointmentID) { 
            TestAppointment appointment = await _repository.GetAppointmentByIDAsync(testAppointmentID);

            if (appointment == null)
            {
                throw new ArgumentNullException("Appointment Not Found");
            }

            return appointment;
            
        }
    }
}
