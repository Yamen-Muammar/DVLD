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

        private  List<clsLocalDrivingLicesnseApplicationView> DataBasePiplineSource 
        {
            get
            {
                try
                {
                    return ApplicationService.GetAllLDLApplications();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    this.Close();
                }
                return null;
            }            
        }
        public frmLocalDrivingLicenseApplication()
        {
            InitializeComponent();
        }
        
        private void frmLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _loadComboBox();
            _refreshData(DataBasePiplineSource);
            _refreshDGVDataSource(DataBasePiplineSource);
        }
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
        private void btnAddNewLDApplication_Click(object sender, EventArgs e)
        {
            frmNewLocalDrivingLicenseApplication frmNewLocalDrivingLicenseApplication = new frmNewLocalDrivingLicenseApplication();
            frmNewLocalDrivingLicenseApplication.ShowDialog();
            _refreshData(DataBasePiplineSource);
            _refreshDGVDataSource(DataBasePiplineSource);
        }
        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int localDrivingLicenseApplicationID = (int)dgvApplicationsList.CurrentRow.Cells[0].Value;

            try
            {
                if (ApplicationService.UpdateLDLApplicationStatus(localDrivingLicenseApplicationID, ApplicationService.enStatus.Canceled))
                {
                    MessageBox.Show("Application Status Updated Successfully", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _refreshData(DataBasePiplineSource);
                    _refreshDGVDataSource(DataBasePiplineSource);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // data logic
        private void _loadApplicationsListData(List<clsLocalDrivingLicesnseApplicationView> source)
        {
            _list = new List<clsLocalDrivingLicesnseApplicationView>();
            try
            {
                _list = source;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
            }

        }   
        private void _refreshData(List<clsLocalDrivingLicesnseApplicationView> source)
        {
            _loadApplicationsListData(source);
        }
        private void _refreshDGVDataSource(List<clsLocalDrivingLicesnseApplicationView> source)
        {
            dgvApplicationsList.DataSource = null;
            dgvApplicationsList.DataSource = source;
            lblRecordsCount.Text = source.Count.ToString();
        }
        

        //Filtering combo Box Logic

        private void _loadComboBox()
        {
            List<string> FilterOptions = new List<string>() { "None", "National NO" };
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
            else if (cbFilterOn.SelectedItem.ToString() == "National NO")
            {
                tbFilterInput.Enabled = true;
            }
            else
            {
                tbFilterInput.Enabled = false;
            }
        }

        private void _restartFilterArea()
        {
            cbFilterOn.SelectedIndex = 0;
            tbFilterInput.Text = string.Empty;
        }
        private void tbFilterInput_TextChanged(object sender, EventArgs e)
        {
              
            if (cbFilterOn.SelectedItem == null)
            {
                MessageBox.Show("Select Filter","Alert",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            string selectedFilter = cbFilterOn.SelectedItem.ToString();
            string searchingTarget = tbFilterInput.Text;

            if (string.IsNullOrEmpty(searchingTarget) || searchingTarget.Length < 2)
            {
                _refreshDGVDataSource(_list);
                return;
            }

            List<clsLocalDrivingLicesnseApplicationView> filteredList = _returnDataOnFilter(selectedFilter, searchingTarget);
            _refreshDGVDataSource(filteredList);
        }

        private List<clsLocalDrivingLicesnseApplicationView> _returnDataOnFilter(string selectedFilter,string serchingTarget)
        {
            List<clsLocalDrivingLicesnseApplicationView> filteredList = new List<clsLocalDrivingLicesnseApplicationView>();
            switch (selectedFilter)
            {
                case "None":

                    break;
                
                case "National NO":
                   
                    filteredList = _list.Where(i => i.NationalNO == serchingTarget.ToUpper()).ToList();
                    break;

                default:
                    tbFilterInput.Enabled = false;
                    break;
            }

            return filteredList;
        }
    }
}
