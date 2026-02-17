using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD__Presentation_Tier.Forms;
using DVLD__Business_Tier.Services;
using DVLD__Core.Models;
namespace DVLD__Presentation_Tier
{
    public partial class crtlPeopleListWithFilter : UserControl
    {
        public event Action OnCloseButtonClicked;
        protected virtual void RaiseCloseClickedEvent()
        {
            Action handler = OnCloseButtonClicked;
            if (handler != null)
            {
                handler.Invoke();
            }            
        }
        public crtlPeopleListWithFilter()
        {
            InitializeComponent();           
        }
        private void crtlPeopleListWithFilter_Load(object sender, EventArgs e)
        {
            _RefreshData();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            RaiseCloseClickedEvent();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddOrUpdatePersonInfo frmAddOrUpdatePersonInfo = new frmAddOrUpdatePersonInfo();
            frmAddOrUpdatePersonInfo.ShowDialog();
            _RefreshData();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddOrUpdatePersonInfo frmAddOrUpdatePersonInfo = new frmAddOrUpdatePersonInfo();
            frmAddOrUpdatePersonInfo.ShowDialog();
            _RefreshData();
        }

        private void editeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddOrUpdatePersonInfo frmAddOrUpdatePersonInfo = new frmAddOrUpdatePersonInfo((int)dgvPeopleList.CurrentRow.Cells[0].Value);
            frmAddOrUpdatePersonInfo.ShowDialog();
            _RefreshData();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            if (MessageBox.Show("Are You Sure ?","Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            int PersonId = (int)dgvPeopleList.CurrentRow.Cells[0].Value;

            if (!PersonService.Delete(PersonId))
            {
                MessageBox.Show("An error occurred while deleting the person.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Person deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _RefreshData();
            }
        }


        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int PersonId = (int)dgvPeopleList.CurrentRow.Cells[0].Value;
            frmPersonInformation frmPersonInformation = new frmPersonInformation(PersonId);
            frmPersonInformation.ShowDialog();
            _RefreshData();
        }

        private void _RefreshData()
        {
            List<Person> people = PersonService.GetAll();
            dgvPeopleList.DataSource = people;
            lblRecordsCount.Text = people.Count.ToString();
        }

       
    }
}
