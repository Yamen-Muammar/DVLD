namespace DVLD__Presentation_Tier.Controls.UserControls
{
    partial class ctrlUsersList
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dgvUsersList = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRecordsCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnAddNewUser = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.addNewUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.sendEmailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.phoneCallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsersList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvUsersList
            // 
            this.dgvUsersList.AllowUserToAddRows = false;
            this.dgvUsersList.AllowUserToDeleteRows = false;
            this.dgvUsersList.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvUsersList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsersList.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvUsersList.Location = new System.Drawing.Point(3, 326);
            this.dgvUsersList.Name = "dgvUsersList";
            this.dgvUsersList.ReadOnly = true;
            this.dgvUsersList.RowHeadersWidth = 62;
            this.dgvUsersList.RowTemplate.Height = 28;
            this.dgvUsersList.Size = new System.Drawing.Size(1102, 465);
            this.dgvUsersList.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 794);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 28);
            this.label1.TabIndex = 2;
            this.label1.Text = "#Records :";
            // 
            // lblRecordsCount
            // 
            this.lblRecordsCount.AutoSize = true;
            this.lblRecordsCount.Location = new System.Drawing.Point(116, 802);
            this.lblRecordsCount.Name = "lblRecordsCount";
            this.lblRecordsCount.Size = new System.Drawing.Size(18, 20);
            this.lblRecordsCount.TabIndex = 2;
            this.lblRecordsCount.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Tai Le", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(462, 189);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(199, 35);
            this.label3.TabIndex = 4;
            this.label3.Text = "Manage Users";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Image = global::DVLD__Presentation_Tier.Properties.Resources.Users400;
            this.pictureBox1.Location = new System.Drawing.Point(468, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(193, 183);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // btnAddNewUser
            // 
            this.btnAddNewUser.Image = global::DVLD__Presentation_Tier.Properties.Resources.NewUser72;
            this.btnAddNewUser.Location = new System.Drawing.Point(1029, 267);
            this.btnAddNewUser.Name = "btnAddNewUser";
            this.btnAddNewUser.Size = new System.Drawing.Size(76, 53);
            this.btnAddNewUser.TabIndex = 1;
            this.btnAddNewUser.UseVisualStyleBackColor = true;
            this.btnAddNewUser.Click += new System.EventHandler(this.btnAddNewUser_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showDetailsToolStripMenuItem,
            this.toolStripSeparator1,
            this.addNewUserToolStripMenuItem,
            this.editeToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.changePasswordToolStripMenuItem,
            this.toolStripSeparator2,
            this.sendEmailToolStripMenuItem,
            this.phoneCallToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(233, 240);
            // 
            // showDetailsToolStripMenuItem
            // 
            this.showDetailsToolStripMenuItem.Image = global::DVLD__Presentation_Tier.Properties.Resources.PersonDetails;
            this.showDetailsToolStripMenuItem.Name = "showDetailsToolStripMenuItem";
            this.showDetailsToolStripMenuItem.Size = new System.Drawing.Size(248, 32);
            this.showDetailsToolStripMenuItem.Text = "Show Details";
            this.showDetailsToolStripMenuItem.Click += new System.EventHandler(this.showDetailsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(229, 6);
            // 
            // addNewUserToolStripMenuItem
            // 
            this.addNewUserToolStripMenuItem.Image = global::DVLD__Presentation_Tier.Properties.Resources.NewUser72;
            this.addNewUserToolStripMenuItem.Name = "addNewUserToolStripMenuItem";
            this.addNewUserToolStripMenuItem.Size = new System.Drawing.Size(248, 32);
            this.addNewUserToolStripMenuItem.Text = "Add New User";
            this.addNewUserToolStripMenuItem.Click += new System.EventHandler(this.addNewUserToolStripMenuItem_Click);
            // 
            // editeToolStripMenuItem
            // 
            this.editeToolStripMenuItem.Image = global::DVLD__Presentation_Tier.Properties.Resources.edit32;
            this.editeToolStripMenuItem.Name = "editeToolStripMenuItem";
            this.editeToolStripMenuItem.Size = new System.Drawing.Size(248, 32);
            this.editeToolStripMenuItem.Text = "Edite";
            this.editeToolStripMenuItem.Click += new System.EventHandler(this.editeToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::DVLD__Presentation_Tier.Properties.Resources.Delete32;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(248, 32);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Image = global::DVLD__Presentation_Tier.Properties.Resources.Password;
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            this.changePasswordToolStripMenuItem.Size = new System.Drawing.Size(248, 32);
            this.changePasswordToolStripMenuItem.Text = "Change Password";
            this.changePasswordToolStripMenuItem.Click += new System.EventHandler(this.changePasswordToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(229, 6);
            // 
            // sendEmailToolStripMenuItem
            // 
            this.sendEmailToolStripMenuItem.Image = global::DVLD__Presentation_Tier.Properties.Resources.sendemail32;
            this.sendEmailToolStripMenuItem.Name = "sendEmailToolStripMenuItem";
            this.sendEmailToolStripMenuItem.Size = new System.Drawing.Size(248, 32);
            this.sendEmailToolStripMenuItem.Text = "Send Email";
            this.sendEmailToolStripMenuItem.Click += new System.EventHandler(this.sendEmailToolStripMenuItem_Click);
            // 
            // phoneCallToolStripMenuItem
            // 
            this.phoneCallToolStripMenuItem.Image = global::DVLD__Presentation_Tier.Properties.Resources.call32;
            this.phoneCallToolStripMenuItem.Name = "phoneCallToolStripMenuItem";
            this.phoneCallToolStripMenuItem.Size = new System.Drawing.Size(248, 32);
            this.phoneCallToolStripMenuItem.Text = "Phone Call";
            this.phoneCallToolStripMenuItem.Click += new System.EventHandler(this.phoneCallToolStripMenuItem_Click);
            // 
            // ctrlUsersList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblRecordsCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddNewUser);
            this.Controls.Add(this.dgvUsersList);
            this.Name = "ctrlUsersList";
            this.Size = new System.Drawing.Size(1108, 837);
            this.Load += new System.EventHandler(this.ctrlUsersList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsersList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvUsersList;
        private System.Windows.Forms.Button btnAddNewUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRecordsCount;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem addNewUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem sendEmailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem phoneCallToolStripMenuItem;
    }
}
