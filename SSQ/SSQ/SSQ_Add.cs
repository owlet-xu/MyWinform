using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SSQDataBase.Entity;
using SSQDataBase.COM;
using D2SPlatform.Core;

namespace SSQ
{
    public partial class SSQ_Add : Form
    {
        private string qh = "";
        public SSQ_Add(string qh)
        {
            InitializeComponent();
            this.qh = qh;
            if (!string.IsNullOrEmpty(qh))
            {
                SsqNumbers item = SsqNumbersManager.GetInstance().GetByQh(qh);
                this.t_qh.Text = item.Qh;
                this.t_rq.Text = item.SsqDate.ToString("yyyy-MM-dd");
                this.t_blue.Text = item.NumBlue7;
                this.t_red1.Text = item.NumRed1;
                this.t_red2.Text = item.NumRed2;
                this.t_red3.Text = item.NumRed3;
                this.t_red4.Text = item.NumRed4;
                this.t_red5.Text = item.NumRed5;
                this.t_red6.Text = item.NumRed6;

            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            string qh = this.t_qh.Text;
            if (string.IsNullOrEmpty(qh))
            {
                MessageBox.Show("请填写期号！");
                return;
            }
            if (string.IsNullOrEmpty(this.t_rq.Text))
            {
                MessageBox.Show("请填写日期！");
                return;
            }
            if (string.IsNullOrEmpty(this.t_blue.Text))
            {
                MessageBox.Show("请填写蓝球！");
                return;
            }
            if (string.IsNullOrEmpty(this.t_red1.Text) || string.IsNullOrEmpty(this.t_red2.Text) || string.IsNullOrEmpty(this.t_red3.Text) || string.IsNullOrEmpty(this.t_red4.Text) || string.IsNullOrEmpty(this.t_red5.Text) || string.IsNullOrEmpty(this.t_red6.Text))
            {
                MessageBox.Show("请填写红球！");
                return;
            }
            if (!string.IsNullOrEmpty(this.qh))
            {
                //确认框
                if (MessageBox.Show("是否修改数据?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SaveData();
                }
            }
            else
            {
                SaveData();
            }
        }

        private void SaveData()
        {
            string qh = this.t_qh.Text;
            SsqNumbers item = SsqNumbersManager.GetInstance().GetByQh(qh);
            if (item != null && string.IsNullOrEmpty(this.qh))
            {
                MessageBox.Show("该期号已经存在");
                return;
            }
            else
            {
                if (item == null)
                {
                    item = new SsqNumbers();
                }
                item.Qh = qh;
                item.SsqDate = Common.ToDateTime(this.t_rq.Text);
                item.NumBlue7 = this.t_blue.Text;
                item.NumRed1 = this.t_red1.Text;
                item.NumRed2 = this.t_red2.Text;
                item.NumRed3 = this.t_red3.Text;
                item.NumRed4 = this.t_red4.Text;
                item.NumRed5 = this.t_red5.Text;
                item.NumRed6 = this.t_red6.Text;
                List<SsqNumbers> aa = new List<SsqNumbers>();
                aa.Add(item);
                SsqNumbersManager.GetInstance().Save(aa);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void t_rq2_ValueChanged(object sender, EventArgs e)
        {
            this.t_rq.Text = this.t_rq2.Value.ToString("yyy-MM-dd");
        }
    }
}
