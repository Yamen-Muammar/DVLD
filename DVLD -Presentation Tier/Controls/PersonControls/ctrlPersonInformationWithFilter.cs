using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD__Business_Tier.Services;
using DVLD__Core.Models;
using DVLD__Presentation_Tier.Forms;
using DVLD__Presentation_Tier.Forms.PersonForms;

namespace DVLD__Presentation_Tier.Controls
{
    public partial class ctrlPersonInformationWithFilter : UserControl
    {
        private Person PersonInfo { get; set; }
        private PersonService _personService;

        //Event to return the person ID on find Peron
        
        public event EventHandler<int> ReturnPersonID_OnFindPerson;
        protected virtual void OnReturnPersonID_OnFindPerson(int PersonID)
        {
            ReturnPersonID_OnFindPerson?.Invoke(this,PersonID);
        }

        public ctrlPersonInformationWithFilter()
        {
            InitializeComponent();
       
        }

        private void ctrlPersonInformationWithFilter_Load(object sender, EventArgs e)
        {
            _loadComboBox();
        }
        private async void btnSerach_Click(object sender, EventArgs e)
        {
            PersonInfo = await RetrivePersonInfoOnSelectedFilter();

            if (PersonInfo == null)
            {
                return;
            }

            OnReturnPersonID_OnFindPerson(PersonInfo.PersonID);
            UpdatePersonInfoANDRefreshUI(PersonInfo);
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddOrUpdatePersonInfo frmAddOrUpdatePersonInfoObj = new frmAddOrUpdatePersonInfo();
            frmAddOrUpdatePersonInfoObj.ctrlAddOrUpdatePerson1.ReturnPersonObject_OnClose += OnRetrundDataEvent;
            frmAddOrUpdatePersonInfoObj.ShowDialog();
        }

        private void OnRetrundDataEvent(object sender,Person person)
        {
            UpdatePersonInfoANDRefreshUI(person);
            OnReturnPersonID_OnFindPerson(person.PersonID);
        }

        private void UpdatePersonInfoANDRefreshUI(Person person)
        {
            PersonInfo = person;
            ctrlPersonInformation1.UpdatePersonInfoANDRefreshUI(person);
            _restartFilterArea();
        }

        private async Task<Person> RetrivePersonInfoOnSelectedFilter()
        {
            _personService = new PersonService();
            Person person = null;
            try
            {
                if (cbFilterOn.SelectedItem.ToString() == "Person ID")
                {
                    int personID = int.Parse(tbFilterInput.Text);
                    person = await _personService.Find(personID);
                }

                if (cbFilterOn.SelectedItem.ToString() == "National NO")
                {
                    string nationalNO = tbFilterInput.Text;
                    person = await _personService.Find(nationalNO.ToUpper());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return person;
        }

        private async Task<Person> RetrivePersonInfoOnSelectedFilter(string filter)
        {
            _personService = new PersonService();
            Person person = null;
            try
            {
                if (filter == "Person ID")
                {
                    int personID = int.Parse(tbFilterInput.Text);
                    person = await _personService.Find(personID);
                }
                else
                if (filter == "National NO")
                {
                    string nationalNO = tbFilterInput.Text;
                    person = await _personService.Find(nationalNO.ToUpper());
                    }
                    else
                    {
                        throw new ArgumentException("Filter not Valid!");
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return person;
        }

        private void _loadComboBox()
        {
            List<string> FilterOptions = new List<string>() { "None", "Person ID", "National NO" };
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

        private void _restartFilterArea()
        {
            cbFilterOn.SelectedIndex = 0;

        }

        public async Task FindPersonByNationalNO(string nationalNO)
        {
            cbFilterOn.SelectedItem = "National NO";
            tbFilterInput.Text = nationalNO;
            cbFilterOn.Enabled = false;
            tbFilterInput.Enabled = false;
            btnSerach.Enabled = false;
            btnAddPerson.Enabled = false;

            PersonInfo = await RetrivePersonInfoOnSelectedFilter("National NO");

            if (PersonInfo == null)
            {
                return;
            }

            OnReturnPersonID_OnFindPerson(PersonInfo.PersonID);
            UpdatePersonInfoANDRefreshUI(PersonInfo);
        }
    }
}
