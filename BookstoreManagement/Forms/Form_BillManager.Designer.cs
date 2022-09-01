namespace BookstoreManagement.Forms
{
    partial class Form_BillManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_BillManager));
            this.panel6 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.btnCanel = new System.Windows.Forms.Button();
            this.btnBaoCun = new System.Windows.Forms.Button();
            this.dtYingBack = new System.Windows.Forms.DateTimePicker();
            this.dtBack = new System.Windows.Forms.DateTimePicker();
            this.dtBorrow = new System.Windows.Forms.DateTimePicker();
            this.nudMoney = new System.Windows.Forms.NumericUpDown();
            this.cmbState = new System.Windows.Forms.ComboBox();
            this.txtBookWriter = new System.Windows.Forms.TextBox();
            this.txtBookId = new System.Windows.Forms.TextBox();
            this.txtManageId = new System.Windows.Forms.TextBox();
            this.txtBookName = new System.Windows.Forms.TextBox();
            this.txtReaderID = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.cbEnable = new System.Windows.Forms.CheckBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMoney)).BeginInit();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label8);
            this.panel6.Location = new System.Drawing.Point(-1, 1);
            this.panel6.Margin = new System.Windows.Forms.Padding(0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(966, 61);
            this.panel6.TabIndex = 95;
            this.panel6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel6_MouseDown);
            this.panel6.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel6_MouseMove);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Century Gothic", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
            this.label8.Location = new System.Drawing.Point(280, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(481, 51);
            this.label8.TabIndex = 79;
            this.label8.Text = "修  改  借  阅  者  信  息";
            // 
            // btnCanel
            // 
            this.btnCanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCanel.BackColor = System.Drawing.Color.SeaGreen;
            this.btnCanel.FlatAppearance.BorderSize = 0;
            this.btnCanel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCanel.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCanel.ForeColor = System.Drawing.Color.White;
            this.btnCanel.Location = new System.Drawing.Point(646, 459);
            this.btnCanel.Name = "btnCanel";
            this.btnCanel.Size = new System.Drawing.Size(126, 43);
            this.btnCanel.TabIndex = 94;
            this.btnCanel.Text = "取消";
            this.btnCanel.UseVisualStyleBackColor = false;
            this.btnCanel.Click += new System.EventHandler(this.btnCanel_Click);
            // 
            // btnBaoCun
            // 
            this.btnBaoCun.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnBaoCun.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnBaoCun.FlatAppearance.BorderSize = 0;
            this.btnBaoCun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBaoCun.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBaoCun.ForeColor = System.Drawing.Color.White;
            this.btnBaoCun.Location = new System.Drawing.Point(808, 459);
            this.btnBaoCun.Name = "btnBaoCun";
            this.btnBaoCun.Size = new System.Drawing.Size(126, 43);
            this.btnBaoCun.TabIndex = 93;
            this.btnBaoCun.Text = "保存修改";
            this.btnBaoCun.UseVisualStyleBackColor = false;
            this.btnBaoCun.Click += new System.EventHandler(this.btnBaoCun_Click);
            // 
            // dtYingBack
            // 
            this.dtYingBack.Location = new System.Drawing.Point(601, 233);
            this.dtYingBack.Name = "dtYingBack";
            this.dtYingBack.Size = new System.Drawing.Size(333, 34);
            this.dtYingBack.TabIndex = 92;
            // 
            // dtBack
            // 
            this.dtBack.Checked = false;
            this.dtBack.Enabled = false;
            this.dtBack.Location = new System.Drawing.Point(114, 297);
            this.dtBack.Name = "dtBack";
            this.dtBack.Size = new System.Drawing.Size(333, 34);
            this.dtBack.TabIndex = 91;
            // 
            // dtBorrow
            // 
            this.dtBorrow.Location = new System.Drawing.Point(114, 233);
            this.dtBorrow.Name = "dtBorrow";
            this.dtBorrow.Size = new System.Drawing.Size(333, 34);
            this.dtBorrow.TabIndex = 90;
            // 
            // nudMoney
            // 
            this.nudMoney.Location = new System.Drawing.Point(601, 297);
            this.nudMoney.Name = "nudMoney";
            this.nudMoney.Size = new System.Drawing.Size(333, 34);
            this.nudMoney.TabIndex = 89;
            // 
            // cmbState
            // 
            this.cmbState.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbState.FormattingEnabled = true;
            this.cmbState.Items.AddRange(new object[] {
            "借阅中",
            "已预约"});
            this.cmbState.Location = new System.Drawing.Point(114, 359);
            this.cmbState.Name = "cmbState";
            this.cmbState.Size = new System.Drawing.Size(333, 35);
            this.cmbState.TabIndex = 88;
            // 
            // txtBookWriter
            // 
            this.txtBookWriter.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtBookWriter.ForeColor = System.Drawing.Color.Black;
            this.txtBookWriter.Location = new System.Drawing.Point(601, 174);
            this.txtBookWriter.Name = "txtBookWriter";
            this.txtBookWriter.Size = new System.Drawing.Size(333, 34);
            this.txtBookWriter.TabIndex = 81;
            // 
            // txtBookId
            // 
            this.txtBookId.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtBookId.ForeColor = System.Drawing.Color.Black;
            this.txtBookId.Location = new System.Drawing.Point(601, 111);
            this.txtBookId.Name = "txtBookId";
            this.txtBookId.Size = new System.Drawing.Size(333, 34);
            this.txtBookId.TabIndex = 82;
            // 
            // txtManageId
            // 
            this.txtManageId.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtManageId.Enabled = false;
            this.txtManageId.ForeColor = System.Drawing.Color.Black;
            this.txtManageId.Location = new System.Drawing.Point(601, 359);
            this.txtManageId.Name = "txtManageId";
            this.txtManageId.Size = new System.Drawing.Size(333, 34);
            this.txtManageId.TabIndex = 83;
            // 
            // txtBookName
            // 
            this.txtBookName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtBookName.ForeColor = System.Drawing.Color.Black;
            this.txtBookName.Location = new System.Drawing.Point(114, 174);
            this.txtBookName.Name = "txtBookName";
            this.txtBookName.Size = new System.Drawing.Size(333, 34);
            this.txtBookName.TabIndex = 84;
            // 
            // txtReaderID
            // 
            this.txtReaderID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtReaderID.Enabled = false;
            this.txtReaderID.ForeColor = System.Drawing.Color.Black;
            this.txtReaderID.Location = new System.Drawing.Point(114, 111);
            this.txtReaderID.Name = "txtReaderID";
            this.txtReaderID.Size = new System.Drawing.Size(333, 34);
            this.txtReaderID.TabIndex = 85;
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
            this.label11.Location = new System.Drawing.Point(521, 237);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(97, 27);
            this.label11.TabIndex = 76;
            this.label11.Text = "应还时间:";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
            this.label6.Location = new System.Drawing.Point(553, 178);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 27);
            this.label6.TabIndex = 70;
            this.label6.Text = "作者:";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
            this.label3.Location = new System.Drawing.Point(505, 363);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 27);
            this.label3.TabIndex = 75;
            this.label3.Text = "管理员编号:";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
            this.label7.Location = new System.Drawing.Point(31, 237);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 27);
            this.label7.TabIndex = 71;
            this.label7.Text = "借阅时间:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
            this.label2.Location = new System.Drawing.Point(31, 363);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 27);
            this.label2.TabIndex = 74;
            this.label2.Text = "处理状态:";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
            this.label9.Location = new System.Drawing.Point(31, 301);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 27);
            this.label9.TabIndex = 72;
            this.label9.Text = "归还时间:";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
            this.label10.Location = new System.Drawing.Point(521, 301);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(97, 27);
            this.label10.TabIndex = 73;
            this.label10.Text = "罚款金额:";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
            this.label5.Location = new System.Drawing.Point(47, 178);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 27);
            this.label5.TabIndex = 77;
            this.label5.Text = "图书名:";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
            this.label4.Location = new System.Drawing.Point(521, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 27);
            this.label4.TabIndex = 78;
            this.label4.Text = "图书编号:";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
            this.label1.Location = new System.Drawing.Point(31, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 27);
            this.label1.TabIndex = 80;
            this.label1.Text = "读者编号:";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.cbEnable);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Controls.Add(this.btnCanel);
            this.panel5.Controls.Add(this.btnBaoCun);
            this.panel5.Controls.Add(this.dtYingBack);
            this.panel5.Controls.Add(this.dtBack);
            this.panel5.Controls.Add(this.dtBorrow);
            this.panel5.Controls.Add(this.nudMoney);
            this.panel5.Controls.Add(this.cmbState);
            this.panel5.Controls.Add(this.txtBookWriter);
            this.panel5.Controls.Add(this.txtBookId);
            this.panel5.Controls.Add(this.txtManageId);
            this.panel5.Controls.Add(this.txtBookName);
            this.panel5.Controls.Add(this.txtReaderID);
            this.panel5.Controls.Add(this.label11);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.label10);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.label1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(10, 10);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(965, 550);
            this.panel5.TabIndex = 19;
            // 
            // cbEnable
            // 
            this.cbEnable.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbEnable.AutoSize = true;
            this.cbEnable.Location = new System.Drawing.Point(453, 304);
            this.cbEnable.Name = "cbEnable";
            this.cbEnable.Size = new System.Drawing.Size(18, 17);
            this.cbEnable.TabIndex = 96;
            this.cbEnable.UseVisualStyleBackColor = true;
            this.cbEnable.CheckedChanged += new System.EventHandler(this.cbEnable_CheckedChanged);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(10, 560);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(965, 10);
            this.panel4.TabIndex = 16;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(10, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(965, 10);
            this.panel3.TabIndex = 17;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(975, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(10, 570);
            this.panel2.TabIndex = 18;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(10, 570);
            this.panel1.TabIndex = 15;
            // 
            // Form_BillManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(985, 570);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Form_BillManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form_BillManager";
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMoney)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnCanel;
        private System.Windows.Forms.Button btnBaoCun;
        private System.Windows.Forms.DateTimePicker dtYingBack;
        private System.Windows.Forms.DateTimePicker dtBack;
        private System.Windows.Forms.DateTimePicker dtBorrow;
        private System.Windows.Forms.NumericUpDown nudMoney;
        private System.Windows.Forms.ComboBox cmbState;
        private System.Windows.Forms.TextBox txtBookWriter;
        private System.Windows.Forms.TextBox txtBookId;
        private System.Windows.Forms.TextBox txtManageId;
        private System.Windows.Forms.TextBox txtBookName;
        private System.Windows.Forms.TextBox txtReaderID;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cbEnable;
    }
}