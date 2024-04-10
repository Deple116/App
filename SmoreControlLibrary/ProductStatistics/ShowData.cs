using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmoreVision.Forms;
using System.Collections.Concurrent;
using SmoreControlLibrary;
using SMLogControlLibrary;

namespace SmoreVision
{
    public partial class ShowData : UserControl
    {
        public static int iTotaNum = 150;
        public static int ColNum = 15;
        public SingleShow[] m_singleShow = new SingleShow[iTotaNum];

        //private string[] title = new string[]{"FAI01","FAI03", "FAI04","FAI05","FAI06","FAI07","FAI08_1","FAI08_2","FAI09_1","FAI09_2","FAI10","FAI12","FAI13",
        //                                    "FAI14","FAI15", "FAI16","FAI17","FAI18","FAI19","FAI20_1","FAI20_2","FAI21_1","FAI21_2","FAI23","FAI24","FAI25",
        //                                    "FAI26","FAI27", "FAI28","FAI29_1","FAI29_2","FAI31","FAI32_1","FAI32_2","FAI33_1","FAI33_2","FAI36","FAI37","FAI38_1",
        //                                    "FAI38_2","FAI39","FAI39_1","FAI39_2","FAI40","FAI40_1", "FAI40_2","FAI45","FAI46","FAI47_1","FAI47_2","FAI48_1","FAI48_2",
        //                                    "FAI49","FAI50_1","FAI50_2","FAI51_1","FAI51_2","FAI52","FAI53","FAI54","FAI55","FAI55_1","FAI55_2","FAI55_3","FAI55_4",
        //                                    "FAI55_5","FAI55_6","FAI56","FAI56_1","FAI56_2","FAI56_3","FAI56_4","FAI56_5","FAI56_6","FAI57","FAI57_1","FAI57_2","FAI57_3",
        //                                    "FAI57_4","FAI57_5","FAI57_6","FAI58","FAI58_1","FAI58_2","FAI58_3" ,"FAI58_4","FAI58_5","FAI58_6","FAI59","FAI60","FAI61","FAI62",
        //                                    "FAI65","FAI66","FAI67_1","FAI67_2","FAI67_3","FAI67_4","FAI68_1","FAI68_2","FAI68_3","FAI68_4","FAI69_1","FAI69_11","FAI69_2",
        //                                    "FAI69_3","FAI69_4","FAI71_1","FAI71_2","FAI73_1","FAI73_2","FAI74_1","FAI74_2","FAI74_3","FAI74_4","FAI78_1","FAI78_2","FAI79_1","FAI79_2"};


        public string[] title = null;

        public ShowData()
        {
            InitializeComponent();
        }

        private void ShowData_Load(object sender, EventArgs e)
        {
            //changeControl(0);
        }

        private void smButtonTest_BtnClick(object sender, EventArgs e)
        {

        }

        public void Show(Dictionary<string, string[]> dic)
        {
            try
            {
               
                Invoke((MethodInvoker)delegate
                {

                    //SMLogWindow.OutLog($"{dic.Count}:start", Color.Green);
                    labelTitle.Text = "OK";
                    labelTitle.ForeColor = System.Drawing.Color.Green;

                    bool bNg = true;

                    dic = dic.OrderBy(p => p.Key).ToDictionary(p => p.Key, o => o.Value); //排序
                    for (int i = 0; i < dic.Count; i++)
                    {
                        
                        m_singleShow[i].labelName.Text = dic.Keys.ElementAt(i);
                        //string[] arrRes = dic.FirstOrDefault(x => x.Key == title[i]).Value;

                        string[] arrRes = dic.Values.ElementAt(i);
                        //if(arrRes != null) SMLogWindow.OutLog($"arrRes_len:{arrRes.Length}", Color.Green);
                        if (arrRes != null && arrRes.Length > 1)
                        {

                            string[] temparr = arrRes[0].Split(',');
                            m_singleShow[i].labelValue.Text = $"X:{temparr[0]}\nY:{temparr[1]}\nZ:{temparr[2]}";

                            //dic.FirstOrDefault(x => x.Key == "FAI57_3").Value?.ToString();
                            if ("OK" == arrRes[1])
                            {
                                m_singleShow[i].ForeColor = System.Drawing.Color.Green;
                            }
                            else
                            {
                                m_singleShow[i].ForeColor = System.Drawing.Color.Red;
                                if (bNg)
                                {
                                    bNg = false;
                                    labelTitle.Text = "NG";
                                    labelTitle.ForeColor = System.Drawing.Color.Red;
                                }

                            }
                        }

                    }
                    //SMLogWindow.OutLog($"{dic.Count}:end", Color.Green);
                });
               
            }
            catch (Exception ex)
            {
                SMLogWindow.OutLog($"{ex.ToString()}", Color.Green);
                SMLogWindow.OutLog($"AlgoShowData_Show:{ex.ToString()}", Color.Green);
            }


            // string pp = dic.FirstOrDefault(x => x.Key == "FAI57_3").Value?.ToString();


        }

        public void changeControl(uint ucount)
        {
            if (ucount != 0) iTotaNum = (int)ucount;
           

            Invoke((MethodInvoker)delegate
            {
                SMLogWindow.OutLog($"{ucount}:start", Color.Green);
                this.tableLayoutPanel1.ColumnCount = ColNum;
                double dColRatio = 100.0 / ColNum;

                if (tableLayoutPanel1.RowStyles.Count > 0) tableLayoutPanel1.RowStyles.Clear();
                if (tableLayoutPanel1.ColumnStyles.Count > 0) tableLayoutPanel1.ColumnStyles.Clear();
                if (tableLayoutPanel1.Controls.Count > 0) tableLayoutPanel1.Controls.Clear();

                for (int i = 0; i < tableLayoutPanel1.ColumnCount; i++)
                {
                    tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, (float)dColRatio));
                }
                this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);

                this.tableLayoutPanel1.RowCount = iTotaNum % ColNum == 0 ? iTotaNum / ColNum + 1 : (iTotaNum / ColNum) + 2;
                double dRowRatio = 100.0 / tableLayoutPanel1.RowCount;
                for (int j = 0; j < tableLayoutPanel1.RowCount; j++)
                {

                    this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, (float)dRowRatio));
                }
                InitSingleControl();
                SMLogWindow.OutLog($"{ucount}:end", Color.Green);
            });
        }

        private void InitSingleControl()
        {
            labelTitle.BackColor = System.Drawing.Color.White;
            //labelTitle.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //labelTitle.ForeColor= System.Drawing.Color.Orange;
            for (int i = 0; i < iTotaNum; i++)
            {
                m_singleShow[i] = new SingleShow();
                int Row = (i + 1) % ColNum == 0 ? (i + 1) / ColNum : (i + 1) / ColNum + 1;
                int Col = (i + 1) % ColNum == 0 ? (ColNum - 1) : (i + 1) % ColNum - 1;
                //Console.WriteLine($"Row:{Row};Col:{Col}");

                this.tableLayoutPanel1.Controls.Add(m_singleShow[i], Col, Row);
                this.m_singleShow[i].BackColor = System.Drawing.Color.White;
                this.m_singleShow[i].Font = new System.Drawing.Font("微软雅黑", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                this.m_singleShow[i].ForeColor = System.Drawing.Color.Green;
                this.m_singleShow[i].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                this.m_singleShow[i].Dock = System.Windows.Forms.DockStyle.Fill;

                //this.ccdlabel[i].Location = new System.Drawing.Point(3, 0);
                //this.ccdlabel[i].Name = "CCD1label";
                //this.ccdlabel[i].Size = new System.Drawing.Size(92, 57);
                //this.m_singleShow[i].Text = $"label{i + 1}";
                //this.m_singleShow[i].TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0014) // 禁掉清除背景消息
                return;
            base.WndProc(ref m);
        }

        /// <summary>
        /// 解决窗体加载慢，卡顿的问题
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // 用双缓冲绘制窗口的所有子控件
                return cp;
            }
        }
    }
}
