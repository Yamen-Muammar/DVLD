using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD__Business_Tier.Services;
using DVLD__Core;
using DVLD__Core.Models;

namespace DVLD__Presentation_Tier.Controls.SechduleTestsControls
{
    public partial class ctrlSechduleVisionTest : UserControl
    {
        public delegate void ReturnAddedRetakeApplicationID(int addedID);
        public event ReturnAddedRetakeApplicationID OnAddedRetakeApplicationID;
        protected void TriggerEvent(int addedID)
        {
            OnAddedRetakeApplicationID?.Invoke(addedID);
        }

        private int _lDLAppID;
        private const int _testTypeID = 1; // test type id in database
        private string _applicantFullName;
        private string _licenseClassName;
        private TestType _testType;

        private int _appointmentID; // to get appointment info for edite.
        private TestAppointment _testAppointment; // for Edite mode.

        private TestTypeService _testTypeService;
        private ApplicationService _appService;
        private AppointmentService _appointmentService;
        public enum enMode
        {
            New = 1, Edite = 2 , Retake = 3
        }
        private enMode _mode;
        public ctrlSechduleVisionTest()
        {
            InitializeComponent();
        }
        public ctrlSechduleVisionTest(int? appointmentID,enMode mode,string applicantFullName,int ldlAppID,string licenseClassName)
        {
            InitializeComponent();
            _mode = mode;
            _testTypeService = new TestTypeService();
            _appointmentService = new AppointmentService();
            _applicantFullName = applicantFullName;
            _lDLAppID = ldlAppID;      
            _appointmentID = appointmentID == null ? -1 : (int)appointmentID;
            _licenseClassName= licenseClassName;
        }

        private async void ctrlSechduleVisionTest_Load(object sender, EventArgs e)
        {
            UILoad(_mode);

            if (_mode == enMode.New)
            {
                await _loadDataInCtrl();
                return;
            }

            if (_mode == enMode.Edite)
            {
                if (_appointmentID == -1)
                {
                    MessageBox.Show("No Appointment Data Passed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _testAppointment = await _getAppointmentByID(_appointmentID);
                if (_testAppointment == null)
                {
                    return;
                }

                await _loadDataInCtrl(_testAppointment);
                return;
            }

            if (_mode == enMode.Retake)
            {
                await _loadDataInCtrl();
                this.ctrlSechduleRetakeTest1.UpdateTestTypeFees(_testType.TestTypeFees);
                return;
            }
        }
        private async void btnSaveTestAppointment_Click(object sender, EventArgs e)
        {
            if (_mode == enMode.New)
            {
                await _addNewAppointment();
                return;
            }

            if (_mode == enMode.Edite)
            {
                await _EditeAppoinment();
                return;
            }

            if (_mode == enMode.Retake)
            {
                await _addRetakeAppointment();
               return;
            }
        }

        // new mode functions
        private async Task _addNewAppointment()
        {
            TestAppointment appointment = CreateTestAppointmentObj();
            if (appointment == null) { return; }
            try
            {
                appointment.TestAppointmentID = await _appointmentService.AddTestAppointmentAsync(appointment);
                if (appointment.TestAppointmentID == -1)
                {
                    MessageBox.Show("Can not Create Appointment", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Appointment Added Successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // edite mode functions
        private async Task _EditeAppoinment()
        {
            DateTime UpdatedDate = dateTimePicker.Value;
            try
            {
                bool isUpdatedDone = await _appointmentService.UpdateAppointmentDateTimeAsync(_appointmentID, UpdatedDate);
                if (!isUpdatedDone)
                {
                    MessageBox.Show("Can not Update Appointment Date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Appointment Updated Successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // retake mode functions
        private async Task _addRetakeAppointment()
        {
            try
            {
                TestAppointment testAppointment = CreateTestAppointmentObj();
                if (testAppointment == null) { return; }
                testAppointment.TestAppointmentID = await _appointmentService.AddRetakeTestAppointmentAsync(testAppointment);

                if (testAppointment.TestAppointmentID == -1)
                {
                    MessageBox.Show("Can not Create Appointment", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    TriggerEvent((int)testAppointment.RetakeTestApplication_ID);
                    MessageBox.Show("Retake Appointment Added Successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // helper functions
        private void UILoad(enMode mode)
        {
            dateTimePicker.MinDate = DateTime.Today;
            if (_mode == enMode.New)
            {
                ctrlSechduleRetakeTest1.Enabled = false;
                ctrlSechduleRetakeTest1.Visible = false;
                return;
            }

            if (_mode == enMode.Edite)
            {

                ctrlSechduleRetakeTest1.Enabled = false;
                ctrlSechduleRetakeTest1.Visible = false;
                lblTitle.Text = "Edite Vision Appointment";
                return;
            }

            if (_mode == enMode.Retake)
            {
                lblTitle.Text = "Retake Vision Test";
                ctrlSechduleRetakeTest1.Enabled = true;
                return;
            }
        }
        private async Task _getDataForLoadUI()
        {
            _testType = await _getTestType(_testTypeID);
            if (_testType == null)
            {
                btnSaveTestAppointment.Enabled = false;
                return;
            }
        }
        private TestAppointment CreateTestAppointmentObj()
        {
            TestAppointment testAppointment = null;
            try
            {
            
                if (!_infoValidation())
                {
                    return testAppointment;
                }
                testAppointment = new TestAppointment();
                testAppointment.LocalDrivingLicenseApplication_ID = _lDLAppID;
                testAppointment.RetakeTestApplication_ID = null;
                testAppointment.TestAppointmentID = 0;
                testAppointment.isLocked = false;
                testAppointment.CreatedByUser_ID = Global.User.UserID;
                testAppointment.PaidFees = _testType.TestTypeFees;
                testAppointment.AppointmentDate = dateTimePicker.Value;
                testAppointment.TestType_ID = _testTypeID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
            return testAppointment;
        }
        private bool _infoValidation()
        {
            if (DateTime.Compare(dateTimePicker.Value,DateTime.Today) <= 0)
            {
                throw new Exception ("Date must be in Future");
            }
            return true;
        }
        private async Task<TestAppointment> _getAppointmentByID(int appointmentID)
        {
            try
            {
                return await _appointmentService.GetAppointmentAsync(_appointmentID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            return null;
        }
        private async Task<TestType> _getTestType(int _testTeypID)
        {
            TestType testType = null;
            try
            {
                testType = await _testTypeService.FindAsync(_testTeypID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return testType;
        }
        private async Task _loadDataInCtrl(int trail = 0)
        {
            try
            {
                await _getDataForLoadUI();
                lblLDLApp.Text = _lDLAppID.ToString();
                lblLClassName.Text = _licenseClassName;
                lblTrail.Text = trail.ToString();
                lblName.Text = _applicantFullName.ToString();
                lblFees.Text = _testType.TestTypeFees.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Error While Load Info To UI","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task _loadDataInCtrl(TestAppointment appointment,int trail = 0)
        {
            try
            {
                await _getDataForLoadUI();
                lblLDLApp.Text = appointment.LocalDrivingLicenseApplication_ID.ToString();
                lblLClassName.Text = _licenseClassName;
                lblTrail.Text = trail.ToString();
                lblName.Text = _applicantFullName.ToString();
                lblFees.Text = _testType.TestTypeFees.ToString();
                dateTimePicker.Value = appointment.AppointmentDate;
            }
            catch (Exception)
            {
                MessageBox.Show("Can not Load Appointment Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
