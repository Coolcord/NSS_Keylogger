namespace NSS_Keylogger
{
    partial class NSSKeyloggerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NSSKeyloggerForm));
            this.tcNSSKeylogger = new System.Windows.Forms.TabControl();
            this.tbKeylogger = new System.Windows.Forms.TabPage();
            this.pbKeyloggerRunning = new System.Windows.Forms.PictureBox();
            this.lblKeyloggerRunning = new System.Windows.Forms.Label();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.tbPreferences = new System.Windows.Forms.TabPage();
            this.cbClearOnStartup = new System.Windows.Forms.CheckBox();
            this.btnViewKeylog = new System.Windows.Forms.Button();
            this.btnClearKeylog = new System.Windows.Forms.Button();
            this.cbUseSubtleModeButton = new System.Windows.Forms.CheckBox();
            this.btnSubtleModeButton = new System.Windows.Forms.Button();
            this.tbSubtleModeButton = new System.Windows.Forms.TextBox();
            this.lblSubtleModeButton = new System.Windows.Forms.Label();
            this.cbStartupSubtleMode = new System.Windows.Forms.CheckBox();
            this.cbSubtleMode = new System.Windows.Forms.CheckBox();
            this.btnKeylogSaveLocation = new System.Windows.Forms.Button();
            this.tbKeylogSaveLocation = new System.Windows.Forms.TextBox();
            this.lblKeylogSaveLocation = new System.Windows.Forms.Label();
            this.cbSuppressHotkeys = new System.Windows.Forms.CheckBox();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnDeactivationButton = new System.Windows.Forms.Button();
            this.btnActivationButton = new System.Windows.Forms.Button();
            this.btnManageProfiles = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tbDeactivationButton = new System.Windows.Forms.TextBox();
            this.lblDeactivationButton = new System.Windows.Forms.Label();
            this.cbUseDeactivationButton = new System.Windows.Forms.CheckBox();
            this.tbActivationButton = new System.Windows.Forms.TextBox();
            this.lblActivationButton = new System.Windows.Forms.Label();
            this.lblSelectProfile = new System.Windows.Forms.Label();
            this.ddbProfile = new System.Windows.Forms.ComboBox();
            this.tcNSSKeylogger.SuspendLayout();
            this.tbKeylogger.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbKeyloggerRunning)).BeginInit();
            this.tbPreferences.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcNSSKeylogger
            // 
            this.tcNSSKeylogger.Controls.Add(this.tbKeylogger);
            this.tcNSSKeylogger.Controls.Add(this.tbPreferences);
            this.tcNSSKeylogger.Location = new System.Drawing.Point(-1, 0);
            this.tcNSSKeylogger.Name = "tcNSSKeylogger";
            this.tcNSSKeylogger.SelectedIndex = 0;
            this.tcNSSKeylogger.Size = new System.Drawing.Size(529, 341);
            this.tcNSSKeylogger.TabIndex = 0;
            this.tcNSSKeylogger.SelectedIndexChanged += new System.EventHandler(this.tcNSSKeylogger_SelectedIndexChanged);
            // 
            // tbKeylogger
            // 
            this.tbKeylogger.BackColor = System.Drawing.Color.Black;
            this.tbKeylogger.Controls.Add(this.pbKeyloggerRunning);
            this.tbKeylogger.Controls.Add(this.lblKeyloggerRunning);
            this.tbKeylogger.Controls.Add(this.lblInstructions);
            this.tbKeylogger.Location = new System.Drawing.Point(4, 25);
            this.tbKeylogger.Name = "tbKeylogger";
            this.tbKeylogger.Padding = new System.Windows.Forms.Padding(3);
            this.tbKeylogger.Size = new System.Drawing.Size(521, 312);
            this.tbKeylogger.TabIndex = 0;
            this.tbKeylogger.Text = "Keylogger";
            // 
            // pbKeyloggerRunning
            // 
            this.pbKeyloggerRunning.Image = global::NSS_Keylogger.Properties.Resources.RedCircle;
            this.pbKeyloggerRunning.ImageLocation = "";
            this.pbKeyloggerRunning.Location = new System.Drawing.Point(203, 239);
            this.pbKeyloggerRunning.Name = "pbKeyloggerRunning";
            this.pbKeyloggerRunning.Size = new System.Drawing.Size(29, 29);
            this.pbKeyloggerRunning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbKeyloggerRunning.TabIndex = 6;
            this.pbKeyloggerRunning.TabStop = false;
            // 
            // lblKeyloggerRunning
            // 
            this.lblKeyloggerRunning.AutoSize = true;
            this.lblKeyloggerRunning.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKeyloggerRunning.ForeColor = System.Drawing.Color.Red;
            this.lblKeyloggerRunning.Location = new System.Drawing.Point(238, 239);
            this.lblKeyloggerRunning.Name = "lblKeyloggerRunning";
            this.lblKeyloggerRunning.Size = new System.Drawing.Size(112, 29);
            this.lblKeyloggerRunning.TabIndex = 4;
            this.lblKeyloggerRunning.Text = "Disabled";
            // 
            // lblInstructions
            // 
            this.lblInstructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstructions.ForeColor = System.Drawing.Color.White;
            this.lblInstructions.Location = new System.Drawing.Point(9, 3);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(499, 198);
            this.lblInstructions.TabIndex = 0;
            this.lblInstructions.Text = "Press ~ to enable Keylogger";
            this.lblInstructions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbPreferences
            // 
            this.tbPreferences.BackColor = System.Drawing.Color.Black;
            this.tbPreferences.Controls.Add(this.cbClearOnStartup);
            this.tbPreferences.Controls.Add(this.btnViewKeylog);
            this.tbPreferences.Controls.Add(this.btnClearKeylog);
            this.tbPreferences.Controls.Add(this.cbUseSubtleModeButton);
            this.tbPreferences.Controls.Add(this.btnSubtleModeButton);
            this.tbPreferences.Controls.Add(this.tbSubtleModeButton);
            this.tbPreferences.Controls.Add(this.lblSubtleModeButton);
            this.tbPreferences.Controls.Add(this.cbStartupSubtleMode);
            this.tbPreferences.Controls.Add(this.cbSubtleMode);
            this.tbPreferences.Controls.Add(this.btnKeylogSaveLocation);
            this.tbPreferences.Controls.Add(this.tbKeylogSaveLocation);
            this.tbPreferences.Controls.Add(this.lblKeylogSaveLocation);
            this.tbPreferences.Controls.Add(this.cbSuppressHotkeys);
            this.tbPreferences.Controls.Add(this.btnAbout);
            this.tbPreferences.Controls.Add(this.btnDeactivationButton);
            this.tbPreferences.Controls.Add(this.btnActivationButton);
            this.tbPreferences.Controls.Add(this.btnManageProfiles);
            this.tbPreferences.Controls.Add(this.btnSave);
            this.tbPreferences.Controls.Add(this.tbDeactivationButton);
            this.tbPreferences.Controls.Add(this.lblDeactivationButton);
            this.tbPreferences.Controls.Add(this.cbUseDeactivationButton);
            this.tbPreferences.Controls.Add(this.tbActivationButton);
            this.tbPreferences.Controls.Add(this.lblActivationButton);
            this.tbPreferences.Controls.Add(this.lblSelectProfile);
            this.tbPreferences.Controls.Add(this.ddbProfile);
            this.tbPreferences.Location = new System.Drawing.Point(4, 25);
            this.tbPreferences.Name = "tbPreferences";
            this.tbPreferences.Padding = new System.Windows.Forms.Padding(3);
            this.tbPreferences.Size = new System.Drawing.Size(521, 312);
            this.tbPreferences.TabIndex = 1;
            this.tbPreferences.Text = "Preferences";
            // 
            // cbClearOnStartup
            // 
            this.cbClearOnStartup.AutoSize = true;
            this.cbClearOnStartup.ForeColor = System.Drawing.Color.White;
            this.cbClearOnStartup.Location = new System.Drawing.Point(10, 175);
            this.cbClearOnStartup.Name = "cbClearOnStartup";
            this.cbClearOnStartup.Size = new System.Drawing.Size(180, 21);
            this.cbClearOnStartup.TabIndex = 42;
            this.cbClearOnStartup.Text = "Clear Keylog on Startup";
            this.cbClearOnStartup.UseVisualStyleBackColor = true;
            this.cbClearOnStartup.CheckedChanged += new System.EventHandler(this.cbClearOnStartup_CheckedChanged);
            // 
            // btnViewKeylog
            // 
            this.btnViewKeylog.Location = new System.Drawing.Point(125, 139);
            this.btnViewKeylog.Name = "btnViewKeylog";
            this.btnViewKeylog.Size = new System.Drawing.Size(111, 30);
            this.btnViewKeylog.TabIndex = 41;
            this.btnViewKeylog.Text = "View Keylog";
            this.btnViewKeylog.UseVisualStyleBackColor = true;
            this.btnViewKeylog.Click += new System.EventHandler(this.btnViewKeylog_Click);
            // 
            // btnClearKeylog
            // 
            this.btnClearKeylog.Location = new System.Drawing.Point(10, 139);
            this.btnClearKeylog.Name = "btnClearKeylog";
            this.btnClearKeylog.Size = new System.Drawing.Size(109, 30);
            this.btnClearKeylog.TabIndex = 40;
            this.btnClearKeylog.Text = "Clear Keylog";
            this.btnClearKeylog.UseVisualStyleBackColor = true;
            this.btnClearKeylog.Click += new System.EventHandler(this.btnClearKeylog_Click);
            // 
            // cbUseSubtleModeButton
            // 
            this.cbUseSubtleModeButton.AutoSize = true;
            this.cbUseSubtleModeButton.Enabled = false;
            this.cbUseSubtleModeButton.ForeColor = System.Drawing.Color.White;
            this.cbUseSubtleModeButton.Location = new System.Drawing.Point(245, 211);
            this.cbUseSubtleModeButton.Name = "cbUseSubtleModeButton";
            this.cbUseSubtleModeButton.Size = new System.Drawing.Size(265, 21);
            this.cbUseSubtleModeButton.TabIndex = 39;
            this.cbUseSubtleModeButton.Text = "Use Different Hotkey for Subtle Mode";
            this.cbUseSubtleModeButton.UseVisualStyleBackColor = true;
            this.cbUseSubtleModeButton.Visible = false;
            this.cbUseSubtleModeButton.CheckedChanged += new System.EventHandler(this.cbUseSubtleModeButton_CheckedChanged);
            // 
            // btnSubtleModeButton
            // 
            this.btnSubtleModeButton.Enabled = false;
            this.btnSubtleModeButton.Location = new System.Drawing.Point(417, 256);
            this.btnSubtleModeButton.Name = "btnSubtleModeButton";
            this.btnSubtleModeButton.Size = new System.Drawing.Size(42, 23);
            this.btnSubtleModeButton.TabIndex = 38;
            this.btnSubtleModeButton.Text = "Set";
            this.btnSubtleModeButton.UseVisualStyleBackColor = true;
            this.btnSubtleModeButton.Visible = false;
            this.btnSubtleModeButton.Click += new System.EventHandler(this.SubtleModeButton_Click);
            // 
            // tbSubtleModeButton
            // 
            this.tbSubtleModeButton.Enabled = false;
            this.tbSubtleModeButton.Location = new System.Drawing.Point(245, 255);
            this.tbSubtleModeButton.Name = "tbSubtleModeButton";
            this.tbSubtleModeButton.ReadOnly = true;
            this.tbSubtleModeButton.Size = new System.Drawing.Size(166, 22);
            this.tbSubtleModeButton.TabIndex = 37;
            this.tbSubtleModeButton.Text = "` (~)";
            this.tbSubtleModeButton.Visible = false;
            this.tbSubtleModeButton.Click += new System.EventHandler(this.SubtleModeButton_Click);
            this.tbSubtleModeButton.TextChanged += new System.EventHandler(this.tbSubtleModeButton_TextChanged);
            // 
            // lblSubtleModeButton
            // 
            this.lblSubtleModeButton.AutoSize = true;
            this.lblSubtleModeButton.Enabled = false;
            this.lblSubtleModeButton.ForeColor = System.Drawing.Color.White;
            this.lblSubtleModeButton.Location = new System.Drawing.Point(242, 235);
            this.lblSubtleModeButton.Name = "lblSubtleModeButton";
            this.lblSubtleModeButton.Size = new System.Drawing.Size(139, 17);
            this.lblSubtleModeButton.TabIndex = 36;
            this.lblSubtleModeButton.Text = "Subtle Mode Hotkey:";
            this.lblSubtleModeButton.Visible = false;
            // 
            // cbStartupSubtleMode
            // 
            this.cbStartupSubtleMode.AutoSize = true;
            this.cbStartupSubtleMode.Enabled = false;
            this.cbStartupSubtleMode.ForeColor = System.Drawing.Color.White;
            this.cbStartupSubtleMode.Location = new System.Drawing.Point(10, 229);
            this.cbStartupSubtleMode.Name = "cbStartupSubtleMode";
            this.cbStartupSubtleMode.Size = new System.Drawing.Size(158, 21);
            this.cbStartupSubtleMode.TabIndex = 35;
            this.cbStartupSubtleMode.Text = "Start in Subtle Mode";
            this.cbStartupSubtleMode.UseVisualStyleBackColor = true;
            this.cbStartupSubtleMode.Visible = false;
            this.cbStartupSubtleMode.CheckedChanged += new System.EventHandler(this.cbStartupSubtleMode_CheckedChanged);
            // 
            // cbSubtleMode
            // 
            this.cbSubtleMode.AutoSize = true;
            this.cbSubtleMode.ForeColor = System.Drawing.Color.White;
            this.cbSubtleMode.Location = new System.Drawing.Point(10, 202);
            this.cbSubtleMode.Name = "cbSubtleMode";
            this.cbSubtleMode.Size = new System.Drawing.Size(109, 21);
            this.cbSubtleMode.TabIndex = 34;
            this.cbSubtleMode.Text = "Subtle Mode";
            this.cbSubtleMode.UseVisualStyleBackColor = true;
            this.cbSubtleMode.CheckedChanged += new System.EventHandler(this.cbSubtleMode_CheckedChanged);
            // 
            // btnKeylogSaveLocation
            // 
            this.btnKeylogSaveLocation.Location = new System.Drawing.Point(194, 112);
            this.btnKeylogSaveLocation.Name = "btnKeylogSaveLocation";
            this.btnKeylogSaveLocation.Size = new System.Drawing.Size(42, 23);
            this.btnKeylogSaveLocation.TabIndex = 33;
            this.btnKeylogSaveLocation.Text = "...";
            this.btnKeylogSaveLocation.UseVisualStyleBackColor = true;
            this.btnKeylogSaveLocation.Click += new System.EventHandler(this.btnKeylogSaveLocation_Click);
            // 
            // tbKeylogSaveLocation
            // 
            this.tbKeylogSaveLocation.Location = new System.Drawing.Point(9, 111);
            this.tbKeylogSaveLocation.Name = "tbKeylogSaveLocation";
            this.tbKeylogSaveLocation.ReadOnly = true;
            this.tbKeylogSaveLocation.Size = new System.Drawing.Size(179, 22);
            this.tbKeylogSaveLocation.TabIndex = 32;
            this.tbKeylogSaveLocation.Click += new System.EventHandler(this.btnKeylogSaveLocation_Click);
            this.tbKeylogSaveLocation.TextChanged += new System.EventHandler(this.tbKeylogSaveLocation_TextChanged);
            // 
            // lblKeylogSaveLocation
            // 
            this.lblKeylogSaveLocation.AutoSize = true;
            this.lblKeylogSaveLocation.ForeColor = System.Drawing.Color.White;
            this.lblKeylogSaveLocation.Location = new System.Drawing.Point(7, 91);
            this.lblKeylogSaveLocation.Name = "lblKeylogSaveLocation";
            this.lblKeylogSaveLocation.Size = new System.Drawing.Size(149, 17);
            this.lblKeylogSaveLocation.TabIndex = 31;
            this.lblKeylogSaveLocation.Text = "Keylog Save Location:";
            // 
            // cbSuppressHotkeys
            // 
            this.cbSuppressHotkeys.AutoSize = true;
            this.cbSuppressHotkeys.ForeColor = System.Drawing.Color.White;
            this.cbSuppressHotkeys.Location = new System.Drawing.Point(245, 67);
            this.cbSuppressHotkeys.Name = "cbSuppressHotkeys";
            this.cbSuppressHotkeys.Size = new System.Drawing.Size(145, 21);
            this.cbSuppressHotkeys.TabIndex = 30;
            this.cbSuppressHotkeys.Text = "Suppress Hotkeys";
            this.cbSuppressHotkeys.UseVisualStyleBackColor = true;
            this.cbSuppressHotkeys.CheckedChanged += new System.EventHandler(this.cbSuppressHotkeys_CheckedChanged);
            // 
            // btnAbout
            // 
            this.btnAbout.Location = new System.Drawing.Point(246, 10);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(264, 51);
            this.btnAbout.TabIndex = 28;
            this.btnAbout.Text = "About Not-So-Subtle Keylogger";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnDeactivationButton
            // 
            this.btnDeactivationButton.Enabled = false;
            this.btnDeactivationButton.Location = new System.Drawing.Point(417, 182);
            this.btnDeactivationButton.Name = "btnDeactivationButton";
            this.btnDeactivationButton.Size = new System.Drawing.Size(42, 23);
            this.btnDeactivationButton.TabIndex = 23;
            this.btnDeactivationButton.Text = "Set";
            this.btnDeactivationButton.UseVisualStyleBackColor = true;
            this.btnDeactivationButton.Visible = false;
            this.btnDeactivationButton.Click += new System.EventHandler(this.DeactivationButton_Click);
            // 
            // btnActivationButton
            // 
            this.btnActivationButton.Location = new System.Drawing.Point(417, 112);
            this.btnActivationButton.Name = "btnActivationButton";
            this.btnActivationButton.Size = new System.Drawing.Size(42, 23);
            this.btnActivationButton.TabIndex = 22;
            this.btnActivationButton.Text = "Set";
            this.btnActivationButton.UseVisualStyleBackColor = true;
            this.btnActivationButton.Click += new System.EventHandler(this.ActivationButton_Click);
            // 
            // btnManageProfiles
            // 
            this.btnManageProfiles.Location = new System.Drawing.Point(96, 57);
            this.btnManageProfiles.Name = "btnManageProfiles";
            this.btnManageProfiles.Size = new System.Drawing.Size(144, 30);
            this.btnManageProfiles.TabIndex = 21;
            this.btnManageProfiles.Text = "Manage Profiles";
            this.btnManageProfiles.UseVisualStyleBackColor = true;
            this.btnManageProfiles.Click += new System.EventHandler(this.btnManageProfiles_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(10, 58);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.TabIndex = 20;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbDeactivationButton
            // 
            this.tbDeactivationButton.Enabled = false;
            this.tbDeactivationButton.Location = new System.Drawing.Point(245, 183);
            this.tbDeactivationButton.Name = "tbDeactivationButton";
            this.tbDeactivationButton.ReadOnly = true;
            this.tbDeactivationButton.Size = new System.Drawing.Size(166, 22);
            this.tbDeactivationButton.TabIndex = 6;
            this.tbDeactivationButton.Text = "` (~)";
            this.tbDeactivationButton.Visible = false;
            this.tbDeactivationButton.Click += new System.EventHandler(this.DeactivationButton_Click);
            this.tbDeactivationButton.TextChanged += new System.EventHandler(this.tbDeactivationButton_TextChanged);
            // 
            // lblDeactivationButton
            // 
            this.lblDeactivationButton.AutoSize = true;
            this.lblDeactivationButton.Enabled = false;
            this.lblDeactivationButton.ForeColor = System.Drawing.Color.White;
            this.lblDeactivationButton.Location = new System.Drawing.Point(242, 162);
            this.lblDeactivationButton.Name = "lblDeactivationButton";
            this.lblDeactivationButton.Size = new System.Drawing.Size(132, 17);
            this.lblDeactivationButton.TabIndex = 5;
            this.lblDeactivationButton.Text = "Deactivator Hotkey:";
            this.lblDeactivationButton.Visible = false;
            // 
            // cbUseDeactivationButton
            // 
            this.cbUseDeactivationButton.AutoSize = true;
            this.cbUseDeactivationButton.ForeColor = System.Drawing.Color.White;
            this.cbUseDeactivationButton.Location = new System.Drawing.Point(245, 139);
            this.cbUseDeactivationButton.Name = "cbUseDeactivationButton";
            this.cbUseDeactivationButton.Size = new System.Drawing.Size(264, 21);
            this.cbUseDeactivationButton.TabIndex = 4;
            this.cbUseDeactivationButton.Text = "Use Different Hotkey for Deactivation";
            this.cbUseDeactivationButton.UseVisualStyleBackColor = true;
            this.cbUseDeactivationButton.CheckedChanged += new System.EventHandler(this.cbUseDeactivationButton_CheckedChanged);
            // 
            // tbActivationButton
            // 
            this.tbActivationButton.Location = new System.Drawing.Point(245, 111);
            this.tbActivationButton.Name = "tbActivationButton";
            this.tbActivationButton.ReadOnly = true;
            this.tbActivationButton.Size = new System.Drawing.Size(166, 22);
            this.tbActivationButton.TabIndex = 3;
            this.tbActivationButton.Text = "` (~)";
            this.tbActivationButton.Click += new System.EventHandler(this.ActivationButton_Click);
            this.tbActivationButton.TextChanged += new System.EventHandler(this.tbActivationButton_TextChanged);
            // 
            // lblActivationButton
            // 
            this.lblActivationButton.AutoSize = true;
            this.lblActivationButton.ForeColor = System.Drawing.Color.White;
            this.lblActivationButton.Location = new System.Drawing.Point(242, 91);
            this.lblActivationButton.Name = "lblActivationButton";
            this.lblActivationButton.Size = new System.Drawing.Size(115, 17);
            this.lblActivationButton.TabIndex = 2;
            this.lblActivationButton.Text = "Activator Hotkey:";
            // 
            // lblSelectProfile
            // 
            this.lblSelectProfile.AutoSize = true;
            this.lblSelectProfile.ForeColor = System.Drawing.Color.White;
            this.lblSelectProfile.Location = new System.Drawing.Point(7, 8);
            this.lblSelectProfile.Name = "lblSelectProfile";
            this.lblSelectProfile.Size = new System.Drawing.Size(95, 17);
            this.lblSelectProfile.TabIndex = 1;
            this.lblSelectProfile.Text = "Select Profile:";
            // 
            // ddbProfile
            // 
            this.ddbProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddbProfile.FormattingEnabled = true;
            this.ddbProfile.Location = new System.Drawing.Point(9, 28);
            this.ddbProfile.MaxDropDownItems = 100;
            this.ddbProfile.Name = "ddbProfile";
            this.ddbProfile.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ddbProfile.Size = new System.Drawing.Size(231, 24);
            this.ddbProfile.TabIndex = 0;
            this.ddbProfile.SelectedIndexChanged += new System.EventHandler(this.ddbProfile_SelectedIndexChanged);
            // 
            // NSSKeyloggerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(523, 334);
            this.Controls.Add(this.tcNSSKeylogger);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "NSSKeyloggerForm";
            this.Text = "NSS Keylogger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NSSKeylogger_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NSSKeylogger_KeyDown);
            this.tcNSSKeylogger.ResumeLayout(false);
            this.tbKeylogger.ResumeLayout(false);
            this.tbKeylogger.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbKeyloggerRunning)).EndInit();
            this.tbPreferences.ResumeLayout(false);
            this.tbPreferences.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcNSSKeylogger;
        private System.Windows.Forms.TabPage tbKeylogger;
        private System.Windows.Forms.TabPage tbPreferences;
        private System.Windows.Forms.ComboBox ddbProfile;
        private System.Windows.Forms.TextBox tbDeactivationButton;
        private System.Windows.Forms.Label lblDeactivationButton;
        private System.Windows.Forms.CheckBox cbUseDeactivationButton;
        private System.Windows.Forms.TextBox tbActivationButton;
        private System.Windows.Forms.Label lblActivationButton;
        private System.Windows.Forms.Label lblSelectProfile;
        private System.Windows.Forms.Button btnManageProfiles;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.Label lblKeyloggerRunning;
        private System.Windows.Forms.PictureBox pbKeyloggerRunning;
        private System.Windows.Forms.Button btnActivationButton;
        private System.Windows.Forms.Button btnDeactivationButton;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.CheckBox cbSuppressHotkeys;
        private System.Windows.Forms.Button btnKeylogSaveLocation;
        private System.Windows.Forms.TextBox tbKeylogSaveLocation;
        private System.Windows.Forms.Label lblKeylogSaveLocation;
        private System.Windows.Forms.CheckBox cbUseSubtleModeButton;
        private System.Windows.Forms.Button btnSubtleModeButton;
        private System.Windows.Forms.TextBox tbSubtleModeButton;
        private System.Windows.Forms.Label lblSubtleModeButton;
        private System.Windows.Forms.CheckBox cbStartupSubtleMode;
        private System.Windows.Forms.CheckBox cbSubtleMode;
        private System.Windows.Forms.Button btnClearKeylog;
        private System.Windows.Forms.Button btnViewKeylog;
        private System.Windows.Forms.CheckBox cbClearOnStartup;
    }
}

