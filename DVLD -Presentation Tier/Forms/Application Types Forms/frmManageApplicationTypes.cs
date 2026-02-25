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
        public frmManageApplicationTypes()
        {
            InitializeComponent();
        }

        private void frmManageApplicationTypes_Load(object sender, EventArgs e)
        {
            _refreshApplicationTypes();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private  void _refreshApplicationTypes()
        {
            _applicationTypesList = null;
            _applicationTypesList = ApplicationsTypeService.GetAllApplicationTypes();
            dgvListOfApplicationsTypes.DataSource = null;
            dgvListOfApplicationsTypes.DataSource = _applicationTypesList;
            lblRecordsCount.Text = _applicationTypesList.Count.ToString();
        }

        private void editeApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_validateSelectedApplicationType())
            {
                return;
            }

            int selectedApplicationTypeID = (int)dgvListOfApplicationsTypes.CurrentRow.Cells[0].Value;
            frmUpdateApplicationTypeInfo frmUpdateApplicationTypeInfo = new frmUpdateApplicationTypeInfo(selectedApplicationTypeID);
            frmUpdateApplicationTypeInfo.ShowDialog();
            _refreshApplicationTypes();

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
