namespace DVLD__Presentation_Tier.Forms.UserForms
{
    partial class frmAddNewUser
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.PersonInfo = new System.Windows.Forms.TabPage();
            this.btnNext = new System.Windows.Forms.Button();
            this.LoginInfo = new System.Windows.Forms.TabPage();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbNewID = new System.Windows.Forms.Label();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tbConfirmPassword = new System.Windows.Forms.TextBox();
            this.cbIsActive = new System.Windows.Forms.CheckBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.ctrlPersonInformationWithFilter1 = new DVLD__Presentation_Tier.Controls.ctrlPersonInformationWithFilter();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.PersonInfo.SuspendLayout();
            this.LoginInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.PersonInfo);
            this.tabControl1.Controls.Add(this.LoginInfo);
            this.tabControl1.Location = new System.Drawing.Point(12, 71);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(963, 589);
            this.tabControl1.TabIndex = 1;
            // 
            // PersonInfo
            // 
            this.PersonInfo.Controls.Add(this.btnNext);
            this.PersonInfo.Controls.Add(this.ctrlPersonInformationWithFilter1);
            this.PersonInfo.Location = new System.Drawing.Point(4, 29);
            this.PersonInfo.Name = "PersonInfo";
            this.PersonInfo.Padding = new System.Windows.Forms.Padding(3);
            this.PersonInfo.Size = new System.Drawing.Size(955, 556);
            this.PersonInfo.TabIndex = 0;
            this.PersonInfo.Text = "Person Info";
            this.PersonInfo.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Microsoft Tai Le", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Image = global::DVLD__Presentation_Tier.Properties.Resources.Next32;
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNext.Location = new System.Drawing.Point(762, 462);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(153, 43);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // LoginInfo
            // 
            this.LoginInfo.Controls.Add(this.cbIsActive);
            this.LoginInfo.Controls.Add(this.tbConfirmPassword);
            this.LoginInfo.Controls.Add(this.tbPassword);
            this.LoginInfo.Controls.Add(this.tbUsername);
            this.LoginInfo.Controls.Add(this.lbNewID);
            this.LoginInfo.Controls.Add(this.label4);
            this.LoginInfo.Controls.Add(this.label3);
            this.LoginInfo.Controls.Add(this.label2);
            this.LoginInfo.Controls.Add(this.label1);
            this.LoginInfo.Location = new System.Drawing.Point(4, 29);
            this.LoginInfo.Name = "LoginInfo";
            this.LoginInfo.Padding = new System.Windows.Forms.Padding(3);
            this.LoginInfo.Size = new System.Drawing.Size(955, 556);
            this.LoginInfo.TabIndex = 1;
            this.LoginInfo.Text = "Login Info";
            this.LoginInfo.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::DVLD__Presentation_Tier.Properties.Resources.Save32;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(824, 663);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(139, 45);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::DVLD__Presentation_Tier.Properties.Resources.btnClose_Image;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(679, 663);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(139, 45);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Symbol", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(177, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "User ID :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Symbol", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(148, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 28);
            this.label2.TabIndex = 0;
            this.label2.Text = "UserName :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Symbol", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(157, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 28);
            this.label3.TabIndex = 0;
            this.label3.Text = "Password :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Symbol", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(74, 211);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(196, 28);
            this.label4.TabIndex = 0;
            this.label4.Text = "Confirm Password :";
            // 
            // lbNewID
            // 
            this.lbNewID.AutoSize = true;
            this.lbNewID.Font = new System.Drawing.Font("Simplified Arabic Fixed", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNewID.ForeColor = System.Drawing.Color.Red;
            this.lbNewID.Location = new System.Drawing.Point(320, 84);
            this.lbNewID.Name = "lbNewID";
            this.lbNewID.Size = new System.Drawing.Size(54, 23);
            this.lbNewID.TabIndex = 0;
            this.lbNewID.Text = "????";
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(277, 124);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(249, 26);
            this.tbUsername.TabIndex = 1;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(277, 171);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(249, 26);
            this.tbPassword.TabIndex = 1;
            // 
            // tbConfirmPassword
            // 
            this.errorProvider1.SetError(this.tbConfirmPassword, "Does not Match");
            this.tbConfirmPassword.Location = new System.Drawing.Point(276, 215);
            this.tbConfirmPassword.Name = "tbConfirmPassword";
            this.tbConfirmPassword.Size = new System.Drawing.Size(249, 26);
            this.tbConfirmPassword.TabIndex = 1;
            this.tbConfirmPassword.Leave += new System.EventHandler(this.tbConfirmPassword_Leave);
            // 
            // cbIsActive
            // 
            this.cbIsActive.AutoSize = true;
            this.cbIsActive.Checked = true;
            this.cbIsActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIsActive.Location = new System.Drawing.Point(277, 273);
            this.cbIsActive.Name = "cbIsActive";
            this.cbIsActive.Size = new System.Drawing.Size(89, 24);
            this.cbIsActive.TabIndex = 2;
            this.cbIsActive.Text = "isActive";
            this.cbIsActive.UseVisualStyleBackColor = true;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ctrlPersonInformationWithFilter1
            // 
            this.ctrlPersonInformationWithFilter1.Location = new System.Drawing.Point(6, 6);
            this.ctrlPersonInformationWithFilter1.Name = "ctrlPersonInformationWithFilter1";
            this.ctrlPersonInformationWithFilter1.Size = new System.Drawing.Size(943, 450);
            this.ctrlPersonInformationWithFilter1.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Simplified Arabic", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Red;
            this.lblTitle.Location = new System.Drawing.Point(345, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(278, 66);
            this.lblTitle.TabIndex = 17;
            this.lblTitle.Text = "Add New User";
            // 
            // frmAddNewUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 720);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmAddNewUser";
            this.Text = "frmAddNewUser";
            this.tabControl1.ResumeLayout(false);
            this.PersonInfo.ResumeLayout(false);
            this.LoginInfo.ResumeLayout(false);
            this.LoginInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.ctrlPersonInformationWithFilter ctrlPersonInformationWithFilter1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage PersonInfo;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.TabPage LoginInfo;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lbNewID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbIsActive;
        private System.Windows.Forms.TextBox tbConfirmPassword;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.Label lblTitle;
    }
}