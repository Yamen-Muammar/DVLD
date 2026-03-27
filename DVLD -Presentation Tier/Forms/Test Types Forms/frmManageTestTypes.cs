using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD__Business_Tier.Services;
using DVLD__Core.Models;

namespace DVLD__Presentation_Tier.Forms.Test_Types_Forms
{
    public partial class frmManageTestTypes : Form
    {
        private List<TestType> _testTypes;

        private TestTypeService _testTypeService;
        public frmManageTestTypes()
        {
            InitializeComponent();
            _testTypeService = new TestTypeService();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            await _refreshTestTypes();
        }

        private async Task _refreshTestTypes()
        {
            _testTypes = null;
            _testTypes = await _getTestTypesList();
            if (_testTypes == null)
            {
                this.Close();
                return;    
            }
            dgvListOfTestTypes.DataSource = null;
            dgvListOfTestTypes.DataSource = _testTypes;
            lblRecordsCount.Text = _testTypes.Count.ToString();
        }     
        private async Task<List<TestType>> _getTestTypesList()
        {
            try
            {
                return await _testTypeService.GetAllTestTypesAsync();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                
                return null;
            }
        }
        private bool _validateSelectedApplicationType()
        {
            int selectrdID = (int)dgvListOfTestTypes.CurrentRow.Cells[0].Value;
            if (selectrdID == 0)
            {
                MessageBox.Show("Please select an application type to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private async void editeTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_validateSelectedApplicationType())
            {
                return;
            }

            int selectedTestTypeID = (int)dgvListOfTestTypes.CurrentRow.Cells[0].Value;
            frmUpdateTestType frmUpdateApplicationTypeInfo = new frmUpdateTestType(selectedTestTypeID);
            frmUpdateApplicationTypeInfo.ShowDialog();
            await _refreshTestTypes();
        }
    }
}
