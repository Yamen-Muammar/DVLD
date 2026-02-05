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
    public partial class frmPeopleListWithFilter : Form
    {
        public frmPeopleListWithFilter()
        {
            InitializeComponent();
        }

        private void crtlPeopleListWithFilter1_OnCloseButtonClicked()
        {
            this.Close();
        }
    }
}
