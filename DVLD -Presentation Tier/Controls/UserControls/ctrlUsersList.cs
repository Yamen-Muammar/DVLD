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
using DVLD__Core.Models;
using DVLD__Core.View_Models;
using DVLD__Presentation_Tier.Forms.UserForms;

namespace DVLD__Presentation_Tier.Controls.UserControls
{
    public partial class ctrlUsersList : UserControl
    {
        private List<clsUserView> UsersList { get; set; }
        public ctrlUsersList()
        {
            InitializeComponent();            
        }

        private void ctrlUsersList_Load(object sender, EventArgs e)
        {
            _RefreshData();
        }
        private void _RefreshData()
        {
            UsersList = _getAllUsers();
            dgvUsersList.DataSource = null;
            dgvUsersList.DataSource = UsersList;
            lblRecordsCount.Text = UsersList.Count.ToString();
            
        }

        private List<clsUserView> _getAllUsers()
        {
            return UserService.GetAllUsers();
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddNewUser frmAddNewUser = new frmAddNewUser();
            frmAddNewUser.ShowDialog();
            _RefreshData();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserId = (int)dgvUsersList.CurrentRow.Cells[0].Value;
            frmUserInfo frmUserInfo = new frmUserInfo(UserId);
            frmUserInfo.ShowDialog();

        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewUser frmAddNewUser = new frmAddNewUser();
            frmAddNewUser.ShowDialog();
            _RefreshData();
        }

        private void editeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            int UserId = (int)dgvUsersList.CurrentRow.Cells[0].Value;
            try
            {
                if (UserService.DeleteUser(UserId))
                {
                    MessageBox.Show("User was deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshData();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }            
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Implemented Yet.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Implemented Yet.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
