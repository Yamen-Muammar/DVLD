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

        private int FormPersonId { get; set; }
        private Person PersonInfo { get; set; }        

        public ctrlAddOrUpdatePerson()
        {
            InitializeComponent();

            FormPersonId = -1;

            Mode = enMode.eAdd;
            PersonInfo = new Person();
            PersonInfo.PersonID = -1; 
        }

        public ctrlAddOrUpdatePerson(int id)
        {
            InitializeComponent();
            FormPersonId = id;

            Mode = (FormPersonId ==-1) ? enMode.eAdd :enMode.eUpdate;            
        }

        private void ctrlAddOrUpdatePerson_Load(object sender, EventArgs e)
        {
            if (Mode == enMode.eUpdate)
            {
                lblTitle.Text = "Update Person";

                PersonInfo = PersonService.Find(FormPersonId);
                if (PersonInfo == null) { MessageBox.Show("Person Not Found", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                _loadDataInForm();               
            }

            // UI Load Logic.
            dateTimePicker.MaxDate = DateTime.Now.AddYears(-18);
            _loadCountriesCB();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //TODO: Save the person information to the database, if PersonId is -1 then add a new person, otherwise update the existing person
            if (!_loadDataInPersonInfo())
            {
                MessageBox.Show("Please fill all the required fields and set an image", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                if (Mode == enMode.eAdd)
                {
                    try
                    {
                        int InsertedPersonId = PersonService.AddPerson(PersonInfo);
                        if (InsertedPersonId == -1)
                        {
                            MessageBox.Show("Failed to save person information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            Mode = enMode.eUpdate;
                            MessageBox.Show("Person information saved successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            PersonInfo = null;
                            PersonInfo = PersonService.Find(InsertedPersonId);
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
                    if (PersonService.Update(PersonInfo))
                    {
                        MessageBox.Show("Person information updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to update person information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            // TODO: Set the person Image to person Object            
            string imagePath = GetImagePath();
           
            if (imagePath!=string.Empty)
            {
                PersonInfo.ImageName = imagePath;
                pbPersonImage.Image = LoadImageWithoutLock(imagePath);
            }            
        }       
        public Image LoadImageWithoutLock(string path)
        {
            // 1. Read all bytes from the file. 
            // This opens the file, reads it, and CLOSES it immediately.
            byte[] imageBytes = File.ReadAllBytes(path);

            // 2. Create a stream from the bytes in memory
            MemoryStream ms = new MemoryStream(imageBytes);
            // 3. Create the image from that memory stream
            return Image.FromStream(ms);
        }

        private string GetImagePath()
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
        private void btnRemove_Click(object sender, EventArgs e)
        {
            //TODO: Remove the person Image
            _clearPictureBox();
            rbGenderMale_CheckedChanged(sender, e);
            PersonInfo.ImageName = null;
        }

        private void tbNationalNo_TextChanged(object sender, EventArgs e)
        {
            //TODO: Check if the national number is valid and if it already exists in the database
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
                    pbPersonImage.Image = Image.FromFile(imagePath);
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine("** Error in LoadDataInForm : " + ex +" **" );                
            }
        }
        private bool _loadDataInPersonInfo()
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

            int countryID = CountryService.GetCountry(cbCountry.SelectedItem.ToString()).CountryID;            
            PersonInfo.Country_ID = countryID;            

            return true;

        }
        private bool _validateUIPersonInfo()
        {

            if (string.IsNullOrEmpty(tbFirstName.Text) || string.IsNullOrEmpty(tbSecondName.Text)
                || string.IsNullOrEmpty(tbThirdName.Text) || string.IsNullOrEmpty(tbLastName.Text)
                || string.IsNullOrEmpty(tbNationalNo.Text) || string.IsNullOrEmpty(tbEmail.Text) ||
                string.IsNullOrEmpty(tbAddress.Text))
            {
                return false;
            }

            if (pbPersonImage.Image == null)
            {
                return false;
            }

            //bool isAgeValid = DateTime.Now.Year - person.DateOfBirth.Year >= 18;
            //if (!isAgeValid)
            //{
            //    return false;
            //}


            return true;
        }
        private void _loadCountriesCB()
        {
            List<Country> countriesList = CountryService.GetAllCountries();

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
                string countryName = CountryService.GetCountry(PersonInfo.Country_ID).CountryName;
                cbCountry.SelectedIndex = cbCountry.FindString(countryName);
            }

            
        }

        private void _clearPictureBox()
        {
            pbPersonImage.Image?.Dispose();
            pbPersonImage.Image = null;
        }

        private void tbNationalNo_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbNationalNo.Text))
            {
                return;
            }

            if (Mode == enMode.eAdd)
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
            
        }
    }
}
