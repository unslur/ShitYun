namespace NIMDemo
{
	partial class LivingStreamForm
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
            this.btn_ls = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rt_push_url = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pb_livingstream = new System.Windows.Forms.PictureBox();
            this.btn = new System.Windows.Forms.Button();
            this.btn_bypass = new System.Windows.Forms.Button();
            this.btn_beauty = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_livingstream)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_ls
            // 
            this.btn_ls.Location = new System.Drawing.Point(583, 389);
            this.btn_ls.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_ls.Name = "btn_ls";
            this.btn_ls.Size = new System.Drawing.Size(159, 29);
            this.btn_ls.TabIndex = 0;
            this.btn_ls.Text = "开始直播";
            this.btn_ls.UseVisualStyleBackColor = true;
            this.btn_ls.Click += new System.EventHandler(this.btn_ls_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 398);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "推流地址:";
            // 
            // rt_push_url
            // 
            this.rt_push_url.Location = new System.Drawing.Point(95, 391);
            this.rt_push_url.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rt_push_url.Name = "rt_push_url";
            this.rt_push_url.Size = new System.Drawing.Size(447, 28);
            this.rt_push_url.TabIndex = 2;
            this.rt_push_url.Text = "rtmp://pb9599a7a.live.126.net/live/449a765b771d46ee962c04d835bbb70f?wsSecret=f4ab" +
    "bffb2ba8a521f703d7c42abcc9c6&wsTime=1515057754";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pb_livingstream);
            this.groupBox1.Location = new System.Drawing.Point(23, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(520, 348);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "视频展示区域";
            // 
            // pb_livingstream
            // 
            this.pb_livingstream.Location = new System.Drawing.Point(9, 26);
            this.pb_livingstream.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pb_livingstream.Name = "pb_livingstream";
            this.pb_livingstream.Size = new System.Drawing.Size(503, 301);
            this.pb_livingstream.TabIndex = 0;
            this.pb_livingstream.TabStop = false;
            // 
            // btn
            // 
            this.btn.Location = new System.Drawing.Point(583, 300);
            this.btn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(159, 29);
            this.btn.TabIndex = 4;
            this.btn.Text = "音视频设置";
            this.btn.UseVisualStyleBackColor = true;
            this.btn.Click += new System.EventHandler(this.btn_Click);
            // 
            // btn_bypass
            // 
            this.btn_bypass.Location = new System.Drawing.Point(583, 250);
            this.btn_bypass.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_bypass.Name = "btn_bypass";
            this.btn_bypass.Size = new System.Drawing.Size(159, 29);
            this.btn_bypass.TabIndex = 5;
            this.btn_bypass.Text = "麦序接口测试";
            this.btn_bypass.UseVisualStyleBackColor = true;
            this.btn_bypass.Click += new System.EventHandler(this.btn_bypass_Click);
            // 
            // btn_beauty
            // 
            this.btn_beauty.Location = new System.Drawing.Point(583, 346);
            this.btn_beauty.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_beauty.Name = "btn_beauty";
            this.btn_beauty.Size = new System.Drawing.Size(159, 29);
            this.btn_beauty.TabIndex = 6;
            this.btn_beauty.Text = "美颜（开）";
            this.btn_beauty.UseVisualStyleBackColor = true;
            this.btn_beauty.Click += new System.EventHandler(this.btn_beauty_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // LivingStreamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 435);
            this.Controls.Add(this.btn_beauty);
            this.Controls.Add(this.btn_bypass);
            this.Controls.Add(this.btn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rt_push_url);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_ls);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "LivingStreamForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "直播相关";
            this.Load += new System.EventHandler(this.LivingStreamForm_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_livingstream)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btn_ls;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RichTextBox rt_push_url;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btn;
		private System.Windows.Forms.Button btn_bypass;
		private System.Windows.Forms.PictureBox pb_livingstream;
        private System.Windows.Forms.Button btn_beauty;
        private System.Windows.Forms.Timer timer1;
    }
}