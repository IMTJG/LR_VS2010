namespace LR_VS2010
{
    partial class LR
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.tbWordFile = new System.Windows.Forms.TextBox();
            this.btnBrowseWordFile = new System.Windows.Forms.Button();
            this.lvAnalysis = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnLRAnalysis = new System.Windows.Forms.Button();
            this.lvMidCodes = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView2 = new System.Windows.Forms.ListView();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "单词序列：";
            // 
            // tbWordFile
            // 
            this.tbWordFile.Location = new System.Drawing.Point(101, 6);
            this.tbWordFile.Margin = new System.Windows.Forms.Padding(4);
            this.tbWordFile.Name = "tbWordFile";
            this.tbWordFile.ReadOnly = true;
            this.tbWordFile.Size = new System.Drawing.Size(1020, 25);
            this.tbWordFile.TabIndex = 1;
            // 
            // btnBrowseWordFile
            // 
            this.btnBrowseWordFile.Location = new System.Drawing.Point(1131, 6);
            this.btnBrowseWordFile.Margin = new System.Windows.Forms.Padding(4);
            this.btnBrowseWordFile.Name = "btnBrowseWordFile";
            this.btnBrowseWordFile.Size = new System.Drawing.Size(45, 29);
            this.btnBrowseWordFile.TabIndex = 2;
            this.btnBrowseWordFile.Text = "...";
            this.btnBrowseWordFile.UseVisualStyleBackColor = true;
            this.btnBrowseWordFile.Click += new System.EventHandler(this.btnBrowseWordFile_Click);
            // 
            // lvAnalysis
            // 
            this.lvAnalysis.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.lvAnalysis.FullRowSelect = true;
            this.lvAnalysis.GridLines = true;
            this.lvAnalysis.Location = new System.Drawing.Point(509, 41);
            this.lvAnalysis.Margin = new System.Windows.Forms.Padding(4);
            this.lvAnalysis.Name = "lvAnalysis";
            this.lvAnalysis.Size = new System.Drawing.Size(871, 495);
            this.lvAnalysis.TabIndex = 3;
            this.lvAnalysis.UseCompatibleStateImageBehavior = false;
            this.lvAnalysis.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "序号";
            this.columnHeader1.Width = 45;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "状态栈";
            this.columnHeader2.Width = 180;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "已归约串";
            this.columnHeader3.Width = 180;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "输入符号串";
            this.columnHeader4.Width = 128;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "所用产生式";
            this.columnHeader5.Width = 80;
            // 
            // btnLRAnalysis
            // 
            this.btnLRAnalysis.Location = new System.Drawing.Point(1184, 6);
            this.btnLRAnalysis.Margin = new System.Windows.Forms.Padding(4);
            this.btnLRAnalysis.Name = "btnLRAnalysis";
            this.btnLRAnalysis.Size = new System.Drawing.Size(100, 29);
            this.btnLRAnalysis.TabIndex = 4;
            this.btnLRAnalysis.Text = "LR分析";
            this.btnLRAnalysis.UseVisualStyleBackColor = true;
            this.btnLRAnalysis.Click += new System.EventHandler(this.btnLRAnalysis_Click);
            // 
            // lvMidCodes
            // 
            this.lvMidCodes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6});
            this.lvMidCodes.FullRowSelect = true;
            this.lvMidCodes.GridLines = true;
            this.lvMidCodes.Location = new System.Drawing.Point(1388, 42);
            this.lvMidCodes.Margin = new System.Windows.Forms.Padding(4);
            this.lvMidCodes.Name = "lvMidCodes";
            this.lvMidCodes.Size = new System.Drawing.Size(160, 494);
            this.lvMidCodes.TabIndex = 5;
            this.lvMidCodes.UseCompatibleStateImageBehavior = false;
            this.lvMidCodes.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "四元式";
            this.columnHeader6.Width = 112;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "单词类别";
            this.columnHeader11.Width = 128;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "单词值";
            this.columnHeader12.Width = 80;
            // 
            // listView2
            // 
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader11,
            this.columnHeader12});
            this.listView2.FullRowSelect = true;
            this.listView2.GridLines = true;
            this.listView2.Location = new System.Drawing.Point(230, 42);
            this.listView2.Margin = new System.Windows.Forms.Padding(4);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(271, 495);
            this.listView2.TabIndex = 7;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 42);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(211, 495);
            this.richTextBox1.TabIndex = 8;
            this.richTextBox1.Text = "";
            // 
            // LR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1561, 552);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.lvMidCodes);
            this.Controls.Add(this.btnLRAnalysis);
            this.Controls.Add(this.lvAnalysis);
            this.Controls.Add(this.btnBrowseWordFile);
            this.Controls.Add(this.tbWordFile);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LR";
            this.Text = "LR编译器";
            this.Load += new System.EventHandler(this.LRForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbWordFile;
        private System.Windows.Forms.Button btnBrowseWordFile;
        private System.Windows.Forms.ListView lvAnalysis;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Button btnLRAnalysis;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ListView lvMidCodes;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

