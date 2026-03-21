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
    public partial class frmManageApplicationTypes : Form
    {
        private List<ApplicationType> _applicationTypesList { get; set; }
        private ApplicationsTypeService _applicationsTypeService;
        public frmManageApplicationTypes()
        {
            InitializeComponent();
            _applicationsTypeService = new ApplicationsTypeService();
        }

        private async void frmManageApplicationTypes_Load(object sender, EventArgs e)
        {
            await _refreshApplicationTypesDataList();
            _refreshDataGridView(_applicationTypesList);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private  async Task _refreshApplicationTypesDataList()
        {
            _applicationTypesList = null;
            try
            {
                _applicationTypesList = await _applicationsTypeService.GetAllApplicationTypes();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _refreshDataGridView(List<ApplicationType> source)
        {
            dgvListOfApplicationsTypes.DataSource = null;
            dgvListOfApplicationsTypes.DataSource = source;
            lblRecordsCount.Text = source.Count.ToString();
        }
        private async void editeApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_validateSelectedApplicationType())
            {
                return;
            }

            int selectedApplicationTypeID = (int)dgvListOfApplicationsTypes.CurrentRow.Cells[0].Value;
            frmUpdateApplicationTypeInfo frmUpdateApplicationTypeInfo = new frmUpdateApplicationTypeInfo(selectedApplicationTypeID);
            frmUpdateApplicationTypeInfo.ShowDialog();
            await _refreshApplicationTypesDataList();
            _refreshDataGridView(_applicationTypesList);
        }

        private bool _validateSelectedApplicationType()
        {
            int selectrdID = (int)dgvListOfApplicationsTypes.CurrentRow.Cells[0].Value;
            if (selectrdID == 0)
            {
                MessageBox.Show("Please select an application type to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
    }
}
