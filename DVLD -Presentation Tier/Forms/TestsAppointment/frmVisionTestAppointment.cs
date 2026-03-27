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

namespace DVLD__Presentation_Tier.Forms.TestsAppointment
{
    public partial class frmVisionTestAppointment : Form
    {
        private int _LDLApplicationID;
        private int _testTeypID;
        private List<clsAppointmentsView> _appointmentsViewsList;


        private AppointmentService _appointmentService;
        
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
        }
        private void btnClosefrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            frmSechduleTest frmSechduleTest = new frmSechduleTest(this.ctrlLDLAwithApplicationInformation1.ApplicatFullName,_LDLApplicationID,_testTeypID);
            frmSechduleTest.ShowDialog();
        }
        private async void frmVisionTestAppointment_Load(object sender, EventArgs e)
        {
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

        
    }
}
