using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;

namespace SmoreControlLibrary.SMData
{
    public partial class SMDataWindow : UserControl
    {
        private int _totalNum;
        [Description("生产总数"), Category("SmoreControl")]
        public int TotalNum
        {
            get { return _totalNum; }
            set { _totalNum = value; labelTotalNum.Text = value.ToString(); }
        }
        private int _okNum;
        [Description("OK数量"), Category("SmoreControl")]
        public  int OKNum
        {
            get { return _okNum; }
            set { _okNum = value; labelOKNum.Text = value.ToString();}
        }
        private int _ngNum;
        [Description("NG数量"), Category("SmoreControl")]
        public int NGNum
        {
            get { return _ngNum; }
            set { _ngNum = value; labelNGNum.Text = value.ToString(); }
        }
        private double _rateNum;
        [Description("良率"), Category("SmoreControl")]
        public double RateNum
        {
            get { return _rateNum; }
            set { _rateNum = value; labelRateNum.Text = value.ToString(); }
        }

        /// <summary>
        /// 按钮点击事件推理整个文件夹中的图片
        /// </summary>
        [Description("推理文件夹中图按钮点击事件"), Category("SmoreControl")]
        public event EventHandler ClearData;

        private const int ERROR_OK = 0;
        private const int ERROR_FAILED = -1;

        private System.Windows.Forms.Timer tag_ShowDataTimer;
        private static object tag_locker;
        private static int is_showData = 10;
        private static string LastError = "";

        private static string configPath = $@"{AppDomain.CurrentDomain.BaseDirectory}" + "Config\\"+ "ProduceData.xml";
        private static ProduceDataParse m_ProduceDataParse = new ProduceDataParse();

        public SMDataWindow()
        {
            InitializeComponent();
            IniChart(this.chart1);
        }

        private void SMDataWindow_Load(object sender, EventArgs e)
        {
            int returnVale = GetProduceData();
            if(returnVale!=ERROR_OK)
            {
                //MessageBox.Show($"{LastError}", "提示!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return;
            }

            tag_locker = new object();
            tag_ShowDataTimer = new System.Windows.Forms.Timer();
            tag_ShowDataTimer.Tick += new EventHandler(UserControl_DataAdd_Show);
            tag_ShowDataTimer.Interval = 10;
            tag_ShowDataTimer.Start();
        }

        private void smButtonClearData_BtnClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否清除数据?", "提示!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {
                return;
            }
            else
            {
                is_showData = 10;
                labelTotalNum.Text = "0";
                labelOKNum.Text = "0";
                labelNGNum.Text = "0";
                labelRateNum.Text = "0.00%";
                ClearProduceData();

                if (this.ClearData != null)
                    ClearData(this, e);
            }
        }

        public void IniChart(Chart cht2)
        {
            try
            {
                string[] xData = new string[] { "OK", "NG" };
                double[] yData = new double[] { 100, 100 };
                #region 饼图

                ////标题
                //cht2.Titles.Add("饼图数据分析");
                //cht2.Titles[0].ForeColor = Color.Green;
                //cht2.Titles[0].Font = new Font("微软雅黑", 14f, FontStyle.Regular);
                //cht2.Titles[0].Alignment = ContentAlignment.TopCenter;

                //控件背景
                cht2.BackColor = Color.Transparent;
                //图表区背景
                cht2.ChartAreas[0].BackColor = Color.Transparent;
                cht2.ChartAreas[0].BorderColor = Color.Transparent;
                //X轴标签间距
                cht2.ChartAreas[0].AxisX.Interval = 10;
                cht2.ChartAreas[0].AxisX.LabelStyle.IsStaggered = true;
                cht2.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
                cht2.ChartAreas[0].AxisX.TitleFont = new Font("微软雅黑", 14f, FontStyle.Regular);
                cht2.ChartAreas[0].AxisX.TitleForeColor = Color.Green;

                //X坐标轴颜色
                cht2.ChartAreas[0].AxisX.LineColor = ColorTranslator.FromHtml("#38587a"); ;
                cht2.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Green;
                cht2.ChartAreas[0].AxisX.LabelStyle.Font = new Font("微软雅黑", 10f, FontStyle.Regular);

                //X轴网络线条
                cht2.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
                cht2.ChartAreas[0].AxisX.MajorGrid.LineColor = ColorTranslator.FromHtml("#2c4c6d");

                //Y坐标轴颜色
                cht2.ChartAreas[0].AxisY.LineColor = ColorTranslator.FromHtml("#38587a");
                cht2.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Green;
                cht2.ChartAreas[0].AxisY.LabelStyle.Font = new Font("微软雅黑", 10f, FontStyle.Regular);

                //Y轴网格线条
                cht2.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
                cht2.ChartAreas[0].AxisY.MajorGrid.LineColor = ColorTranslator.FromHtml("#2c4c6d");

                cht2.ChartAreas[0].AxisY2.LineColor = Color.Transparent;

                //背景渐变
                cht2.ChartAreas[0].BackGradientStyle = GradientStyle.None;

                //图例样式
                Legend legend2 = new Legend("#VALX");
                legend2.Title = "图例";
                legend2.TitleBackColor = Color.Transparent;
                legend2.BackColor = Color.Transparent;
                legend2.TitleForeColor = Color.Green;
                legend2.TitleFont = new Font("微软雅黑", 10f, FontStyle.Regular);
                legend2.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
                legend2.ForeColor = Color.Red;

                cht2.Series[0].XValueType = ChartValueType.String;  //设置X轴上的值类型
                cht2.Series[0].Label = "#VAL";                //设置显示X Y的值    
                cht2.Series[0].LabelForeColor = Color.Black;
                cht2.Series[0].ToolTip = "#VALX:#VAL";     //鼠标移动到对应点显示数值
                cht2.Series[0].ChartType = SeriesChartType.Pie;    //图类型(折线)

                cht2.Series[0].Color = Color.Lime;
                cht2.Series[0].LegendText = legend2.Name;
                cht2.Series[0].IsValueShownAsLabel = true;
                cht2.Series[0].LabelForeColor = Color.Black;//饼图数字颜色
                cht2.Series[0].CustomProperties = "DrawingStyle = Cylinder";
                cht2.Series[0].CustomProperties = "PieLabelStyle = Outside";
                cht2.Legends.Add(legend2);
                cht2.Legends[0].Position.Auto = true;
                cht2.Series[0].IsValueShownAsLabel = true;
                //是否显示图例
                cht2.Series[0].IsVisibleInLegend = true;
                cht2.Series[0].ShadowOffset = 0;

                //饼图折线
                cht2.Series[0]["PieLineColor"] = "Blue";
                //绑定数据
                cht2.Series[0].Points.DataBindXY(xData, yData);
                cht2.Series[0].Points[0].Color = Color.Green;
                cht2.Series[0].Points[1].Color = Color.Red;
                //绑定颜色
                cht2.Series[0].Palette = ChartColorPalette.BrightPastel;

                //设置环为空心
                cht2.Series[0].ChartType = SeriesChartType.Doughnut;

                //设位置空心的大小
                cht2.Series[0].CustomProperties = "DoughnutRadius = 35";

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.ToString()}", "提示!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        public void SetTitle(string title)
        {
            this.chart1.Titles.Clear();
            this.chart1.Titles.Add(title);
            this.chart1.Titles[0].ForeColor = Color.Green;
            this.chart1.Titles[0].Font = new Font("微软雅黑", 14f, FontStyle.Regular);
            this.chart1.Titles[0].Alignment = ContentAlignment.TopCenter;
        }

        private void UserControl_DataAdd_Show(object sender,EventArgs e)
        {
            try
            {
                if (is_showData>0 && m_ProduceDataParse != null)
                {
                    is_showData--;
                    string[] X = { "OK", "NG" };
                    List<UInt64> vs = new List<UInt64>();
                    vs.Add(m_ProduceDataParse.ProduceData.OK);
                    vs.Add(m_ProduceDataParse.ProduceData.NG);
                    this.chart1.Series[0].Points.DataBindXY(X, vs);
                    chart1.Series[0].Points[0].Color = Color.Green;
                    chart1.Series[0].Points[1].Color = Color.Red;
                    labelTotalNum.Text = m_ProduceDataParse.ProduceData.Total.ToString();
                    labelOKNum.Text = m_ProduceDataParse.ProduceData.OK.ToString();
                    labelNGNum.Text = m_ProduceDataParse.ProduceData.NG.ToString();
                    labelRateNum.Text = m_ProduceDataParse.ProduceData.Total == 0 ?
                    "0" : ((float)m_ProduceDataParse.ProduceData.OK / (float)m_ProduceDataParse.ProduceData.Total).ToString("P");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.ToString()}", "提示!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        public int ClearProduceData()
        {
            m_ProduceDataParse.ProduceData.Total = 0;
            m_ProduceDataParse.ProduceData.OK = 0;
            m_ProduceDataParse.ProduceData.NG = 0;
            return XMLSerialize.WriteToXML(configPath, m_ProduceDataParse, ref LastError);
        }

        public int GetProduceData()
        {
            return XMLSerialize.DeserializeFromXml<ProduceDataParse>(configPath, ref m_ProduceDataParse, ref LastError);
        }

        public static void AddData(bool DataResult)
        {
            lock (tag_locker)
            {
                try
                {
                    ++m_ProduceDataParse.ProduceData.Total;

                    if (DataResult)
                    {
                        ++m_ProduceDataParse.ProduceData.OK;
                    }
                    else
                    {
                        ++m_ProduceDataParse.ProduceData.NG;
                    }
                    XMLSerialize.SerializeToXml<ProduceDataParse>(configPath, m_ProduceDataParse, ref LastError);
                    is_showData = 10;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.ToString()}", "提示!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
            }
        }
    }
}
