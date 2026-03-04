namespace DVLD__Presentation_Tier.Forms.LocalDrivingLicenseForms
{
    partial class frmLocalDrivingLicenseApplication
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
            this.dgvApplicationsList = new System.Windows.Forms.DataGridView();
            this.btnAddNewLDApplication = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblRecordsCount = new System.Windows.Forms.Label();
            this.tbFilterInput = new System.Windows.Forms.TextBox();
            this.cbFilterOn = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvApplicationsList)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(409, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(413, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Local Driving License Application";
            // 
            // dgvApplicationsList
            // 
            this.dgvApplicationsList.AllowUserToAddRows = false;
            this.dgvApplicationsList.AllowUserToDeleteRows = false;
            this.dgvApplicationsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvApplicationsList.Location = new System.Drawing.Point(12, 228);
            this.dgvApplicationsList.Name = "dgvApplicationsList";
            this.dgvApplicationsList.ReadOnly = true;
            this.dgvApplicationsList.RowHeadersWidth = 62;
            this.dgvApplicationsList.RowTemplate.Height = 28;
            this.dgvApplicationsList.Size = new System.Drawing.Size(1257, 476);
            this.dgvApplicationsList.TabIndex = 1;
            // 
            // btnAddNewLDApplication
            // 
            this.btnAddNewLDApplication.Image = global::DVLD__Presentation_Tier.Properties.Resources.NewApplication64;
            this.btnAddNewLDApplication.Location = new System.Drawing.Point(1174, 155);
            this.btnAddNewLDApplication.Name = "btnAddNewLDApplication";
            this.btnAddNewLDApplication.Size = new System.Drawing.Size(95, 67);
            this.btnAddNewLDApplication.TabIndex = 2;
            this.btnAddNewLDApplication.UseVisualStyleBackColor = true;
            this.btnAddNewLDApplication.Click += new System.EventHandler(this.btnAddNewLDApplication_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 711);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "# Records :";
            // 
            // lblRecordsCount
            // 
            this.lblRecordsCount.AutoSize = true;
            this.lblRecordsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordsCount.Location = new System.Drawing.Point(141, 714);
            this.lblRecordsCount.Name = "lblRecordsCount";
            this.lblRecordsCount.Size = new System.Drawing.Size(24, 25);
            this.lblRecordsCount.TabIndex = 3;
            this.lblRecordsCount.Text = "0";
            // 
            // tbFilterInput
            // 
            this.tbFilterInput.Location = new System.Drawing.Point(386, 195);
            this.tbFilterInput.MaxLength = 100;
            this.tbFilterInput.Name = "tbFilterInput";
            this.tbFilterInput.Size = new System.Drawing.Size(264, 26);
            this.tbFilterInput.TabIndex = 8;
            // 
            // cbFilterOn
            // 
            this.cbFilterOn.BackColor = System.Drawing.SystemColors.MenuBar;
            this.cbFilterOn.FormattingEnabled = true;
            this.cbFilterOn.Location = new System.Drawing.Point(123, 194);
            this.cbFilterOn.Name = "cbFilterOn";
            this.cbFilterOn.Size = new System.Drawing.Size(246, 28);
            this.cbFilterOn.TabIndex = 7;
            this.cbFilterOn.SelectedIndexChanged += new System.EventHandler(this.cbFilterOn_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Nirmala Text", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 194);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 28);
            this.label3.TabIndex = 6;
            this.label3.Text = "Filter By :";
            // 
            // frmLocalDrivingLicenseApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1281, 748);
            this.Controls.Add(this.tbFilterInput);
            this.Controls.Add(this.cbFilterOn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblRecordsCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnAddNewLDApplication);
            this.Controls.Add(this.dgvApplicationsList);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLocalDrivingLicenseApplication";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmLocalDrivingLicenseApplication";
            this.Load += new System.EventHandler(this.frmLocalDrivingLicenseApplication_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvApplicationsList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvApplicationsList;
        private System.Windows.Forms.Button btnAddNewLDApplication;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblRecordsCount;
        private System.Windows.Forms.TextBox tbFilterInput;
        private System.Windows.Forms.ComboBox cbFilterOn;
        private System.Windows.Forms.Label label3;
    }
}