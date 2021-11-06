
namespace ACNHPoker
{
    partial class Freezer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Freezer));
            this.saveMapBtn = new System.Windows.Forms.Button();
            this.formToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.changeRateBtn = new System.Windows.Forms.Button();
            this.UnFreezeAllBtn = new System.Windows.Forms.Button();
            this.FreezeCountLabel = new System.Windows.Forms.Label();
            this.SlotLabel = new System.Windows.Forms.Label();
            this.RateBar = new System.Windows.Forms.TrackBar();
            this.RateValue = new System.Windows.Forms.Label();
            this.EnableTextBtn = new System.Windows.Forms.Button();
            this.DisableTextBtn = new System.Windows.Forms.Button();
            this.UnFreezeInvBtn = new System.Windows.Forms.Button();
            this.FreezeInvBtn = new System.Windows.Forms.Button();
            this.FreezeMapBtn = new System.Windows.Forms.Button();
            this.UnFreezeMapBtn = new System.Windows.Forms.Button();
            this.PleaseWaitPanel = new System.Windows.Forms.Panel();
            this.MapProgressBar = new System.Windows.Forms.ProgressBar();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.ProgressTimer = new System.Windows.Forms.Timer(this.components);
            this.mainPanel = new System.Windows.Forms.Panel();
            this.FinMsg = new System.Windows.Forms.RichTextBox();
            this.WaitMessagebox = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.RateBar)).BeginInit();
            this.PleaseWaitPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveMapBtn
            // 
            this.saveMapBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.saveMapBtn.FlatAppearance.BorderSize = 0;
            this.saveMapBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveMapBtn.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.saveMapBtn.ForeColor = System.Drawing.Color.White;
            this.saveMapBtn.Location = new System.Drawing.Point(13, 214);
            this.saveMapBtn.Margin = new System.Windows.Forms.Padding(4);
            this.saveMapBtn.Name = "saveMapBtn";
            this.saveMapBtn.Size = new System.Drawing.Size(208, 25);
            this.saveMapBtn.TabIndex = 218;
            this.saveMapBtn.Text = "Create Map Template";
            this.formToolTip.SetToolTip(this.saveMapBtn, "Create a Map template and save it to a .nhf2 file. (Layer 1 & 2)\r\n");
            this.saveMapBtn.UseVisualStyleBackColor = false;
            this.saveMapBtn.Click += new System.EventHandler(this.saveMapBtn_Click);
            // 
            // formToolTip
            // 
            this.formToolTip.AutomaticDelay = 100;
            this.formToolTip.AutoPopDelay = 10000;
            this.formToolTip.InitialDelay = 100;
            this.formToolTip.IsBalloon = true;
            this.formToolTip.ReshowDelay = 20;
            this.formToolTip.ShowAlways = true;
            this.formToolTip.UseAnimation = false;
            this.formToolTip.UseFading = false;
            // 
            // changeRateBtn
            // 
            this.changeRateBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.changeRateBtn.FlatAppearance.BorderSize = 0;
            this.changeRateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.changeRateBtn.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.changeRateBtn.ForeColor = System.Drawing.Color.White;
            this.changeRateBtn.Location = new System.Drawing.Point(13, 72);
            this.changeRateBtn.Margin = new System.Windows.Forms.Padding(4);
            this.changeRateBtn.Name = "changeRateBtn";
            this.changeRateBtn.Size = new System.Drawing.Size(208, 25);
            this.changeRateBtn.TabIndex = 229;
            this.changeRateBtn.Text = "Change Refresh Delay";
            this.formToolTip.SetToolTip(this.changeRateBtn, "Change the refresh rate. \r\nLower number means it refresh more frequently.\r\nYou sh" +
        "ould keep this as high as possible to reduce the load.");
            this.changeRateBtn.UseVisualStyleBackColor = false;
            this.changeRateBtn.Click += new System.EventHandler(this.changeRateBtn_Click);
            // 
            // UnFreezeAllBtn
            // 
            this.UnFreezeAllBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.UnFreezeAllBtn.FlatAppearance.BorderSize = 0;
            this.UnFreezeAllBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UnFreezeAllBtn.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.UnFreezeAllBtn.ForeColor = System.Drawing.Color.White;
            this.UnFreezeAllBtn.Location = new System.Drawing.Point(13, 326);
            this.UnFreezeAllBtn.Margin = new System.Windows.Forms.Padding(4);
            this.UnFreezeAllBtn.Name = "UnFreezeAllBtn";
            this.UnFreezeAllBtn.Size = new System.Drawing.Size(208, 25);
            this.UnFreezeAllBtn.TabIndex = 237;
            this.UnFreezeAllBtn.Text = "UnFreeze Everything";
            this.UnFreezeAllBtn.UseVisualStyleBackColor = false;
            this.UnFreezeAllBtn.Click += new System.EventHandler(this.UnFreezeAllBtn_Click);
            // 
            // FreezeCountLabel
            // 
            this.FreezeCountLabel.AutoSize = true;
            this.FreezeCountLabel.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FreezeCountLabel.ForeColor = System.Drawing.Color.White;
            this.FreezeCountLabel.Location = new System.Drawing.Point(54, 4);
            this.FreezeCountLabel.Name = "FreezeCountLabel";
            this.FreezeCountLabel.Size = new System.Drawing.Size(99, 32);
            this.FreezeCountLabel.TabIndex = 226;
            this.FreezeCountLabel.Text = "0 / 255";
            this.FreezeCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SlotLabel
            // 
            this.SlotLabel.AutoSize = true;
            this.SlotLabel.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.SlotLabel.ForeColor = System.Drawing.Color.White;
            this.SlotLabel.Location = new System.Drawing.Point(12, 11);
            this.SlotLabel.Name = "SlotLabel";
            this.SlotLabel.Size = new System.Drawing.Size(43, 16);
            this.SlotLabel.TabIndex = 227;
            this.SlotLabel.Text = "Slot :";
            this.SlotLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RateBar
            // 
            this.RateBar.LargeChange = 1000;
            this.RateBar.Location = new System.Drawing.Point(5, 30);
            this.RateBar.Maximum = 10000;
            this.RateBar.Minimum = 100;
            this.RateBar.Name = "RateBar";
            this.RateBar.Size = new System.Drawing.Size(165, 45);
            this.RateBar.SmallChange = 100;
            this.RateBar.TabIndex = 228;
            this.RateBar.TickFrequency = 1000;
            this.RateBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.RateBar.Value = 100;
            this.RateBar.ValueChanged += new System.EventHandler(this.RateBar_ValueChanged);
            // 
            // RateValue
            // 
            this.RateValue.AutoSize = true;
            this.RateValue.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.RateValue.ForeColor = System.Drawing.Color.White;
            this.RateValue.Location = new System.Drawing.Point(163, 43);
            this.RateValue.Name = "RateValue";
            this.RateValue.Size = new System.Drawing.Size(56, 16);
            this.RateValue.TabIndex = 230;
            this.RateValue.Text = "100 ms";
            this.RateValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EnableTextBtn
            // 
            this.EnableTextBtn.BackColor = System.Drawing.Color.DarkGreen;
            this.EnableTextBtn.FlatAppearance.BorderSize = 0;
            this.EnableTextBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EnableTextBtn.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.EnableTextBtn.ForeColor = System.Drawing.Color.White;
            this.EnableTextBtn.Location = new System.Drawing.Point(13, 105);
            this.EnableTextBtn.Margin = new System.Windows.Forms.Padding(4);
            this.EnableTextBtn.Name = "EnableTextBtn";
            this.EnableTextBtn.Size = new System.Drawing.Size(100, 45);
            this.EnableTextBtn.TabIndex = 231;
            this.EnableTextBtn.Text = "Enable Instant Text";
            this.EnableTextBtn.UseVisualStyleBackColor = false;
            this.EnableTextBtn.Click += new System.EventHandler(this.EnableTextBtn_Click);
            // 
            // DisableTextBtn
            // 
            this.DisableTextBtn.BackColor = System.Drawing.Color.DarkRed;
            this.DisableTextBtn.FlatAppearance.BorderSize = 0;
            this.DisableTextBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DisableTextBtn.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.DisableTextBtn.ForeColor = System.Drawing.Color.White;
            this.DisableTextBtn.Location = new System.Drawing.Point(121, 105);
            this.DisableTextBtn.Margin = new System.Windows.Forms.Padding(4);
            this.DisableTextBtn.Name = "DisableTextBtn";
            this.DisableTextBtn.Size = new System.Drawing.Size(100, 45);
            this.DisableTextBtn.TabIndex = 232;
            this.DisableTextBtn.Text = "Disable Instant Text";
            this.DisableTextBtn.UseVisualStyleBackColor = false;
            this.DisableTextBtn.Click += new System.EventHandler(this.DisableTextBtn_Click);
            // 
            // UnFreezeInvBtn
            // 
            this.UnFreezeInvBtn.BackColor = System.Drawing.Color.DarkRed;
            this.UnFreezeInvBtn.FlatAppearance.BorderSize = 0;
            this.UnFreezeInvBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UnFreezeInvBtn.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.UnFreezeInvBtn.ForeColor = System.Drawing.Color.White;
            this.UnFreezeInvBtn.Location = new System.Drawing.Point(121, 158);
            this.UnFreezeInvBtn.Margin = new System.Windows.Forms.Padding(4);
            this.UnFreezeInvBtn.Name = "UnFreezeInvBtn";
            this.UnFreezeInvBtn.Size = new System.Drawing.Size(100, 45);
            this.UnFreezeInvBtn.TabIndex = 234;
            this.UnFreezeInvBtn.Text = "UnFreeze Inventory";
            this.UnFreezeInvBtn.UseVisualStyleBackColor = false;
            this.UnFreezeInvBtn.Click += new System.EventHandler(this.UnFreezeInvBtn_Click);
            // 
            // FreezeInvBtn
            // 
            this.FreezeInvBtn.BackColor = System.Drawing.Color.DarkGreen;
            this.FreezeInvBtn.FlatAppearance.BorderSize = 0;
            this.FreezeInvBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FreezeInvBtn.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.FreezeInvBtn.ForeColor = System.Drawing.Color.White;
            this.FreezeInvBtn.Location = new System.Drawing.Point(13, 158);
            this.FreezeInvBtn.Margin = new System.Windows.Forms.Padding(4);
            this.FreezeInvBtn.Name = "FreezeInvBtn";
            this.FreezeInvBtn.Size = new System.Drawing.Size(100, 45);
            this.FreezeInvBtn.TabIndex = 233;
            this.FreezeInvBtn.Text = "Freeze Inventory";
            this.FreezeInvBtn.UseVisualStyleBackColor = false;
            this.FreezeInvBtn.Click += new System.EventHandler(this.FreezeInvBtn_Click);
            // 
            // FreezeMapBtn
            // 
            this.FreezeMapBtn.BackColor = System.Drawing.Color.DarkGreen;
            this.FreezeMapBtn.FlatAppearance.BorderSize = 0;
            this.FreezeMapBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FreezeMapBtn.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.FreezeMapBtn.ForeColor = System.Drawing.Color.White;
            this.FreezeMapBtn.Location = new System.Drawing.Point(13, 247);
            this.FreezeMapBtn.Margin = new System.Windows.Forms.Padding(4);
            this.FreezeMapBtn.Name = "FreezeMapBtn";
            this.FreezeMapBtn.Size = new System.Drawing.Size(100, 45);
            this.FreezeMapBtn.TabIndex = 235;
            this.FreezeMapBtn.Text = "Freeze Map";
            this.FreezeMapBtn.UseVisualStyleBackColor = false;
            this.FreezeMapBtn.Click += new System.EventHandler(this.FreezeMapBtn_Click);
            // 
            // UnFreezeMapBtn
            // 
            this.UnFreezeMapBtn.BackColor = System.Drawing.Color.DarkRed;
            this.UnFreezeMapBtn.FlatAppearance.BorderSize = 0;
            this.UnFreezeMapBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UnFreezeMapBtn.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.UnFreezeMapBtn.ForeColor = System.Drawing.Color.White;
            this.UnFreezeMapBtn.Location = new System.Drawing.Point(121, 247);
            this.UnFreezeMapBtn.Margin = new System.Windows.Forms.Padding(4);
            this.UnFreezeMapBtn.Name = "UnFreezeMapBtn";
            this.UnFreezeMapBtn.Size = new System.Drawing.Size(100, 45);
            this.UnFreezeMapBtn.TabIndex = 236;
            this.UnFreezeMapBtn.Text = "UnFreeze Map";
            this.UnFreezeMapBtn.UseVisualStyleBackColor = false;
            this.UnFreezeMapBtn.Click += new System.EventHandler(this.UnFreezeMapBtn_Click);
            // 
            // PleaseWaitPanel
            // 
            this.PleaseWaitPanel.Controls.Add(this.WaitMessagebox);
            this.PleaseWaitPanel.Controls.Add(this.MapProgressBar);
            this.PleaseWaitPanel.Controls.Add(this.pictureBox2);
            this.PleaseWaitPanel.Location = new System.Drawing.Point(2, 293);
            this.PleaseWaitPanel.Name = "PleaseWaitPanel";
            this.PleaseWaitPanel.Size = new System.Drawing.Size(230, 35);
            this.PleaseWaitPanel.TabIndex = 238;
            this.PleaseWaitPanel.Visible = false;
            // 
            // MapProgressBar
            // 
            this.MapProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.MapProgressBar.ForeColor = System.Drawing.Color.LawnGreen;
            this.MapProgressBar.Location = new System.Drawing.Point(4, 28);
            this.MapProgressBar.Maximum = 260;
            this.MapProgressBar.Name = "MapProgressBar";
            this.MapProgressBar.Size = new System.Drawing.Size(220, 3);
            this.MapProgressBar.TabIndex = 215;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::ACNHPoker.Properties.Resources.loading;
            this.pictureBox2.Location = new System.Drawing.Point(65, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(24, 24);
            this.pictureBox2.TabIndex = 216;
            this.pictureBox2.TabStop = false;
            // 
            // ProgressTimer
            // 
            this.ProgressTimer.Interval = 500;
            this.ProgressTimer.Tick += new System.EventHandler(this.ProgressTimer_Tick);
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.SlotLabel);
            this.mainPanel.Controls.Add(this.RateValue);
            this.mainPanel.Controls.Add(this.saveMapBtn);
            this.mainPanel.Controls.Add(this.changeRateBtn);
            this.mainPanel.Controls.Add(this.FreezeCountLabel);
            this.mainPanel.Controls.Add(this.RateBar);
            this.mainPanel.Controls.Add(this.EnableTextBtn);
            this.mainPanel.Controls.Add(this.FinMsg);
            this.mainPanel.Controls.Add(this.DisableTextBtn);
            this.mainPanel.Controls.Add(this.UnFreezeAllBtn);
            this.mainPanel.Controls.Add(this.FreezeInvBtn);
            this.mainPanel.Controls.Add(this.UnFreezeMapBtn);
            this.mainPanel.Controls.Add(this.UnFreezeInvBtn);
            this.mainPanel.Controls.Add(this.FreezeMapBtn);
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(250, 395);
            this.mainPanel.TabIndex = 240;
            // 
            // FinMsg
            // 
            this.FinMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.FinMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FinMsg.Cursor = System.Windows.Forms.Cursors.Default;
            this.FinMsg.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.FinMsg.ForeColor = System.Drawing.Color.White;
            this.FinMsg.Location = new System.Drawing.Point(14, 298);
            this.FinMsg.Multiline = false;
            this.FinMsg.Name = "FinMsg";
            this.FinMsg.ReadOnly = true;
            this.FinMsg.Size = new System.Drawing.Size(207, 21);
            this.FinMsg.TabIndex = 239;
            this.FinMsg.Text = "";
            this.FinMsg.Visible = false;
            // 
            // WaitMessagebox
            // 
            this.WaitMessagebox.AutoSize = true;
            this.WaitMessagebox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.WaitMessagebox.ForeColor = System.Drawing.Color.White;
            this.WaitMessagebox.Location = new System.Drawing.Point(89, 6);
            this.WaitMessagebox.Name = "WaitMessagebox";
            this.WaitMessagebox.Size = new System.Drawing.Size(57, 16);
            this.WaitMessagebox.TabIndex = 240;
            this.WaitMessagebox.Text = "testing";
            this.WaitMessagebox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Freezer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.ClientSize = new System.Drawing.Size(234, 356);
            this.Controls.Add(this.PleaseWaitPanel);
            this.Controls.Add(this.mainPanel);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(250, 395);
            this.Name = "Freezer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Freezer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Freezer_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.RateBar)).EndInit();
            this.PleaseWaitPanel.ResumeLayout(false);
            this.PleaseWaitPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button saveMapBtn;
        private System.Windows.Forms.ToolTip formToolTip;
        private System.Windows.Forms.Label FreezeCountLabel;
        private System.Windows.Forms.Label SlotLabel;
        private System.Windows.Forms.TrackBar RateBar;
        private System.Windows.Forms.Button changeRateBtn;
        private System.Windows.Forms.Label RateValue;
        private System.Windows.Forms.Button EnableTextBtn;
        private System.Windows.Forms.Button DisableTextBtn;
        private System.Windows.Forms.Button UnFreezeInvBtn;
        private System.Windows.Forms.Button FreezeInvBtn;
        private System.Windows.Forms.Button FreezeMapBtn;
        private System.Windows.Forms.Button UnFreezeMapBtn;
        private System.Windows.Forms.Button UnFreezeAllBtn;
        private System.Windows.Forms.Panel PleaseWaitPanel;
        private System.Windows.Forms.ProgressBar MapProgressBar;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Timer ProgressTimer;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Label WaitMessagebox;
        private System.Windows.Forms.RichTextBox FinMsg;
    }
}