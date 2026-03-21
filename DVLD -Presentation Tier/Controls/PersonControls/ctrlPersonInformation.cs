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
        protected virtual void TriggerCloseEvent()
        {
            Action handler = OnClose_Clicked;
            if (handler != null)
            {
                handler.Invoke();
            }
        }

        //for send the id to the AddOrUpdate form.
        private int _personId;
        private CountryService _countryService;
        private Person PersonInfo { get; set; }
        public ctrlPersonInformation()
        {
            InitializeComponent();
            _countryService = new CountryService();
        }

        public ctrlPersonInformation(int personId)
        {
            InitializeComponent();
            _countryService = new CountryService();
            SetPersonInfo(personId);
            if (PersonInfo == null)
            {
                return;
            }
            _personId = PersonInfo.PersonID;
        }

        private async void ctrlPersonInformation_Load(object sender, EventArgs e)
        {
            if (PersonInfo == null)
            {
                return;
            }
            await _loadDataInForm();
        }

        private Person _getPerson(int personId)
        {
            Person personInfo = null;
            personInfo = PersonService.Find(personId);
            return personInfo;
        }


        //Button Events
        private void btnEditePersonInfo_Click(object sender, EventArgs e)
        {
            if (PersonInfo == null)
            {
                MessageBox.Show("No Person Info To Edit", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frmAddOrUpdatePersonInfo frmAddOrUpdatePersonInfoObj = new frmAddOrUpdatePersonInfo(_personId);
            frmAddOrUpdatePersonInfoObj.ctrlAddOrUpdatePerson1.ReturnPersonObject_OnClose += OnRetrundDataEvent;
            frmAddOrUpdatePersonInfoObj.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            TriggerCloseEvent();
        }

        // Helper Methods
        private async Task _loadDataInForm()
        {
            if (PersonInfo == null)
            {
                return;
            }
            lblPersonID.Text = PersonInfo.PersonID.ToString();
            lblFullName.Text = PersonInfo.FirstName + " " + PersonInfo.MiddelName + " " + PersonInfo.ThirdName + " " + PersonInfo.LastName;
            lblNationalNo.Text = PersonInfo.NationalNO;
            lblGender.Text = PersonInfo.Gender;
            lblPhoneNumber.Text = PersonInfo.Phone;
            lblEmail.Text = PersonInfo.Email;
            lblAddress.Text = PersonInfo.Address;
            lblCountry.Text = await _getCountryNameOnPersonID();
            lblDateOfBirth.Text = PersonInfo.DateOfBirth.ToString("d");

            if (PersonInfo.ImageName != "")
            {
                string imagePath = Path.Combine(@"F:\yamen - 2024\C#\Course\projects\PersonPic", PersonInfo.ImageName);
                if (File.Exists(imagePath))
                {
                    pbImage.Image = _loadImageWithoutLock(imagePath);
                }

            }

        }
        private Image _loadImageWithoutLock(string path)
        {
            // 1. Read all bytes from the file. 
            // This opens the file, reads it, and CLOSES it immediately.
            byte[] imageBytes = File.ReadAllBytes(path);

            // 2. Create a stream from the bytes in memory
            MemoryStream ms = new MemoryStream(imageBytes);
            // 3. Create the image from that memory stream
            return Image.FromStream(ms);
        }
        private async Task<string> _getCountryNameOnPersonID()
        {
            string countryName = string.Empty;
            try
            {
                 countryName = (await _countryService.GetCountry(PersonInfo.Country_ID)).CountryName;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            return countryName;
        }
        private void SetPersonInfo(int PersonID)
        {
            try
            {
                PersonInfo = _getPerson(PersonID);                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);                              
            }
        }

        //OUTSIDE CALLs TO UPDATE PERSON INFO IN THIS CONTROL AND REFRESH THE UI
        public async void UpdatePersonInfoANDRefreshUI(Person person)
        {
            if (person == null)
            {
                MessageBox.Show("Error While Handel Person Info , try Again Later", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            PersonInfo = person;
            _personId = PersonInfo.PersonID;
             await _loadDataInForm();
        }
        public async void OnRetrundDataEvent(Person person)
        {
            if (person == null)
            {
                MessageBox.Show("Error While Handel Person Info , try Again Later", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            PersonInfo = null;
            PersonInfo = person;
            await _loadDataInForm();
        }
    }
}
