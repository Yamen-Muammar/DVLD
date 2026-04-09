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
using DVLD__Presentation_Tier.Forms;

namespace DVLD__Presentation_Tier.Controls.LocalDLApplicationsControls
{
    public partial class ctrlLDLAwithApplicationInformation : UserControl
    {
        private int _lDLAppID;
        private int _passedTestCount;
        private int _personId;
        public string ApplicatFullName { get; set; }
        public string licenseClassName { get; set; }

        public DVLD__Core.Models.Application application;
        public LicenseClass licenseClass;

        private TestService _testService;
        private ApplicationService _appService;
        private PersonService _personService;
        private LicenseClassService _licenseClassService;
        private ApplicationsTypeService _applicationsTypeService;           
        private UserService _userService;


        //to ignore complier creation Exceptions
        private enum enPassedID
        {
            Passed = 1 , NotPassed = 2
        }
        private enPassedID _mode;
        public ctrlLDLAwithApplicationInformation()
        {
            InitializeComponent();
            _mode = enPassedID.NotPassed;
        }
        public ctrlLDLAwithApplicationInformation(int LDLAppID)
        {
            InitializeComponent();
            _applicationsTypeService = new ApplicationsTypeService();
            _appService = new ApplicationService();
            _testService = new TestService();
            _personService = new PersonService();
            _licenseClassService = new LicenseClassService();
            _userService = new UserService();

            _lDLAppID = LDLAppID;
            _mode = enPassedID.Passed;
        }
        private async void ctrlLDLAwithApplicationInformation_Load(object sender, EventArgs e)
        {
            if (_mode == enPassedID.NotPassed)
            {
                return;
            }

            application = await _getApplicationInfo(this._lDLAppID);
            if (application == null)
            {
                return;
            }
            _personId = application.Person_ID;

            licenseClass = await _getLicenseClassbyLDLAppID(this._lDLAppID);
            if (licenseClass == null)
            {
                return;
            }
            licenseClassName = licenseClass.ClassName.ToString();

            _passedTestCount = await _getPassedTestCount();
            if (_passedTestCount == -1)
            {
                return;
            }

            _fillGroupBoxLDLApplicationDataFields(this._lDLAppID, licenseClass.ClassName, _passedTestCount);
            await _fillGroupBoxApplicationDataFields(application);
        }
        private void btnPersonInfo_Click(object sender, EventArgs e)
        {
            frmPersonInformation frmPersonInformation1 = new frmPersonInformation(_personId);
            frmPersonInformation1.ctrlPersonInformation1.ReturnPersonObject_OnUpdate += UpdateApplicantName;
            frmPersonInformation1.ShowDialog();
        }

        private async Task<DVLD__Core.Models.Application> _getApplicationInfo(int LDLAppID)
        {
            DVLD__Core.Models.Application app = null;
            try
            {
                app = await _appService.GetApplicationOnLDLA_ID(LDLAppID);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return app;
        }
        private async Task<int> _getPassedTestCount()
        {
            int passedTestCount = -1;
            try
            {
                passedTestCount = await _testService.PassedTestCount(_lDLAppID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            return passedTestCount;
        }
        private async Task<LicenseClass > _getLicenseClassbyLDLAppID(int ldlAppID)
        {
            LicenseClass licenseClass = null;
            try
            {
                licenseClass =await _licenseClassService.GetLicenseClassByLDLAppIDAsync(ldlAppID);          
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return licenseClass;
        }  
        private void _fillGroupBoxLDLApplicationDataFields(int LDLAppID,string licenseClassName,int passedTestCount)
        {
            lblLDLAppID.Text = LDLAppID.ToString();
            lblLicenseClassName.Text = licenseClassName;
            lblPassedTestsCount.Text = $"{passedTestCount.ToString()}/3"; 
        }
        private async Task _fillGroupBoxApplicationDataFields(DVLD__Core.Models.Application application)
        {
            lblApplicationID.Text = application.ApplicationID.ToString();
            lblStatus.Text = application.ApplicationStatus.ToString();
            lblFees.Text= application.PaidFees.ToString();
            lblType.Text= (await _applicationsTypeService.GetApplicationTypeByID(application.ApplicationType_ID)).ApplicationTypeTitle.ToString();
            lblApplicant.Text = (await _personService.Find(application.Person_ID)).FullName();
            ApplicatFullName = lblApplicant.Text;
            lblDate.Text = application.ApplicationDate.ToString("d");
            lblLastStatusDate.Text = (application.LastStatusDate == null ) ?application.ApplicationDate.ToString("d") :application.LastStatusDate?.ToString("d");
            lblCreatedBy.Text =(await _userService.GetUserByIdAsync(application.CreatedByUser_ID)).Username.ToString();
        }

        // for outside calls 
        public void UpdateApplicantName(Person person)
        {
            lblApplicant.Text = person.FullName().ToString();
        }
        public void UpdatePassedTestCount()
        {
            if (_passedTestCount == 3)
            {
                return;
            }
            int newCount = _passedTestCount + 1;
            lblPassedTestsCount.Text = lblPassedTestsCount.Text = $"{newCount.ToString()}/3"; 
        }
    }
}
