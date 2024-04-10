using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmoreControlLibrary.SMForm
{
    public partial class SMCountSet : UserControl
    {
        private SMDataGridViewShow m_SMDataGridViewShow = null;
        SqlClass m_sqlclass = new SqlClass();
        public SMCountSet()
        {
            InitializeComponent();
        }

        private void SMCountSet_Load(object sender, EventArgs e)
        {
            m_SMDataGridViewShow = new SMDataGridViewShow();
            m_SMDataGridViewShow.Dock = DockStyle.Fill;
            panelChart.Controls.Add(m_SMDataGridViewShow);
            m_SMDataGridViewShow.Show();

            cmTestResults.SelectedIndex = 0;
            m_sqlclass.InitSql();

            m_sqlclass.RegisterCallBack(funcToDelegate);
        }

        private void funcToDelegate(DataTable dtdata)
        {

            //dtdata.Columns.RemoveAt(0);
            if (m_SMDataGridViewShow != null)
            {
                Invoke((MethodInvoker)delegate
                {
                    m_SMDataGridViewShow.InitData(dtdata);
                });
                //}
                //smDataGridViewShow2.dgvInfo.DataSource = data.Tables[0].DefaultView;
            }
        }

        private void smButtonRefresh_BtnClick(object sender, EventArgs e)
        {
            DateTime timeStart = dateTimeStart.Value;
            DateTime timeEnd = dateTimeEnd.Value;



            TimeSpan tsStart = new TimeSpan(timeStart.Ticks);
            TimeSpan tsEnd = new TimeSpan(timeEnd.Ticks);
            TimeSpan tsSum = tsEnd - tsStart;

            if (tsSum.Ticks <= 0)
            {
                MessageBox.Show($"时间设置错误!", "提示!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            double dSum = tsSum.Days;
            string strFormat = "yyyy/MM/dd HH:mm:ss";
            String str = "";
            List<TimePoint> tp = new List<TimePoint>();
            switch (cmTestResults.SelectedIndex)
            {
                case 0:// All
                    str = String.Format("SELECT * FROM deple.product WHERE product_datetime between '{0}' and '{1}' order by ID desc", timeStart.ToString(strFormat), timeEnd.ToString(strFormat));

                   
                    break;
                case 1:// 产能/天
                    timeStart = timeStart.Date;
                    timeEnd = timeEnd.Date.AddDays(1);
                    strFormat = "yyyy-MM-dd";
                    dSum = (timeEnd - timeStart).TotalDays;
                    str = String.Format("SELECT tHistory.ProductionTime FROM tHistory WHERE ProductionTime between #{0}# and #{1}# ", timeStart.ToString(), timeEnd.ToString());
                    break;
                case 2: // 产能/小时
                    timeStart = timeStart.Add(new TimeSpan(0, -timeStart.Minute, -timeStart.Second));
                    timeEnd = timeEnd.Add(new TimeSpan(1, -timeEnd.Minute, -timeEnd.Second));
                    strFormat = "MM-dd HH时";
                    dSum = (timeEnd - timeStart).TotalHours;
                    str = String.Format("SELECT tHistory.ProductionTime FROM tHistory WHERE ProductionTime between #{0}# and #{1}# ", timeStart.ToString(), timeEnd.ToString());
                    break;
                case 3: // 抛料/天
                    timeStart = timeStart.Date;
                    timeEnd = timeEnd.Date.AddDays(1);
                    strFormat = "yyyy-MM-dd";
                    dSum = (timeEnd - timeStart).TotalDays;
                    str = String.Format(@"SELECT tHistory.ProductionTime FROM tHistory WHERE ProductionTime between #{0}# and #{1}# and Pass = false", timeStart.ToString(), timeEnd.ToString());
                    break;
                case 4: // 抛料/小时
                    timeStart = timeStart.Add(new TimeSpan(0, -timeStart.Minute, -timeStart.Second));
                    timeEnd = timeEnd.Add(new TimeSpan(1, -timeEnd.Minute, -timeEnd.Second));
                    strFormat = "MM-dd HH时";
                    dSum = (timeEnd - timeStart).TotalHours;
                    str = String.Format(@"SELECT tHistory.ProductionTime FROM tHistory WHERE ProductionTime between #{0}# and #{1}# and Pass = false", timeStart.ToString(), timeEnd.ToString());
                    break;
            }

            m_sqlclass.ReadAllSqlData(str);

            //DataTable dt = DatabasePara.database.ReadData(str);
            //if (null == dt)
            //{
            //    return;
            //}

            //if (cmTestResults.SelectedIndex != 0)
            //{

            //}
            //else
            //{
            //    dt.Columns.RemoveAt(0);
            //    m_SMDataGridViewShow.InitData(dt);
            //}
        }

        private void smButton1_BtnClick(object sender, EventArgs e)
        {

        }
    }
}
