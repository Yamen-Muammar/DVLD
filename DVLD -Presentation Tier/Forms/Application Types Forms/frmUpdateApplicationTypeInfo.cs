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

namespace DVLD__Presentation_Tier.Forms.Application_Types_Forms
{
    public partial class frmUpdateApplicationTypeInfo : Form
    {
        private int _applicationTypeID;
        private ApplicationType _applicationType;
        private ApplicationsTypeService _applicationsTypeService;
        public frmUpdateApplicationTypeInfo()
        {
            InitializeComponent();
            _applicationsTypeService = new ApplicationsTypeService();
        }
        public frmUpdateApplicationTypeInfo(int applicationTypeID)
        {
            InitializeComponent();
            _applicationTypeID = applicationTypeID;
            _applicationsTypeService = new ApplicationsTypeService();
        }

        private async void frmUpdateApplicationTypeInfo_Load(object sender, EventArgs e)
        {
            try
            {
                await _loadApplcationINobject();

                if (_applicationType == null)
                {
                    this.Close();
                    return;
                }
                LoadDataTOForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void LoadDataTOForm()
        {
            lblID.Text = _applicationType.ApplicationTypeID.ToString();
            lblTitle.Text = _applicationType.ApplicationTypeTitle.ToString();
            tbFees.Text = _applicationType.ApplicationTypeFees.ToString("F2");
        }
        private async Task _loadApplcationINobject()
        {
            try
            {
                _applicationType = await _applicationsTypeService.GetApplicationTypeByID(_applicationTypeID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);        
            }
        }
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!_loadInfoToObject())
            {
                return;
            }

            try
            {                
                if (await _applicationsTypeService.UpdateApplicationType(_applicationType))
                {
                    MessageBox.Show("Application Type updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to update Application Type. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(tbFees.Text))
            {
                MessageBox.Show("Please enter the application type fees.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!decimal.TryParse(tbFees.Text, out decimal fees) || fees < 0)
            {
                MessageBox.Show("Please enter a valid non-negative number for fees.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private bool _loadInfoToObject()
        {
            if (!ValidateInput())
            {
                return false;
            }

            _applicationType.ApplicationTypeFees = decimal.Parse(tbFees.Text);
            return
                true;
        }
    }
}
