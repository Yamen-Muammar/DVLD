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
    public partial class ctrlAddOrUpdatePerson : UserControl
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

        private enum enMode
        {
            eAdd = 1,eUpdate = 2
        }
        private enMode Mode { get; set; }

        private int PersonId { get; set; }

        public ctrlAddOrUpdatePerson()
        {
            InitializeComponent();

            PersonId = -1;

            Mode = enMode.eAdd;
        }

        public ctrlAddOrUpdatePerson(int id)
        {
            InitializeComponent();
            PersonId = id;

            Mode = (PersonId == -1)?enMode.eAdd : enMode.eUpdate;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //TODO: Save the person information to the database, if PersonId is -1 then add a new person, otherwise update the existing person
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseEvent();
        }

        private void btnSetImage_Click(object sender, EventArgs e)
        {
            // TODO: Set the person Image
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            //TODO: Remove the person Image            
        }

        private void tbNationalNo_TextChanged(object sender, EventArgs e)
        {
            //TODO: Check if the national number is valid and if it already exists in the database
        }
    }
}
