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
using DVLD__Presentation_Tier.Forms.License_Forms;

namespace DVLD__Presentation_Tier.Forms.DetainedLicenseForms
{
    public partial class frmManageDeainedLicense : Form
    {
        private DetainedLicenseService _detainedLicenseService;
        private List<DetainedLicense> _detainedLicensesList;
        public frmManageDeainedLicense()
        {
            InitializeComponent();
        }

        private async void frmManageDeainedLicense_Load(object sender, EventArgs e)
        {
            _loadComboBox();
            _detainedLicenseService = new DetainedLicenseService();
            await _refreshDatainDGV();

        }

        private async Task _refreshDatainDGV()
        {
            _detainedLicensesList = await _getAllDetainedLicenses();
            _bindDataToDGV(_detainedLicensesList);

        }
        private void _bindDataToDGV(List<DetainedLicense> source)
        {
            this.dgvDetainedLicense.DataSource = null;
            this.dgvDetainedLicense.DataSource = source;
            this.lblRecordsCount.Text = source.Count.ToString();
        }

        private async Task<List<DetainedLicense>> _getAllDetainedLicenses()
        {
            return await _detainedLicenseService.GetAllDetainedLicenses();
        }

        private async void btnAddDeateinLicense_Click(object sender, EventArgs e)
        {
            frmDetainLicense frmDetainLicense = new frmDetainLicense();
            frmDetainLicense.ShowDialog();
             await   _refreshDatainDGV();
        }

        // filter data

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (_detainedLicensesList == null)
            {
                return;
            }

            if (string.IsNullOrEmpty( tbFilterInput.Text) || string.IsNullOrWhiteSpace(tbFilterInput.Text))
            {
                _bindDataToDGV(_detainedLicensesList);
                return;
            }
            int searchTerm = -1;

            if (int.TryParse(tbFilterInput.Text.ToString(), out searchTerm) && searchTerm != -1)
            {
                List<DetainedLicense> filteredList = _detainedLicensesList.Where(dl =>
                dl.License_ID.Equals(searchTerm)).ToList();

                _bindDataToDGV(filteredList);
            }
        }   

        private void _loadComboBox()
        {
            List<string> filterOptions = new List<string>()
            {
                "License ID",
            };
            cbFilterOn.DataSource = filterOptions;
        }

        private void showLicenseDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int licenseID = (int)dgvDetainedLicense.CurrentRow?.Cells["License_ID"].Value;
            frmLicenseInformation frm = new frmLicenseInformation(licenseID);
            frm.ShowDialog();   
        }
    }
}
