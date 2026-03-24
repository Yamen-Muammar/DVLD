using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD__Presentation_Tier.Forms;
using DVLD__Business_Tier.Services;
using DVLD__Core.Models;
using DVLD__Core.View_Models;
namespace DVLD__Presentation_Tier
{
    public partial class crtlPeopleListWithFilter : UserControl
    {        
        private List<clsPersonView> people { get; set; }
        private PersonService _personService;
        public crtlPeopleListWithFilter()
        {
            InitializeComponent(); 
            _personService = new PersonService();
        }
        private async void crtlPeopleListWithFilter_Load(object sender, EventArgs e)
        {            
            await _RefreshData();
            _loadFilterComboBoxItems();
        }

        //Button Click Events
        private void btnClose_Click(object sender, EventArgs e)
        {
            RaiseCloseClickedEvent();
        }
        private async void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddOrUpdatePersonInfo frmAddOrUpdatePersonInfo = new frmAddOrUpdatePersonInfo();
            frmAddOrUpdatePersonInfo.ShowDialog();
            await _RefreshData();
        }
        private async void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddOrUpdatePersonInfo frmAddOrUpdatePersonInfo = new frmAddOrUpdatePersonInfo();
            frmAddOrUpdatePersonInfo.ShowDialog();
            await _RefreshData();
        }
        private async void editeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonId = (int)dgvPeopleList.CurrentRow.Cells[0].Value;
            frmAddOrUpdatePersonInfo frmAddOrUpdatePersonInfo = new frmAddOrUpdatePersonInfo(PersonId);
            frmAddOrUpdatePersonInfo.ShowDialog();
            await _RefreshData();
        }
        private async void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            if (MessageBox.Show("Are You Sure ?","Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            int PersonId = (int)dgvPeopleList.CurrentRow.Cells[0].Value;
            try
            {
                if (await _personService.Delete(PersonId))
                {
                    MessageBox.Show("Person Deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await _RefreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
           
        }
        private async void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int PersonId = (int)dgvPeopleList.CurrentRow.Cells[0].Value;
            frmPersonInformation frmPersonInformation = new frmPersonInformation(PersonId);
            frmPersonInformation.ShowDialog();
            await _RefreshData();
        }
        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented yet.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented yet.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Events
        private void cbFilterOn_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbFilterInput.Text = string.Empty;
            tbFilterInput.Enabled = (cbFilterOn.SelectedItem.ToString() == "None") ? false:true;

            if (cbFilterOn.SelectedItem.ToString() == "None")
            {
                dgvPeopleList.DataSource = people;                
            }
        }        
        private void NumberValidation(object sender, KeyPressEventArgs e)
        {
            if (cbFilterOn.SelectedItem.ToString() != "PersonID")
            {
                return;
            }

            // Allow digits, backspace.
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Block the key
            }            
        }

        public event Action OnCloseButtonClicked;
        protected virtual void RaiseCloseClickedEvent()
        {
            Action handler = OnCloseButtonClicked;
            if (handler != null)
            {
                handler.Invoke();
            }
        }

        private void tbFilterInput_TextChanged(object sender, EventArgs e)
        {
            string filterValue = tbFilterInput.Text;

            List<clsPersonView> filteredPeopleList = new List<clsPersonView>();                     

            if (string.IsNullOrEmpty(filterValue) || cbFilterOn.SelectedItem.ToString() == "None")
            {
                filteredPeopleList.Clear();
                _refreshDGVDataSource(people);
                return;
            }

            switch (cbFilterOn.SelectedItem.ToString())
            {
                case "PersonID":
                    if (int.TryParse(filterValue, out int personId))
                    {
                        filteredPeopleList = people.Where(p => p.PersonID == personId).ToList();
                    }
                    break;
                case "Country Name":
                    filteredPeopleList = people.Where(p => p.CountryName.ToLower().Trim().Contains(filterValue.Trim().ToLower())).ToList();
                    break;
                case "First Name":
                    filteredPeopleList = people.Where(p => p.FirstName.Contains(filterValue)).ToList();
                    break;
                case "NationalNo.":
                    filteredPeopleList = people.Where(p => p.NationalNO.Contains(filterValue.ToUpper())).ToList();
                    break;

                default:
                    break;
            }

            _refreshDGVDataSource(filteredPeopleList);
        }

        // Helper Methods
        private async Task _RefreshData()
        {
            try
            {
                people = await _personService.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            _refreshDGVDataSource(people);
        }

        private void _refreshDGVDataSource(List<clsPersonView> source)
        {
            dgvPeopleList.DataSource = source;
            lblRecordsCount.Text = source.Count.ToString();
           
        }

        private void _loadFilterComboBoxItems()
        {
            List<string> filters = new List<string> { "None","NationalNo.", "PersonID", "Country Name", "First Name" };
            foreach (string filter in filters)
            {
                cbFilterOn.Items.Add(filter);
            }
            cbFilterOn.SelectedIndex = 0;
        }
    }
}
