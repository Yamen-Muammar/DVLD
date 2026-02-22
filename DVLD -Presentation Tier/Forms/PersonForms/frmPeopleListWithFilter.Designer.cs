namespace DVLD__Presentation_Tier.Forms
{
    partial class frmPeopleListWithFilter
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.crtlPeopleListWithFilter1 = new DVLD__Presentation_Tier.crtlPeopleListWithFilter();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Simplified Arabic", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(490, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(293, 66);
            this.label1.TabIndex = 1;
            this.label1.Text = "Manage People";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // crtlPeopleListWithFilter1
            // 
            this.crtlPeopleListWithFilter1.AutoSize = true;
            this.crtlPeopleListWithFilter1.Location = new System.Drawing.Point(-3, 76);
            this.crtlPeopleListWithFilter1.Name = "crtlPeopleListWithFilter1";
            this.crtlPeopleListWithFilter1.Size = new System.Drawing.Size(1324, 682);
            this.crtlPeopleListWithFilter1.TabIndex = 0;
            this.crtlPeopleListWithFilter1.OnCloseButtonClicked += new System.Action(this.crtlPeopleListWithFilter1_OnCloseButtonClicked);
            // 
            // frmPeopleListWithFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1328, 794);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.crtlPeopleListWithFilter1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPeopleListWithFilter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmPeopleListWithFilter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private crtlPeopleListWithFilter crtlPeopleListWithFilter1;
        private System.Windows.Forms.Label label1;
    }
}