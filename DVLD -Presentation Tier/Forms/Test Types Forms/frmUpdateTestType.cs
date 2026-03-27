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

namespace DVLD__Presentation_Tier.Forms.Test_Types_Forms
{
    public partial class frmUpdateTestType : Form
    {
        private int _testTypeID = -1;
        private TestType TestType { get; set; }
        private TestTypeService _testTypeService;
        public frmUpdateTestType()
        {
            InitializeComponent();
            _testTypeService = new TestTypeService();
        }

        public frmUpdateTestType(int testTypeID)
        {
            InitializeComponent();
            _testTypeID = testTypeID;
            _testTypeService = new TestTypeService();
        }

        private async void frmUpdateTestType_Load(object sender, EventArgs e)
        {
            if (! await _loadTestTypeDataToObject(_testTypeID))
            {
                this.Close();
                return;
            }
            _loadDataInForm();
        }

        private async Task<bool> _loadTestTypeDataToObject(int id)
        {
            try
            {
                TestType =await _testTypeService.FindAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }                       
        }

        private void _loadDataInForm()
        {
            lblID .Text = _testTypeID.ToString();
            tbTitle.Text = TestType.TestTypeTitle.ToString();
            tbDescription.Text =TestType.TestTypeDescription.ToString();
            tbFees.Text = TestType.TestTypeFees.ToString();
        }
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!_loadInfoToObject())
            {
                return;
            }

            try
            {
                if (await _testTypeService.UpdateTestTypeAsync(TestType))
                {
                    MessageBox.Show("Test Type updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to update Test Type. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool _loadInfoToObject()
        {
            if (!ValidateInput())
            {
                return false;
            }

            TestType.TestTypeTitle = tbTitle.Text.ToString();
            tbDescription.Text = tbDescription.Text.ToString();
            TestType.TestTypeFees = decimal.Parse(tbFees.Text);
            return
                true;
        }
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(tbTitle.Text))
            {
                MessageBox.Show("Please enter the test type fees.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbTitle.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(tbDescription.Text))
            {
                MessageBox.Show("Please enter the test type fees.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbTitle.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(tbFees.Text))
            {
                MessageBox.Show("Please enter the test type fees.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!decimal.TryParse(tbFees.Text, out decimal fees) || fees < 0)
            {
                MessageBox.Show("Please enter a valid non-negative number for fees.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
           
            
            return true;
        }
    }
}
