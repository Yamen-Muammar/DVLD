using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD__Presentation_Tier
{
    public partial class ctrlPersonInformation : UserControl
    {
        public event Action OnClose_Clicked;
        protected virtual void CloseEvent()
        {
            Action handler = OnClose_Clicked;
            if (handler != null)
            {
                handler.Invoke();
            }
        }
        public ctrlPersonInformation()
        {
            InitializeComponent();
        }

        public ctrlPersonInformation(int PersonId)
        {
            InitializeComponent();
        }

        private void btnEditePersonInfo_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseEvent();
        }
    }
}
