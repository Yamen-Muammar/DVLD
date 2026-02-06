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
            eAdd = 1,eUpdate = 2
        }
        private enMode Mode { get; set; }

        private int PersonId { get; set; }

        public ctrlAddOrUpdatePerson()
        {
            InitializeComponent();

            PersonId = -1;

            Mode = enMode.eAdd;
        }

        public ctrlAddOrUpdatePerson(int id)
        {
            InitializeComponent();
            PersonId = id;

            Mode = (PersonId == -1)?enMode.eAdd : enMode.eUpdate;
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
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseEvent();
        }

        private void btnSetImage_Click(object sender, EventArgs e)
        {
            // TODO: Set the person Image
            string imagePath = GetImagePath();
            string copiedFileName = CopyImage(imagePath);
            if (copiedFileName != string.Empty)
            {
                MessageBox.Show("Image set successfully." + copiedFileName, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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

        private string CopyImage(string sourcePath)
        {
            if (string.IsNullOrEmpty(sourcePath))
            {
                MessageBox.Show("No image selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }

            string fileExtension = Path.GetExtension(sourcePath);
            string sourceFileName = Guid.NewGuid().ToString()+fileExtension;
            string destinationPath = Path.Combine(@"F:\yamen - 2024\C#\Course\projects\PersonPic", sourceFileName);

            try
            {
                File.Copy(sourcePath, destinationPath, true);
                return sourceFileName;
            }
            catch (Exception ex)
            {

                return string.Empty;
            }
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            //TODO: Remove the person Image
            rbGenderMale_CheckedChanged(sender,e);
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
    }
}
