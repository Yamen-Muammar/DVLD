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

namespace DVLD__Presentation_Tier.Forms
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        //Event Handeling 
        private void frmLogin_Load(object sender, EventArgs e)
        {
            _loadSavedDataInfoAtFrom();
        }

        //Button Events 
        private void btnShowPassword_Click(object sender, EventArgs e)
        {
            if (tbPassword.UseSystemPasswordChar == true)
            {
                tbPassword.UseSystemPasswordChar = false;
            }
            else
            {
                tbPassword.UseSystemPasswordChar = true;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {            
            //TODO: Implement actual login logic here, such as validating the username and password against a database or an authentication service.
            string username = tbUsername.Text;
            string password = tbPassword.Text;
            bool isRememberMeChecked = cbRemaindme.Checked;
            bool isLoginSuccessful = false;

            try
            {
                isLoginSuccessful = UserService.Login(username, password, isRememberMeChecked); // Placeholder for login success status
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

            if (isLoginSuccessful == true)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }            
        }

        private void _loadSavedDataInfoAtFrom()
        {
            try
            {
                List<string> data = UserService.GetRemaindInfo();
                if (data != null && data.Count > 0)
                {
                    tbUsername.Text = data[0];
                    tbPassword.Text = data[1];
                }
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
