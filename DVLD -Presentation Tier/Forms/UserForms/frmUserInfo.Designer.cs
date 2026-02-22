using DVLD__Presentation_Tier.Controls.UserControls;

namespace DVLD__Presentation_Tier.Forms.UserForms
{
    partial class frmUserInfo
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
            this.ctrlUserInfo1 = new DVLD__Presentation_Tier.Controls.UserControls.ctrlUserInfo();
            this.SuspendLayout();
            // 
            // ctrlUserInfo1
            // 
            this.ctrlUserInfo1.Location = new System.Drawing.Point(12, 12);
            this.ctrlUserInfo1.Name = "ctrlUserInfo1";
            this.ctrlUserInfo1.Size = new System.Drawing.Size(956, 503);
            this.ctrlUserInfo1.TabIndex = 0;
            // 
            // frmUserInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(978, 514);
            this.Controls.Add(this.ctrlUserInfo1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUserInfo";
            this.Text = "frmUserInfo";
            this.ResumeLayout(false);

        }
        private void InitializeComponent(int UserId)
        {
            this.ctrlUserInfo1 = new DVLD__Presentation_Tier.Controls.UserControls.ctrlUserInfo(UserId);
            this.SuspendLayout();
            // 
            // ctrlUserInfo1
            // 
            this.ctrlUserInfo1.Location = new System.Drawing.Point(12, 12);
            this.ctrlUserInfo1.Name = "ctrlUserInfo1";
            this.ctrlUserInfo1.Size = new System.Drawing.Size(956, 503);
            this.ctrlUserInfo1.TabIndex = 0;
            // 
            // frmUserInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(978, 514);
            this.Controls.Add(this.ctrlUserInfo1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUserInfo";
            this.Text = "frmUserInfo";
            this.ResumeLayout(false);

        }
        #endregion

        private ctrlUserInfo ctrlUserInfo1;
    }
}