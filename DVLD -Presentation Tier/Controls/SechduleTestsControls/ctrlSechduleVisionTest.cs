using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        private int _lDLAppID;
        private const int _testTypeID = 1;
        private TestType _testType;
        private LicenseClass _licenseClass;
        private string _applicantFullName;

        private TestTypeService _testTypeService;
        private ApplicationService _appService;
        private LicenseClassService _licenseClassService;
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
        public ctrlSechduleVisionTest(enMode mode,string applicantFullName,int ldlAppID)
        {
            InitializeComponent();
            _testTypeService = new TestTypeService();
            _licenseClassService = new LicenseClassService();
            _appointmentService = new AppointmentService();
            _mode = mode;
            _applicantFullName = applicantFullName;
            _lDLAppID = ldlAppID;
        }
        private async void ctrlSechduleVisionTest_Load(object sender, EventArgs e)
        {
            UIVisibltyOnMode(_mode);

            if (_mode == enMode.New)
            {
                _testType = await _getTestType(_testTypeID);
                if (_testType == null)
                {
                    btnSaveTestAppointment.Enabled = false;
                    return;
                }

                _licenseClass = await _getLicenseClassAsync(_lDLAppID);
                if (_licenseClass == null)
                {
                    btnSaveTestAppointment.Enabled = false;
                    return;
                }

                _loadDataInCtrl();
                return;
            }

            if (_mode == enMode.Edite)
            {
                return;
            }

            if (_mode == enMode.Retake)
            {
                return;
            }
        }
        private async void btnSaveTestAppointment_Click(object sender, EventArgs e)
        {
            if (_mode == enMode.New)
            {
                TestAppointment appointment = CreateTestAppointmentObj();
                try
                {
                    appointment.TestAppointmentID = await _appointmentService.AddTestAppointmentAsync(appointment);
                    if (appointment.TestAppointmentID == -1)
                    {
                        MessageBox.Show("Can not Create Appointment","Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
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
                return;
            }

            if (_mode == enMode.Edite)
            {
                return;
            }

            if (_mode == enMode.Retake)
            {
                return;
            }
        }
        private void UIVisibltyOnMode(enMode mode)
        {
            if (_mode == enMode.New)
            {
                ctrlSechduleRetakeTest1.Enabled = false;
                return;
            }

            if (_mode == enMode.Edite)
            {
                lblTitle.Text = "Edite Vision Appointment";
                ctrlSechduleRetakeTest1.Enabled = false;
                return;
            }

            if (_mode == enMode.Retake)
            {
                lblTitle.Text = "Retake Vision Test";
                ctrlSechduleRetakeTest1.Enabled = true;
                return;
            }
        }

        private TestAppointment CreateTestAppointmentObj()
        {
            TestAppointment testAppointment = new TestAppointment();
            if (!_infoValidation())
            {
                return testAppointment;
            }
            try
            {
                testAppointment.LocalDrivingLicenseApplication_ID = _lDLAppID;
                testAppointment.RetakeTestApplication_ID = null;
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
            if (DateTime.Compare(dateTimePicker.Value,DateTime.Now) <= 0)
            {
                return false;
            }
            return true;
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
        private async Task<LicenseClass> _getLicenseClassAsync(int ldlAppID)
        {
            LicenseClass licenseClass = null;
            try
            {
                licenseClass = await _licenseClassService.GetLicenseClassByLDLAppIDAsync(ldlAppID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return licenseClass;
        }
        private void _loadDataInCtrl(int trail = 0)
        {
            lblLDLApp.Text = _lDLAppID.ToString();
            lblLClassName.Text = _licenseClass.ClassName.ToString();
            lblTrail.Text = trail.ToString();
            lblName.Text = _applicantFullName.ToString();
            lblFees .Text = _licenseClass.ClassFees.ToString();
        }
    }
}
