namespace DVLD__Presentation_Tier.Forms.DetainedLicenseForms
{
    partial class frmManageDeainedLicense
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
            this.tbFilterInput = new System.Windows.Forms.TextBox();
            this.cbFilterOn = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRecordsCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvDetainedLicense = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.relaseLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnAddDeateinLicense = new System.Windows.Forms.Button();
            this.showLicenseDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetainedLicense)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbFilterInput
            // 
            this.tbFilterInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbFilterInput.Location = new System.Drawing.Point(378, 219);
            this.tbFilterInput.MaxLength = 100;
            this.tbFilterInput.Name = "tbFilterInput";
            this.tbFilterInput.Size = new System.Drawing.Size(264, 26);
            this.tbFilterInput.TabIndex = 25;
            this.tbFilterInput.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // cbFilterOn
            // 
            this.cbFilterOn.BackColor = System.Drawing.SystemColors.MenuBar;
            this.cbFilterOn.FormattingEnabled = true;
            this.cbFilterOn.Location = new System.Drawing.Point(115, 218);
            this.cbFilterOn.Name = "cbFilterOn";
            this.cbFilterOn.Size = new System.Drawing.Size(246, 28);
            this.cbFilterOn.TabIndex = 24;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Nirmala Text", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 218);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 28);
            this.label3.TabIndex = 23;
            this.label3.Text = "Filter By :";
            // 
            // lblRecordsCount
            // 
            this.lblRecordsCount.AutoSize = true;
            this.lblRecordsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordsCount.Location = new System.Drawing.Point(141, 761);
            this.lblRecordsCount.Name = "lblRecordsCount";
            this.lblRecordsCount.Size = new System.Drawing.Size(24, 25);
            this.lblRecordsCount.TabIndex = 21;
            this.lblRecordsCount.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 758);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 25);
            this.label2.TabIndex = 22;
            this.label2.Text = "# Records :";
            // 
            // dgvDetainedLicense
            // 
            this.dgvDetainedLicense.AllowUserToAddRows = false;
            this.dgvDetainedLicense.AllowUserToDeleteRows = false;
            this.dgvDetainedLicense.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetainedLicense.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvDetainedLicense.Location = new System.Drawing.Point(12, 275);
            this.dgvDetainedLicense.Name = "dgvDetainedLicense";
            this.dgvDetainedLicense.ReadOnly = true;
            this.dgvDetainedLicense.RowHeadersWidth = 62;
            this.dgvDetainedLicense.RowTemplate.Height = 28;
            this.dgvDetainedLicense.Size = new System.Drawing.Size(1257, 476);
            this.dgvDetainedLicense.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(433, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(337, 31);
            this.label1.TabIndex = 18;
            this.label1.Text = "Manage Detained Licenses";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showLicenseDataToolStripMenuItem,
            this.toolStripSeparator1,
            this.relaseLicenseToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(279, 107);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(275, 6);
            // 
            // relaseLicenseToolStripMenuItem
            // 
            this.relaseLicenseToolStripMenuItem.Image = global::DVLD__Presentation_Tier.Properties.Resources.ReleaseDetainedLicense32;
            this.relaseLicenseToolStripMenuItem.Name = "relaseLicenseToolStripMenuItem";
            this.relaseLicenseToolStripMenuItem.Size = new System.Drawing.Size(278, 32);
            this.relaseLicenseToolStripMenuItem.Text = "Relase Detained License";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD__Presentation_Tier.Properties.Resources.Detain64;
            this.pictureBox1.Location = new System.Drawing.Point(559, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(117, 114);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 26;
            this.pictureBox1.TabStop = false;
            // 
            // btnAddDeateinLicense
            // 
            this.btnAddDeateinLicense.Image = global::DVLD__Presentation_Tier.Properties.Resources.NewApplication64;
            this.btnAddDeateinLicense.Location = new System.Drawing.Point(1174, 202);
            this.btnAddDeateinLicense.Name = "btnAddDeateinLicense";
            this.btnAddDeateinLicense.Size = new System.Drawing.Size(95, 67);
            this.btnAddDeateinLicense.TabIndex = 20;
            this.btnAddDeateinLicense.UseVisualStyleBackColor = true;
            this.btnAddDeateinLicense.Click += new System.EventHandler(this.btnAddDeateinLicense_Click);
            // 
            // showLicenseDataToolStripMenuItem
            // 
            this.showLicenseDataToolStripMenuItem.Image = global::DVLD__Presentation_Tier.Properties.Resources.DriverLicense48;
            this.showLicenseDataToolStripMenuItem.Name = "showLicenseDataToolStripMenuItem";
            this.showLicenseDataToolStripMenuItem.Size = new System.Drawing.Size(278, 32);
            this.showLicenseDataToolStripMenuItem.Text = "Show License Data";
            this.showLicenseDataToolStripMenuItem.Click += new System.EventHandler(this.showLicenseDataToolStripMenuItem_Click);
            // 
            // frmManageDeainedLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1300, 792);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tbFilterInput);
            this.Controls.Add(this.cbFilterOn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblRecordsCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnAddDeateinLicense);
            this.Controls.Add(this.dgvDetainedLicense);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmManageDeainedLicense";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmManageDeainedLicense";
            this.Load += new System.EventHandler(this.frmManageDeainedLicense_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetainedLicense)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox tbFilterInput;
        private System.Windows.Forms.ComboBox cbFilterOn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblRecordsCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAddDeateinLicense;
        private System.Windows.Forms.DataGridView dgvDetainedLicense;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showLicenseDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem relaseLicenseToolStripMenuItem;
    }
}