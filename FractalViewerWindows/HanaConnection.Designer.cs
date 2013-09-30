namespace FractalViewerWindows
{
    partial class HanaConnection
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HanaConnection));
         this.buttonGetData = new System.Windows.Forms.Button();
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.label5 = new System.Windows.Forms.Label();
         this.textBoxYMax = new System.Windows.Forms.TextBox();
         this.textBoxYMin = new System.Windows.Forms.TextBox();
         this.textBoxXMax = new System.Windows.Forms.TextBox();
         this.textBoxXMin = new System.Windows.Forms.TextBox();
         this.comboBoxResolution = new System.Windows.Forms.ComboBox();
         this.labelRequestedRanges = new System.Windows.Forms.Label();
         this.labelResolution = new System.Windows.Forms.Label();
         this.label4 = new System.Windows.Forms.Label();
         this.labelResultSet = new System.Windows.Forms.Label();
         this.label1 = new System.Windows.Forms.Label();
         this.comboBoxBrushWidth = new System.Windows.Forms.ComboBox();
         this.buttonRefreshDisplay = new System.Windows.Forms.Button();
         this.comboBoxBrushShape = new System.Windows.Forms.ComboBox();
         this.groupBox2 = new System.Windows.Forms.GroupBox();
         this.label7 = new System.Windows.Forms.Label();
         this.label6 = new System.Windows.Forms.Label();
         this.label3 = new System.Windows.Forms.Label();
         this.comboBoxScheme = new System.Windows.Forms.ComboBox();
         this.txtAlpha = new System.Windows.Forms.TextBox();
         this.label2 = new System.Windows.Forms.Label();
         this.labelResultMeta = new System.Windows.Forms.Label();
         this.groupBox3 = new System.Windows.Forms.GroupBox();
         this.labelRangeRecY = new System.Windows.Forms.Label();
         this.labelRangeRecX = new System.Windows.Forms.Label();
         this.groupBox1.SuspendLayout();
         this.groupBox2.SuspendLayout();
         this.groupBox3.SuspendLayout();
         this.SuspendLayout();
         // 
         // buttonGetData
         // 
         this.buttonGetData.Location = new System.Drawing.Point(6, 19);
         this.buttonGetData.Name = "buttonGetData";
         this.buttonGetData.Size = new System.Drawing.Size(110, 23);
         this.buttonGetData.TabIndex = 3;
         this.buttonGetData.Text = "Get Data";
         this.buttonGetData.UseVisualStyleBackColor = true;
         this.buttonGetData.Click += new System.EventHandler(this.buttonGetData_Click);
         // 
         // groupBox1
         // 
         this.groupBox1.Controls.Add(this.label5);
         this.groupBox1.Controls.Add(this.textBoxYMax);
         this.groupBox1.Controls.Add(this.textBoxYMin);
         this.groupBox1.Controls.Add(this.textBoxXMax);
         this.groupBox1.Controls.Add(this.textBoxXMin);
         this.groupBox1.Controls.Add(this.comboBoxResolution);
         this.groupBox1.Controls.Add(this.labelRequestedRanges);
         this.groupBox1.Controls.Add(this.labelResolution);
         this.groupBox1.Controls.Add(this.buttonGetData);
         this.groupBox1.Controls.Add(this.label4);
         this.groupBox1.Location = new System.Drawing.Point(12, 12);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(298, 105);
         this.groupBox1.TabIndex = 2;
         this.groupBox1.TabStop = false;
         this.groupBox1.Text = "Data Requested";
         // 
         // label5
         // 
         this.label5.AutoSize = true;
         this.label5.Location = new System.Drawing.Point(284, 72);
         this.label5.Name = "label5";
         this.label5.Size = new System.Drawing.Size(11, 13);
         this.label5.TabIndex = 20;
         this.label5.Text = "}";
         // 
         // textBoxYMax
         // 
         this.textBoxYMax.Location = new System.Drawing.Point(236, 69);
         this.textBoxYMax.MaxLength = 10;
         this.textBoxYMax.Name = "textBoxYMax";
         this.textBoxYMax.Size = new System.Drawing.Size(47, 20);
         this.textBoxYMax.TabIndex = 18;
         this.textBoxYMax.Text = "0.45";
         // 
         // textBoxYMin
         // 
         this.textBoxYMin.Location = new System.Drawing.Point(188, 69);
         this.textBoxYMin.MaxLength = 10;
         this.textBoxYMin.Name = "textBoxYMin";
         this.textBoxYMin.Size = new System.Drawing.Size(44, 20);
         this.textBoxYMin.TabIndex = 17;
         this.textBoxYMin.Text = "-0.11";
         // 
         // textBoxXMax
         // 
         this.textBoxXMax.Location = new System.Drawing.Point(118, 69);
         this.textBoxXMax.MaxLength = 10;
         this.textBoxXMax.Name = "textBoxXMax";
         this.textBoxXMax.Size = new System.Drawing.Size(40, 20);
         this.textBoxXMax.TabIndex = 16;
         this.textBoxXMax.Text = "0.4";
         // 
         // textBoxXMin
         // 
         this.textBoxXMin.Location = new System.Drawing.Point(70, 69);
         this.textBoxXMin.MaxLength = 10;
         this.textBoxXMin.Name = "textBoxXMin";
         this.textBoxXMin.Size = new System.Drawing.Size(42, 20);
         this.textBoxXMin.TabIndex = 15;
         this.textBoxXMin.Text = "-1.0";
         // 
         // comboBoxResolution
         // 
         this.comboBoxResolution.FormattingEnabled = true;
         this.comboBoxResolution.Items.AddRange(new object[] {
            "100",
            "200",
            "400",
            "800",
            "6000"});
         this.comboBoxResolution.Location = new System.Drawing.Point(70, 46);
         this.comboBoxResolution.Name = "comboBoxResolution";
         this.comboBoxResolution.Size = new System.Drawing.Size(46, 21);
         this.comboBoxResolution.TabIndex = 14;
         this.comboBoxResolution.Text = "100";
         // 
         // labelRequestedRanges
         // 
         this.labelRequestedRanges.AutoSize = true;
         this.labelRequestedRanges.Location = new System.Drawing.Point(6, 72);
         this.labelRequestedRanges.Name = "labelRequestedRanges";
         this.labelRequestedRanges.Size = new System.Drawing.Size(65, 13);
         this.labelRequestedRanges.TabIndex = 13;
         this.labelRequestedRanges.Text = "Ranges:  x {";
         // 
         // labelResolution
         // 
         this.labelResolution.AutoSize = true;
         this.labelResolution.Location = new System.Drawing.Point(6, 49);
         this.labelResolution.Name = "labelResolution";
         this.labelResolution.Size = new System.Drawing.Size(60, 13);
         this.labelResolution.TabIndex = 7;
         this.labelResolution.Text = "Resolution:";
         // 
         // label4
         // 
         this.label4.AutoSize = true;
         this.label4.Location = new System.Drawing.Point(159, 72);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(29, 13);
         this.label4.TabIndex = 19;
         this.label4.Text = "}  y {";
         // 
         // labelResultSet
         // 
         this.labelResultSet.AutoSize = true;
         this.labelResultSet.Location = new System.Drawing.Point(6, 24);
         this.labelResultSet.Name = "labelResultSet";
         this.labelResultSet.Size = new System.Drawing.Size(41, 13);
         this.labelResultSet.TabIndex = 6;
         this.labelResultSet.Text = "Count: ";
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(57, 49);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(35, 13);
         this.label1.TabIndex = 9;
         this.label1.Text = "Width";
         // 
         // comboBoxBrushWidth
         // 
         this.comboBoxBrushWidth.FormattingEnabled = true;
         this.comboBoxBrushWidth.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
         this.comboBoxBrushWidth.Location = new System.Drawing.Point(98, 46);
         this.comboBoxBrushWidth.Name = "comboBoxBrushWidth";
         this.comboBoxBrushWidth.Size = new System.Drawing.Size(52, 21);
         this.comboBoxBrushWidth.TabIndex = 8;
         this.comboBoxBrushWidth.Text = "04";
         // 
         // buttonRefreshDisplay
         // 
         this.buttonRefreshDisplay.Location = new System.Drawing.Point(12, 17);
         this.buttonRefreshDisplay.Name = "buttonRefreshDisplay";
         this.buttonRefreshDisplay.Size = new System.Drawing.Size(115, 23);
         this.buttonRefreshDisplay.TabIndex = 5;
         this.buttonRefreshDisplay.Text = "Refresh Display";
         this.buttonRefreshDisplay.UseVisualStyleBackColor = true;
         this.buttonRefreshDisplay.Click += new System.EventHandler(this.buttonRefreshDisplay_Click);
         // 
         // comboBoxBrushShape
         // 
         this.comboBoxBrushShape.FormattingEnabled = true;
         this.comboBoxBrushShape.Items.AddRange(new object[] {
            "Circle",
            "Square"});
         this.comboBoxBrushShape.Location = new System.Drawing.Point(205, 45);
         this.comboBoxBrushShape.Name = "comboBoxBrushShape";
         this.comboBoxBrushShape.Size = new System.Drawing.Size(76, 21);
         this.comboBoxBrushShape.TabIndex = 10;
         this.comboBoxBrushShape.Text = "Circle";
         // 
         // groupBox2
         // 
         this.groupBox2.Controls.Add(this.label7);
         this.groupBox2.Controls.Add(this.label6);
         this.groupBox2.Controls.Add(this.label3);
         this.groupBox2.Controls.Add(this.comboBoxScheme);
         this.groupBox2.Controls.Add(this.txtAlpha);
         this.groupBox2.Controls.Add(this.label2);
         this.groupBox2.Controls.Add(this.buttonRefreshDisplay);
         this.groupBox2.Controls.Add(this.comboBoxBrushShape);
         this.groupBox2.Controls.Add(this.label1);
         this.groupBox2.Controls.Add(this.comboBoxBrushWidth);
         this.groupBox2.Location = new System.Drawing.Point(477, 12);
         this.groupBox2.Name = "groupBox2";
         this.groupBox2.Size = new System.Drawing.Size(287, 105);
         this.groupBox2.TabIndex = 11;
         this.groupBox2.TabStop = false;
         this.groupBox2.Text = "Data Drawn";
         // 
         // label7
         // 
         this.label7.AutoSize = true;
         this.label7.Location = new System.Drawing.Point(14, 49);
         this.label7.Name = "label7";
         this.label7.Size = new System.Drawing.Size(37, 13);
         this.label7.TabIndex = 16;
         this.label7.Text = "Brush:";
         // 
         // label6
         // 
         this.label6.AutoSize = true;
         this.label6.Location = new System.Drawing.Point(162, 49);
         this.label6.Name = "label6";
         this.label6.Size = new System.Drawing.Size(38, 13);
         this.label6.TabIndex = 15;
         this.label6.Text = "Shape";
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(161, 72);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(46, 13);
         this.label3.TabIndex = 14;
         this.label3.Text = "Scheme";
         // 
         // comboBoxScheme
         // 
         this.comboBoxScheme.FormattingEnabled = true;
         this.comboBoxScheme.Items.AddRange(new object[] {
            "Red",
            "Blue",
            "Orange"});
         this.comboBoxScheme.Location = new System.Drawing.Point(205, 68);
         this.comboBoxScheme.Name = "comboBoxScheme";
         this.comboBoxScheme.Size = new System.Drawing.Size(76, 21);
         this.comboBoxScheme.TabIndex = 13;
         this.comboBoxScheme.Text = "Red";
         // 
         // txtAlpha
         // 
         this.txtAlpha.Location = new System.Drawing.Point(98, 69);
         this.txtAlpha.MaxLength = 3;
         this.txtAlpha.Name = "txtAlpha";
         this.txtAlpha.Size = new System.Drawing.Size(52, 20);
         this.txtAlpha.TabIndex = 12;
         this.txtAlpha.Text = "127";
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(58, 72);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(34, 13);
         this.label2.TabIndex = 11;
         this.label2.Text = "Alpha";
         // 
         // labelResultMeta
         // 
         this.labelResultMeta.AutoSize = true;
         this.labelResultMeta.Location = new System.Drawing.Point(6, 46);
         this.labelResultMeta.Name = "labelResultMeta";
         this.labelResultMeta.Size = new System.Drawing.Size(50, 13);
         this.labelResultMeta.TabIndex = 12;
         this.labelResultMeta.Text = "Ranges: ";
         // 
         // groupBox3
         // 
         this.groupBox3.Controls.Add(this.labelRangeRecY);
         this.groupBox3.Controls.Add(this.labelRangeRecX);
         this.groupBox3.Controls.Add(this.labelResultMeta);
         this.groupBox3.Controls.Add(this.labelResultSet);
         this.groupBox3.Location = new System.Drawing.Point(316, 12);
         this.groupBox3.Name = "groupBox3";
         this.groupBox3.Size = new System.Drawing.Size(155, 105);
         this.groupBox3.TabIndex = 12;
         this.groupBox3.TabStop = false;
         this.groupBox3.Text = "Data Received";
         // 
         // labelRangeRecY
         // 
         this.labelRangeRecY.AutoSize = true;
         this.labelRangeRecY.Location = new System.Drawing.Point(53, 69);
         this.labelRangeRecY.Name = "labelRangeRecY";
         this.labelRangeRecY.Size = new System.Drawing.Size(15, 13);
         this.labelRangeRecY.TabIndex = 14;
         this.labelRangeRecY.Text = "y:";
         // 
         // labelRangeRecX
         // 
         this.labelRangeRecX.AutoSize = true;
         this.labelRangeRecX.Location = new System.Drawing.Point(53, 46);
         this.labelRangeRecX.Name = "labelRangeRecX";
         this.labelRangeRecX.Size = new System.Drawing.Size(15, 13);
         this.labelRangeRecX.TabIndex = 13;
         this.labelRangeRecX.Text = "x:";
         // 
         // HanaConnection
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(774, 629);
         this.Controls.Add(this.groupBox3);
         this.Controls.Add(this.groupBox2);
         this.Controls.Add(this.groupBox1);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.Name = "HanaConnection";
         this.Text = "HANA Fractal Viewer";
         this.Paint += new System.Windows.Forms.PaintEventHandler(this.HanaConnection_Paint);
         this.groupBox1.ResumeLayout(false);
         this.groupBox1.PerformLayout();
         this.groupBox2.ResumeLayout(false);
         this.groupBox2.PerformLayout();
         this.groupBox3.ResumeLayout(false);
         this.groupBox3.PerformLayout();
         this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonGetData;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonRefreshDisplay;
        private System.Windows.Forms.Label labelResultSet;
        private System.Windows.Forms.ComboBox comboBoxBrushWidth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxBrushShape;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtAlpha;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelResultMeta;
        private System.Windows.Forms.Label labelResolution;
        private System.Windows.Forms.Label labelRequestedRanges;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxScheme;
        private System.Windows.Forms.TextBox textBoxYMax;
        private System.Windows.Forms.TextBox textBoxYMin;
        private System.Windows.Forms.TextBox textBoxXMax;
        private System.Windows.Forms.TextBox textBoxXMin;
        private System.Windows.Forms.ComboBox comboBoxResolution;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelRangeRecY;
        private System.Windows.Forms.Label labelRangeRecX;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;

    }
}

