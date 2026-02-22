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
    public partial class frmAddOrUpdatePersonInfo : Form
    {
        public frmAddOrUpdatePersonInfo()
        {
            InitializeComponent();
        }
        public frmAddOrUpdatePersonInfo(int id)
        {
            InitializeComponent(id);
        }

        private void ctrlAddOrUpdatePerson1_OnClose_Clicked()
        {
            this.Close();
        }
    }
}
