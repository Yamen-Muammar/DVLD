using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD__Presentation_Tier.Forms.License_Forms
{
    public partial class frmLicenseHistory : Form
    {
        private string nationalNo;
        private int personID;
        public frmLicenseHistory(string nationalNo)
        {
            InitializeComponent();
            this.nationalNo = nationalNo;
            this.ctrlPersonInformationWithFilter1.ReturnPersonID_OnFindPerson += OnReturnPersonID_OnFindPerson;

        }
        private void OnReturnPersonID_OnFindPerson(object sender,int PersonID)
        {
            personID= PersonID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void frmLicenseHistory_Load(object sender, EventArgs e)
        {
            await this.ctrlPersonInformationWithFilter1.FindPersonByNationalNO(nationalNo);
            this.ctrlDriverLicensesHistory1.SetPersonID(personID);
        }
    }
}
