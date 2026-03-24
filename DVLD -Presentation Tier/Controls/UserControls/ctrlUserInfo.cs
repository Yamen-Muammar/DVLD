using System;
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

        private UserService _userService;
        private PersonService _personService;
        private int _userId;
        public ctrlUserInfo()
        {
            InitializeComponent();
            _userService = new UserService();
            _personService = new PersonService();
            _userId = -1;
        }

        public ctrlUserInfo(int UserId)
        {
            InitializeComponent();
            _userService = new UserService();
            _personService = new PersonService();
            _userId = UserId;
        }
        private async void ctrlUserInfo_Load(object sender, EventArgs e)
        {
            if (_userId == -1)
            {
                UserInfo = Global.User;
            }
            else
            {
                UserInfo = await _getUserInfo(_userId);
            }

            if (UserInfo == null)
            {
                return;
            }
            await _loadUserInforamtionInForm(UserInfo);
        }
        private async Task<User> _getUserInfo(int userId)
        {
            User userInfo = null;
            try
            {
                userInfo = await _userService.GetUserById(userId);
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
        private async Task _loadUserInforamtionInForm(User user)
        {
            if (user == null)
            {
                return;
            }

            Person person = await _loadPersonInformation(user.Person_ID);
            if (person == null)
            {
                return;
            }
            ctrlPersonInformation1.UpdatePersonInfoANDRefreshUI(person);
            lblUserID.Text = user.UserID.ToString();
            lblUsername.Text = user.Username;
            lblIsActive.Text = (user.isActive) ? "Active" : "InActive";
        }
        private async Task<Person> _loadPersonInformation(int personID)
        {
            Person person = null;
            try
            {
                person = await _personService.Find(personID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return person;
        }

    }
}
