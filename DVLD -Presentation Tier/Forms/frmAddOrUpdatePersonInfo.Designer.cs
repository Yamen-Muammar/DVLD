namespace DVLD__Presentation_Tier.Forms
{
    partial class frmAddOrUpdatePersonInfo
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
            this.ctrlAddOrUpdatePerson1 = new DVLD__Presentation_Tier.ctrlAddOrUpdatePerson();
            this.SuspendLayout();
            // 
            // ctrlAddOrUpdatePerson1
            // 
            this.ctrlAddOrUpdatePerson1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlAddOrUpdatePerson1.Location = new System.Drawing.Point(0, 0);
            this.ctrlAddOrUpdatePerson1.Name = "ctrlAddOrUpdatePerson1";
            this.ctrlAddOrUpdatePerson1.Size = new System.Drawing.Size(1016, 563);
            this.ctrlAddOrUpdatePerson1.TabIndex = 0;
            this.ctrlAddOrUpdatePerson1.OnClose_Clicked += new System.Action(this.ctrlAddOrUpdatePerson1_OnClose_Clicked);
            // 
            // frmAddOrUpdatePersonInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 563);
            this.Controls.Add(this.ctrlAddOrUpdatePerson1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmAddOrUpdatePersonInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAddOrUpdatePersonInfo";
            this.ResumeLayout(false);

        }
        private void InitializeComponent(int id)
        {
            this.ctrlAddOrUpdatePerson1 = new DVLD__Presentation_Tier.ctrlAddOrUpdatePerson();
            this.SuspendLayout();
            // 
            // ctrlAddOrUpdatePerson1
            // 
            this.ctrlAddOrUpdatePerson1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlAddOrUpdatePerson1.Location = new System.Drawing.Point(0, 0);
            this.ctrlAddOrUpdatePerson1.Name = "ctrlAddOrUpdatePerson1";
            this.ctrlAddOrUpdatePerson1.Size = new System.Drawing.Size(1016, 563);
            this.ctrlAddOrUpdatePerson1.TabIndex = 0;
            this.ctrlAddOrUpdatePerson1.OnClose_Clicked += new System.Action(this.ctrlAddOrUpdatePerson1_OnClose_Clicked);
            // 
            // frmAddOrUpdatePersonInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1016, 563);
            this.Controls.Add(this.ctrlAddOrUpdatePerson1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddOrUpdatePersonInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAddOrUpdatePersonInfo";
            this.ResumeLayout(false);

        }

        #endregion

        public ctrlAddOrUpdatePerson ctrlAddOrUpdatePerson1;
    }
}