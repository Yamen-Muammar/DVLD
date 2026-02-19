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
            //ShowAfterLoginSuccessForm();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ShowAfterLoginSuccessForm()
        {
            //this.Close();
            
            //MainForm mainForm = new MainForm();            
            //mainForm.Show();
        }
    }
}
