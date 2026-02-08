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
        }

        public ctrlAddOrUpdatePerson(int id)
        {
            InitializeComponent();
            FormPersonId = id;

            Mode = (FormPersonId ==-1) ? enMode.eAdd :enMode.eUpdate;            
        }

        private void ctrlAddOrUpdatePerson_Load(object sender, EventArgs e)
        {
            // UI Load Logic.
            dateTimePicker.MaxDate = DateTime.Now.AddYears(-18);

            if (Mode == enMode.eUpdate)
            {
                lblTitle.Text = "Update Person";

                PersonInfo = PersonService.Find(FormPersonId);
                if (PersonInfo == null) { MessageBox.Show("Person Not Found", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                LoadDataInForm();

                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //TODO: Save the person information to the database, if PersonId is -1 then add a new person, otherwise update the existing person
            if (Mode == enMode.eAdd)
            {
                PersonService.AddPerson(PersonInfo);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseEvent();
        }

        private void btnSetImage_Click(object sender, EventArgs e)
        {
            // TODO: Set the person Image to person Object            
            string imagePath = GetImagePath();
            PersonInfo.ImageName = imagePath;
            pbPersonImage.Image = Image.FromFile(imagePath);

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

        private void LoadDataInForm()
        {
            lblPersonID.Text = PersonInfo.PersonID.ToString();
            tbFirstName.Text = PersonInfo.FirstName;
            tbSecondName.Text = PersonInfo.SecondName;
            tbThirdName.Text = PersonInfo.ThirdName;
            tbLastName.Text = PersonInfo.LastName;
            tbNationalNo.Text = PersonInfo.NationalNO;
            dateTimePicker.Value = PersonInfo.DateOfBirth;
            tbEmail.Text = PersonInfo.Email;
            tbPhone.Text = PersonInfo.Phone;
            if (PersonInfo.Gender == "Male")
            {
                rbGenderMale.Checked = true;
            }
            else
            {
                rbGenderFemale.Checked = true;
            }
            //TODO:Get the country name from the database using the Country_ID and set it to the combo box
            cbCountry.SelectedIndex = cbCountry.FindString("CountryName");
            if (!string.IsNullOrEmpty(PersonInfo.ImageName))
            {
                string imagePath = Path.Combine(@"F:\yamen - 2024\C#\Course\projects\PersonPic", PersonInfo.ImageName);
                pbPersonImage.Image = Image.FromFile(imagePath);
            }
            tbAddress.Text = PersonInfo.Address;
        }
        private bool LoadDataInPersonInfo()
        {
            return false;
        }
        private bool ValidatePersonInfo()
        {

            return false;
        }
    }
}
