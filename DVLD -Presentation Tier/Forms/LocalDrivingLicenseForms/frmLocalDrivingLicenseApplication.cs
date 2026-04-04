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
using DVLD__Core.Models;
using DVLD__Core.View_Models;
using DVLD__Presentation_Tier.Forms.TestsAppointment;

namespace DVLD__Presentation_Tier.Forms.LocalDrivingLicenseForms
{
    public partial class frmLocalDrivingLicenseApplication : Form
    {

        private ApplicationService _applicationService;
        private List<clsLocalDrivingLicesnseApplicationView> _list { get; set; }

        private  List<clsLocalDrivingLicesnseApplicationView> _dataBaseSource { get; set; }
        private TestService _testService;
        public frmLocalDrivingLicenseApplication()
        {
            InitializeComponent();    
            _applicationService = new ApplicationService();
            _testService = new TestService();
        }
       
        // button Events
        private async void frmLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _loadComboBox();
            await _refreshUIDataHoldersAsync(_dataBaseSource);
        }
        private async void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            string selecedItemStatus = _getSelectedItemStatus();
            string selectedItemClassName = _getSelectedItemClassName();
            if (selecedItemStatus == "Canceled")
            {
                showApplicationDetailsToolStripMenuItem.Enabled = false;
                cancelApplicationToolStripMenuItem.Enabled = false;
                SetVisibleMenuItems(false,false, false, false, false, false);
                showPersonLicenseHistoryToolStripMenuItem.Enabled = false;
                return;
            }

            if (selecedItemStatus == "New")
            {
                showApplicationDetailsToolStripMenuItem.Enabled = true;
                cancelApplicationToolStripMenuItem.Enabled = true;
                showPersonLicenseHistoryToolStripMenuItem.Enabled = true;   
            }
            string nationalNo = _getSelectedItemNationalNo();
            // int CountOfPassedTest= await _testService.PassedTestCount(nationalNo, selectedItemClassName);
            int CountOfPassedTest = (int)dgvApplicationsList.CurrentRow.Cells["PassedTests"].Value;
            _visbleComboItemsOnPassedTests(CountOfPassedTest);
        }
        private async void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int lDLAppID = _getSelectedLDLApplicationID();
            bool isDeleted = false;

            if (MessageBox.Show($"Application ID :{lDLAppID} Will Be Deleted, Are You Sure ?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) != DialogResult.OK)
            {
                return;
            }

            try
            {
                isDeleted = await _applicationService.DeleteLDLApplicationAsync(lDLAppID);
                if (!isDeleted)
                {
                    MessageBox.Show("Something Went Wrong!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Application Deleted Successfully", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await _refreshUIDataHoldersAsync(_dataBaseSource);
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }      
        private async void btnAddNewLDApplication_Click(object sender, EventArgs e)
        {
            frmNewLocalDrivingLicenseApplication frmNewLocalDrivingLicenseApplication = new frmNewLocalDrivingLicenseApplication();
            frmNewLocalDrivingLicenseApplication.ShowDialog();
            await _refreshUIDataHoldersAsync(_dataBaseSource);
        }
        private async void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int localDrivingLicenseApplicationID = _getSelectedLDLApplicationID();

            try
            {
                if (await _applicationService.UpdateLDLApplicationStatus(localDrivingLicenseApplicationID, ApplicationService.enStatus.Canceled))
                {
                    MessageBox.Show("Application Status Updated Successfully", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     await _refreshUIDataHoldersAsync(_dataBaseSource);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void editeApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private async void sechduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int lDLAppID = _getSelectedLDLApplicationID();
            frmVisionTestAppointment frmVisionTestAppointment = new frmVisionTestAppointment(lDLAppID);
            frmVisionTestAppointment.ShowDialog();
            await _refreshUIDataHoldersAsync(_dataBaseSource);
        }
        private void sechduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void sechduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private async Task _refreshUIDataHoldersAsync(List<clsLocalDrivingLicesnseApplicationView> source)
        {
            await _refreshDataOnSource(source);
            _refreshDGVDataSource(_list);
        }
        private int _getSelectedLDLApplicationID()
        {
            return (int)dgvApplicationsList.CurrentRow.Cells[0].Value;
        }
        private string _getSelectedItemNationalNo()
        {
            return (string)dgvApplicationsList.CurrentRow.Cells["NationalNo"].Value;
        }
        private string _getSelectedItemStatus()
        {

            return (string)dgvApplicationsList.CurrentRow.Cells["Status"].Value;
        }
        private async Task _loadDataAsync()
        {
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

        private string _getSelectedItemClassName()
        {
            return dgvApplicationsList.CurrentRow.Cells["DrivingClassTitle"].Value.ToString();
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

        private void _visbleComboItemsOnPassedTests(int PassedTestsCount)
        {
            switch (PassedTestsCount)
            {
                case 0:
                    SetVisibleMenuItems(true,true, false, false, false, false);
                    break;
                case 1:
                    SetVisibleMenuItems(true, false, true, false, false, false);
                    break;
                case 2:
                    SetVisibleMenuItems(true, false, false, true, false, false);
                    break;
                case 3:
                    SetVisibleMenuItems(false,false, false, false, true, false);
                    break;
                default:
                    SetVisibleMenuItems(false,false, false, false, false, false);
                    break;
            }
        }

        private void SetVisibleMenuItems(bool sechduleTests,bool VisionTest,bool WrittenTest,bool StreetTest,bool issueDrivingLicense, bool showLicense)
        { 
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = issueDrivingLicense;
            sechduleTestsToolStripMenuItem.Enabled = sechduleTests;
            // TODO : if he has license you can show this( create it when we make the license service class).
            showLicenseToolStripMenuItem.Enabled = showLicense;
            sechduleStreetTestToolStripMenuItem.Enabled = StreetTest;
            sechduleVisionTestToolStripMenuItem.Enabled = VisionTest;
            sechduleWrittenTestToolStripMenuItem.Enabled = WrittenTest;
        }

        
    }
}
