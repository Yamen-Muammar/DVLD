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
    public partial class frmPersonInformation : Form
    {
        public frmPersonInformation()
        {
            InitializeComponent();
        }
        public frmPersonInformation(int PersonId)
        {
            InitializeComponent(PersonId);
        }

        private void ctrlPersonInformation1_OnClose_Clicked()
        {
            this.Close();
        }
    }
}
