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
            ShowAfterLoginSuccessForm();
        }

        private void ShowAfterLoginSuccessForm()
        {
            MainForm mainForm = new MainForm();
            mainForm.Show();
        }
    }
}
