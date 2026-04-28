namespace DVLD__Presentation_Tier.Forms
{
    partial class frmLogin
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
            this.guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.guna2ProgressIndicator1 = new Guna.UI2.WinForms.Guna2ProgressIndicator();
            this.btnShowPassword = new Guna.UI2.WinForms.Guna2CircleButton();
            this.tbPassword = new Guna.UI2.WinForms.Guna2TextBox();
            this.tbUsername = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnLogin = new Guna.UI2.WinForms.Guna2GradientButton();
            this.cbRemaindme = new Guna.UI2.WinForms.Guna2CustomCheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.guna2GradientPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SteelBlue;
            this.label1.Location = new System.Drawing.Point(184, 43);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Welcom";
            // 
            // guna2GradientPanel1
            // 
            this.guna2GradientPanel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2GradientPanel1.BorderRadius = 40;
            this.guna2GradientPanel1.BorderThickness = 1;
            this.guna2GradientPanel1.Controls.Add(this.guna2ProgressIndicator1);
            this.guna2GradientPanel1.Controls.Add(this.btnShowPassword);
            this.guna2GradientPanel1.Controls.Add(this.tbPassword);
            this.guna2GradientPanel1.Controls.Add(this.tbUsername);
            this.guna2GradientPanel1.Controls.Add(this.btnLogin);
            this.guna2GradientPanel1.Controls.Add(this.cbRemaindme);
            this.guna2GradientPanel1.Controls.Add(this.label2);
            this.guna2GradientPanel1.Controls.Add(this.label1);
            this.guna2GradientPanel1.CustomizableEdges.BottomLeft = false;
            this.guna2GradientPanel1.CustomizableEdges.TopRight = false;
            this.guna2GradientPanel1.FillColor = System.Drawing.Color.AliceBlue;
            this.guna2GradientPanel1.FillColor2 = System.Drawing.Color.AliceBlue;
            this.guna2GradientPanel1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            this.guna2GradientPanel1.Location = new System.Drawing.Point(220, 60);
            this.guna2GradientPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.guna2GradientPanel1.Name = "guna2GradientPanel1";
            this.guna2GradientPanel1.ShadowDecoration.BorderRadius = 40;
            this.guna2GradientPanel1.ShadowDecoration.Color = System.Drawing.Color.DarkGray;
            this.guna2GradientPanel1.ShadowDecoration.Depth = 5;
            this.guna2GradientPanel1.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(10);
            this.guna2GradientPanel1.Size = new System.Drawing.Size(468, 434);
            this.guna2GradientPanel1.TabIndex = 7;
            // 
            // guna2ProgressIndicator1
            // 
            this.guna2ProgressIndicator1.AutoStart = true;
            this.guna2ProgressIndicator1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ProgressIndicator1.CircleSize = 0.7F;
            this.guna2ProgressIndicator1.Enabled = false;
            this.guna2ProgressIndicator1.Location = new System.Drawing.Point(190, 353);
            this.guna2ProgressIndicator1.Name = "guna2ProgressIndicator1";
            this.guna2ProgressIndicator1.ProgressColor = System.Drawing.Color.SteelBlue;
            this.guna2ProgressIndicator1.Size = new System.Drawing.Size(74, 68);
            this.guna2ProgressIndicator1.TabIndex = 11;
            this.guna2ProgressIndicator1.UseTransparentBackground = true;
            this.guna2ProgressIndicator1.Visible = false;
            // 
            // btnShowPassword
            // 
            this.btnShowPassword.Animated = true;
            this.btnShowPassword.BorderColor = System.Drawing.Color.SteelBlue;
            this.btnShowPassword.BorderThickness = 2;
            this.btnShowPassword.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnShowPassword.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnShowPassword.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnShowPassword.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnShowPassword.FillColor = System.Drawing.Color.White;
            this.btnShowPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnShowPassword.ForeColor = System.Drawing.Color.White;
            this.btnShowPassword.Image = global::DVLD__Presentation_Tier.Properties.Resources.Vision;
            this.btnShowPassword.Location = new System.Drawing.Point(335, 212);
            this.btnShowPassword.Margin = new System.Windows.Forms.Padding(2);
            this.btnShowPassword.Name = "btnShowPassword";
            this.btnShowPassword.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnShowPassword.Size = new System.Drawing.Size(47, 47);
            this.btnShowPassword.TabIndex = 5;
            this.btnShowPassword.Click += new System.EventHandler(this.btnShowPassword_Click);
            // 
            // tbPassword
            // 
            this.tbPassword.Animated = true;
            this.tbPassword.BorderRadius = 10;
            this.tbPassword.BorderThickness = 2;
            this.tbPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbPassword.DefaultText = "";
            this.tbPassword.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tbPassword.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tbPassword.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbPassword.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbPassword.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.tbPassword.ForeColor = System.Drawing.Color.Black;
            this.tbPassword.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbPassword.IconRight = global::DVLD__Presentation_Tier.Properties.Resources.Password;
            this.tbPassword.IconRightSize = new System.Drawing.Size(25, 25);
            this.tbPassword.Location = new System.Drawing.Point(77, 212);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PlaceholderForeColor = System.Drawing.Color.SteelBlue;
            this.tbPassword.PlaceholderText = "Password";
            this.tbPassword.SelectedText = "";
            this.tbPassword.Size = new System.Drawing.Size(253, 47);
            this.tbPassword.TabIndex = 2;
            // 
            // tbUsername
            // 
            this.tbUsername.Animated = true;
            this.tbUsername.BorderRadius = 10;
            this.tbUsername.BorderThickness = 2;
            this.tbUsername.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbUsername.DefaultText = "";
            this.tbUsername.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tbUsername.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tbUsername.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbUsername.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbUsername.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbUsername.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.tbUsername.ForeColor = System.Drawing.Color.Black;
            this.tbUsername.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbUsername.IconRight = global::DVLD__Presentation_Tier.Properties.Resources.Person32;
            this.tbUsername.IconRightSize = new System.Drawing.Size(25, 25);
            this.tbUsername.Location = new System.Drawing.Point(77, 135);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.PlaceholderForeColor = System.Drawing.Color.SteelBlue;
            this.tbUsername.PlaceholderText = "Username";
            this.tbUsername.SelectedText = "";
            this.tbUsername.Size = new System.Drawing.Size(305, 46);
            this.tbUsername.TabIndex = 1;
            // 
            // btnLogin
            // 
            this.btnLogin.Animated = true;
            this.btnLogin.BorderColor = System.Drawing.Color.SteelBlue;
            this.btnLogin.BorderRadius = 10;
            this.btnLogin.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLogin.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLogin.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLogin.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLogin.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLogin.FillColor = System.Drawing.Color.SteelBlue;
            this.btnLogin.FillColor2 = System.Drawing.Color.SteelBlue;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.HoverState.BorderColor = System.Drawing.Color.SteelBlue;
            this.btnLogin.Image = global::DVLD__Presentation_Tier.Properties.Resources.Next32;
            this.btnLogin.ImageAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.btnLogin.ImageOffset = new System.Drawing.Point(10, 0);
            this.btnLogin.ImageSize = new System.Drawing.Size(30, 30);
            this.btnLogin.Location = new System.Drawing.Point(114, 365);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(2);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(239, 47);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "Login";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // cbRemaindme
            // 
            this.cbRemaindme.Checked = true;
            this.cbRemaindme.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbRemaindme.CheckedState.BorderRadius = 2;
            this.cbRemaindme.CheckedState.BorderThickness = 0;
            this.cbRemaindme.CheckedState.FillColor = System.Drawing.Color.SteelBlue;
            this.cbRemaindme.Location = new System.Drawing.Point(77, 282);
            this.cbRemaindme.Margin = new System.Windows.Forms.Padding(2);
            this.cbRemaindme.Name = "cbRemaindme";
            this.cbRemaindme.Size = new System.Drawing.Size(21, 19);
            this.cbRemaindme.TabIndex = 3;
            this.cbRemaindme.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.cbRemaindme.UncheckedState.BorderRadius = 10;
            this.cbRemaindme.UncheckedState.BorderThickness = 1;
            this.cbRemaindme.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.SteelBlue;
            this.label2.Location = new System.Drawing.Point(102, 282);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Remaind Me";
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::DVLD__Presentation_Tier.Properties.Resources.Gemini_Generated_Image_i55v35i55v35i55v;
            this.ClientSize = new System.Drawing.Size(907, 551);
            this.Controls.Add(this.guna2GradientPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmLogin";
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.guna2GradientPanel1.ResumeLayout(false);
            this.guna2GradientPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private Guna.UI2.WinForms.Guna2GradientButton btnLogin;
        private Guna.UI2.WinForms.Guna2CustomCheckBox cbRemaindme;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2TextBox tbUsername;
        private Guna.UI2.WinForms.Guna2CircleButton btnShowPassword;
        private Guna.UI2.WinForms.Guna2TextBox tbPassword;
        private Guna.UI2.WinForms.Guna2ProgressIndicator guna2ProgressIndicator1;
    }
}