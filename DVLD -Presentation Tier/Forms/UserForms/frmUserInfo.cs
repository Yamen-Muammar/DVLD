using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace DVLD__Presentation_Tier.Forms.UserForms
{
    public partial class frmUserInfo : Form
    {
        public frmUserInfo()
        {
            InitializeComponent();
        }

        public frmUserInfo(int UserId)
        {
            InitializeComponent(UserId);
        }
    }
}
