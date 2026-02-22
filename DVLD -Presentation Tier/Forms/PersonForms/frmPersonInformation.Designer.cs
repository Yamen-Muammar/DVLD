namespace DVLD__Presentation_Tier.Forms
{
    partial class frmPersonInformation
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
            this.ctrlPersonInformation1 = new DVLD__Presentation_Tier.ctrlPersonInformation();
            this.SuspendLayout();
            // 
            // ctrlPersonInformation1
            // 
            this.ctrlPersonInformation1.Location = new System.Drawing.Point(0, 12);
            this.ctrlPersonInformation1.Name = "ctrlPersonInformation1";
            this.ctrlPersonInformation1.Size = new System.Drawing.Size(968, 435);
            this.ctrlPersonInformation1.TabIndex = 0;
            this.ctrlPersonInformation1.OnClose_Clicked += new System.Action(this.ctrlPersonInformation1_OnClose_Clicked);
            // 
            // frmPersonInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 456);
            this.Controls.Add(this.ctrlPersonInformation1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmPersonInformation";
            this.Text = "frmPersonInformation";
            this.ResumeLayout(false);

        }
        private void InitializeComponent(int PersonId)
        {
            this.ctrlPersonInformation1 = new DVLD__Presentation_Tier.ctrlPersonInformation(PersonId);
            this.SuspendLayout();
            // 
            // ctrlPersonInformation1
            // 
            this.ctrlPersonInformation1.Location = new System.Drawing.Point(0, 12);
            this.ctrlPersonInformation1.Name = "ctrlPersonInformation1";
            this.ctrlPersonInformation1.Size = new System.Drawing.Size(968, 435);
            this.ctrlPersonInformation1.TabIndex = 0;
            this.ctrlPersonInformation1.OnClose_Clicked += new System.Action(this.ctrlPersonInformation1_OnClose_Clicked);
            // 
            // frmPersonInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 456);
            this.Controls.Add(this.ctrlPersonInformation1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmPersonInformation";
            this.Text = "frmPersonInformation";
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlPersonInformation ctrlPersonInformation1;
    }
}