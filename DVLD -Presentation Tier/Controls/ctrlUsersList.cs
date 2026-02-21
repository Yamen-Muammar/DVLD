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

namespace DVLD__Presentation_Tier.Controls
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
            _refreshUI();
        }
        private void _refreshUI()
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


        
    }
}
