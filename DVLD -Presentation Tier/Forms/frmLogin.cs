using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            bool isLoginSuccessful = false; // Placeholder for login success status

            if (isLoginSuccessful == true)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
