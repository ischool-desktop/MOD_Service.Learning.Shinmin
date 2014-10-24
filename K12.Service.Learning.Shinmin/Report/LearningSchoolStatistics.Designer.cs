namespace K12.Service.Learning.Shinmin
{
    partial class LearningSchoolStatistics
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
            this.btnPrint = new DevComponents.DotNetBar.ButtonX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.lbSchoolYear = new DevComponents.DotNetBar.LabelX();
            this.lbSemester = new DevComponents.DotNetBar.LabelX();
            this.intSchoolyear = new DevComponents.Editors.IntegerInput();
            this.intSemester = new DevComponents.Editors.IntegerInput();
            this.lbHelper1 = new DevComponents.DotNetBar.LabelX();
            this.lbHelp5 = new DevComponents.DotNetBar.LabelX();
            this.lbTempAll = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.intSchoolyear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intSemester)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPrint
            // 
            this.btnPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPrint.AutoSize = true;
            this.btnPrint.BackColor = System.Drawing.Color.Transparent;
            this.btnPrint.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPrint.Location = new System.Drawing.Point(270, 140);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 25);
            this.btnPrint.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "列印";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.AutoSize = true;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(351, 140);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 25);
            this.btnExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "取消";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lbSchoolYear
            // 
            this.lbSchoolYear.AutoSize = true;
            this.lbSchoolYear.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lbSchoolYear.BackgroundStyle.Class = "";
            this.lbSchoolYear.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbSchoolYear.Location = new System.Drawing.Point(66, 67);
            this.lbSchoolYear.Name = "lbSchoolYear";
            this.lbSchoolYear.Size = new System.Drawing.Size(47, 21);
            this.lbSchoolYear.TabIndex = 2;
            this.lbSchoolYear.Text = "學年度";
            // 
            // lbSemester
            // 
            this.lbSemester.AutoSize = true;
            this.lbSemester.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lbSemester.BackgroundStyle.Class = "";
            this.lbSemester.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbSemester.Location = new System.Drawing.Point(245, 67);
            this.lbSemester.Name = "lbSemester";
            this.lbSemester.Size = new System.Drawing.Size(34, 21);
            this.lbSemester.TabIndex = 3;
            this.lbSemester.Text = "學期";
            // 
            // intSchoolyear
            // 
            this.intSchoolyear.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.intSchoolyear.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intSchoolyear.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intSchoolyear.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intSchoolyear.Location = new System.Drawing.Point(126, 65);
            this.intSchoolyear.MaxValue = 200;
            this.intSchoolyear.MinValue = 90;
            this.intSchoolyear.Name = "intSchoolyear";
            this.intSchoolyear.ShowUpDown = true;
            this.intSchoolyear.Size = new System.Drawing.Size(80, 25);
            this.intSchoolyear.TabIndex = 4;
            this.intSchoolyear.Value = 90;
            // 
            // intSemester
            // 
            this.intSemester.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.intSemester.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intSemester.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intSemester.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intSemester.Location = new System.Drawing.Point(292, 65);
            this.intSemester.MaxValue = 2;
            this.intSemester.MinValue = 1;
            this.intSemester.Name = "intSemester";
            this.intSemester.ShowUpDown = true;
            this.intSemester.Size = new System.Drawing.Size(80, 25);
            this.intSemester.TabIndex = 5;
            this.intSemester.Value = 1;
            // 
            // lbHelper1
            // 
            this.lbHelper1.AutoSize = true;
            this.lbHelper1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lbHelper1.BackgroundStyle.Class = "";
            this.lbHelper1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbHelper1.Location = new System.Drawing.Point(15, 24);
            this.lbHelper1.Name = "lbHelper1";
            this.lbHelper1.Size = new System.Drawing.Size(127, 21);
            this.lbHelper1.TabIndex = 6;
            this.lbHelper1.Text = "請選擇列印學年期：";
            // 
            // lbHelp5
            // 
            this.lbHelp5.AutoSize = true;
            this.lbHelp5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lbHelp5.BackgroundStyle.Class = "";
            this.lbHelp5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbHelp5.Location = new System.Drawing.Point(66, 98);
            this.lbHelp5.Name = "lbHelp5";
            this.lbHelp5.Size = new System.Drawing.Size(243, 21);
            this.lbHelp5.TabIndex = 7;
            this.lbHelp5.Text = "(說明:平均值欄位皆4捨5入小數點後2位)";
            // 
            // lbTempAll
            // 
            this.lbTempAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbTempAll.AutoSize = true;
            this.lbTempAll.BackColor = System.Drawing.Color.Transparent;
            this.lbTempAll.Location = new System.Drawing.Point(91, 145);
            this.lbTempAll.Name = "lbTempAll";
            this.lbTempAll.Size = new System.Drawing.Size(86, 17);
            this.lbTempAll.TabIndex = 23;
            this.lbTempAll.TabStop = true;
            this.lbTempAll.Text = "功能變數總表";
            this.lbTempAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbTempAll_LinkClicked);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel1.Font = new System.Drawing.Font("微軟正黑體", 9.75F);
            this.linkLabel1.Location = new System.Drawing.Point(12, 145);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(60, 17);
            this.linkLabel1.TabIndex = 22;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "設定樣板";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // LearningSchoolStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 170);
            this.Controls.Add(this.lbTempAll);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.lbHelp5);
            this.Controls.Add(this.lbHelper1);
            this.Controls.Add(this.intSemester);
            this.Controls.Add(this.intSchoolyear);
            this.Controls.Add(this.lbSemester);
            this.Controls.Add(this.lbSchoolYear);
            this.DoubleBuffered = true;
            this.Name = "LearningSchoolStatistics";
            this.Text = "校園服務時數統計表";
            ((System.ComponentModel.ISupportInitialize)(this.intSchoolyear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intSemester)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnPrint;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.LabelX lbSchoolYear;
        private DevComponents.DotNetBar.LabelX lbSemester;
        private DevComponents.Editors.IntegerInput intSchoolyear;
        private DevComponents.Editors.IntegerInput intSemester;
        private DevComponents.DotNetBar.LabelX lbHelper1;
        private DevComponents.DotNetBar.LabelX lbHelp5;
        private System.Windows.Forms.LinkLabel lbTempAll;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}