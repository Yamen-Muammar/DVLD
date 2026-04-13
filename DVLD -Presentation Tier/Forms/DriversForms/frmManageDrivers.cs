using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.Design.WebControls;
using System.Windows.Forms;
using DVLD__Business_Tier.Services;
using DVLD__Core.View_Models;
using DVLD__Presentation_Tier.Forms.License_Forms;

namespace DVLD__Presentation_Tier.Forms.DriversForms
{
    public partial class frmManageDrivers : Form
    {
        private DriverService _driverService;
        private List<clsDriversView> _driversList;
        public frmManageDrivers()
        {
            InitializeComponent();
        }

        private async void frmManageDrivers_Load(object sender, EventArgs e)
        {
            _driverService = new DriverService();
            await _refreshDriversGrid();
            UpdateListCounter();
        }

        private async Task _refreshDrivers()
        {
            _driversList = await _getAllDriversAsync(); 
        }

        private async Task _refreshDriversGrid()
        {
            await _refreshDrivers();
            dgvDriversList.DataSource = null;
            dgvDriversList.DataSource = _driversList;
        }
        private async Task<List<clsDriversView>> _getAllDriversAsync()
        {
            try
            {
                return await _driverService.GetDriversListAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while fetching drivers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<clsDriversView>();
            }
        }

        private void UpdateListCounter()
        {
            lblRecordsCount.Text = _driversList.Count.ToString();
        }

        private void personLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string noationalID = (string)dgvDriversList.CurrentRow.Cells["NationalNO"].Value;
            frmLicenseHistory frmLicense = new frmLicenseHistory(noationalID);
            frmLicense.ShowDialog();

        }
    }
}
