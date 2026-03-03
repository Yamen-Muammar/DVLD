namespace DVLD__Presentation_Tier.Forms.LocalDrivingLicenseForms
{
    partial class frmNewLocalDrivingLicenseApplication
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbPersonalInfo = new System.Windows.Forms.TabPage();
            this.ctrlPersonInformationWithFilter1 = new DVLD__Presentation_Tier.Controls.ctrlPersonInformationWithFilter();
            this.tbApplicationInfo = new System.Windows.Forms.TabPage();
            this.cbLicenseClasses = new System.Windows.Forms.ComboBox();
            this.lblCreatedName = new System.Windows.Forms.Label();
            this.lblApplicationFees = new System.Windows.Forms.Label();
            this.lblApplicationDate = new System.Windows.Forms.Label();
            this.lblApplicationID = new System.Windows.Forms.Label();
            this.l4 = new System.Windows.Forms.Label();
            this.lblLicenseClass = new System.Windows.Forms.Label();
            this.l3 = new System.Windows.Forms.Label();
            this.l2 = new System.Windows.Forms.Label();
            this.l1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tbPersonalInfo.SuspendLayout();
            this.tbApplicationInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Crimson;
            this.label1.Location = new System.Drawing.Point(265, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(458, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "New Local Driving Licese Application";
            // 
            // btnNext
            // 
            this.btnNext.Image = global::DVLD__Presentation_Tier.Properties.Resources.Next32;
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNext.Location = new System.Drawing.Point(796, 505);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(175, 43);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbPersonalInfo);
            this.tabControl1.Controls.Add(this.tbApplicationInfo);
            this.tabControl1.Location = new System.Drawing.Point(12, 101);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1002, 589);
            this.tabControl1.TabIndex = 4;
            // 
            // tbPersonalInfo
            // 
            this.tbPersonalInfo.Controls.Add(this.ctrlPersonInformationWithFilter1);
            this.tbPersonalInfo.Controls.Add(this.btnNext);
            this.tbPersonalInfo.Location = new System.Drawing.Point(4, 29);
            this.tbPersonalInfo.Name = "tbPersonalInfo";
            this.tbPersonalInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tbPersonalInfo.Size = new System.Drawing.Size(994, 556);
            this.tbPersonalInfo.TabIndex = 0;
            this.tbPersonalInfo.Text = "Personal Info";
            this.tbPersonalInfo.UseVisualStyleBackColor = true;
            // 
            // ctrlPersonInformationWithFilter1
            // 
            this.ctrlPersonInformationWithFilter1.Location = new System.Drawing.Point(13, 6);
            this.ctrlPersonInformationWithFilter1.Name = "ctrlPersonInformationWithFilter1";
            this.ctrlPersonInformationWithFilter1.Size = new System.Drawing.Size(975, 493);
            this.ctrlPersonInformationWithFilter1.TabIndex = 0;
            this.ctrlPersonInformationWithFilter1.ReturnPersonID_OnFindPerson += new DVLD__Presentation_Tier.Controls.ctrlPersonInformationWithFilter.ReturnPersonIDEvent(this.ctrlPersonInformationWithFilter1_ReturnPersonID_OnFindPerson);
            // 
            // tbApplicationInfo
            // 
            this.tbApplicationInfo.Controls.Add(this.cbLicenseClasses);
            this.tbApplicationInfo.Controls.Add(this.lblCreatedName);
            this.tbApplicationInfo.Controls.Add(this.lblApplicationFees);
            this.tbApplicationInfo.Controls.Add(this.lblApplicationDate);
            this.tbApplicationInfo.Controls.Add(this.lblApplicationID);
            this.tbApplicationInfo.Controls.Add(this.l4);
            this.tbApplicationInfo.Controls.Add(this.lblLicenseClass);
            this.tbApplicationInfo.Controls.Add(this.l3);
            this.tbApplicationInfo.Controls.Add(this.l2);
            this.tbApplicationInfo.Controls.Add(this.l1);
            this.tbApplicationInfo.Location = new System.Drawing.Point(4, 29);
            this.tbApplicationInfo.Name = "tbApplicationInfo";
            this.tbApplicationInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tbApplicationInfo.Size = new System.Drawing.Size(994, 556);
            this.tbApplicationInfo.TabIndex = 1;
            this.tbApplicationInfo.Text = "Application Info";
            this.tbApplicationInfo.UseVisualStyleBackColor = true;
            // 
            // cbLicenseClasses
            // 
            this.cbLicenseClasses.FormattingEnabled = true;
            this.cbLicenseClasses.Location = new System.Drawing.Point(299, 176);
            this.cbLicenseClasses.Name = "cbLicenseClasses";
            this.cbLicenseClasses.Size = new System.Drawing.Size(364, 28);
            this.cbLicenseClasses.TabIndex = 10;
            // 
            // lblCreatedName
            // 
            this.lblCreatedName.AutoSize = true;
            this.lblCreatedName.Font = new System.Drawing.Font("Simplified Arabic Fixed", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreatedName.ForeColor = System.Drawing.Color.Red;
            this.lblCreatedName.Location = new System.Drawing.Point(299, 270);
            this.lblCreatedName.Name = "lblCreatedName";
            this.lblCreatedName.Size = new System.Drawing.Size(54, 23);
            this.lblCreatedName.TabIndex = 5;
            this.lblCreatedName.Text = "????";
            // 
            // lblApplicationFees
            // 
            this.lblApplicationFees.AutoSize = true;
            this.lblApplicationFees.Font = new System.Drawing.Font("Simplified Arabic Fixed", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplicationFees.ForeColor = System.Drawing.Color.Red;
            this.lblApplicationFees.Location = new System.Drawing.Point(299, 226);
            this.lblApplicationFees.Name = "lblApplicationFees";
            this.lblApplicationFees.Size = new System.Drawing.Size(54, 23);
            this.lblApplicationFees.TabIndex = 5;
            this.lblApplicationFees.Text = "????";
            // 
            // lblApplicationDate
            // 
            this.lblApplicationDate.AutoSize = true;
            this.lblApplicationDate.Font = new System.Drawing.Font("Simplified Arabic Fixed", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplicationDate.ForeColor = System.Drawing.Color.Red;
            this.lblApplicationDate.Location = new System.Drawing.Point(299, 128);
            this.lblApplicationDate.Name = "lblApplicationDate";
            this.lblApplicationDate.Size = new System.Drawing.Size(54, 23);
            this.lblApplicationDate.TabIndex = 5;
            this.lblApplicationDate.Text = "????";
            // 
            // lblApplicationID
            // 
            this.lblApplicationID.AutoSize = true;
            this.lblApplicationID.Font = new System.Drawing.Font("Simplified Arabic Fixed", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplicationID.ForeColor = System.Drawing.Color.Red;
            this.lblApplicationID.Location = new System.Drawing.Point(299, 81);
            this.lblApplicationID.Name = "lblApplicationID";
            this.lblApplicationID.Size = new System.Drawing.Size(54, 23);
            this.lblApplicationID.TabIndex = 5;
            this.lblApplicationID.Text = "????";
            // 
            // l4
            // 
            this.l4.AutoSize = true;
            this.l4.Font = new System.Drawing.Font("Segoe UI Symbol", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l4.Location = new System.Drawing.Point(92, 263);
            this.l4.Name = "l4";
            this.l4.Size = new System.Drawing.Size(127, 28);
            this.l4.TabIndex = 6;
            this.l4.Text = "Created By :";
            // 
            // lblLicenseClass
            // 
            this.lblLicenseClass.AutoSize = true;
            this.lblLicenseClass.Font = new System.Drawing.Font("Segoe UI Symbol", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLicenseClass.Location = new System.Drawing.Point(73, 172);
            this.lblLicenseClass.Name = "lblLicenseClass";
            this.lblLicenseClass.Size = new System.Drawing.Size(146, 28);
            this.lblLicenseClass.TabIndex = 7;
            this.lblLicenseClass.Text = "License Class :";
            // 
            // l3
            // 
            this.l3.AutoSize = true;
            this.l3.Font = new System.Drawing.Font("Segoe UI Symbol", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l3.Location = new System.Drawing.Point(36, 219);
            this.l3.Name = "l3";
            this.l3.Size = new System.Drawing.Size(183, 28);
            this.l3.TabIndex = 7;
            this.l3.Text = "Application Fees :";
            // 
            // l2
            // 
            this.l2.AutoSize = true;
            this.l2.Font = new System.Drawing.Font("Segoe UI Symbol", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l2.Location = new System.Drawing.Point(33, 121);
            this.l2.Name = "l2";
            this.l2.Size = new System.Drawing.Size(186, 28);
            this.l2.TabIndex = 8;
            this.l2.Text = "Application Date :";
            // 
            // l1
            // 
            this.l1.AutoSize = true;
            this.l1.Font = new System.Drawing.Font("Segoe UI Symbol", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l1.Location = new System.Drawing.Point(22, 76);
            this.l1.Name = "l1";
            this.l1.Size = new System.Drawing.Size(197, 28);
            this.l1.TabIndex = 9;
            this.l1.Text = "D.L.Application ID :";
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::DVLD__Presentation_Tier.Properties.Resources.btnClose_Image;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(703, 701);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(139, 45);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::DVLD__Presentation_Tier.Properties.Resources.Save32;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(848, 701);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(139, 45);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmNewLocalDrivingLicenseApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1016, 758);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNewLocalDrivingLicenseApplication";
            this.Text = "frmNewLocalDrivingLicenseApplication";
            this.Load += new System.EventHandler(this.frmNewLocalDrivingLicenseApplication_Load);
            this.tabControl1.ResumeLayout(false);
            this.tbPersonalInfo.ResumeLayout(false);
            this.tbApplicationInfo.ResumeLayout(false);
            this.tbApplicationInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.ctrlPersonInformationWithFilter ctrlPersonInformationWithFilter1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbPersonalInfo;
        private System.Windows.Forms.TabPage tbApplicationInfo;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label l1;
        private System.Windows.Forms.Label l4;
        private System.Windows.Forms.Label l3;
        private System.Windows.Forms.Label l2;
        private System.Windows.Forms.Label lblApplicationID;
        private System.Windows.Forms.Label lblCreatedName;
        private System.Windows.Forms.Label lblApplicationFees;
        private System.Windows.Forms.Label lblApplicationDate;
        private System.Windows.Forms.Label lblLicenseClass;
        private System.Windows.Forms.ComboBox cbLicenseClasses;
    }
}