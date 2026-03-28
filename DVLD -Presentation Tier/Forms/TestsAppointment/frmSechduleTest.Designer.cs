using DVLD__Presentation_Tier.Controls.SechduleTestsControls;

namespace DVLD__Presentation_Tier.Forms.TestsAppointment
{
    partial class frmSechduleTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ctrlSechduleVisionTest1 = new DVLD__Presentation_Tier.Controls.SechduleTestsControls.ctrlSechduleVisionTest();
            this.SuspendLayout();
            // 
            // ctrlSechduleVisionTest1
            // 
            this.ctrlSechduleVisionTest1.Location = new System.Drawing.Point(12, 3);
            this.ctrlSechduleVisionTest1.Name = "ctrlSechduleVisionTest1";
            this.ctrlSechduleVisionTest1.Size = new System.Drawing.Size(609, 810);
            this.ctrlSechduleVisionTest1.TabIndex = 0;
            // 
            // frmSechduleTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(631, 905);
            this.Controls.Add(this.ctrlSechduleVisionTest1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSechduleTest";
            this.Text = "frmSechduleTest";
            this.ResumeLayout(false);

        }

        private void InitializeComponent(int? appointmentID,ctrlSechduleVisionTest.enMode mode,int ldlAppId,string applicantName)
        {
            this.ctrlSechduleVisionTest1 = new DVLD__Presentation_Tier.Controls.SechduleTestsControls.ctrlSechduleVisionTest(appointmentID, mode,applicantName,ldlAppId);
            this.SuspendLayout();
            // 
            // ctrlSechduleVisionTest1
            // 
            this.ctrlSechduleVisionTest1.Location = new System.Drawing.Point(12, 3);
            this.ctrlSechduleVisionTest1.Name = "ctrlSechduleVisionTest1";
            this.ctrlSechduleVisionTest1.Size = new System.Drawing.Size(609, 864);
            this.ctrlSechduleVisionTest1.TabIndex = 0;
            // 
            // frmSechduleTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(631, 905);
            this.Controls.Add(this.ctrlSechduleVisionTest1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSechduleTest";
            this.Text = "frmSechduleTest";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.SechduleTestsControls.ctrlSechduleVisionTest ctrlSechduleVisionTest1;
    }
}