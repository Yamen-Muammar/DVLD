using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD__Business_Tier.Services;
using DVLD__Core.Models;
using DVLD__Presentation_Tier.Forms;

namespace DVLD__Presentation_Tier
{
    public partial class ctrlPersonInformation : UserControl
    {
        public event Action OnClose_Clicked;
        protected virtual void CloseEvent()
        {
            Action handler = OnClose_Clicked;
            if (handler != null)
            {
                handler.Invoke();
            }
        }

        private int PersonId;

        private Person PersonInfo {  get; set; }
        public ctrlPersonInformation()
        {
            InitializeComponent();
        }

        public ctrlPersonInformation(int personId)
        {
            InitializeComponent();
            PersonId = personId;
            PersonInfo = PersonService.Find(PersonId);
            if (PersonInfo == null)
            {
                MessageBox.Show("Person Not Found","Alert",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void ctrlPersonInformation_Load(object sender, EventArgs e)
        {
            _loadDataInForm();
        }

        private void btnEditePersonInfo_Click(object sender, EventArgs e)
        {
            frmAddOrUpdatePersonInfo frmAddOrUpdatePersonInfoObj = new frmAddOrUpdatePersonInfo(PersonId);
            frmAddOrUpdatePersonInfoObj.ctrlAddOrUpdatePerson1.ReturnPersonObject_OnClose += OnRetrundDataEvent;
            frmAddOrUpdatePersonInfoObj.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseEvent();
        }

        private void _loadDataInForm()
        {
            if (PersonInfo == null)
            {
                return;
            }
            lblPersonID.Text = PersonInfo.PersonID.ToString();
            lblFullName.Text = PersonInfo.FirstName + " "+PersonInfo.MiddelName+" "+ PersonInfo.ThirdName+" " + PersonInfo.LastName;
            lblNationalNo.Text = PersonInfo.NationalNO;
            lblGender.Text = PersonInfo.Gender;
            lblPhoneNumber.Text = PersonInfo.Phone;
            lblEmail.Text = PersonInfo.Email;
            lblAddress.Text = PersonInfo.Address;
            lblCountry.Text = GetCountryName();
            lblDateOfBirth.Text = PersonInfo.DateOfBirth.ToString("d");

            if (PersonInfo.ImageName != "")
            {
                string imagePath = Path.Combine(@"F:\yamen - 2024\C#\Course\projects\PersonPic", PersonInfo.ImageName);
                if (File.Exists(imagePath))
                {
                    pbImage.Image = Image.FromFile(imagePath);
                }
                
            }
           
        }

        private string GetCountryName()
        {
            //TODO:Get Country Name From Data Base >> Based on Country_ID;
            string countryName = CountryService.GetCountry(PersonInfo.Country_ID).CountryName;
            return countryName;
        }

        public void OnRetrundDataEvent(Person person)
        {
            PersonInfo = null;
            PersonInfo = person;
            _loadDataInForm();
        }
        
        
    }
}
