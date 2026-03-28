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
        private void btnClosefrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void frmVisionTestAppointment_Load_1(object sender, EventArgs e)
        {
            await _refreshDataInDGV();
        }

        private async void btnAddAppointment_Click(object sender, EventArgs e)
        {
            bool isHas =((await _appointmentService.DoesApplicantHasAnActiveAppointmentAsync(_LDLApplicationID,_testTeypID)) > 0);
            if (isHas)
            {
                MessageBox.Show($"Applicant already has an Active appointment","Alert",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            int appointmentID = (int)dgvAppointments.CurrentRow.Cells["TestAppointmentID"].Value;

            if (await _testService.isAppointmentHasFailTestResultAsync(appointmentID))
            {
                frmSechduleTest frmSechduleTest = new frmSechduleTest(null,ctrlSechduleVisionTest.enMode.Retake, this.ctrlLDLAwithApplicationInformation1.ApplicatFullName, _LDLApplicationID, _testTeypID);
                frmSechduleTest.ShowDialog();
            }
            else
            {
                frmSechduleTest frmSechduleTest = new frmSechduleTest(null,ctrlSechduleVisionTest.enMode.New,this.ctrlLDLAwithApplicationInformation1.ApplicatFullName, _LDLApplicationID, _testTeypID);
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

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception)
            {

            }
        }

        private async void editeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int appointmentID = (int)dgvAppointments.CurrentRow.Cells["TestAppointmentID"].Value;
            frmSechduleTest frmSechduleTest = new frmSechduleTest(appointmentID, ctrlSechduleVisionTest.enMode.New, this.ctrlLDLAwithApplicationInformation1.ApplicatFullName, _LDLApplicationID, _testTeypID);
            frmSechduleTest.ShowDialog();
            await _refreshDataInDGV();
        }

       
    }
}
