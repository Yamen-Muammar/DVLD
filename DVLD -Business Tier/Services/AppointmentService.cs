using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
        private ApplicationsTypeService _applicationsTypeService;
        private PersonService _personService;
        public AppointmentService()
        {
            _repository = new AppointmentRepository();
            _applicationService = new ApplicationService();
            _personService = new PersonService();
            _applicationsTypeService = new ApplicationsTypeService();
        }

        //add
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

        public async Task<int> AddRetakeTestAppointmentAsync( TestAppointment testAppointment)
        {
            if (!_validateAppointment(testAppointment))
            {
                return -1;
            }

            int doesHasAppointment = await DoesApplicantHasAnActiveAppointmentAsync(testAppointment.LocalDrivingLicenseApplication_ID, testAppointment.TestType_ID);
            if (doesHasAppointment != -1)
            {
                throw new Exception($"Applicant already has an appointment : {doesHasAppointment}");
            }


            DVLD__Core.Models.Application applicationForGetPerson_ID = await _applicationService.GetApplicationOnLDLA_ID(testAppointment.LocalDrivingLicenseApplication_ID);
            if (applicationForGetPerson_ID == null)
            {
                throw new Exception("Error While Getting application Data");
            }


            ApplicationType appType = await _applicationsTypeService.GetApplicationTypeByID(7); // 7 = retake test application type ID in database
            if (appType == null)
            {
                throw new Exception("Error While Getting application Type Data");
            }
            

            return await _repository.AddNewRetakeTestAppoitmentAsync(testAppointment, applicationForGetPerson_ID.Person_ID,appType.ApplicationTypeFees);
        }


        //get

        public async Task<int> DoesApplicantHasAnActiveAppointmentAsync(int LDLAppID, int testType)
        {
            return await _repository.DoesApplicationHasActiveAppointmentAsync(LDLAppID, testType);
        }
        public async Task<List<clsAppointmentsView>> GetAllAppointmentsAsync(int lDLAppID, int testType)
        {
            return await _repository.GetAllAppointmentsAsync(lDLAppID, testType);
        }
        public async Task<TestAppointment> GetAppointmentAsync(int testAppointmentID)
        {
            TestAppointment appointment = await _repository.GetAppointmentByIDAsync(testAppointmentID);

            if (appointment == null)
            {
                throw new ArgumentNullException("Appointment Not Found");
            }

            return appointment;

        }

        //update
        public async Task<bool> UpdateAppointmentDateTimeAsync(int testAppointmentID, DateTime date)
        {
            if (!_validateAppointment(new TestAppointment { PaidFees = 1, AppointmentDate = date, TestAppointmentID = testAppointmentID }))
            {
                return false;
            }
            return await _repository.UpdateAppointmentDateAsync(testAppointmentID, date);
        }

        //delete 

        //helper functions
        private bool _validateAppointment(TestAppointment appointment)
        {
            if (appointment == null)
            {
                throw new ArgumentNullException("appointment has no data");
            }
            if (appointment.TestAppointmentID == -1)
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


    }
}
