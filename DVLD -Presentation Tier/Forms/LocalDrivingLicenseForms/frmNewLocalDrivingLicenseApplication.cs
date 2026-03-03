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

namespace DVLD__Presentation_Tier.Forms.LocalDrivingLicenseForms
{
    public partial class frmNewLocalDrivingLicenseApplication : Form
    {
        private int _personID = -1; //store id from retriveing event from ctrlPersonInformationWithFilter.
        private ApplicationType _applicationType {  get; set; }
        private List<LicenseClass> _licenseClasses;

        public frmNewLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            _applicationType = ApplicationsTypeService.GetApplicationTypeByID(2);
            if (_applicationType == null)
            {
                MessageBox.Show("Error While geting the Application Type.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void frmNewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _loadDataInForm();
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_personID == -1)
            {
                MessageBox.Show("Select a Person First", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            tabControl1.SelectedTab = tbApplicationInfo;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_validateInputs())
            {
                return;
            }

            DVLD__Core.Models.Application application = _fillApplicationInfo();
            string selectedClass = cbLicenseClasses.SelectedItem.ToString();
            int ClassTypeID = _licenseClasses.FirstOrDefault(c => c.ClassName == selectedClass).LicenseClassID;

            try
            {
                if(ApplicationService.SaveLocalDrivingLicenseApplication(application, ClassTypeID))
                {
                    MessageBox.Show("Application Added Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Alert",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlPersonInformationWithFilter1_ReturnPersonID_OnFindPerson(int PersonID)
        {
            _personID = PersonID;
        }

        private void _loadDataInForm()
        {
            lblApplicationDate.Text = DateTime.Now.ToString("d");
            lblCreatedName.Text = Global.User.Username;
            lblApplicationFees.Text = _applicationType.ApplicationTypeFees.ToString();
            _loadLicenseClassComboBox();
        }

        private void _loadLicenseClassComboBox()
        {
            _licenseClasses = new List<LicenseClass>();
            try
            {
                _licenseClasses = LicenseClassService.GetAlllicenseClasses();
                if (_licenseClasses.Count < 1)
                {
                    MessageBox.Show("Get License Classes Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                foreach (LicenseClass licenseClass in _licenseClasses) {
                    cbLicenseClasses.Items.Add(licenseClass.ClassName);
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }            
            cbLicenseClasses.SelectedIndex = 0;
        }

        private bool _validateInputs()
        {
            if (_personID == -1)
            {
                MessageBox.Show("You Have To Select Person Before", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (Global.User.UserID == -1)
            {
                MessageBox.Show("UnKnown User", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 
                    false;
            }

            if (_applicationType.ApplicationTypeID == -1)
            {
                MessageBox.Show("UnKnown ApplicationType", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return 
                true;
        }

        private DVLD__Core.Models.Application _fillApplicationInfo()
        {
            return new DVLD__Core.Models.Application
            {
                ApplicationID = -1,
                ApplicationType_ID = _applicationType.ApplicationTypeID,
                CreatedByUser_ID = Global.User.UserID,
                ApplicationDate = DateTime.Now,
                ApplicationStatus = "New",
                LastStatusDate = null,
                PaidFees = _applicationType.ApplicationTypeFees,
                Person_ID = _personID,
                           
            };
        }
        
    }
}
