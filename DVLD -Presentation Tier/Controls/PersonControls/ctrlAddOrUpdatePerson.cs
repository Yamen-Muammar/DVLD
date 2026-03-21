using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Ookii.Dialogs.WinForms;
using DVLD__Core.Models;
using DVLD__Business_Tier.Services;
using System.Diagnostics;
namespace DVLD__Presentation_Tier
{
    public partial class ctrlAddOrUpdatePerson : UserControl
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

        // Event for Send Person Object to the Subs.
        public delegate void ReturnPersonObject(Person person);
        public event ReturnPersonObject ReturnPersonObject_OnClose;
        private void TriggerReturnPersonEvent(Person person)
        {
            ReturnPersonObject_OnClose?.Invoke(person);
        }

        private enum enMode
        {
            eAdd = 1, eUpdate = 2
        }
        private enMode Mode { get; set; }
        private string _imagePath { get; set; }
        private int FormPersonId { get; set; }
        private Person PersonInfo { get; set; }

        private CountryService _countryService;
        public ctrlAddOrUpdatePerson()
        {
            InitializeComponent();
            _countryService = new CountryService();
            FormPersonId = -1;

            Mode = enMode.eAdd;
            PersonInfo = new Person();
            PersonInfo.PersonID = -1; 
        }

        public ctrlAddOrUpdatePerson(int id)
        {
            InitializeComponent();
            _countryService = new CountryService();
            FormPersonId = id;

            Mode = (FormPersonId == -1) ? enMode.eAdd :enMode.eUpdate;            
        }

        private void ctrlAddOrUpdatePerson_Load(object sender, EventArgs e)
        {
            if (Mode == enMode.eUpdate)
            {
                lblTitle.Text = "Update Person";
                try
                {
                    PersonInfo = _getPerson(FormPersonId);
                    if (PersonInfo == null) 
                    { 
                        MessageBox.Show("Person Not Found", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    _loadDataInForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  
                }        
            }

            // UI Load Logic.
            dateTimePicker.MaxDate = DateTime.Now.AddYears(-18);
            _loadCountriesCB();
        }

        // Button Event Handlers
        private async void btnSave_Click(object sender, EventArgs e)
        {            
            if (!await _loadDataInPersonInfoObject())
            {
                MessageBox.Show("Please fill all the required fields and set an image", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            if (Mode == enMode.eAdd)
            {
                try
                {
                    int InsertedPersonId = PersonService.AddPerson(PersonInfo);
                    if (InsertedPersonId == -1)
                    {
                        MessageBox.Show("Failed in Save Person Information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Mode = enMode.eUpdate;
                        MessageBox.Show("Person information Saved successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);                        
                        PersonInfo = _getPerson(InsertedPersonId);
                        if (PersonInfo == null)
                        { 
                            MessageBox.Show("Person Not Found after Insert", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        lblTitle.Text = "Update Person";
                        _loadDataInForm();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return;
            }

            if (Mode == enMode.eUpdate)
            {
                try
                {
                    PersonService.Update(PersonInfo);
                    MessageBox.Show("Person information updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                return;
            }
                       
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            _clearPictureBox();
            TriggerReturnPersonEvent(PersonInfo);
            CloseEvent();
        }
        private void btnSetImage_Click(object sender, EventArgs e)
        {
            
            string imagePath = _getImagePath();
           
            if (imagePath!=string.Empty)
            {
                _imagePath = imagePath;
                pbPersonImage.Image = _loadImageWithoutLock(imagePath);
            }            
        }       
        private void btnRemove_Click(object sender, EventArgs e)
        {            
            _clearPictureBox();
            rbGenderMale_CheckedChanged(sender, e);
            _imagePath= string.Empty;
        }        
        private void rbGenderMale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbGenderFemale.Checked)
            {
                pbPersonImage.Image = Properties.Resources.Female;
            }
            else
            {
                pbPersonImage.Image = Properties.Resources.Male512;
            }
        }


        // Load Functions
        private void _loadDataInForm()
        {
            try
            {
                lblPersonID.Text = PersonInfo.PersonID.ToString();
                tbFirstName.Text = PersonInfo.FirstName;
                tbSecondName.Text = PersonInfo.MiddelName;
                tbThirdName.Text = PersonInfo.ThirdName;
                tbLastName.Text = PersonInfo.LastName;
                tbNationalNo.Text = PersonInfo.NationalNO;               
                tbEmail.Text = PersonInfo.Email;
                tbPhone.Text = PersonInfo.Phone;
                tbAddress.Text = PersonInfo.Address;
                dateTimePicker.Value = PersonInfo.DateOfBirth;
                if (PersonInfo.Gender == "Male")
                {
                    rbGenderMale.Checked = true;
                }
                else
                {
                    rbGenderFemale.Checked = true;
                }
                
                // combo box Data load Sepirated in Private Function.

                if (!string.IsNullOrEmpty(PersonInfo.ImageName))
                {
                    string imagePath = Path.Combine(@"F:\yamen - 2024\C#\Course\projects\PersonPic", PersonInfo.ImageName);
                    pbPersonImage.Image = _loadImageWithoutLock(imagePath);
                    _imagePath = PersonInfo.ImageName;
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine("** Error in LoadDataInForm : " + ex +" **" );                
            }
        }
        private async Task<bool> _loadDataInPersonInfoObject()
        {
            if (!_validateUIPersonInfo())
            {
                return false;
            }
            
            PersonInfo.FirstName = tbFirstName.Text;
            PersonInfo.MiddelName = tbSecondName.Text;
            PersonInfo.ThirdName = tbThirdName.Text;
            PersonInfo.LastName = tbLastName.Text;
            PersonInfo.NationalNO = tbNationalNo.Text;
            PersonInfo.DateOfBirth = dateTimePicker.Value;
            PersonInfo.Email = tbEmail.Text;
            PersonInfo.Phone = tbPhone.Text;
            PersonInfo.Address = tbAddress.Text;
            PersonInfo.Gender = (rbGenderMale.Checked) ? "Male" : "Female";

            string selectedCountryName = cbCountry.SelectedItem.ToString();
            int countryID = await _getCountryIDOnName(selectedCountryName);
            if (countryID == -1)
            {
                return false;
            }

            PersonInfo.Country_ID = countryID;     
            PersonInfo.ImageName = _imagePath;
            return true;

        }        
        private async void _loadCountriesCB()
        {
            List<Country> countriesList =await _loadCountriesList();
            
            foreach (Country country in countriesList)
            {
                cbCountry.Items.Add(country.CountryName);                
            }

            if (PersonInfo.PersonID == -1)
            {
                cbCountry.SelectedIndex = cbCountry.FindString("Palestine State");
            }
            else
            {
                string countryName = await _getCountryNameOnPersonID();
                cbCountry.SelectedIndex = cbCountry.FindString(countryName);
            }

            
        }
        private async Task<int> _getCountryIDOnName(string selectedCountryName)
        {
            try
            {
                return (int)((await _countryService.GetCountry(selectedCountryName)).CountryID);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return -1;
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
        private async Task<List<Country>> _loadCountriesList()
        {
            List<Country> countriesList = new List<Country>();
            try
            {
                countriesList = await _countryService.GetAllCountries();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return countriesList;
        }

        private Person _getPerson(int personId)
        {
            Person personInfo = null;
            personInfo = PersonService.Find(personId);
            return personInfo;
        }

        //Image Handling Functions
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
        private string _getImagePath()
        {
            string selectedFilePath = "";
            Ookii.Dialogs.WinForms.VistaOpenFileDialog openFileDialog = new Ookii.Dialogs.WinForms.VistaOpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedFilePath = openFileDialog.FileName;
            }
            return selectedFilePath;
        }
        private void _clearPictureBox()
        {
            pbPersonImage.Image?.Dispose();
            pbPersonImage.Image = null;
        }

        // UI Validation Event Handlers
        private void tbNationalNo_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbNationalNo.Text))
            {
                return;
            }

            if (Mode == enMode.eAdd)
            {
                try
                {
                    if (PersonService.IsPersonExist(tbNationalNo.Text))
                    {
                        EPNationalNO.SetError(tbNationalNo, "This National Number already exists");
                        tbNationalNo.Focus();
                    }
                    else
                    {
                        EPNationalNO.SetError(tbNationalNo, string.Empty);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               
            }
            
        }
        private bool _validateUIPersonInfo()
        {

            if (string.IsNullOrEmpty(tbFirstName.Text) || string.IsNullOrEmpty(tbSecondName.Text)
                || string.IsNullOrEmpty(tbThirdName.Text) || string.IsNullOrEmpty(tbLastName.Text)
                || string.IsNullOrEmpty(tbNationalNo.Text) || string.IsNullOrEmpty(tbEmail.Text) ||
                string.IsNullOrEmpty(tbAddress.Text) || string.IsNullOrEmpty(_imagePath))
            {
                return false;
            }

            if (!tbEmail.Text.Contains("@"))
            {
                return false;
            }

            try
            {
                if (Mode == enMode.eAdd && PersonService.IsPersonExist(tbNationalNo.Text))
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return true;
        }
    }
}
