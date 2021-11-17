﻿using System;
using System.Drawing;
using System.Net.Sockets;
using System.Windows.Forms;

namespace ACNHPoker
{
    public partial class Friendship : Form
    {
        Form1 mainForm;
        int[] friendshipValue = new int[8];
        ISysBot _sysBot;
        int Index;
        bool sound;
        public Friendship(Form1 main, int i, ISysBot sysBot, Image img, string name, bool Sound)
        {
            InitializeComponent();
            mainForm = main;
            _sysBot = sysBot;
            Index = i;
            sound = Sound;
            VillagerImage.Image = img;
            this.Text = name;

            for (int p = 0; p < 8; p++)
            {
                //byte[] b = Utilities.GetVillager(_sysBot, i, (int)(Utilities.VillagerMemoryTinySize), ref counter);
                byte[] b = Utilities.GetPlayerDataVillager(_sysBot, i, p, (int)(Utilities.VillagerMemoryTinySize));
                if (b == null)
                    break;
                friendshipValue[p] = b[70];
                mainForm.loadFriendship(b, i, p);
                switch (p)
                {
                    case 0:
                        PlayerName1.Text = mainForm.PassPlayerName(i, p);
                        FriendshipBar1.Value = friendshipValue[p];
                        FriendshipValue1.Text = friendshipValue[p].ToString();
                        break;
                    case 1:
                        PlayerName2.Text = mainForm.PassPlayerName(i, p);
                        FriendshipBar2.Value = friendshipValue[p];
                        FriendshipValue2.Text = friendshipValue[p].ToString();
                        break;
                    case 2:
                        PlayerName3.Text = mainForm.PassPlayerName(i, p);
                        FriendshipBar3.Value = friendshipValue[p];
                        FriendshipValue3.Text = friendshipValue[p].ToString();
                        break;
                    case 3:
                        PlayerName4.Text = mainForm.PassPlayerName(i, p);
                        FriendshipBar4.Value = friendshipValue[p];
                        FriendshipValue4.Text = friendshipValue[p].ToString();
                        break;
                    case 4:
                        PlayerName5.Text = mainForm.PassPlayerName(i, p);
                        FriendshipBar5.Value = friendshipValue[p];
                        FriendshipValue5.Text = friendshipValue[p].ToString();
                        break;
                    case 5:
                        PlayerName6.Text = mainForm.PassPlayerName(i, p);
                        FriendshipBar6.Value = friendshipValue[p];
                        FriendshipValue6.Text = friendshipValue[p].ToString();
                        break;
                    case 6:
                        PlayerName7.Text = mainForm.PassPlayerName(i, p);
                        FriendshipBar7.Value = friendshipValue[p];
                        FriendshipValue7.Text = friendshipValue[p].ToString();
                        break;
                    case 7:
                        PlayerName8.Text = mainForm.PassPlayerName(i, p);
                        FriendshipBar8.Value = friendshipValue[p];
                        FriendshipValue8.Text = friendshipValue[p].ToString();
                        break;
                }
            }
        }

        private void FriendshipBar1_ValueChanged(object sender, EventArgs e)
        {
            FriendshipValue1.Text = FriendshipBar1.Value.ToString();
        }

        private void FriendshipBar2_ValueChanged(object sender, EventArgs e)
        {
            FriendshipValue2.Text = FriendshipBar2.Value.ToString();
        }

        private void FriendshipBar3_ValueChanged(object sender, EventArgs e)
        {
            FriendshipValue3.Text = FriendshipBar3.Value.ToString();
        }

        private void FriendshipBar4_ValueChanged(object sender, EventArgs e)
        {
            FriendshipValue4.Text = FriendshipBar4.Value.ToString();
        }

        private void FriendshipBar5_ValueChanged(object sender, EventArgs e)
        {
            FriendshipValue5.Text = FriendshipBar5.Value.ToString();
        }

        private void FriendshipBar6_ValueChanged(object sender, EventArgs e)
        {
            FriendshipValue6.Text = FriendshipBar6.Value.ToString();
        }

        private void FriendshipBar7_ValueChanged(object sender, EventArgs e)
        {
            FriendshipValue7.Text = FriendshipBar7.Value.ToString();
        }

        private void FriendshipBar8_ValueChanged(object sender, EventArgs e)
        {
            FriendshipValue8.Text = FriendshipBar8.Value.ToString();
        }

        private void FriendshipValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void FriendshipValue1_Validated(object sender, EventArgs e)
        {
            RichTextBox box = (RichTextBox)sender;
            int value = Int16.Parse(box.Text);
            if (value > 255)
            {
                value = 255;
                box.Text = "255";
            }
            FriendshipBar1.Value = value;
        }

        private void FriendshipValue2_Validated(object sender, EventArgs e)
        {
            RichTextBox box = (RichTextBox)sender;
            int value = Int16.Parse(box.Text);
            if (value > 255)
            {
                value = 255;
                box.Text = "255";
            }
            FriendshipBar2.Value = value;
        }

        private void FriendshipValue3_Validated(object sender, EventArgs e)
        {
            RichTextBox box = (RichTextBox)sender;
            int value = Int16.Parse(box.Text);
            if (value > 255)
            {
                value = 255;
                box.Text = "255";
            }
            FriendshipBar3.Value = value;
        }

        private void FriendshipValue4_Validated(object sender, EventArgs e)
        {
            RichTextBox box = (RichTextBox)sender;
            int value = Int16.Parse(box.Text);
            if (value > 255)
            {
                value = 255;
                box.Text = "255";
            }
            FriendshipBar4.Value = value;
        }

        private void FriendshipValue5_Validated(object sender, EventArgs e)
        {
            RichTextBox box = (RichTextBox)sender;
            int value = Int16.Parse(box.Text);
            if (value > 255)
            {
                value = 255;
                box.Text = "255";
            }
            FriendshipBar5.Value = value;
        }

        private void FriendshipValue6_Validated(object sender, EventArgs e)
        {
            RichTextBox box = (RichTextBox)sender;
            int value = Int16.Parse(box.Text);
            if (value > 255)
            {
                value = 255;
                box.Text = "255";
            }
            FriendshipBar6.Value = value;
        }

        private void FriendshipValue7_Validated(object sender, EventArgs e)
        {
            RichTextBox box = (RichTextBox)sender;
            int value = Int16.Parse(box.Text);
            if (value > 255)
            {
                value = 255;
                box.Text = "255";
            }
            FriendshipBar7.Value = value;
        }

        private void FriendshipValue8_Validated(object sender, EventArgs e)
        {
            RichTextBox box = (RichTextBox)sender;
            int value = Int16.Parse(box.Text);
            if (value > 255)
            {
                value = 255;
                box.Text = "255";
            }
            FriendshipBar8.Value = value;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            int[] SetValue = new int[8];

            SetValue[0] = FriendshipBar1.Value;
            SetValue[1] = FriendshipBar2.Value;
            SetValue[2] = FriendshipBar3.Value;
            SetValue[3] = FriendshipBar4.Value;
            SetValue[4] = FriendshipBar5.Value;
            SetValue[5] = FriendshipBar6.Value;
            SetValue[6] = FriendshipBar7.Value;
            SetValue[7] = FriendshipBar8.Value;

            for (int p = 0; p < 8; p++)
            {
                if (SetValue[p] != friendshipValue[p])
                {
                    Utilities.SetFriendship(_sysBot, Index, p, SetValue[p].ToString("X"));
                    mainForm.SetFriendship(Index, p, SetValue[p]);
                }
            }
            if (sound)
                System.Media.SystemSounds.Asterisk.Play();

            this.Close();

            mainForm.RefreshVillagerUI(false);
        }
    }
}
