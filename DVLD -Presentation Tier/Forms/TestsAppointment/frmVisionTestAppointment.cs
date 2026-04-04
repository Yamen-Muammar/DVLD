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
using DVLD__Core.View_Models;
using DVLD__Presentation_Tier.Controls.SechduleTestsControls;
using DVLD__Presentation_Tier.Forms.TestsForms;

namespace DVLD__Presentation_Tier.Forms.TestsAppointment
{
    public partial class frmVisionTestAppointment : Form
    {
        private int _LDLApplicationID;
        private int _testTeypID;
        private List<clsAppointmentsView> _appointmentsViewsList;

        private AppointmentService _appointmentService;
        private TestService _testService;
        public frmVisionTestAppointment()
        {
            InitializeComponent();
        }

        public frmVisionTestAppointment(int LDLAppId)
        {
            InitializeComponent(LDLAppId);
            _LDLApplicationID = LDLAppId;
            _testTeypID = 1;
            _appointmentService = new AppointmentService();
            _testService = new TestService();
        }

        private async void frmVisionTestAppointment_Load_1(object sender, EventArgs e)
        {
            await _refreshDataInDGV();
        }

        private async void btnAddAppointment_Click(object sender, EventArgs e)
        {
            int FoundAppointmentID = await _appointmentService.DoesApplicantHasAnActiveAppointmentAsync(_LDLApplicationID,_testTeypID);
            if (FoundAppointmentID > 0)
            {
                MessageBox.Show($"Applicant already has an Active appointment","Not Allowed",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            int appointmentID = -1;
       
            if (dgvAppointments?.CurrentRow != null)
            {
                 appointmentID = (int)dgvAppointments.CurrentRow.Cells["TestAppointmentID"].Value;
                if (await _testService.isAppointmentHasFailTestResultAsync(appointmentID))
                {
                    frmSechduleTest frmSechduleTest = new frmSechduleTest(appointmentID, ctrlSechduleVisionTest.enMode.Retake, this.ctrlLDLAwithApplicationInformation1.ApplicatFullName, _LDLApplicationID, _testTeypID, this.ctrlLDLAwithApplicationInformation1.licenseClassName);
                    frmSechduleTest.ShowDialog();
                }
                else
                {
                    MessageBox.Show($"Applicant already Passed The Test", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                frmSechduleTest frmSechduleTest = new frmSechduleTest(null, ctrlSechduleVisionTest.enMode.New, this.ctrlLDLAwithApplicationInformation1.ApplicatFullName, _LDLApplicationID, _testTeypID, this.ctrlLDLAwithApplicationInformation1.licenseClassName);
                frmSechduleTest.ShowDialog();
            }
            
            
            await _refreshDataInDGV();
        }
        private async Task _loadDataIntoTheList()
        {
            _appointmentsViewsList = new List<clsAppointmentsView>();
            try
            {
                _appointmentsViewsList =await _appointmentService.GetAllAppointmentsAsync(_LDLApplicationID,_testTeypID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _refreshDGV(List<clsAppointmentsView> list)
        {
            dgvAppointments.DataSource = null;
            dgvAppointments.DataSource = list;
        }
        private async Task _refreshDataInDGV()
        {
             await _loadDataIntoTheList();
            _refreshDGV(_appointmentsViewsList);
             
        }

        private async void editeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (_isAppointmentLocked())
            {
                MessageBox.Show("Appointment Locked", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int appointmentID = (int)dgvAppointments.CurrentRow.Cells["TestAppointmentID"].Value;
            frmSechduleTest frmSechduleTest = new frmSechduleTest(appointmentID, ctrlSechduleVisionTest.enMode.Edite, this.ctrlLDLAwithApplicationInformation1.ApplicatFullName, _LDLApplicationID, _testTeypID, this.ctrlLDLAwithApplicationInformation1.licenseClassName);
            frmSechduleTest.ShowDialog();
            await _refreshDataInDGV();
        }

        private async void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_isAppointmentLocked())
            {
                MessageBox.Show("Appointment Locked", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DateTime date = _getDateFromDGV();
            decimal fees = _getFeesFromDGV();
            int appointmentID = (int)dgvAppointments.CurrentRow.Cells["TestAppointmentID"].Value;

            frmTakeTest frmTakeTest = new frmTakeTest(appointmentID,_LDLApplicationID, this.ctrlLDLAwithApplicationInformation1.licenseClassName, 0, this.ctrlLDLAwithApplicationInformation1.ApplicatFullName, date, fees);
            frmTakeTest.ShowDialog();
            await _refreshDataInDGV();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool _isAppointmentLocked()
        {
            return (bool)dgvAppointments.CurrentRow.Cells["isLocked"].Value;
        }
        private DateTime _getDateFromDGV()
        {
            DateTime date = (DateTime)dgvAppointments.CurrentRow.Cells["AppointmentDate"].Value;
            if (date == null)
            {
                MessageBox.Show("Date is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return DateTime.MinValue;
            }
            return date;
        }
        private decimal _getFeesFromDGV()
        {
            decimal fees = (decimal)dgvAppointments.CurrentRow.Cells["PaidFees"].Value;
            return fees;
        }
    }
}
