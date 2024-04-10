
using SMLogControlLibrary;
using SmoreControlLibrary.SMData;
using SmoreControlLibrary.SMForm;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SmoreControlLibrary.ProductStatistics
{
	public partial class FormProductNumber : UserControl
	{
		private const int ERROR_OK = 0;
		private const int ERROR_FAILED = -1;
		public bool isopen = false;

		//private static string[] x = new string[24];
		//private static double[] y1 = new double[24];
		//private static double[] y2 = new double[24];
		//private static double[] Y1 = new double[24];
		//private static double[] Y2 = new double[24];

		public static bool bRun = true;

        int icountData = 24;
        string[] x;
        double[] y1;
        double[] y2;
        double[] Y1;
        
        string str;
        bool bRefreshHour = false;

        string strPreviousHour = "";

        public FormProductNumber()
		{
			InitializeComponent();
            x = new string[icountData];
            y1 = new double[icountData];
            y2 = new double[icountData];
            Y1 = new double[icountData];

        }

		~FormProductNumber()
        {
			//this.Close();
        }

		private void FormProductNumber_Load(object sender, EventArgs e)
		{
			isopen = true;
			dateTimeStart.Value = DateTime.Now.Date;
			dateTimeEnd.Value = DateTime.Now.Date.AddDays(1);
            smButton1.Enabled = false;

            InitialChart();

            chart1.Series[0].Color = Color.Orange;
            chart1.Series[1].Color = Color.Green;
            chart1.Series[2].Color = Color.Red;
        }
        /// <summary>
        /// 24小时刷新
        /// </summary>
		public void RefreshNumber()
		{
			if (radioButtonHour.Checked)
			{
				//smButton1_BtnClick(sender,e);
				SMLogWindow.OutLog("RefreshNumber:start", Color.Green);
                RefreshHour();
				SMLogWindow.OutLog("RefreshNumber:end", Color.Green);
			}
		}

		public void Process(bool brunstate)
        {
			gBselect.Enabled = brunstate;
			smButton1.Enabled = brunstate;
            radioButtonHour.Checked = !brunstate;

        }

		/// <summary>
		/// 软件启动从数据库中获取前24小时产量
		/// </summary>
		public void initData()
		{
			//前24小时
			for (int i = 0; i < icountData; i++)
			{

				UpdateData(i, DateTime.Now.AddHours(i - icountData + 1), DateTime.Now.AddHours(i - icountData + 2));
			}

			strPreviousHour = DateTime.Now.ToString("HH");

			//GlobalVariables.Variable.uCurHourTotal = (UInt64)y1[23];
			//GlobalVariables.Variable.uCurHourOK = (UInt64)y2[23];

			chart1.Series[0].Points.DataBindXY(x, y1);
			chart1.Series[1].Points.DataBindXY(x, y2);
			chart2.Series[0].Points.DataBindXY(x, Y1);
		}

		private void InitialChart()
		{
			// 图表区背景
			chart1.ChartAreas[0].BackColor = Color.White;
			// X轴标签间距
			chart1.ChartAreas[0].AxisX.Interval = 1;
			chart1.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;
			// chart1.ChartAreas[0].AxisX.LabelStyle.Angle = -30;
			chart1.ChartAreas[0].AxisX.TitleFont = new Font("微软雅黑", 14f, FontStyle.Regular);
			chart1.ChartAreas[0].AxisX.TitleForeColor = Color.Black;

			// X坐标轴颜色
			//chartView.ChartAreas[0].AxisX.LineColor = ColorTranslator.FromHtml("#808080");
			chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Black;
			chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
			//// X坐标轴标题
			//chart1.ChartAreas[0].AxisX.Title = "月份";
			//chart1.ChartAreas[0].AxisX.TitleFont = new Font("微软雅黑", 10f, FontStyle.Regular);
			//chart1.ChartAreas[0].AxisX.TitleForeColor = Color.Black;
			//chart1.ChartAreas[0].AxisX.TextOrientation = TextOrientation.Horizontal;
			// X轴网络线条
			chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
			//chartView.ChartAreas[0].AxisX.MajorGrid.LineColor = ColorTranslator.FromHtml("#E6E6FA");
			// Y坐标轴颜色
			//chartView.ChartAreas[0].AxisY.LineColor = ColorTranslator.FromHtml("#808080");
			chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Black;
			chart1.ChartAreas[0].AxisY.LabelStyle.Font = new Font("微软雅黑", 10f, FontStyle.Regular);

			//// Y坐标轴标题
			//chart1.ChartAreas[0].AxisY.Title = "数量(台)";
			//chart1.ChartAreas[0].AxisY.TitleFont = new Font("微软雅黑", 10f, FontStyle.Regular);
			//chart1.ChartAreas[0].AxisY.TitleForeColor = Color.Black;
			//chart1.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
			//chart1.ChartAreas[0].AxisY.ToolTip = "数量(台)";

			//chart1.ChartAreas[0].AxisY.Maximum = 8000;
			//chart1.ChartAreas[0].AxisY.Interval = 2000;

			// Y轴网格线条
			chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
			//chartView.ChartAreas[0].AxisY.MajorGrid.LineColor = ColorTranslator.FromHtml("#E6E6FA");
			//chartView.ChartAreas[0].AxisY2.LineColor = Color.Transparent;
			// 背景渐变
			chart1.ChartAreas[0].BackGradientStyle = GradientStyle.None;
			// 图例样式
			//Legend legend = new Legend("Target");
			//legend.BackColor = Color.Transparent;
			//legend.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
			//legend.ForeColor = Color.Black;


			chart2.ChartAreas[0].BackColor = Color.White;

			chart2.ChartAreas[0].AxisX.Interval = 1;
			chart2.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;

			chart2.ChartAreas[0].AxisX.TitleFont = new Font("微软雅黑", 14f, FontStyle.Regular);
			chart2.ChartAreas[0].AxisX.TitleForeColor = Color.Black;

			chart2.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Black;
			chart2.ChartAreas[0].AxisX.LabelStyle.Font = new Font("微软雅黑", 8f, FontStyle.Regular);

			chart2.ChartAreas[0].AxisX.MajorGrid.Enabled = false;

			chart2.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Black;
			chart2.ChartAreas[0].AxisY.LabelStyle.Font = new Font("微软雅黑", 10f, FontStyle.Regular);

			chart2.ChartAreas[0].AxisY.Maximum = 1;
			//  chart2.ChartAreas[0].AxisY.Interval = 400;
			chart2.ChartAreas[0].AxisY.LabelStyle.Format = "0%";

			// Y轴网格线条
			chart2.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

		}

		private void smButton1_BtnClick(object sender, EventArgs e)
		{
			if(bRun)
            {
                chart1.Titles[0].Text = "UPH";
				if (radionButtonDay.Checked)
				{
					RefreshDay();
				}
				else if(radioButtonHour.Checked)
                {
					RefreshHour();

				}
			}

		}

        /// <summary>
        /// 24小时刷新
        /// </summary>
        private void RefreshHour()
        {
            SMLogWindow.OutLog($"Hour:{radioButtonHour.Checked}:start:strPreviousHour:{strPreviousHour}", Color.Green);
            if (radioButtonHour.Checked)
            {
                SMLogWindow.OutLog($"radioButtonHour:start", Color.Green);

                if (DateTime.Now.ToString("HH") != strPreviousHour)
                {
                    strPreviousHour = DateTime.Now.ToString("HH");
                    bRefreshHour = true;
                }

                if (bRefreshHour)
                {
                    bRefreshHour = false;


					//前24小时--前1小时
					//for (int i = 0; i < icountData - 1; i++)
					//{

					//    UpdateData(i, DateTime.Now.AddHours(i - icountData + 1), DateTime.Now.AddHours(i - icountData + 2));
					//}

					//SmoreControlLibrary.GlobalVariables.Variable.uCurHourTotal = 1;
					//SmoreControlLibrary.GlobalVariables.Variable.uCurHourOK = 1;
					OffsetData();

				}



				//最近一小时
				//UpdateData(23, DateTime.Now.AddHours(0), DateTime.Now.AddHours(1));

				//x[23] = DateTime.Now.ToString("HH");
				//y1[23] = SmoreControlLibrary.GlobalVariables.Variable.uCurHourTotal;//总数
				//y2[23] = SmoreControlLibrary.GlobalVariables.Variable.uCurHourOK;//OK数
				//Y1[23] = GlobalVariables.Variable.uCurHourTotal == 0 ? 0 : (double)GlobalVariables.Variable.uCurHourOK / GlobalVariables.Variable.uCurHourTotal;

				chart1.Series[0].Points.DataBindXY(x, y1);
                chart1.Series[1].Points.DataBindXY(x, y2);
                chart2.Series[0].Points.DataBindXY(x, Y1);

                SMLogWindow.OutLog($"radioButtonHour:end:strPreviousHour:{strPreviousHour}", Color.Green);

            }
        }

        /// <summary>
        /// 显示选择那天的明细
        /// </summary>
        private void ShowDayDetail(string strDate,int iStart, int iEnd)
        {
            int itotal = iEnd - iStart;
            string[] xDetail = new string[itotal];
            double[] y1Detail = new double[itotal];
            double[] y2Detail = new double[itotal];
            double[] Y1Detail = new double[itotal];


            //前24小时
            for (int index = 0; index < itotal; index++)
            {

                int iRealityIndex = index + iStart;
               

                str = String.Format("SELECT `NGName` FROM `tHistory` WHERE TimeEx between #{0}# and #{1}# order by ID desc", strDate+" "+ iRealityIndex.ToString("00")+":00:00",
                                   strDate + " " + (iRealityIndex + 1).ToString("00") + ":00:00");
                SMLogWindow.OutLog($"query:start:{str}", Color.Green);

               // string[] SummaryDetail = DatabasePara.database.CountTotalNum(str);
                //SMLogWindow.OutLog("query:end", Color.Green);
                //xDetail[index] = iRealityIndex.ToString("00");
                //y1Detail[index] = double.Parse(SummaryDetail[2]);
                //y2Detail[index] = double.Parse(SummaryDetail[0]);
                //Y1Detail[index] = double.Parse(SummaryDetail[2]) == 0 ? 0 : double.Parse(SummaryDetail[0]) / double.Parse(SummaryDetail[2]);
            }

            chart1.Series[0].Points.DataBindXY(xDetail, y1Detail);
            chart1.Series[1].Points.DataBindXY(xDetail, y2Detail);
            chart2.Series[0].Points.DataBindXY(xDetail, Y1Detail);
        }

        /// <summary>
        /// 日期刷新
        /// </summary>
        private void RefreshDay()
        {
			DateTime timeStart = dateTimeStart.Value;
			DateTime timeEnd = dateTimeEnd.Value;

            if (DateTime.Compare(timeStart, timeEnd) > 0)
            {
				SMLogWindow.OutLog($"timeStart:{timeStart}:timeEnd:{timeEnd}", Color.Green);
                MessageBox.Show("起始时间比终止时间晚，请重新选择");
                return;
            }

			SMLogWindow.OutLog($"timeStart:{timeStart}:timeEnd:{timeEnd}", Color.Green);
			TimeSpan timespan = timeStart.Subtract(timeEnd).Duration();

            //if (timespan.Hours == 0 && timespan.Days == 0)
            //{
            //    timeEnd = timeEnd.AddHours(1);
            //}
            int iDays = timespan.Days > 0 ? timespan.Days+1 : 1;

            SMLogWindow.OutLog($"day:{radionButtonDay.Checked}:start", Color.Green);
			
			if (radionButtonDay.Checked)
			{
				//chart1.Series[0].Points.Clear();
				//chart2.Series[0].Points.Clear();

                string[] xday = new string[iDays];
                double[] y1day = new double[iDays];
                double[] y2day = new double[iDays];
                double[] Y1day = new double[iDays];

                double dTotalCount = 0;
                double dOKCount = 0;

				for (int i = 0; i < iDays; i++)
				{
					DateTime ChartStart, ChartEnd;

					if (i == 0)
					{
						ChartStart = timeStart;
					}
					else
					{
						ChartStart = timeStart.AddDays(i);
					}
                    if(iDays==1)
                    {
                        ChartEnd = timeEnd;
                    }
                    else
                    {
                        if(i==(iDays-1))
                        {
                            ChartEnd = timeEnd;
                        }
                        else
                        {
                            ChartEnd = timeStart.Date.AddDays(i + 1);
                        }
                        
                    }
					

					str = String.Format("SELECT `NGName` FROM `tHistory` WHERE TimeEx between #{0}# and #{1}# order by ID desc", ChartStart.ToString(), ChartEnd.ToString());

                    SMLogWindow.OutLog($"RefreshDay:sql:{str}",Color.Green);

                    //string[] Summary = DatabasePara.database.CountTotalNum(str);

					//xday[i] = timeStart.Date.AddDays(i).ToString("yyyy/MM/dd");
                    //y1day[i] = double.Parse(Summary[2]);
                    //dTotalCount+= double.Parse(Summary[2]);
                    //y2day[i] = double.Parse(Summary[0]);
                    //dOKCount += double.Parse(Summary[0]);
                    //Y1day[i] = double.Parse(Summary[2]) == 0 ? 0 : double.Parse(Summary[0]) / double.Parse(Summary[2]);
				}

               


                chart1.Series[0].Points.DataBindXY(xday, y1day);
				chart1.Series[1].Points.DataBindXY(xday, y2day);
				chart2.Series[0].Points.DataBindXY(xday, Y1day);

                chart1.Series[0].Name = $"Total:{dTotalCount}";
                chart1.Series[1].Name = $"OK:{dOKCount}";
                chart1.Series[2].Name = $"NG:{dTotalCount-dOKCount}";
                //MessageBox.Show("查询成功", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

			SMLogWindow.OutLog($"Hour:{radioButtonHour.Checked}:day:{radionButtonDay.Checked}:end", Color.Green);
		}

		/// <summary>
		/// 移动数据
		/// </summary>
		private void OffsetData()
		{

			for (int i = 0; i < icountData - 1; i++)
			{
				x[i] = x[i + 1];
				y1[i] = y1[i + 1];
				y2[i] = y2[i + 1];
				Y1[i] = Y1[i + 1];
			}
		}

		private void UpdateData(int index, DateTime ChartStart, DateTime ChartEnd)
        {
            string[] Summary;
           
            str = String.Format("SELECT `NGName` FROM `tHistory` WHERE TimeEx between #{0}# and #{1}# order by ID desc", ChartStart.ToString("yyyy/MM/dd HH") + ":00:00", ChartEnd.ToString("yyyy/MM/dd HH") + ":00:00");

            SMLogWindow.OutLog("query:start", Color.Green);
            //Summary = DatabasePara.database.CountTotalNum(str);
            //SMLogWindow.OutLog("query:end", Color.Green);
            //x[index] = ChartStart.ToString("HH");
            //y1[index] = double.Parse(Summary[2]);
            //y2[index] = double.Parse(Summary[0]);
            //Y1[index] = double.Parse(Summary[2]) == 0 ? 0 : double.Parse(Summary[0]) / double.Parse(Summary[2]);

           
        }

        private void FormProductNumber_FormClosing(object sender, FormClosingEventArgs e)
        {
			Task.Run(()=> {
				MessageBox.Show("请点击主界面'实时产量'按钮隐藏");
			});
			e.Cancel = true;
        }

        private void radioButtonHour_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButtonHour.Checked) smButton1.Enabled = false;

        }

        private void radionButtonDay_CheckedChanged(object sender, EventArgs e)
        {
            smButton1.Enabled = radionButtonDay.Checked;
        }

        private void chart1_Click(object sender, EventArgs e)
        {

            
        }

        private void chart1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
        }

        private void chart1_MouseClick(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.DataVisualization.Charting.HitTestResult Result = new System.Windows.Forms.DataVisualization.Charting.HitTestResult();
            Result = chart1.HitTest(e.X, e.Y);

            if (Result.PointIndex == -1)
            {
                Trace.WriteLine(Result.PointIndex);
                return;
            }
            if (Result.Series == null) return;
            string value = Result.Series.Points[Result.PointIndex].AxisLabel;
            if (!value.Contains("/")) return;
            //yyyy/MM/dd
            //Trace.WriteLine(value);
            chart1.Titles[0].Text = "UPH " + value;


            string tempHStart = "0";
            string tempHEnd = "24";
            //bool basc = true;
            if(value== dateTimeStart.Value.ToString("yyyy/MM/dd"))
            {
                tempHStart = dateTimeStart.Value.ToString("HH");
            }
            if(value == dateTimeEnd.Value.ToString("yyyy/MM/dd"))
            {
                tempHEnd = dateTimeEnd.Value.ToString("HH");
             
            }

            if(value == dateTimeStart.Value.ToString("yyyy/MM/dd")&& value == dateTimeEnd.Value.ToString("yyyy/MM/dd"))
            {
                tempHStart = dateTimeStart.Value.ToString("HH");
                tempHEnd = dateTimeEnd.Value.ToString("HH");
            }
         
            ShowDayDetail(value, int.Parse(tempHStart), int.Parse(tempHEnd));

            
        }

        private void radioButtonHour_CheckedChanged_1(object sender, EventArgs e)
        {
            chart1.Titles[0].Text = "UPH";
        }

        private void radionButtonDay_CheckedChanged_1(object sender, EventArgs e)
        {
            chart1.Titles[0].Text = "UPH";
        }

        /// <summary>
        /// 解决窗体加载慢，卡顿的问题
        /// </summary>
        //protected override CreateParams CreateParams
        //{
        //	get
        //	{
        //		CreateParams cp = base.CreateParams;
        //		cp.ExStyle |= 0x02000000; // 用双缓冲绘制窗口的所有子控件
        //		return cp;
        //	}
        //}
    }
}
