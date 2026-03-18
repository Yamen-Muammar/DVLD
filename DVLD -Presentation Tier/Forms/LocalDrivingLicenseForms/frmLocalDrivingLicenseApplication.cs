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

        private ApplicationService _applicationService;
        private List<clsLocalDrivingLicesnseApplicationView> _list { get; set; }

        private  List<clsLocalDrivingLicesnseApplicationView> _dataBaseSource { get; set; }
        public frmLocalDrivingLicenseApplication()
        {
            InitializeComponent();
        }
       
        private async void frmLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _loadComboBox();
            await _refreshDataOnSource(_dataBaseSource);
            _refreshDGVDataSource(_dataBaseSource);
        }
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
        private async void btnAddNewLDApplication_Click(object sender, EventArgs e)
        {
            frmNewLocalDrivingLicenseApplication frmNewLocalDrivingLicenseApplication = new frmNewLocalDrivingLicenseApplication();
            frmNewLocalDrivingLicenseApplication.ShowDialog();
            await _refreshDataOnSource(_dataBaseSource);
            _refreshDGVDataSource(_dataBaseSource);
        }
        private async void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int localDrivingLicenseApplicationID = (int)dgvApplicationsList.CurrentRow.Cells[0].Value;

            try
            {
                if (await _applicationService.UpdateLDLApplicationStatus(localDrivingLicenseApplicationID, ApplicationService.enStatus.Canceled))
                {
                    MessageBox.Show("Application Status Updated Successfully", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await _refreshDataOnSource(_dataBaseSource);
                    _refreshDGVDataSource(_dataBaseSource);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // data logic
        private void _storeListDataFromSource(List<clsLocalDrivingLicesnseApplicationView> source)
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
        private async Task _refreshDataOnSource(List<clsLocalDrivingLicesnseApplicationView> source)
        {
            if (source == _dataBaseSource)
            {
                await _loadDataAsync();
                _storeListDataFromSource(_dataBaseSource);
                return;
            }

            _storeListDataFromSource(source);
        }
        private void _refreshDGVDataSource(List<clsLocalDrivingLicesnseApplicationView> source)
        {
            dgvApplicationsList.DataSource = null;
            if (source == null)
            {
                this.Close();
                return;
            }
            dgvApplicationsList.DataSource = source;
            lblRecordsCount.Text = source.Count.ToString();
        }
        private async Task _loadDataAsync()
        {
            _applicationService = new ApplicationService();
            _dataBaseSource = new List<clsLocalDrivingLicesnseApplicationView>();
            try
            {
                _dataBaseSource = await _applicationService.GetAllLDLApplications();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
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
