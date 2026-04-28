using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD__Business_Tier.Services;

namespace DVLD__Presentation_Tier.Forms
{
    public partial class frmLogin : Form
    {
        private UserService _userService;
        public frmLogin()
        {
            InitializeComponent();
            _userService= new UserService();
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

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            _startLoading(true);
            btnLogin.Enabled = false;
            btnLogin.Visible = false;
            
            bool isLoginSuccessful = false;

            try
            {
                if (!_validationInput())
                {
                    throw new Exception("Please Fill All Fields With Right Inforamtion");
                }
                string username = tbUsername.Text;
                string password = tbPassword.Text;
                bool isRememberMeChecked = cbRemaindme.Checked;
   
                isLoginSuccessful = await _userService.Login(username, password, isRememberMeChecked);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _startLoading(false);
                btnLogin.Visible = true;
                btnLogin.Enabled = true;
            }
            

            if (isLoginSuccessful == true)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }            
        }

        private bool _validationInput()
        {
            if (string.IsNullOrEmpty(tbUsername.Text) || string.IsNullOrEmpty(tbPassword.Text))
            {
                return false;
            }
            return true;
        } 

        private void _loadSavedDataInfoAtFrom()
        {
            try
            {
                List<string> data = _userService.GetRemaindMeInfo();
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

        private void _startLoading(bool enable)
        {
           
            guna2ProgressIndicator1.Visible = enable;
            guna2ProgressIndicator1.Enabled = enable;
            if (enable)
            {
                guna2ProgressIndicator1.Start();
            }
            else
            {
                guna2ProgressIndicator1.Stop();
            }
        }
    }
}
