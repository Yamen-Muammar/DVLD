using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD__Core.Models;
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

            int doesHasAppointment = await _doesApplicantHasAnActiveAppointmentAsync(testAppointment);

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

            if(appointment.PaidFees < 0)
            {
                throw new ArgumentException("inValid PaidFess");
            }

            return true;
        }

        private async Task<int> _doesApplicantHasAnActiveAppointmentAsync(TestAppointment appointment)
        {
            return await _repository.DoesApplicationHasActiveAppointmentAsync(appointment);
        }


    }
}
