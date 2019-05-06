namespace SSQ
{
    partial class Main
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_dr = new System.Windows.Forms.Button();
            this.listView = new System.Windows.Forms.ListView();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.l_count = new System.Windows.Forms.Label();
            this.btn_search = new System.Windows.Forms.Button();
            this.list_fx = new System.Windows.Forms.ListView();
            this.list_blue = new System.Windows.Forms.ListView();
            this.t_xsrq_e = new System.Windows.Forms.TextBox();
            this.t_xsrq_s = new System.Windows.Forms.TextBox();
            this.s_exhrq = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.s_sxhrq = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.l_winning = new System.Windows.Forms.Label();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_less = new System.Windows.Forms.Button();
            this.btn_more = new System.Windows.Forms.Button();
            this.btn_normal = new System.Windows.Forms.Button();
            this.btn_4c2h = new System.Windows.Forms.Button();
            this.btn_4c1h1n = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_dr
            // 
            this.btn_dr.Location = new System.Drawing.Point(12, 12);
            this.btn_dr.Name = "btn_dr";
            this.btn_dr.Size = new System.Drawing.Size(75, 23);
            this.btn_dr.TabIndex = 1;
            this.btn_dr.Text = "导入excel";
            this.btn_dr.UseVisualStyleBackColor = true;
            this.btn_dr.Click += new System.EventHandler(this.btn_dr_Click);
            // 
            // listView
            // 
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.Location = new System.Drawing.Point(7, 60);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(728, 379);
            this.listView.TabIndex = 2;
            this.listView.UseCompatibleStateImageBehavior = false;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // l_count
            // 
            this.l_count.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.l_count.AutoSize = true;
            this.l_count.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.l_count.ForeColor = System.Drawing.Color.Red;
            this.l_count.Location = new System.Drawing.Point(1046, 31);
            this.l_count.Name = "l_count";
            this.l_count.Size = new System.Drawing.Size(71, 12);
            this.l_count.TabIndex = 3;
            this.l_count.Text = "共xxx条数据";
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(379, 12);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(75, 23);
            this.btn_search.TabIndex = 4;
            this.btn_search.Text = "查询";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // list_fx
            // 
            this.list_fx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list_fx.Location = new System.Drawing.Point(745, 60);
            this.list_fx.Name = "list_fx";
            this.list_fx.Size = new System.Drawing.Size(377, 206);
            this.list_fx.TabIndex = 6;
            this.list_fx.UseCompatibleStateImageBehavior = false;
            // 
            // list_blue
            // 
            this.list_blue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.list_blue.Location = new System.Drawing.Point(745, 273);
            this.list_blue.Name = "list_blue";
            this.list_blue.Size = new System.Drawing.Size(377, 166);
            this.list_blue.TabIndex = 7;
            this.list_blue.UseCompatibleStateImageBehavior = false;
            // 
            // t_xsrq_e
            // 
            this.t_xsrq_e.Location = new System.Drawing.Point(271, 13);
            this.t_xsrq_e.Name = "t_xsrq_e";
            this.t_xsrq_e.Size = new System.Drawing.Size(85, 21);
            this.t_xsrq_e.TabIndex = 49;
            // 
            // t_xsrq_s
            // 
            this.t_xsrq_s.Location = new System.Drawing.Point(151, 13);
            this.t_xsrq_s.Name = "t_xsrq_s";
            this.t_xsrq_s.Size = new System.Drawing.Size(85, 21);
            this.t_xsrq_s.TabIndex = 54;
            // 
            // s_exhrq
            // 
            this.s_exhrq.Location = new System.Drawing.Point(271, 13);
            this.s_exhrq.Name = "s_exhrq";
            this.s_exhrq.Size = new System.Drawing.Size(102, 21);
            this.s_exhrq.TabIndex = 53;
            this.s_exhrq.ValueChanged += new System.EventHandler(this.s_exhrq_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(254, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 52;
            this.label3.Text = "至";
            // 
            // s_sxhrq
            // 
            this.s_sxhrq.Location = new System.Drawing.Point(151, 13);
            this.s_sxhrq.Name = "s_sxhrq";
            this.s_sxhrq.Size = new System.Drawing.Size(102, 21);
            this.s_sxhrq.TabIndex = 51;
            this.s_sxhrq.ValueChanged += new System.EventHandler(this.s_sxhrq_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(93, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 50;
            this.label2.Text = "销售日期";
            // 
            // l_winning
            // 
            this.l_winning.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.l_winning.AutoSize = true;
            this.l_winning.Location = new System.Drawing.Point(746, 41);
            this.l_winning.Name = "l_winning";
            this.l_winning.Size = new System.Drawing.Size(65, 12);
            this.l_winning.TabIndex = 55;
            this.l_winning.Text = "中奖号码：";
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(461, 11);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(75, 23);
            this.btn_add.TabIndex = 56;
            this.btn_add.Text = "添加";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_less
            // 
            this.btn_less.Location = new System.Drawing.Point(542, 12);
            this.btn_less.Name = "btn_less";
            this.btn_less.Size = new System.Drawing.Size(75, 23);
            this.btn_less.TabIndex = 57;
            this.btn_less.Text = "最冷";
            this.btn_less.UseVisualStyleBackColor = true;
            this.btn_less.Click += new System.EventHandler(this.btn_less_Click);
            // 
            // btn_more
            // 
            this.btn_more.Location = new System.Drawing.Point(623, 12);
            this.btn_more.Name = "btn_more";
            this.btn_more.Size = new System.Drawing.Size(75, 23);
            this.btn_more.TabIndex = 58;
            this.btn_more.Text = "最热";
            this.btn_more.UseVisualStyleBackColor = true;
            this.btn_more.Click += new System.EventHandler(this.btn_more_Click);
            // 
            // btn_normal
            // 
            this.btn_normal.Location = new System.Drawing.Point(704, 11);
            this.btn_normal.Name = "btn_normal";
            this.btn_normal.Size = new System.Drawing.Size(75, 23);
            this.btn_normal.TabIndex = 59;
            this.btn_normal.Text = "半冷半热";
            this.btn_normal.UseVisualStyleBackColor = true;
            this.btn_normal.Click += new System.EventHandler(this.btn_normal_Click);
            // 
            // btn_4c2h
            // 
            this.btn_4c2h.Location = new System.Drawing.Point(785, 11);
            this.btn_4c2h.Name = "btn_4c2h";
            this.btn_4c2h.Size = new System.Drawing.Size(75, 23);
            this.btn_4c2h.TabIndex = 60;
            this.btn_4c2h.Text = "4冷2热";
            this.btn_4c2h.UseVisualStyleBackColor = true;
            this.btn_4c2h.Click += new System.EventHandler(this.btn_4c2h_Click);
            // 
            // btn_4c1h1n
            // 
            this.btn_4c1h1n.Location = new System.Drawing.Point(866, 11);
            this.btn_4c1h1n.Name = "btn_4c1h1n";
            this.btn_4c1h1n.Size = new System.Drawing.Size(75, 23);
            this.btn_4c1h1n.TabIndex = 61;
            this.btn_4c1h1n.Text = "4冷1热1中间";
            this.btn_4c1h1n.UseVisualStyleBackColor = true;
            this.btn_4c1h1n.Click += new System.EventHandler(this.btn_4c1h1n_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1131, 444);
            this.Controls.Add(this.btn_4c1h1n);
            this.Controls.Add(this.btn_4c2h);
            this.Controls.Add(this.btn_normal);
            this.Controls.Add(this.btn_more);
            this.Controls.Add(this.btn_less);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.l_winning);
            this.Controls.Add(this.t_xsrq_e);
            this.Controls.Add(this.t_xsrq_s);
            this.Controls.Add(this.s_exhrq);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.s_sxhrq);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.list_blue);
            this.Controls.Add(this.list_fx);
            this.Controls.Add(this.btn_search);
            this.Controls.Add(this.l_count);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.btn_dr);
            this.Name = "Main";
            this.Text = "主界面";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_dr;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label l_count;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.ListView list_fx;
        private System.Windows.Forms.ListView list_blue;
        private System.Windows.Forms.TextBox t_xsrq_e;
        private System.Windows.Forms.TextBox t_xsrq_s;
        private System.Windows.Forms.DateTimePicker s_exhrq;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker s_sxhrq;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label l_winning;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btn_less;
        private System.Windows.Forms.Button btn_more;
        private System.Windows.Forms.Button btn_normal;
        private System.Windows.Forms.Button btn_4c2h;
        private System.Windows.Forms.Button btn_4c1h1n;
    }
}

