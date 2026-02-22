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
using DVLD__Core;
using DVLD__Core.Models;

namespace DVLD__Presentation_Tier.Controls.UserControls
{
    public partial class ctrlUserInfo : UserControl
    {
        private User UserInfo { get; set; }
        public ctrlUserInfo()
        {
            InitializeComponent();
            UserInfo = Global.User;
        }

        public ctrlUserInfo(int UserId)
        {
            InitializeComponent();
            UserInfo = _getUserInfo(UserId);
        }
        private void ctrlUserInfo_Load(object sender, EventArgs e)
        {      
            _loadUserInforamtionInForm(UserInfo);
        }
        private User _getUserInfo(int userId)
        {
            User userInfo = null;
            try
            {
                userInfo = UserService.GetUserById(userId);
                if (userInfo == null)
                {
                    MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while fetching user information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return userInfo;
        }

        private void _loadUserInforamtionInForm(User user)
        {
            if (user == null)
            {
                return;
            }

            Person person = PersonService.Find(user.Person_ID);
            if (person == null)
            {
               MessageBox.Show("Associated person information not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ctrlPersonInformation1.UpdatePersonInfoANDRefreshUI(person);
            lblUserID.Text = user.UserID.ToString();
            lblUsername.Text = user.Username;
            lblIsActive.Text = (user.isActive) ? "Active" : "InActive";  
        }

        
    }
}
