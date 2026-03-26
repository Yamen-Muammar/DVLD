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


        private TestService _testService;
        private ApplicationService _appService;
        private PersonService _personService;
        private LicenseClassService _licenseClassService;
        private ApplicationsTypeService _applicationsTypeService;
        private LicenseClass _licenseClass;
        private DVLD__Core.Models.Application _application;
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

            _application = await _getApplicationInfo(this._lDLAppID);
            if (_application == null)
            {
                return;
            }
            _personId = _application.Person_ID;

            _licenseClass = await _getLicenseClassbyLDLAppID(this._lDLAppID);
            if (_licenseClass == null)
            {
                return;
            }

            _passedTestCount = await _getPassedTestCount(_application.Person_ID);
            if (_passedTestCount == -1)
            {
                return;
            }

            _fillGroupBoxLDLApplicationDataFields(this._lDLAppID, _licenseClass.ClassName, _passedTestCount);
            await _fillGroupBoxApplicationDataFields(_application);
        }
        private void btnPersonInfo_Click(object sender, EventArgs e)
        {
            frmPersonInformation frmPersonInformation = new frmPersonInformation(_personId);
            frmPersonInformation.ShowDialog();
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
        private async Task<int> _getPassedTestCount(int personID)
        {
            int passedTestCount = -1;
            string personNationalNo = string.Empty;


            try
            {
                personNationalNo = (await _personService.Find(personID)).NationalNO;
                if (personNationalNo == string.Empty) { throw new ArgumentNullException("National No is null"); }
                passedTestCount = await _testService.PassedTestCount(personNationalNo);
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
            lblDate.Text = application.ApplicationDate.ToString("d");
            lblLastStatusDate.Text = (application.LastStatusDate == null ) ?application.ApplicationDate.ToString("d") :application.LastStatusDate?.ToString("d");
            lblCreatedBy.Text =(await _userService.GetUserByIdAsync(application.CreatedByUser_ID)).Username.ToString();
        }
    }
}
