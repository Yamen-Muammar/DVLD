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

namespace DVLD__Presentation_Tier.Forms.TestsForms
{
    public partial class frmTakeTest : Form
    {
        public event EventHandler<int> OnTestPass;
        
        protected void TriggerOnTestPass(int passedTestID)
        {
            OnTestPass?.Invoke(this,passedTestID);
        }
        


        private Test _test;
        private int _testAppointmentID;
        private bool _isPass;
        private string _notes;
        private int _createdByUserID;

        private TestService _testService;
        public frmTakeTest()
        {
            InitializeComponent();
        }
        public frmTakeTest(int appointmentID,int LDLAppID, int testTypeID,string licenseClassName, int tril, string applicantName, DateTime date, decimal fees)
        {
            InitializeComponent(LDLAppID, licenseClassName, testTypeID,tril, applicantName, date, fees);
            _testService = new TestService();
            _testAppointmentID  = appointmentID;
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            _test = new Test(-1,_testAppointmentID,false,"",Global.User.UserID);

        }

        private async void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                _test.TestResult = _getResultFromForm();
                _test.Notes = _getNotesFromForm();
                if (await _testService.SaveTestAsync(_test))
                {
                    MessageBox.Show("Test Added Successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblTestID.Text = _test.TestID.ToString();
                    if (_test.TestResult)
                    {
                        TriggerOnTestPass(_test.TestID);
                    }
                
                }
                else
                {
                    MessageBox.Show("Can not add the Test Result", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex )
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private bool _getResultFromForm()
        {
            if (btnPass.Checked)
            {
                return true;
            }
            else if (btnFail.Checked)
            {
                return false;
            }
            else
            {
                throw new Exception("Please select a test result.");
            }
        }

        private string _getNotesFromForm()
        {
            return textBox1.Text.Trim();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
