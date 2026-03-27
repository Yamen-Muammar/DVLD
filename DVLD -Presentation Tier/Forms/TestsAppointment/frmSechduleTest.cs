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
    public partial class frmSechduleTest : Form
    {
        public frmSechduleTest()
        {
            InitializeComponent();
        }
        public frmSechduleTest(string applicantFullName,int LDLAppID , int _testTypeID)
        {
            if (_testTypeID == 1)
            {
                InitializeComponent(LDLAppID,applicantFullName);
            }
            else if (_testTypeID == 2)
            {
                InitializeComponent();
            }
            else if (_testTypeID == 3)
            {
                InitializeComponent();
            }
            else
            {
                InitializeComponent();
            }
        }
    }
}
