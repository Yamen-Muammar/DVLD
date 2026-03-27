using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD__Presentation_Tier.Forms.TestsAppointment
{
    public partial class frmVisionTestAppointment : Form
    {
        private 
        public frmVisionTestAppointment()
        {
            InitializeComponent();
        }

        public frmVisionTestAppointment(int LDLAppId)
        {
            InitializeComponent(LDLAppId);
        }
        private void btnClosefrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {

        }
    }
}
