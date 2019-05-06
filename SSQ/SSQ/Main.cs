using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using D2SPlatform.Core;
using SSQDataBase.COM;
using SSQDataBase.Entity;
using System.Data.OleDb;
using SSQ.Tools;

namespace SSQ
{
    public partial class Main : Form
    {
        private int winningType = 0; //0：最冷，1：最热，2：半冷半热,3：4冷2热，4：4冷1热1中
        private string winningTypeName = "最冷";
        string[] winningRed = new string[6];
        string winningBlue = "";
        List<int> random1_7_4 = random(1,7,4);
        List<int> random28_34_1 = random(28, 34, 1);
        List<int> random8_28_1 = random(8, 28, 1);

        private List<SsqNumbers> list = new List<SsqNumbers>();
        public Main()
        {
            InitializeComponent();
            InitViews();
        }

        private void btn_dr_Click(object sender, EventArgs e)
        {
            int pos = 0;
            try
            {
                List<SsqNumbers> sList = new List<SsqNumbers>();
                if (this.openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = this.openFileDialog.FileName;


                    string connStr = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 12.0;", @fileName);
                    OleDbConnection conn = new OleDbConnection(connStr);
                    conn.Open();

                    DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new Object[] { null, null, null, "TABLE" });
                    string tableName = dt.Rows[0]["TABLE_NAME"].ToString();
                    string sql = String.Format("SELECT * FROM [{0}]", tableName);
                    OleDbCommand cmd = new OleDbCommand(sql, conn);
                    OleDbDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string xh = ReadExcelCell(reader, 0).Trim();
                        System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(@"^\d+$");
      
                        if (!rex.IsMatch(xh))
                        {
                            continue;   
                        }

                        string ssqDate = ReadExcelCell(reader, 1).Trim();
                        string qh = ReadExcelCell(reader, 2).Trim();
                        string numbers = ReadExcelCell(reader, 3).Trim();
                        string[] numberss = numbers.Split(' ');
                        if (numberss.Length != 7)
                        {
                            continue;
                        }

                        SsqNumbers s = new SsqNumbers();
                        s.SsqDate = Common.ToDateTime(ssqDate);
                        s.Qh = qh;
                        List<int> temp = new List<int>();
                        temp.Add(Common.ToInt32(numberss[0]));
                        temp.Add(Common.ToInt32(numberss[1]));
                        temp.Add(Common.ToInt32(numberss[2]));
                        temp.Add(Common.ToInt32(numberss[3]));
                        temp.Add(Common.ToInt32(numberss[4]));
                        temp.Add(Common.ToInt32(numberss[5]));
                        temp.Sort();

                        s.NumBlue7 = Common.ToInt32(numberss[6]).ToString();
                        s.NumRed1 = temp[0].ToString();
                        s.NumRed2 = temp[1].ToString();
                        s.NumRed3 = temp[2].ToString();
                        s.NumRed4 = temp[3].ToString();
                        s.NumRed5 = temp[4].ToString();
                        s.NumRed6 = temp[5].ToString();

                        sList.Add(s);
                        ++pos;
                    }
                    SsqNumbersManager.GetInstance().Save(sList);
                    MessageBox.Show(string.Format("导入成功{0}条数据", pos));
                    InitList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log.GetInstance().WriteLog(ex.Message + ex.StackTrace,Log.Level.Error);
            }
        }
         public string ReadExcelCell(OleDbDataReader reader, int pos)
        {
            try
            {
                string result = "";
                string sectNoDbType = reader.GetDataTypeName(pos); ;//单元格类型
                switch (sectNoDbType)
                {
                    case "DBTYPE_WVARCHAR"://文本
                        result = Common.ReadString(reader, pos);
                        break;
                    case "DBTYPE_R8"://数值
                        result = Common.ReadDouble(reader, pos).ToString();
                        break;
                    case "DBTYPE_STR"://字符串
                        result = Common.ReadString(reader, pos);
                        break;
                    case "DBTYPE_DATE"://时间类型
                        result = Common.ReadDateTime(reader, pos).ToString("yyyy-MM-dd HH:mm:ss");
                        break;
                    case "DBTYPE_DECIMAL"://具有固定精度和范围的精确数字值
                        result = Common.ReadDecimal(reader, pos).ToString();
                        break;
                    case "DBTYPE_I8"://8 字节带符号的整数
                        result = Common.ReadInt32(reader, pos).ToString();
                        break;
                    default:
                        break;
                }
                return result;
            }
            catch (Exception e)
            {
                Log.GetInstance().WriteLog(e.Message + e.StackTrace, Log.Level.Error);
                return "";
            }
        }

        private void InitViews()
        {
            //初始化list
            this.listView.FullRowSelect = true;
            this.list_blue.FullRowSelect = true;
            this.list_fx.FullRowSelect = true;
            this.listView.MouseDoubleClick += this.btn_bindCont_Click;
            InitList();
        }

        //双击修改
        private void btn_bindCont_Click(object sender, EventArgs e)
        {
            try
            {
                ListView.SelectedListViewItemCollection l = this.listView.SelectedItems;
                SsqNumbers ssq = l[0].Tag as SsqNumbers;
                SSQ_Add add = new SSQ_Add(ssq.Qh);
                add.StartPosition = FormStartPosition.CenterScreen;
                DialogResult dr = add.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    InitList();
                }
            }
            catch (Exception ex)
            {
                Log.GetInstance().WriteLog(ex.Message + ex.StackTrace, Log.Level.Error);
            }
        }


        public void InitList()
        {
            try
            {
               
                this.listView.Clear();
                this.listView.View = View.Details;

                this.listView.Columns.Add("0", "序号", 50, HorizontalAlignment.Center, 0);
                this.listView.Columns.Add("1", "时间", 90, HorizontalAlignment.Center, 0);
                this.listView.Columns.Add("2", "期号", 90, HorizontalAlignment.Center, 0);
                this.listView.Columns.Add("3", "红1", 70, HorizontalAlignment.Center, 0);
                this.listView.Columns.Add("4", "红2", 70, HorizontalAlignment.Center, 0);
                this.listView.Columns.Add("5", "红3", 70, HorizontalAlignment.Center, 0);
                this.listView.Columns.Add("6", "红4", 70, HorizontalAlignment.Center, 0);
                this.listView.Columns.Add("7", "红5", 70, HorizontalAlignment.Center, 0);
                this.listView.Columns.Add("8", "红6", 70, HorizontalAlignment.Center, 0);
                this.listView.Columns.Add("9", "蓝1", 70, HorizontalAlignment.Center, 0);
                this.listView.Columns.Add("10", "中奖", 200, HorizontalAlignment.Center, 0);

                list = SsqNumbersManager.GetInstance().GetList(this.t_xsrq_s.Text,this.t_xsrq_e.Text);
                //分析数据
                FxSSQ(list);
                //中奖号码
                this.l_winning.Text = string.Format("中奖号码（{7}）：{0} {1} {2} {3} {4} {5}     {6}", winningRed[0], winningRed[1], winningRed[2], winningRed[3], winningRed[4], winningRed[5], winningBlue, winningTypeName);

                if (list.Count > 0)
                {
                    int pos = 0;
                    foreach (SsqNumbers item in list)
                    {
                        ++pos;
                        ListViewItem listItem = new ListViewItem();
                        listItem.SubItems.Clear();

                        listItem.Text = pos.ToString();
                        listItem.Tag = item;
                        listItem.SubItems.Add(item.SsqDate.ToString("yyyy-MM-dd"));
                        listItem.SubItems.Add(item.Qh);
                        listItem.SubItems.Add(item.NumRed1);
                        listItem.SubItems.Add(item.NumRed2);
                        listItem.SubItems.Add(item.NumRed3);
                        listItem.SubItems.Add(item.NumRed4);
                        listItem.SubItems.Add(item.NumRed5);
                        listItem.SubItems.Add(item.NumRed6);
                        listItem.SubItems.Add(item.NumBlue7);

                        bool isBlue = false;
                        //蓝色球相同
                        if (winningBlue.Equals(Common.ToInt32(item.NumBlue7).ToString()))
                        {
                            isBlue = true;
                        }
                        //红球几个
                        int winCount = 0;
                        if (winningRed.Contains(item.NumRed1)) ++winCount;
                        if (winningRed.Contains(item.NumRed2)) ++winCount;
                        if (winningRed.Contains(item.NumRed3)) ++winCount;
                        if (winningRed.Contains(item.NumRed4)) ++winCount;
                        if (winningRed.Contains(item.NumRed5)) ++winCount;
                        if (winningRed.Contains(item.NumRed6)) ++winCount;
                        if (isBlue && winCount == 0)
                        {
                            listItem.SubItems.Add(string.Format("0+1，六等奖（5.88/100），5元"));
                            listItem.SubItems[10].BackColor = Color.Yellow;
                        }
                        if (isBlue && winCount == 1)
                        {
                            listItem.SubItems.Add(string.Format("1+1，六等奖（5.88/100），5元"));
                            listItem.SubItems[10].BackColor = Color.Yellow;
                        }
                        if (isBlue && winCount == 2)
                        {
                            listItem.SubItems.Add(string.Format("2+1，六等奖（5.88/100），5元"));
                            listItem.SubItems[10].BackColor = Color.Yellow;
                        }
                        if (!isBlue && winCount == 4)
                        {
                            listItem.SubItems.Add(string.Format("4+0，五等奖（1/2728），10元"));
                            listItem.SubItems[10].BackColor = Color.Yellow;
                        }
                        if (isBlue && winCount == 3)
                        {
                            listItem.SubItems.Add(string.Format("3+1，五等奖（1/2728），10元"));
                            listItem.SubItems[10].BackColor = Color.Yellow;
                        }
                        if (!isBlue && winCount == 5)
                        {
                            listItem.SubItems.Add(string.Format("5+0，四等奖（1/2万），200元"));
                            listItem.SubItems[10].BackColor = Color.Red;
                        }
                        if (isBlue && winCount == 4)
                        {
                            listItem.SubItems.Add(string.Format("4+1，四等奖（1/2万），200元"));
                            listItem.SubItems[10].BackColor = Color.Red;
                        }
                        if (isBlue && winCount == 5)
                        {
                            listItem.SubItems.Add(string.Format("5+1，三等奖（162/1770万），3000元"));
                            listItem.SubItems[10].BackColor = Color.Red;
                        }
                        if (!isBlue && winCount == 6)
                        {
                            listItem.SubItems.Add(string.Format("6+0，二等奖（15/1770万），25%"));
                            listItem.SubItems[10].BackColor = Color.Red;
                        }
                        if (isBlue && winCount == 6)
                        {
                            listItem.SubItems.Add(string.Format("6+0，一等奖（1/1770万），100%"));
                            listItem.SubItems[10].BackColor = Color.Red;
                        }
                        if (!isBlue && winCount == 0)
                        {
                            listItem.SubItems.Add(string.Format("0+0，一个未中"));
                            listItem.SubItems[10].BackColor = Color.Green;
                        }

                        listItem.SubItems[3].ForeColor = Color.Red;
                        listItem.SubItems[4].ForeColor = Color.Red;
                        listItem.SubItems[5].ForeColor = Color.Red;
                        listItem.SubItems[6].ForeColor = Color.Red;
                        listItem.SubItems[7].ForeColor = Color.Red;
                        listItem.SubItems[8].ForeColor = Color.Red;

                        listItem.SubItems[9].ForeColor = Color.Blue;

                        this.listView.Items.Add(listItem);
                        this.listView.Items[pos - 1].UseItemStyleForSubItems = false; //可以单独设置listview的颜色
                    }
                    this.l_count.Text = string.Format("共{0}条数据",pos);
                }
            }
            catch (Exception e)
            {
                Log.GetInstance().WriteLog(e.Message, Log.Level.Error);
                Log.GetInstance().WriteLog(e.StackTrace, Log.Level.Error);
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            InitList();
        }

        //分析
        public void FxSSQ(List<SsqNumbers> list)
        {
            if (list == null) return;
            if (list.Count == 0) return;
            //刷新随机数
            random1_7_4 = random(1, 7, 4);
            random28_34_1 = random(28, 34, 1);
            random8_28_1 = random(8, 28, 1);

            int redCount1 = 0, redCount2 = 0, redCount3 = 0, redCount4 = 0, redCount5 = 0, redCount6 = 0, redCount7 = 0, redCount8 = 0, redCount9 = 0, redCount10 = 0;
            int redCount11 = 0, redCount12 = 0, redCount13 = 0, redCount14 = 0, redCount15 = 0, redCount16 = 0, redCount17 = 0, redCount18 = 0, redCount19 = 0, redCount20 = 0;
            int redCount21 = 0, redCount22 = 0, redCount23 = 0, redCount24 = 0, redCount25 = 0, redCount26 = 0, redCount27 = 0, redCount28 = 0, redCount29 = 0, redCount30 = 0, redCount31 = 0, redCount32 = 0, redCount33 = 0;

            int blueCount1 = 0, blueCount2 = 0, blueCount3 = 0, blueCount4 = 0, blueCount5 = 0, blueCount6 = 0, blueCount7 = 0, blueCount8 = 0, blueCount9 = 0, blueCount10 = 0;
            int blueCount11 = 0, blueCount12 = 0, blueCount13 = 0, blueCount14 = 0, blueCount15 = 0, blueCount16 = 0;

            for (int i = 0; i < list.Count; i++)
            {
                SsqNumbers item = list[i];
                //红球统计出现次数
                for (int j = 1; j < 7; j++)
                {
                    string numRed = "";
                    if (j == 1)
                    {
                        numRed = item.NumRed1;
                    }
                    else if (j == 2)
                    {
                        numRed = item.NumRed2;
                    }
                    else if (j == 3)
                    {
                        numRed = item.NumRed3;
                    }
                    else if (j == 4)
                    {
                        numRed = item.NumRed4;
                    }
                    else if (j == 5)
                    {
                        numRed = item.NumRed5;
                    }
                    else if (j == 6)
                    {
                        numRed = item.NumRed6;
                    }

                    switch (numRed)
                    {
                        case "1":
                            ++redCount1;
                            break;
                        case "2":
                            ++redCount2;
                            break;
                        case "3":
                            ++redCount3;
                            break;
                        case "4":
                            ++redCount4;
                            break;
                        case "5":
                            ++redCount5;
                            break;
                        case "6":
                            ++redCount6;
                            break;
                        case "7":
                            ++redCount7;
                            break;
                        case "8":
                            ++redCount8;
                            break;
                        case "9":
                            ++redCount9;
                            break;
                        case "10":
                            ++redCount10;
                            break;
                        case "11":
                            ++redCount11;
                            break;
                        case "12":
                            ++redCount12;
                            break;
                        case "13":
                            ++redCount13;
                            break;
                        case "14":
                            ++redCount14;
                            break;
                        case "15":
                            ++redCount15;
                            break;
                        case "16":
                            ++redCount16;
                            break;
                        case "17":
                            ++redCount17;
                            break;
                        case "18":
                            ++redCount18;
                            break;
                        case "19":
                            ++redCount19;
                            break;
                        case "20":
                            ++redCount20;
                            break;
                        case "21":
                            ++redCount21;
                            break;
                        case "22":
                            ++redCount22;
                            break;
                        case "23":
                            ++redCount23;
                            break;
                        case "24":
                            ++redCount24;
                            break;
                        case "25":
                            ++redCount25;
                            break;
                        case "26":
                            ++redCount26;
                            break;
                        case "27":
                            ++redCount27;
                            break;
                        case "28":
                            ++redCount28;
                            break;
                        case "29":
                            ++redCount29;
                            break;
                        case "30":
                            ++redCount30;
                            break;
                        case "31":
                            ++redCount31;
                            break;
                        case "32":
                            ++redCount32;
                            break;
                        case "33":
                            ++redCount33;
                            break;
                        default:
                            Log.GetInstance().WriteLog(string.Format("期号为{0}的没统计", item.Qh), Log.Level.Error);
                            break;
                    }
                }
                //篮球统计出现次数
                string numBlue = Common.ToInt32(item.NumBlue7).ToString();
                switch (numBlue)
                {
                    case "1":
                        ++blueCount1;
                        break;
                    case "2":
                        ++blueCount2;
                        break;
                    case "3":
                        ++blueCount3;
                        break;
                    case "4":
                        ++blueCount4;
                        break;
                    case "5":
                        ++blueCount5;
                        break;
                    case "6":
                        ++blueCount6;
                        break;
                    case "7":
                        ++blueCount7;
                        break;
                    case "8":
                        ++blueCount8;
                        break;
                    case "9":
                        ++blueCount9;
                        break;
                    case "10":
                        ++blueCount10;
                        break;
                    case "11":
                        ++blueCount11;
                        break;
                    case "12":
                        ++blueCount12;
                        break;
                    case "13":
                        ++blueCount13;
                        break;
                    case "14":
                        ++blueCount14;
                        break;
                    case "15":
                        ++blueCount15;
                        break;
                    case "16":
                        ++blueCount16;
                        break;
                    default:
                        Log.GetInstance().WriteLog(string.Format("期号为{0}的没统计", item.Qh), Log.Level.Error);
                        break;
                }
            }//所有期号循环完毕

            //计算概率
            decimal redP1 = (decimal)redCount1 / list.Count;
            decimal redP2 = (decimal)redCount2 / list.Count;
            decimal redP3 = (decimal)redCount3 / list.Count;
            decimal redP4 = (decimal)redCount4 / list.Count;
            decimal redP5 = (decimal)redCount5 / list.Count;
            decimal redP6 = (decimal)redCount6 / list.Count;
            decimal redP7 = (decimal)redCount7 / list.Count;
            decimal redP8 = (decimal)redCount8 / list.Count;
            decimal redP9 = (decimal)redCount9 / list.Count;
            decimal redP10 = (decimal)redCount10 / list.Count;
            decimal redP11 = (decimal)redCount11 / list.Count;
            decimal redP12 = (decimal)redCount12 / list.Count;
            decimal redP13 = (decimal)redCount13 / list.Count;
            decimal redP14 = (decimal)redCount14 / list.Count;
            decimal redP15 = (decimal)redCount15 / list.Count;
            decimal redP16 = (decimal)redCount16 / list.Count;
            decimal redP17 = (decimal)redCount17 / list.Count;
            decimal redP18 = (decimal)redCount18 / list.Count;
            decimal redP19 = (decimal)redCount19 / list.Count;
            decimal redP20 = (decimal)redCount20 / list.Count;
            decimal redP21 = (decimal)redCount21 / list.Count;
            decimal redP22 = (decimal)redCount22 / list.Count;
            decimal redP23 = (decimal)redCount23 / list.Count;
            decimal redP24 = (decimal)redCount24 / list.Count;
            decimal redP25 = (decimal)redCount25 / list.Count;
            decimal redP26 = (decimal)redCount26 / list.Count;
            decimal redP27 = (decimal)redCount27 / list.Count;
            decimal redP28 = (decimal)redCount28 / list.Count;
            decimal redP29 = (decimal)redCount29 / list.Count;
            decimal redP30 = (decimal)redCount30 / list.Count;
            decimal redP31 = (decimal)redCount31 / list.Count;
            decimal redP32 = (decimal)redCount32/ list.Count;
            decimal redP33 = (decimal)redCount33 / list.Count;
            //次数存到Dictionary中
            Dictionary<string, int> redCs = new Dictionary<string, int>();
            redCs.Add("1", redCount1);
            redCs.Add("2", redCount2);
            redCs.Add("3", redCount3);
            redCs.Add("4", redCount4);
            redCs.Add("5", redCount5);
            redCs.Add("6", redCount6);
            redCs.Add("7", redCount7);
            redCs.Add("8", redCount8);
            redCs.Add("9", redCount9);
            redCs.Add("10", redCount10);
            redCs.Add("11", redCount11);
            redCs.Add("12", redCount12);
            redCs.Add("13", redCount13);
            redCs.Add("14", redCount14);
            redCs.Add("15", redCount15);
            redCs.Add("16", redCount16);
            redCs.Add("17", redCount17);
            redCs.Add("18", redCount18);
            redCs.Add("19", redCount19);
            redCs.Add("20", redCount20);
            redCs.Add("21", redCount21);
            redCs.Add("22", redCount22);
            redCs.Add("23", redCount23);
            redCs.Add("24", redCount24);
            redCs.Add("25", redCount25);
            redCs.Add("26", redCount26);
            redCs.Add("27", redCount27);
            redCs.Add("28", redCount28);
            redCs.Add("29", redCount29);
            redCs.Add("30", redCount30);
            redCs.Add("31", redCount31);
            redCs.Add("32", redCount32);
            redCs.Add("33", redCount33);
            //红球概率排序
            Dictionary<string, decimal> redPs = new Dictionary<string, decimal>();
            redPs.Add("1", redP1);
            redPs.Add("2", redP2);
            redPs.Add("3", redP3);
            redPs.Add("4", redP4);
            redPs.Add("5", redP5);
            redPs.Add("6", redP6);
            redPs.Add("7", redP7);
            redPs.Add("8", redP8);
            redPs.Add("9", redP9);
            redPs.Add("10", redP10);
            redPs.Add("11", redP11);
            redPs.Add("12", redP12);
            redPs.Add("13", redP13);
            redPs.Add("14", redP14);
            redPs.Add("15", redP15);
            redPs.Add("16", redP16);
            redPs.Add("17", redP17);
            redPs.Add("18", redP18);
            redPs.Add("19", redP19);
            redPs.Add("20", redP20);
            redPs.Add("21", redP21);
            redPs.Add("22", redP22);
            redPs.Add("23", redP23);
            redPs.Add("24", redP24);
            redPs.Add("25", redP25);
            redPs.Add("26", redP26);
            redPs.Add("27", redP27);
            redPs.Add("28", redP28);
            redPs.Add("29", redP29);
            redPs.Add("30", redP30);
            redPs.Add("31", redP31);
            redPs.Add("32", redP32);
            redPs.Add("33", redP33);
            var dicSort = from objDic in redPs orderby objDic.Value select objDic;

            this.list_fx.Clear();
            this.list_fx.View = View.Details;

            this.list_fx.Columns.Add("0", "序号", 30, HorizontalAlignment.Center, 0);
            this.list_fx.Columns.Add("1", "球种类", 80, HorizontalAlignment.Center, 0);
            this.list_fx.Columns.Add("2", "出现次数", 80, HorizontalAlignment.Center, 0);
            this.list_fx.Columns.Add("3", "总次数", 80, HorizontalAlignment.Center, 0);
            this.list_fx.Columns.Add("4", "出现概率", 80, HorizontalAlignment.Center, 0);
            int rPos = 0;
            foreach (KeyValuePair<string, decimal> kvp in dicSort)
            {
                rPos++;
                ListViewItem listItem = new ListViewItem();
                listItem.SubItems.Clear();
                string name = "红球" + kvp.Key;
                int redC = 0;
                redCs.TryGetValue(kvp.Key,out redC);

                listItem.Text = rPos.ToString();
                listItem.SubItems.Add(name);
                listItem.SubItems.Add(redC.ToString());
                listItem.SubItems.Add(list.Count.ToString());
                listItem.SubItems.Add(kvp.Value.ToString());
                if (winningType == 0)
                {
                    if (rPos == 1 || rPos == 2 || rPos == 3 || rPos == 4 || rPos == 5 || rPos == 6)
                    {
                        listItem.BackColor = Color.Yellow;
                        winningRed[rPos - 1] = kvp.Key;
                    }
                }
                else if (winningType == 1)
                {
                    if (rPos == 33 || rPos == 32 || rPos == 31 || rPos == 30 || rPos == 29 || rPos == 28)
                    {
                        listItem.BackColor = Color.Yellow;
                        winningRed[rPos - 28] = kvp.Key;
                    }
                }
                else if (winningType == 2)
                {
                    if (rPos == 1 || rPos == 2 || rPos == 3)
                    {
                        listItem.BackColor = Color.Yellow;
                        winningRed[rPos - 1] = kvp.Key;
                    }
                    if (rPos == 33 || rPos == 32 || rPos == 31)
                    {
                        listItem.BackColor = Color.Yellow;
                        winningRed[rPos - 28] = kvp.Key;
                    }
                }
                else if (winningType == 3)
                {
                    if (rPos == 1 || rPos == 2 || rPos == 3 || rPos == 4)
                    {
                        listItem.BackColor = Color.Yellow;
                        winningRed[rPos - 1] = kvp.Key;
                    }
                    if (rPos == 33 || rPos == 32)
                    {
                        listItem.BackColor = Color.Yellow;
                        winningRed[rPos - 28] = kvp.Key;
                    }
                }
                else if (winningType == 4)
                {
                    if (rPos == 1 || rPos == 2 || rPos == 3 || rPos == 4 || rPos == 5 || rPos == 6)
                    {
                        if (random1_7_4.Contains(rPos))
                        {
                            int pos = getListIndexByValue(random1_7_4, rPos);
                            listItem.BackColor = Color.Yellow;
                            winningRed[pos] = kvp.Key;
                        }
                    }
                    else if (rPos == random28_34_1[0])
                    {
                        listItem.BackColor = Color.Yellow;
                        winningRed[4] = kvp.Key;
                    }
                    else if (rPos == random8_28_1[0])
                    {
                        listItem.BackColor = Color.Yellow;
                        winningRed[5] = kvp.Key;
                    }
                }

                this.list_fx.Items.Add(listItem);
            }
            decimal blueP1 = (decimal)blueCount1 / list.Count;
            decimal blueP2 = (decimal)blueCount2 / list.Count;
            decimal blueP3 = (decimal)blueCount3 / list.Count;
            decimal blueP4 = (decimal)blueCount4 / list.Count;
            decimal blueP5 = (decimal)blueCount5 / list.Count;
            decimal blueP6 = (decimal)blueCount6 / list.Count;
            decimal blueP7 = (decimal)blueCount7 / list.Count;
            decimal blueP8 = (decimal)blueCount8 / list.Count;
            decimal blueP9 = (decimal)blueCount9 / list.Count;
            decimal blueP10 = (decimal)blueCount10 / list.Count;
            decimal blueP11 = (decimal)blueCount11 / list.Count;
            decimal blueP12 = (decimal)blueCount12 / list.Count;
            decimal blueP13 = (decimal)blueCount13 / list.Count;
            decimal blueP14 = (decimal)blueCount14 / list.Count;
            decimal blueP15 = (decimal)blueCount15 / list.Count;
            decimal blueP16 = (decimal)blueCount16 / list.Count;
            //次数存到Dictionary中
            Dictionary<string, int> blueCs = new Dictionary<string, int>();
            blueCs.Add("1", blueCount1);
            blueCs.Add("2", blueCount2);
            blueCs.Add("3", blueCount3);
            blueCs.Add("4", blueCount4);
            blueCs.Add("5", blueCount5);
            blueCs.Add("6", blueCount6);
            blueCs.Add("7", blueCount7);
            blueCs.Add("8", blueCount8);
            blueCs.Add("9", blueCount9);
            blueCs.Add("10", blueCount10);
            blueCs.Add("11", blueCount11);
            blueCs.Add("12", blueCount12);
            blueCs.Add("13", blueCount13);
            blueCs.Add("14", blueCount14);
            blueCs.Add("15", blueCount15);
            blueCs.Add("16", blueCount16);
            //蓝球概率排序
            Dictionary<string, decimal> bluePs = new Dictionary<string, decimal>();
            bluePs.Add("1", blueP1);
            bluePs.Add("2", blueP2);
            bluePs.Add("3", blueP3);
            bluePs.Add("4", blueP4);
            bluePs.Add("5", blueP5);
            bluePs.Add("6", blueP6);
            bluePs.Add("7", blueP7);
            bluePs.Add("8", blueP8);
            bluePs.Add("9", blueP9);
            bluePs.Add("10", blueP10);
            bluePs.Add("11", blueP11);
            bluePs.Add("12", blueP12);
            bluePs.Add("13", blueP13);
            bluePs.Add("14", blueP14);
            bluePs.Add("15", blueP15);
            bluePs.Add("16", blueP16);
            var dicSortBlue = from objDicBlue in bluePs orderby objDicBlue.Value select objDicBlue;

            this.list_blue.Clear();
            this.list_blue.View = View.Details;

            this.list_blue.Columns.Add("4", "序号", 30, HorizontalAlignment.Center, 0);
            this.list_blue.Columns.Add("0", "球种类", 80, HorizontalAlignment.Center, 0);
            this.list_blue.Columns.Add("1", "出现次数", 80, HorizontalAlignment.Center, 0);
            this.list_blue.Columns.Add("2", "总次数", 80, HorizontalAlignment.Center, 0);
            this.list_blue.Columns.Add("3", "出现概率", 80, HorizontalAlignment.Center, 0);
            int bPos = 0;
            foreach (KeyValuePair<string, decimal> kvp in dicSortBlue)
            {
                bPos++;
                ListViewItem listItem = new ListViewItem();

                string name = "蓝球" + kvp.Key;
                int blueC = 0;
                blueCs.TryGetValue(kvp.Key, out blueC);

                listItem.Text = bPos.ToString();
                listItem.SubItems.Add(name);
                listItem.SubItems.Add(blueC.ToString());
                listItem.SubItems.Add(list.Count.ToString());
                listItem.SubItems.Add(kvp.Value.ToString());
                if (winningType == 0)
                {
                    if (bPos == 1)
                    {
                        listItem.BackColor = Color.Yellow;
                        winningBlue = kvp.Key.ToString();
                    }
                }
                else if (winningType == 1)
                {
                    if (bPos == 16)
                    {
                        listItem.BackColor = Color.Yellow;
                        winningBlue = kvp.Key.ToString();
                    } 
                }
                else
                {
                    Random ran = new Random();
                    int n = ran.Next(1, 3); //产生1和2的随机数，范围是: >=参数1, 而且 < 参数2
                    if (n == 1)
                    {
                        if (bPos == 1)
                        {
                            listItem.BackColor = Color.Yellow;
                            winningBlue = kvp.Key.ToString();
                        } 
                    }
                    else if (n == 2)
                    {
                        if (bPos == 16)
                        {
                            listItem.BackColor = Color.Yellow;
                            winningBlue = kvp.Key.ToString();
                        }
                    }
                }
                this.list_blue.Items.Add(listItem);
            }

        }

        private void btn_fx_Click(object sender, EventArgs e)
        {
          
        }

        private void s_sxhrq_ValueChanged(object sender, EventArgs e)
        {
            this.t_xsrq_s.Text = this.s_sxhrq.Value.ToString("yyy-MM-dd");
        }

        private void s_exhrq_ValueChanged(object sender, EventArgs e)
        {
            this.t_xsrq_e.Text = this.s_sxhrq.Value.ToString("yyy-MM-dd");
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            SSQ_Add add = new SSQ_Add("");
            add.StartPosition = FormStartPosition.CenterParent;
            if (add.ShowDialog() == DialogResult.OK)
            {
                InitList();
            }
        }

        private void btn_less_Click(object sender, EventArgs e)
        {
            winningType = 0;
            winningTypeName = "最冷";
            InitList();
        }

        private void btn_more_Click(object sender, EventArgs e)
        {
            winningType = 1;
            winningTypeName = "最热";
            InitList();
        }

        private void btn_normal_Click(object sender, EventArgs e)
        {
            winningType = 2;
            winningTypeName = "半冷半热";
            InitList();
        }

        private void btn_4c2h_Click(object sender, EventArgs e)
        {
            winningType = 3;
            winningTypeName = "4冷2热";
            InitList();
        }

        private void btn_4c1h1n_Click(object sender, EventArgs e)
        {
            winningType = 4;
            winningTypeName = "4冷1热1中";
            InitList();
        }

        /// <summary>
        /// 在范围内生成指定个数的随机数
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<int> random(int min,int max,int count)
        {
            List<int> result = new List<int>();
            Random r = new Random();
            for (int i = 0; i < count;i++)
            {
                int n = r.Next(min, max);
                while (result.Contains(n))
                {
                    n = r.Next(min, max);
                }
                result.Add(n);
            }
            if (result.Count == count)
            {
                return result;
            }
            return new List<int>();
        }

        public static int getListIndexByValue(List<int> ls,int value)
        {
            int result = -1;
            for(int i = 0;i < ls.Count;i++)
            {
                if (ls[i] == value)
                {
                    result = i;
                    break;
                }
            }
            return result;
        }
    }
}
