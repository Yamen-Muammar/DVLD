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

namespace DVLD__Presentation_Tier.Forms.LocalDrivingLicenseForms
{
    public partial class frmLocalDrivingLicenseApplication : Form
    {
        private List<clsLocalDrivingLicesnseApplicationView> _list { get; set; } 
        public frmLocalDrivingLicenseApplication()
        {
            InitializeComponent();
        }
        
        private void frmLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _loadComboBox();
            _refreshData();
        }

        private void _loadApplicationsListData()
        {
            _list = new List<clsLocalDrivingLicesnseApplicationView>();
            try
            {
                _list = ApplicationService.GetAllLDLApplications();
            }
            catch (Exception)
            {

                MessageBox.Show("Error While Retriving Date","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void _loadComboBox()
        {
            List<string> FilterOptions = new List<string>() { "None", "Person ID", "National NO" };
            cbFilterOn.DataSource = FilterOptions;
            cbFilterOn.SelectedIndex = 0;
        }
        private void cbFilterOn_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbFilterInput.Text = string.Empty;

            if (cbFilterOn.SelectedItem.ToString() == "None")
            {
                tbFilterInput.Enabled = false;
            }
            else
            {
                tbFilterInput.Enabled = true;
            }
        }

        private void _restartFilterArea()
        {
            cbFilterOn.SelectedIndex = 0;
            tbFilterInput.Text = string.Empty;
        }  
        private void btnAddNewLDApplication_Click(object sender, EventArgs e)
        {
            frmNewLocalDrivingLicenseApplication frmNewLocalDrivingLicenseApplication = new frmNewLocalDrivingLicenseApplication();
            frmNewLocalDrivingLicenseApplication.ShowDialog();
            _refreshData();
        }

        private void _refreshData()
        {
            dgvApplicationsList.DataSource = null;
            _loadApplicationsListData();
            dgvApplicationsList.DataSource = _list;
            lblRecordsCount.Text = _list.Count.ToString();
            _restartFilterArea();

        }
 
        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int localDrivingLicenseApplicationID = (int)dgvApplicationsList.CurrentRow.Cells[0].Value;

            try
            {
                if (ApplicationService.UpdateLDLApplicationStatus(localDrivingLicenseApplicationID, ApplicationService.enStatus.Canceled))
                {
                    MessageBox.Show("Application Status Updated Successfully", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _refreshData();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void tbFilterInput_TextChanged(object sender, EventArgs e)
        {
            // TODO : CREATE FILTER LOGIC
        }
    }
}
