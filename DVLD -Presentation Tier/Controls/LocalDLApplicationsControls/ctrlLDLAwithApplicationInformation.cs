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

namespace DVLD__Presentation_Tier.Controls.LocalDLApplicationsControls
{
    public partial class ctrlLDLAwithApplicationInformation : UserControl
    {
        private int _lDLAppID;
        private ApplicationService _appService;
        private int _passedTestCount = -1;
        private TestService _testService;
        private PersonService _personService;
        public ctrlLDLAwithApplicationInformation()
        {
            InitializeComponent();
        }
        public ctrlLDLAwithApplicationInformation(int LDLAppID)
        {
            InitializeComponent();
            _appService = new ApplicationService();
            _testService = new TestService();
            _personService = new PersonService();
            _lDLAppID = LDLAppID;

        }
        private void ctrlLDLAwithApplicationInformation_Load(object sender, EventArgs e)
        {

        }
        private void btnPersonInfo_Click(object sender, EventArgs e)
        {

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

        
    }
}
