using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD__Business_Tier;
using DVLD__Business_Tier.Services;
using DVLD__Core.Models;

namespace DVLD__Presentation_Tier.Forms.UserForms
{
    public partial class frmAddNewUser : Form
    {
        private int _personID { get; set; }
        public frmAddNewUser()
        {
            InitializeComponent();
            ctrlPersonInformationWithFilter1.ReturnPersonID_OnFindPerson += OnReturnPersonID_OnFindPerson;
        }

        // Button Events
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_validateInputs())
            {
                return;
            }

            User user = _loadInfoInUserObject();

            if (user == null)
            {
                MessageBox.Show("An error occurred while loading user information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            try
            {
                int NewSavedUserID = UserService.AddNewUser(user);
                if (NewSavedUserID != -1)
                {
                    MessageBox.Show("User saved successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = LoginInfo;

        }

        private void tbConfirmPassword_Leave(object sender, EventArgs e)
        {
            if(tbConfirmPassword.Text != tbPassword.Text)
            {
                string errorMessage = "Password and Confirm Password must be the same";
                errorProvider1.SetError(tbConfirmPassword, errorMessage);                
            }
        }

        private void OnReturnPersonID_OnFindPerson(int PersonID)
        {
            _personID = PersonID;
        }

        private bool _validateInputs()
        {
            if (_personID == 0 || _personID == -1 )
            {
                MessageBox.Show("Please select a person", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            
            if (string.IsNullOrEmpty(tbUsername.Text) || string.IsNullOrEmpty(tbPassword.Text) || string.IsNullOrEmpty(tbConfirmPassword.Text))
            {
                MessageBox.Show("Please fill all required fields", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (tbPassword.Text != tbConfirmPassword.Text )
            {
                MessageBox.Show("Password and Confirm Password must be the same", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private User _loadInfoInUserObject()
        {
            string username = tbUsername.Text;
            string HashedPassword = clsPasswordHasher.HashPassword(tbPassword.Text);
            bool isActive = cbIsActive.Checked;
            int personID = _personID;            


            return new User { Username=username,HashedPassword=HashedPassword,isActive=isActive,Person_ID =personID };
        }
    }
}
