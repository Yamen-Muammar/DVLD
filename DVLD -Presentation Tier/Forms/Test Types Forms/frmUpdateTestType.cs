using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD__Core.Models;

namespace DVLD__Presentation_Tier.Forms.Test_Types_Forms
{
    public partial class frmUpdateTestType : Form
    {
        private int _testTypeID = -1;
        private TestType _testType;
        public frmUpdateTestType()
        {
            InitializeComponent();            
        }

        public frmUpdateTestType(int testTypeID)
        {
            InitializeComponent();
            _testTypeID = testTypeID;
        }

        private void frmUpdateTestType_Load(object sender, EventArgs e)
        {
            if (_testType == null)
            {
                return;
            }
        }

        private TestType _loadTestTypeDataToObject(int id)
        {

        }
    }
}
