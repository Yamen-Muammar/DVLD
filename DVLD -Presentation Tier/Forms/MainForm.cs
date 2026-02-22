using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD__Presentation_Tier.Forms;
using DVLD__Presentation_Tier.Forms.UserForms;
using DVLD__Core;
namespace DVLD__Presentation_Tier
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPeopleListWithFilter frmPeopleListWithFilter = new frmPeopleListWithFilter();
            frmPeopleListWithFilter.ShowDialog();
        }

        private void currentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frmUserInfo = new frmUserInfo();
            frmUserInfo.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.User = null;
            Application.Restart();

        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageUsers frmManageUsers = new frmManageUsers();
            frmManageUsers.ShowDialog();
        }
    }
}
