using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACNHPoker
{
    public partial class variationSpawn : Form
    {
        inventorySlot[,] mainSlot = new inventorySlot[1 ,3];
        inventorySlot[,] subSlot = new inventorySlot[1, 3];
        inventorySlot[,] allSlot = new inventorySlot[2, 3];
        int main;
        int sub;

        public variationSpawn(inventorySlot[,] variationList)
        {
            InitializeComponent();
            map.numOfColumn = (int)columnBox.Value;

            mainSlot[0, 0] = main00;
            mainSlot[0, 1] = main01;
            mainSlot[0, 2] = main02;
            subSlot[0, 0] = sub00;
            subSlot[0, 1] = sub01;
            subSlot[0, 2] = sub02;
            allSlot[0, 0] = all00;
            allSlot[0, 1] = all01;
            allSlot[0, 2] = all02;
            allSlot[1, 0] = all10;
            allSlot[1, 1] = all11;
            allSlot[1, 2] = all12;

            main = variationList.GetLength(0);
            sub = variationList.GetLength(1);

            if (main <= 1)
            {
                mainOnly.Enabled = false;
                mainPanel.Enabled = false;
                all.Enabled = false;
                allPanel.Enabled = false;

                mainOnly.Checked = false;
                subOnly.Checked = true;
                okBtn.DialogResult = DialogResult.Yes;
            }
            else if (sub <= 1)
            {
                subOnly.Enabled = false;
                subPanel.Enabled = false;
                all.Enabled = false;
                allPanel.Enabled = false;

                mainOnly.Checked = true;
                subOnly.Checked = false;
                okBtn.DialogResult = DialogResult.OK;
            }

            //main
            for (int i = 0; i < main; i++)
            {
                if (i >= 3)
                    break;
                mainSlot[0, i].setup(variationList[i,0]);
            }

            //sub
            for (int j = 0; j < sub; j++)
            {
                if (j >= 3)
                    break;
                subSlot[0, j].setup(variationList[0, j]);
            }

            for (int m = 0; m < main; m++)
            {
                if (m >= 2)
                    break;
                for (int n = 0; n < sub; n++)
                {
                    if (n >= 3)
                        continue;
                    allSlot[m, n].setup(variationList[m, n]);
                }
            }

            updateSize();
        }

        private void mainOnly_CheckedChanged(object sender, EventArgs e)
        {
            okBtn.DialogResult = DialogResult.OK;
            columnPanel.Enabled = true;
            updateSize();
        }

        private void subOnly_CheckedChanged(object sender, EventArgs e)
        {
            okBtn.DialogResult = DialogResult.Yes;
            columnPanel.Enabled = true;
            updateSize();
        }

        private void all_CheckedChanged(object sender, EventArgs e)
        {
            okBtn.DialogResult = DialogResult.Ignore;
            columnPanel.Enabled = false;
            updateSize();
        }

        private void columnBox_ValueChanged(object sender, EventArgs e)
        {
            map.numOfColumn = (int)columnBox.Value;
            timesLabel1.Text = "× " + columnBox.Value;
            timesLabel2.Text = "× " + columnBox.Value;
            updateSize();
        }


        private void updateSize()
        {
            if (mainOnly.Checked)
            {
                size.Text = main.ToString() + " × " + columnBox.Value;
            }
            else if (subOnly.Checked)
            {
                size.Text = sub.ToString() + " × " + columnBox.Value;
            }
            else
            {
                size.Text = sub.ToString() + " × " + main.ToString();
            }
        }
    }
}
