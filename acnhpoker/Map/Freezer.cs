using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ACNHPoker
{
    public partial class Freezer : Form
    {
        private static Socket s;
        private Form1 main;
        private bool sound;
        private int counter = 0;
        public Freezer(Socket S, Form1 Main, bool Sound)
        {
            s = S;
            main = Main;
            sound = Sound;

            InitializeComponent();

            int freezeCount = Utilities.GetFreezeCount(s);
            updateFreezeCountLabel(freezeCount);

            FinMsg.SelectionAlignment = HorizontalAlignment.Center;

            Log.logEvent("Freeze", "Freeze Form Started Successfully");
        }

        private void saveMapBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog file = new SaveFileDialog()
                {
                    Filter = "New Horizons Fasil 2 (*.nhf2)|*.nhf2",
                };

                Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

                string savepath;

                if (config.AppSettings.Settings["LastSave"].Value.Equals(string.Empty))
                    savepath = Directory.GetCurrentDirectory() + @"\save";
                else
                    savepath = config.AppSettings.Settings["LastSave"].Value;

                if (Directory.Exists(savepath))
                {
                    file.InitialDirectory = savepath;
                }
                else
                {
                    file.InitialDirectory = @"C:\";
                }

                if (file.ShowDialog() != DialogResult.OK)
                    return;

                string[] temp = file.FileName.Split('\\');
                string path = "";
                for (int i = 0; i < temp.Length - 1; i++)
                    path = path + temp[i] + "\\";

                config.AppSettings.Settings["LastSave"].Value = path;
                config.Save(ConfigurationSaveMode.Minimal);

                UInt32 address = Utilities.mapZero;

                Thread LoadThread = new Thread(delegate () { saveMapFloor(address, file); });
                LoadThread.Start();

            }
            catch (Exception ex)
            {
                Log.logEvent("Regen", "Save: " + ex.Message.ToString());
                return;
            }
        }

        private void saveMapFloor(UInt32 address, SaveFileDialog file)
        {
            showMapWait(84, "Saving...");

            lockControl();

            byte[] save = Utilities.ReadByteArray8(s, address, 0x54000*2, ref counter);

            File.WriteAllBytes(file.FileName, save);

            if (sound)
                System.Media.SystemSounds.Asterisk.Play();

            hideMapWait();

            unlockControl();

            this.Invoke((MethodInvoker)delegate
            {
                FinMsg.Visible = true;
                FinMsg.Text = "Template Saved!";
            });
        }

        private void showMapWait(int size, string msg = "")
        {
            this.Invoke((MethodInvoker)delegate
            {
                FinMsg.Visible = false;
                WaitMessagebox.Text = msg;
                counter = 0;
                MapProgressBar.Maximum = size + 5;
                MapProgressBar.Value = counter;
                PleaseWaitPanel.Visible = true;
                ProgressTimer.Start();
            });
        }

        private void hideMapWait()
        {
            this.Invoke((MethodInvoker)delegate
            {
                PleaseWaitPanel.Visible = false;
                ProgressTimer.Stop();
            });
        }

        private void ProgressTimer_Tick(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                if (counter <= MapProgressBar.Maximum)
                    MapProgressBar.Value = counter;
                else
                    MapProgressBar.Value = MapProgressBar.Maximum;
            });
        }

        private void Freezer_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseCleaning();
        }

        private void CloseCleaning()
        {
            Log.logEvent("Freeze", "Form Closed");
            main.F = null;
            main.Show();
        }

        private void UnFreezeAllBtn_Click(object sender, EventArgs e)
        {
            showMapWait(1, "Unfreezing...");
            lockControl();
            Utilities.SendString(s, Utilities.FreezeClear());
            Thread.Sleep(100);
            int freezeCount = Utilities.GetFreezeCount(s);
            Thread.Sleep(3000);
            if (sound)
                System.Media.SystemSounds.Asterisk.Play();
            hideMapWait();
            unlockControl();
            this.Invoke((MethodInvoker)delegate
            {
                FinMsg.Visible = true;
                FinMsg.Text = "Build a snowman?";
                updateFreezeCountLabel(freezeCount);
            });

            if (sound)
                System.Media.SystemSounds.Asterisk.Play();
        }

        private void updateFreezeCountLabel(int value)
        {
            FreezeCountLabel.Text = value.ToString() + " / 255";
        }

        private void changeRateBtn_Click(object sender, EventArgs e)
        {
            string value = RateBar.Value.ToString();
            Utilities.SendString(s, Utilities.FreezeRate(value));

            this.Invoke((MethodInvoker)delegate
            {
                FinMsg.Visible = true;
                FinMsg.Text = "Delay Updated! " + value + " ms";
            });

            if (sound)
                System.Media.SystemSounds.Asterisk.Play();
        }

        private void EnableTextBtn_Click(object sender, EventArgs e)
        {
            Utilities.SendString(s, Utilities.Freeze(Utilities.TextSpeedAddress, new byte[1] {3}));
            
            int freezeCount = Utilities.GetFreezeCount(s);

            this.Invoke((MethodInvoker)delegate
            {
                FinMsg.Visible = true;
                FinMsg.Text = "Instant Text Activated!";
                updateFreezeCountLabel(freezeCount);
            });

            if (sound)
                System.Media.SystemSounds.Asterisk.Play();
        }

        private void DisableTextBtn_Click(object sender, EventArgs e)
        {
            Utilities.SendString(s, Utilities.UnFreeze(Utilities.TextSpeedAddress));

            int freezeCount = Utilities.GetFreezeCount(s);

            this.Invoke((MethodInvoker)delegate
            {
                FinMsg.Visible = true;
                FinMsg.Text = "Instant Text Deactivated!";
                updateFreezeCountLabel(freezeCount);
            });

            if (sound)
                System.Media.SystemSounds.Asterisk.Play();
        }

        private void RateBar_ValueChanged(object sender, EventArgs e)
        {
            RateValue.Text = RateBar.Value.ToString() + " ms";
        }

        private void FreezeInvBtn_Click(object sender, EventArgs e)
        {
            byte[] Bank01to20 = Utilities.GetInventoryBank(s, null, 1);
            byte[] Bank21to40 = Utilities.GetInventoryBank(s, null, 21);

            Utilities.SendString(s, Utilities.Freeze(Utilities.ItemSlotBase, Bank01to20));
            Utilities.SendString(s, Utilities.Freeze(Utilities.ItemSlot21Base, Bank21to40));

            int freezeCount = Utilities.GetFreezeCount(s);

            this.Invoke((MethodInvoker)delegate
            {
                FinMsg.Visible = true;
                FinMsg.Text = "Inventory Freeze Activated!";
                updateFreezeCountLabel(freezeCount);
            });

            if (sound)
                System.Media.SystemSounds.Asterisk.Play();
        }

        private void UnFreezeInvBtn_Click(object sender, EventArgs e)
        {
            Utilities.SendString(s, Utilities.UnFreeze(Utilities.ItemSlotBase));
            Utilities.SendString(s, Utilities.UnFreeze(Utilities.ItemSlot21Base));

            int freezeCount = Utilities.GetFreezeCount(s);

            this.Invoke((MethodInvoker)delegate
            {
                FinMsg.Visible = true;
                FinMsg.Text = "Inventory Freeze Deactivated!";
                updateFreezeCountLabel(freezeCount);
            });

            if (sound)
                System.Media.SystemSounds.Asterisk.Play();
        }

        private void FreezeMapBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog()
            {
                Filter = "New Horizons Fasil 2 (*.nhf2)|*.nhf2|All files (*.*)|*.*",
            };

            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

            string savepath;

            if (config.AppSettings.Settings["LastLoad"].Value.Equals(string.Empty))
                savepath = Directory.GetCurrentDirectory() + @"\save";
            else
                savepath = config.AppSettings.Settings["LastLoad"].Value;

            if (Directory.Exists(savepath))
            {
                file.InitialDirectory = savepath;
            }
            else
            {
                file.InitialDirectory = @"C:\";
            }

            if (file.ShowDialog() != DialogResult.OK)
                return;

            string[] temp = file.FileName.Split('\\');
            string path = "";
            for (int i = 0; i < temp.Length - 1; i++)
                path = path + temp[i] + "\\";

            config.AppSettings.Settings["LastLoad"].Value = path;
            config.Save(ConfigurationSaveMode.Minimal);

            byte[] data = File.ReadAllBytes(file.FileName);

            if (data.Length != Utilities.mapSize * 2)
            {
                myMessageBox.Show("Invalid File Size!", "Your map file size is invalid!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UInt32 address = Utilities.mapZero;

            string[] name = file.FileName.Split('\\');

            Log.logEvent("Regen", "Regen3 Started: " + name[name.Length - 1]);

            Thread FreezeThread = new Thread(delegate () { FreezeMapFloor(address, data); });
            FreezeThread.Start();
        }

        private void FreezeMapFloor(UInt32 address, byte[] data)
        {
            showMapWait(84, "Freezing...");

            lockControl();

            byte[][] b = new byte[84][];

            for (int i = 0; i < 84; i++)
            {
                b[i] = new byte[0x2000];
                Buffer.BlockCopy(data, i * 0x2000, b[i], 0x0, 0x2000);
                Utilities.SendString(s, Utilities.Freeze((uint)(address + (i * 0x2000)), b[i]));
                counter++;
                Thread.Sleep(100);
            }

            int freezeCount = Utilities.GetFreezeCount(s);

            hideMapWait();

            unlockControl();

            this.Invoke((MethodInvoker)delegate
            {
                FinMsg.Visible = true;
                FinMsg.Text = "Let it go!";
                updateFreezeCountLabel(freezeCount);
            });

            if (sound)
                System.Media.SystemSounds.Asterisk.Play();
        }

        private void UnFreezeMapBtn_Click(object sender, EventArgs e)
        {
            Log.logEvent("Regen", "Regen3 Stopped") ;

            UInt32 address = Utilities.mapZero;

            Thread UnFreezeThread = new Thread(delegate () { UnFreezeMapFloor(address); });
            UnFreezeThread.Start();
        }

        private void UnFreezeMapFloor(UInt32 address)
        {
            showMapWait(84, "Freezing...");

            lockControl();

            for (int i = 0; i < 84; i++)
            {
                Utilities.SendString(s, Utilities.UnFreeze((uint)(address + (i * 0x2000))));
                counter++;
                Thread.Sleep(100);
            }

            int freezeCount = Utilities.GetFreezeCount(s);

            hideMapWait();

            unlockControl();

            this.Invoke((MethodInvoker)delegate
            {
                FinMsg.Visible = true;
                FinMsg.Text = "Build a snowman?";
                updateFreezeCountLabel(freezeCount);
            });

            if (sound)
                System.Media.SystemSounds.Asterisk.Play();
        }

        private void lockControl()
        {
            this.Invoke((MethodInvoker)delegate
            {
                mainPanel.Enabled = false;
            });
        }

        private void unlockControl()
        {
            this.Invoke((MethodInvoker)delegate
            {
                mainPanel.Enabled = true;
            });
        }
    }
}
