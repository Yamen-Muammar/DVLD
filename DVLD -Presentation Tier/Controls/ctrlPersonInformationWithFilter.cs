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
using DVLD__Presentation_Tier.Forms;

namespace DVLD__Presentation_Tier.Controls
{
    public partial class ctrlPersonInformationWithFilter : UserControl
    {
        private Person PersonInfo { get; set; }
        public ctrlPersonInformationWithFilter()
        {
            InitializeComponent();
        }

        private void ctrlPersonInformationWithFilter_Load(object sender, EventArgs e)
        {
            _loadComboBox();
        }
        private void btnSerach_Click(object sender, EventArgs e)
        {            
            PersonInfo = RetrivePersonInfoOnSelectedFilter();

            if (PersonInfo == null)
            {
                MessageBox.Show("No Person Found","Alter",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            UpdatePersonInfoANDRefreshUI(PersonInfo);
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddOrUpdatePersonInfo frmAddOrUpdatePersonInfoObj = new frmAddOrUpdatePersonInfo();
            frmAddOrUpdatePersonInfoObj.ctrlAddOrUpdatePerson1.ReturnPersonObject_OnClose += OnRetrundDataEvent;
            frmAddOrUpdatePersonInfoObj.ShowDialog();
        }

        private void OnRetrundDataEvent(Person person)
        {
            //todo : Updat the ui after colse the add or update form
            UpdatePersonInfoANDRefreshUI(person);
        }

        private void UpdatePersonInfoANDRefreshUI(Person person)
        {
            PersonInfo = person;
            ctrlPersonInformation1.UpdatePersonInfoANDRefreshUI(person);
        }

        private Person RetrivePersonInfoOnSelectedFilter()
        {
            Person person = null;
            if(cbFilterOn.SelectedItem.ToString() == "Person ID")
            {
                int personID = int.Parse(tbFilterInput.Text);
                person = PersonService.Find(personID);
            }

            if (cbFilterOn.SelectedItem.ToString() == "National NO")
            {
                string nationalNO = tbFilterInput.Text;
                person = PersonService.Find(nationalNO);
            }

            return person;
            
        }

        private void _loadComboBox()
        {
            List<string> FilterOptions = new List<string>() { "None","Person ID", "National NO" };
            cbFilterOn.DataSource = FilterOptions;
            cbFilterOn.SelectedIndex = 0;
        }

        private void cbFilterOn_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbFilterInput.Text = string.Empty;

            if (cbFilterOn.SelectedItem.ToString() == "None")
            {                                
                tbFilterInput.Enabled = false;
            }
            else
            {
                tbFilterInput.Enabled = true;
            }
        }        
    }
}
