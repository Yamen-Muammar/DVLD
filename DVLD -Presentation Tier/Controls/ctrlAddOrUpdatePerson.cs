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
        private string ImagePath { get; set; }
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

            if (FormPersonId == -1)
            {
                Mode = enMode.eAdd;
            }
            else
            {
                Mode = enMode.eUpdate;
                PersonInfo = PersonService.Find(FormPersonId);
            }
        }

        private void ctrlAddOrUpdatePerson_Load(object sender, EventArgs e)
        {
            // UI Load Logic.
            dateTimePicker.MaxDate = DateTime.Now.AddYears(-18);

            if (Mode == enMode.eUpdate)
            {
                lblTitle.Text = "Update Person";
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
            PersonInfo.ImagePath = imagePath;
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
            PersonInfo.ImagePath = string.Empty;
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

        private bool ValidatePersonInfo()
        {

            return false;
        }
    }
}
