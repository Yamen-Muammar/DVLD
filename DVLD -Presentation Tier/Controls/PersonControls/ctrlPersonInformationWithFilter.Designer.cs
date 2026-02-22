namespace DVLD__Presentation_Tier.Controls
{
    partial class ctrlPersonInformationWithFilter
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
            this.ctrlPersonInformation1 = new DVLD__Presentation_Tier.ctrlPersonInformation();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAddPerson = new System.Windows.Forms.Button();
            this.btnSerach = new System.Windows.Forms.Button();
            this.tbFilterInput = new System.Windows.Forms.TextBox();
            this.cbFilterOn = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctrlPersonInformation1
            // 
            this.ctrlPersonInformation1.Location = new System.Drawing.Point(3, 109);
            this.ctrlPersonInformation1.Name = "ctrlPersonInformation1";
            this.ctrlPersonInformation1.Size = new System.Drawing.Size(968, 390);
            this.ctrlPersonInformation1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAddPerson);
            this.groupBox1.Controls.Add(this.btnSerach);
            this.groupBox1.Controls.Add(this.tbFilterInput);
            this.groupBox1.Controls.Add(this.cbFilterOn);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(951, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter";
            // 
            // btnAddPerson
            // 
            this.btnAddPerson.Image = global::DVLD__Presentation_Tier.Properties.Resources.AddPerson;
            this.btnAddPerson.Location = new System.Drawing.Point(714, 36);
            this.btnAddPerson.Name = "btnAddPerson";
            this.btnAddPerson.Size = new System.Drawing.Size(61, 38);
            this.btnAddPerson.TabIndex = 6;
            this.btnAddPerson.UseVisualStyleBackColor = true;
            this.btnAddPerson.Click += new System.EventHandler(this.btnAddPerson_Click);
            // 
            // btnSerach
            // 
            this.btnSerach.Image = global::DVLD__Presentation_Tier.Properties.Resources.SearchPerson;
            this.btnSerach.Location = new System.Drawing.Point(647, 36);
            this.btnSerach.Name = "btnSerach";
            this.btnSerach.Size = new System.Drawing.Size(61, 38);
            this.btnSerach.TabIndex = 6;
            this.btnSerach.UseVisualStyleBackColor = true;
            this.btnSerach.Click += new System.EventHandler(this.btnSerach_Click);
            // 
            // tbFilterInput
            // 
            this.tbFilterInput.Location = new System.Drawing.Point(377, 44);
            this.tbFilterInput.MaxLength = 100;
            this.tbFilterInput.Name = "tbFilterInput";
            this.tbFilterInput.Size = new System.Drawing.Size(264, 26);
            this.tbFilterInput.TabIndex = 5;
            // 
            // cbFilterOn
            // 
            this.cbFilterOn.BackColor = System.Drawing.SystemColors.MenuBar;
            this.cbFilterOn.FormattingEnabled = true;
            this.cbFilterOn.Location = new System.Drawing.Point(114, 43);
            this.cbFilterOn.Name = "cbFilterOn";
            this.cbFilterOn.Size = new System.Drawing.Size(246, 28);
            this.cbFilterOn.TabIndex = 4;
            this.cbFilterOn.SelectedIndexChanged += new System.EventHandler(this.cbFilterOn_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Nirmala Text", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 28);
            this.label1.TabIndex = 3;
            this.label1.Text = "Filter By :";
            // 
            // ctrlPersonInformationWithFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ctrlPersonInformation1);
            this.Name = "ctrlPersonInformationWithFilter";
            this.Size = new System.Drawing.Size(975, 570);
            this.Load += new System.EventHandler(this.ctrlPersonInformationWithFilter_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlPersonInformation ctrlPersonInformation1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAddPerson;
        private System.Windows.Forms.Button btnSerach;
        private System.Windows.Forms.TextBox tbFilterInput;
        private System.Windows.Forms.ComboBox cbFilterOn;
        private System.Windows.Forms.Label label1;
    }
}
