namespace DVLD__Presentation_Tier.Forms.TestsAppointment
{
    partial class frmVisionTestAppointment
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
            this.ctrlLDLAwithApplicationInformation1 = new DVLD__Presentation_Tier.Controls.LocalDLApplicationsControls.ctrlLDLAwithApplicationInformation();
            this.SuspendLayout();
            // 
            // ctrlLDLAwithApplicationInformation1
            // 
            this.ctrlLDLAwithApplicationInformation1.Location = new System.Drawing.Point(12, 12);
            this.ctrlLDLAwithApplicationInformation1.Name = "ctrlLDLAwithApplicationInformation1";
            this.ctrlLDLAwithApplicationInformation1.Size = new System.Drawing.Size(826, 595);
            this.ctrlLDLAwithApplicationInformation1.TabIndex = 0;
            // 
            // frmVisionTestAppointment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(849, 734);
            this.Controls.Add(this.ctrlLDLAwithApplicationInformation1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmVisionTestAppointment";
            this.Text = "frmVisionTestAppointment";
            this.ResumeLayout(false);

        }
        private void InitializeComponent(int LDLAppID)
        {
            this.ctrlLDLAwithApplicationInformation1 = new DVLD__Presentation_Tier.Controls.LocalDLApplicationsControls.ctrlLDLAwithApplicationInformation();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgvAppointments = new System.Windows.Forms.DataGridView();
            this.btnClosefrm = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrlLDLAwithApplicationInformation1
            // 
            this.ctrlLDLAwithApplicationInformation1.Location = new System.Drawing.Point(12, 117);
            this.ctrlLDLAwithApplicationInformation1.Name = "ctrlLDLAwithApplicationInformation1";
            this.ctrlLDLAwithApplicationInformation1.Size = new System.Drawing.Size(826, 595);
            this.ctrlLDLAwithApplicationInformation1.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Image = global::DVLD__Presentation_Tier.Properties.Resources.AddAppointment32;
            this.btnSave.Location = new System.Drawing.Point(762, 718);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 63);
            this.btnSave.TabIndex = 1;
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // dgvAppointments
            // 
            this.dgvAppointments.AllowUserToAddRows = false;
            this.dgvAppointments.AllowUserToDeleteRows = false;
            this.dgvAppointments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAppointments.Location = new System.Drawing.Point(12, 787);
            this.dgvAppointments.Name = "dgvAppointments";
            this.dgvAppointments.ReadOnly = true;
            this.dgvAppointments.RowHeadersWidth = 62;
            this.dgvAppointments.RowTemplate.Height = 28;
            this.dgvAppointments.Size = new System.Drawing.Size(825, 197);
            this.dgvAppointments.TabIndex = 2;
            // 
            // btnClosefrm
            // 
            this.btnClosefrm.Image = global::DVLD__Presentation_Tier.Properties.Resources.btnClose_Image;
            this.btnClosefrm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClosefrm.Location = new System.Drawing.Point(626, 990);
            this.btnClosefrm.Name = "btnClosefrm";
            this.btnClosefrm.Size = new System.Drawing.Size(212, 42);
            this.btnClosefrm.TabIndex = 1;
            this.btnClosefrm.Text = "Close";
            this.btnClosefrm.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("72 Monospace", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 737);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 22);
            this.label2.TabIndex = 3;
            this.label2.Text = "Appointments";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("72 Monospace", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(194, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(422, 31);
            this.label3.TabIndex = 4;
            this.label3.Text = "Vision Test Appointments";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD__Presentation_Tier.Properties.Resources.Vision;
            this.pictureBox1.Location = new System.Drawing.Point(362, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(84, 73);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // frmVisionTestAppointment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(849, 1044);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvAppointments);
            this.Controls.Add(this.btnClosefrm);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.ctrlLDLAwithApplicationInformation1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmVisionTestAppointment";
            this.Text = "frmVisionTestAppointment";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.LocalDLApplicationsControls.ctrlLDLAwithApplicationInformation ctrlLDLAwithApplicationInformation1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddNewAppointment;
        private System.Windows.Forms.DataGridView dgvAppointment;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgvAppointments;
        private System.Windows.Forms.Button btnClosefrm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}