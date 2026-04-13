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
using DVLD__Presentation_Tier.Forms.License_Forms;

namespace DVLD__Presentation_Tier.Forms
{
    public partial class frmReplaceLicense : Form
    {
        public DVLD__Core.Models.License PreviousLicenseInfo { get; set; }

       // private ApplicationType _applicationType { get; set; }

        private readonly int damegedApplicationTypeIDinDB;
        private readonly int lostApplicationTypeIDinDB;
        private ApplicationType _lostApplicationType;
        private ApplicationType _damegedApplicationType;

        private DVLD__Core.Models.Application application;
        private ApplicationsTypeService _applicationsTypeService;

        private LicenseService _licenseService;
        private LicenseClassService _licenseClassService;
        private LicenseClass _licenseClass;
        public frmReplaceLicense()
        {
            InitializeComponent();
            damegedApplicationTypeIDinDB = 5;
            lostApplicationTypeIDinDB = 6;
        }
        private async void frmReplaceLicense_Load(object sender, EventArgs e)
        {
            _applicationsTypeService = new ApplicationsTypeService();
            _licenseService = new LicenseService();
            _licenseClassService = new LicenseClassService();
            _toggleControls(false);
            try
            {
                _damegedApplicationType = await _applicationsTypeService.GetApplicationTypeByID(damegedApplicationTypeIDinDB);
                if (_damegedApplicationType == null)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private async void btnFindLicense_Click(object sender, EventArgs e)
        {
            int enteredlicenseID = _getLicenseID();
            if (enteredlicenseID == -1)
            {
                MessageBox.Show("Enter a valid ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                await this.ctrlDriverLicenseInfo1.LoadDate(enteredlicenseID);
                this.PreviousLicenseInfo = this.ctrlDriverLicenseInfo1.LicenseInfo;

                _licenseClass = await _getLicenseClassByIDAsync((int)PreviousLicenseInfo.LicenseClass_ID);
                if (_licenseClass == null)
                {
                    this.Close();
                    return;
                }

                application = _prepareAppObj();
                _fillApplicationUIForm(application);

                _toggleControls(true);
            }
            catch (ArgumentNullException ex)
            {
                PreviousLicenseInfo = null;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _toggleControls(false);
                return;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _toggleControls(false);
                return;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private async void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            if (PreviousLicenseInfo.isActive == false)
            {
                MessageBox.Show("License is not active, you cannot Replace the license", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _toggleControls(false);
                return;
            }

            try
            {
                DVLD__Core.Models.License newLicense = _prepareNewLicenseObj();

                newLicense.LicenseID = await _licenseService.ReplaceLicenseAsync(application, PreviousLicenseInfo, newLicense);
                if (newLicense.LicenseID <= 0)
                {
                    MessageBox.Show("License Does not Issued", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("License Issued Successfully", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lbRenewLAppID.Text = newLicense.Application_ID.ToString();
                    lblRenewLicenseID.Text = newLicense.LicenseID.ToString();
                    _toggleControls(false);
                    gbFinder.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void llblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
        private void llblLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseHistory frmLicenseHistory = new frmLicenseHistory(this.ctrlDriverLicenseInfo1.PersonInfo.NationalNO);
            frmLicenseHistory.ShowDialog();
        }

        //helper functions

        private DVLD__Core.Models.License _prepareNewLicenseObj()
        {
            DVLD__Core.Models.License newLicense = new DVLD__Core.Models.License();
            DateTime date = DateTime.Now;
            newLicense.IssueDate = PreviousLicenseInfo.IssueDate;
            newLicense.ExpirationDate = PreviousLicenseInfo.ExpirationDate;
            newLicense.isActive = true;
            newLicense.CreateByUser_ID = Global.User.UserID;
            newLicense.Driver_ID = PreviousLicenseInfo.Driver_ID;
            newLicense.Note = PreviousLicenseInfo.Note;
            newLicense.LicenseClass_ID = PreviousLicenseInfo.LicenseClass_ID;

            if (rbtnDamage.Checked)
            {
                newLicense.IssueReasen = "Damege Replace";
            }
            else if (rbtnLost.Checked)
            {
                newLicense.IssueReasen = "Lost Replace";
            }

            return newLicense;
        }
        private DVLD__Core.Models.Application _prepareAppObj()
        {
            DVLD__Core.Models.Application application = new DVLD__Core.Models.Application();
            application.ApplicationStatus = "Completed";
            DateTime date = DateTime.Now;
            application.ApplicationDate = date;
            application.LastStatusDate = date;
            application.CreatedByUser_ID = Global.User.UserID;
            application.Person_ID = this.ctrlDriverLicenseInfo1.PersonInfo.PersonID;

            if (rbtnDamage.Checked)
            {
                application.PaidFees = _damegedApplicationType.ApplicationTypeFees;
                application.ApplicationType_ID = _damegedApplicationType.ApplicationTypeID;
            }
            else if(rbtnLost.Checked)
            {
                application.PaidFees = _lostApplicationType.ApplicationTypeFees;
                application.ApplicationType_ID = _lostApplicationType.ApplicationTypeID;
            }

            return application;
        }
        private int _getLicenseID()
        {
            if (!int.TryParse(tbLicenseID.Text, out int id) || id <= 0)
            {
                return -1;
            }
            return id;
        }
        private void _toggleControls(bool enabled)
        {
            btnIssueReplacement.Enabled = enabled;
            gbReplacementType.Enabled = enabled;
        }
        private void _fillApplicationUIForm(DVLD__Core.Models.Application application)
        {
            lblAppDate.Text = application.ApplicationDate.ToString("d");
            lblAppFees.Text = application.PaidFees.ToString();
            lblLocalLicenseID.Text = PreviousLicenseInfo.LicenseID.ToString();
            lblCreatedBy.Text = application.CreatedByUser_ID.ToString();
        }
        private async Task<LicenseClass> _getLicenseClassByIDAsync(int id)
        {

            return await _licenseClassService.GetLicenseClassByIDAsync(id);
        }

        private async void rbtnDamage_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnDamage.Checked)
            {
                if (_damegedApplicationType == null)
                {
                    try
                    {
                        _damegedApplicationType = await _applicationsTypeService.GetApplicationTypeByID(damegedApplicationTypeIDinDB);
                        if (_damegedApplicationType == null)
                        {
                            this.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }
                

            }else if (rbtnLost.Checked)
            {
                if (_lostApplicationType == null)
                {
                    try
                    {
                        _lostApplicationType = await _applicationsTypeService.GetApplicationTypeByID(lostApplicationTypeIDinDB);
                        if (_lostApplicationType == null)
                        {
                            this.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }
            }

            application = _prepareAppObj();
            _fillApplicationUIForm(application);
        }
    }
}
