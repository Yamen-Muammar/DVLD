namespace DVLD__Presentation_Tier.Forms
{
    partial class frmReplaceLicense
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
            this.gbFinder = new System.Windows.Forms.GroupBox();
            this.btnFindLicense = new System.Windows.Forms.Button();
            this.tbLicenseID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblLocalLicenseID = new System.Windows.Forms.Label();
            this.lblCreatedBy = new System.Windows.Forms.Label();
            this.lblAppFees = new System.Windows.Forms.Label();
            this.lblAppDate = new System.Windows.Forms.Label();
            this.lblRenewLicenseID = new System.Windows.Forms.Label();
            this.lbRenewLAppID = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblApplicationFees = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.llblLicenseHistory = new System.Windows.Forms.LinkLabel();
            this.llblShowLicenseInfo = new System.Windows.Forms.LinkLabel();
            this.button3 = new System.Windows.Forms.Button();
            this.btnIssueReplacement = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.gbReplacementType = new System.Windows.Forms.GroupBox();
            this.rbtnLost = new System.Windows.Forms.RadioButton();
            this.rbtnDamage = new System.Windows.Forms.RadioButton();
            this.ctrlDriverLicenseInfo1 = new DVLD__Presentation_Tier.Controls.LicenseControls.ctrlDriverLicenseInfo();
            this.gbFinder.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbReplacementType.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbFinder
            // 
            this.gbFinder.Controls.Add(this.btnFindLicense);
            this.gbFinder.Controls.Add(this.tbLicenseID);
            this.gbFinder.Controls.Add(this.label2);
            this.gbFinder.Location = new System.Drawing.Point(12, 109);
            this.gbFinder.Name = "gbFinder";
            this.gbFinder.Size = new System.Drawing.Size(634, 111);
            this.gbFinder.TabIndex = 12;
            this.gbFinder.TabStop = false;
            this.gbFinder.Text = "License Finder";
            // 
            // btnFindLicense
            // 
            this.btnFindLicense.Image = global::DVLD__Presentation_Tier.Properties.Resources.LicenseView32;
            this.btnFindLicense.Location = new System.Drawing.Point(529, 25);
            this.btnFindLicense.Name = "btnFindLicense";
            this.btnFindLicense.Size = new System.Drawing.Size(83, 61);
            this.btnFindLicense.TabIndex = 2;
            this.btnFindLicense.UseVisualStyleBackColor = true;
            this.btnFindLicense.Click += new System.EventHandler(this.btnFindLicense_Click);
            // 
            // tbLicenseID
            // 
            this.tbLicenseID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbLicenseID.Location = new System.Drawing.Point(125, 52);
            this.tbLicenseID.Name = "tbLicenseID";
            this.tbLicenseID.Size = new System.Drawing.Size(315, 26);
            this.tbLicenseID.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "License ID : ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblLocalLicenseID);
            this.groupBox2.Controls.Add(this.lblCreatedBy);
            this.groupBox2.Controls.Add(this.lblAppFees);
            this.groupBox2.Controls.Add(this.lblAppDate);
            this.groupBox2.Controls.Add(this.lblRenewLicenseID);
            this.groupBox2.Controls.Add(this.lbRenewLAppID);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.lblApplicationFees);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 728);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(877, 181);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Application Info";
            // 
            // lblLocalLicenseID
            // 
            this.lblLocalLicenseID.AutoSize = true;
            this.lblLocalLicenseID.Location = new System.Drawing.Point(646, 80);
            this.lblLocalLicenseID.Name = "lblLocalLicenseID";
            this.lblLocalLicenseID.Size = new System.Drawing.Size(45, 20);
            this.lblLocalLicenseID.TabIndex = 2;
            this.lblLocalLicenseID.Text = "????";
            // 
            // lblCreatedBy
            // 
            this.lblCreatedBy.AutoSize = true;
            this.lblCreatedBy.Location = new System.Drawing.Point(646, 129);
            this.lblCreatedBy.Name = "lblCreatedBy";
            this.lblCreatedBy.Size = new System.Drawing.Size(45, 20);
            this.lblCreatedBy.TabIndex = 2;
            this.lblCreatedBy.Text = "????";
            // 
            // lblAppFees
            // 
            this.lblAppFees.AutoSize = true;
            this.lblAppFees.Location = new System.Drawing.Point(188, 128);
            this.lblAppFees.Name = "lblAppFees";
            this.lblAppFees.Size = new System.Drawing.Size(45, 20);
            this.lblAppFees.TabIndex = 2;
            this.lblAppFees.Text = "????";
            // 
            // lblAppDate
            // 
            this.lblAppDate.AutoSize = true;
            this.lblAppDate.Location = new System.Drawing.Point(188, 79);
            this.lblAppDate.Name = "lblAppDate";
            this.lblAppDate.Size = new System.Drawing.Size(45, 20);
            this.lblAppDate.TabIndex = 2;
            this.lblAppDate.Text = "????";
            // 
            // lblRenewLicenseID
            // 
            this.lblRenewLicenseID.AutoSize = true;
            this.lblRenewLicenseID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRenewLicenseID.Location = new System.Drawing.Point(646, 38);
            this.lblRenewLicenseID.Name = "lblRenewLicenseID";
            this.lblRenewLicenseID.Size = new System.Drawing.Size(39, 20);
            this.lblRenewLicenseID.TabIndex = 1;
            this.lblRenewLicenseID.Text = "???";
            // 
            // lbRenewLAppID
            // 
            this.lbRenewLAppID.AutoSize = true;
            this.lbRenewLAppID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRenewLAppID.Location = new System.Drawing.Point(188, 38);
            this.lbRenewLAppID.Name = "lbRenewLAppID";
            this.lbRenewLAppID.Size = new System.Drawing.Size(39, 20);
            this.lbRenewLAppID.TabIndex = 1;
            this.lbRenewLAppID.Text = "???";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("72 Condensed", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(514, 128);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(98, 21);
            this.label12.TabIndex = 0;
            this.label12.Text = "Created By : ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("72 Condensed", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(447, 37);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(165, 21);
            this.label10.TabIndex = 0;
            this.label10.Text = "Replaced License ID : ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("72 Condensed", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(490, 78);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 21);
            this.label8.TabIndex = 0;
            this.label8.Text = "Old License ID : ";
            // 
            // lblApplicationFees
            // 
            this.lblApplicationFees.AutoSize = true;
            this.lblApplicationFees.Font = new System.Drawing.Font("72 Condensed", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplicationFees.Location = new System.Drawing.Point(23, 127);
            this.lblApplicationFees.Name = "lblApplicationFees";
            this.lblApplicationFees.Size = new System.Drawing.Size(138, 21);
            this.lblApplicationFees.TabIndex = 0;
            this.lblApplicationFees.Text = "Application Fees : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("72 Condensed", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(26, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 21);
            this.label4.TabIndex = 0;
            this.label4.Text = "Application Date : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("72 Condensed", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(23, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "R.L.Application ID : ";
            // 
            // llblLicenseHistory
            // 
            this.llblLicenseHistory.AutoSize = true;
            this.llblLicenseHistory.Location = new System.Drawing.Point(23, 950);
            this.llblLicenseHistory.Name = "llblLicenseHistory";
            this.llblLicenseHistory.Size = new System.Drawing.Size(158, 20);
            this.llblLicenseHistory.TabIndex = 17;
            this.llblLicenseHistory.TabStop = true;
            this.llblLicenseHistory.Text = "show License History";
            this.llblLicenseHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblLicenseHistory_LinkClicked);
            // 
            // llblShowLicenseInfo
            // 
            this.llblShowLicenseInfo.AutoSize = true;
            this.llblShowLicenseInfo.Enabled = false;
            this.llblShowLicenseInfo.Location = new System.Drawing.Point(205, 950);
            this.llblShowLicenseInfo.Name = "llblShowLicenseInfo";
            this.llblShowLicenseInfo.Size = new System.Drawing.Size(137, 20);
            this.llblShowLicenseInfo.TabIndex = 18;
            this.llblShowLicenseInfo.TabStop = true;
            this.llblShowLicenseInfo.Text = "show License Info";
            this.llblShowLicenseInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblShowLicenseInfo_LinkClicked);
            // 
            // button3
            // 
            this.button3.Image = global::DVLD__Presentation_Tier.Properties.Resources.btnClose_Image;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(427, 934);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(192, 54);
            this.button3.TabIndex = 15;
            this.button3.Text = "Cancel";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnIssueReplacement
            // 
            this.btnIssueReplacement.Enabled = false;
            this.btnIssueReplacement.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIssueReplacement.Image = global::DVLD__Presentation_Tier.Properties.Resources.IssueDrivingLicense32;
            this.btnIssueReplacement.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIssueReplacement.Location = new System.Drawing.Point(662, 933);
            this.btnIssueReplacement.Name = "btnIssueReplacement";
            this.btnIssueReplacement.Size = new System.Drawing.Size(226, 54);
            this.btnIssueReplacement.TabIndex = 16;
            this.btnIssueReplacement.Text = "Issue Replacement";
            this.btnIssueReplacement.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnIssueReplacement.UseVisualStyleBackColor = true;
            this.btnIssueReplacement.Click += new System.EventHandler(this.btnIssueReplacement_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Malgun Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(173, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(560, 38);
            this.label1.TabIndex = 19;
            this.label1.Text = "Replace For Damage or Lost Application";
            // 
            // gbReplacementType
            // 
            this.gbReplacementType.Controls.Add(this.rbtnLost);
            this.gbReplacementType.Controls.Add(this.rbtnDamage);
            this.gbReplacementType.Enabled = false;
            this.gbReplacementType.Location = new System.Drawing.Point(662, 109);
            this.gbReplacementType.Name = "gbReplacementType";
            this.gbReplacementType.Size = new System.Drawing.Size(200, 111);
            this.gbReplacementType.TabIndex = 20;
            this.gbReplacementType.TabStop = false;
            this.gbReplacementType.Text = "Replace For ";
            // 
            // rbtnLost
            // 
            this.rbtnLost.AutoSize = true;
            this.rbtnLost.Location = new System.Drawing.Point(6, 62);
            this.rbtnLost.Name = "rbtnLost";
            this.rbtnLost.Size = new System.Drawing.Size(124, 24);
            this.rbtnLost.TabIndex = 0;
            this.rbtnLost.Text = "Lost License";
            this.rbtnLost.UseVisualStyleBackColor = true;
            // 
            // rbtnDamage
            // 
            this.rbtnDamage.AutoSize = true;
            this.rbtnDamage.Checked = true;
            this.rbtnDamage.Location = new System.Drawing.Point(6, 32);
            this.rbtnDamage.Name = "rbtnDamage";
            this.rbtnDamage.Size = new System.Drawing.Size(163, 24);
            this.rbtnDamage.TabIndex = 0;
            this.rbtnDamage.TabStop = true;
            this.rbtnDamage.Text = "Damaged License";
            this.rbtnDamage.UseVisualStyleBackColor = true;
            this.rbtnDamage.CheckedChanged += new System.EventHandler(this.rbtnDamage_CheckedChanged);
            // 
            // ctrlDriverLicenseInfo1
            // 
            this.ctrlDriverLicenseInfo1.Location = new System.Drawing.Point(12, 237);
            this.ctrlDriverLicenseInfo1.Name = "ctrlDriverLicenseInfo1";
            this.ctrlDriverLicenseInfo1.Size = new System.Drawing.Size(877, 484);
            this.ctrlDriverLicenseInfo1.TabIndex = 13;
            // 
            // frmReplaceLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(900, 992);
            this.Controls.Add(this.gbReplacementType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.llblLicenseHistory);
            this.Controls.Add(this.llblShowLicenseInfo);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnIssueReplacement);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.ctrlDriverLicenseInfo1);
            this.Controls.Add(this.gbFinder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReplaceLicense";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmReplaceLicense";
            this.Load += new System.EventHandler(this.frmReplaceLicense_Load);
            this.gbFinder.ResumeLayout(false);
            this.gbFinder.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gbReplacementType.ResumeLayout(false);
            this.gbReplacementType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbFinder;
        private System.Windows.Forms.Button btnFindLicense;
        private System.Windows.Forms.TextBox tbLicenseID;
        private System.Windows.Forms.Label label2;
        private Controls.LicenseControls.ctrlDriverLicenseInfo ctrlDriverLicenseInfo1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblLocalLicenseID;
        private System.Windows.Forms.Label lblCreatedBy;
        private System.Windows.Forms.Label lblAppFees;
        private System.Windows.Forms.Label lblAppDate;
        private System.Windows.Forms.Label lblRenewLicenseID;
        private System.Windows.Forms.Label lbRenewLAppID;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblApplicationFees;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel llblLicenseHistory;
        private System.Windows.Forms.LinkLabel llblShowLicenseInfo;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnIssueReplacement;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbReplacementType;
        private System.Windows.Forms.RadioButton rbtnLost;
        private System.Windows.Forms.RadioButton rbtnDamage;
    }
}