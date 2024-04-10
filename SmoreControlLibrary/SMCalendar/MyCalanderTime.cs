using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace DateSVN.Controls
{
    [ToolboxItem(true)]
    public partial class MyCalanderTime : UserControl
    {
        #region 变量

        /// <summary>
        /// 选中颜色
        /// </summary>
        private Color _selectedTimeColor = Color.FromArgb(51, 153, 255);

        /// <summary>
        /// 时
        /// </summary>
        private int _hour = 0;

        /// <summary>
        /// 分
        /// </summary>
        private int _minute = 0;

        #endregion 变量

        #region 属性

        /// <summary>
        /// 选中的时间
        /// </summary>
        public DateTime SelectedDateTime
        {
            get
            {
                DateTime calenderDate = DateTime.Now;
                calenderDate =monthCalendar.SelectionStart;

                DateTime date = new DateTime(calenderDate.Year, calenderDate.Month, calenderDate.Day, _hour, _minute, 00);
                return date;
            }
            set
            {
                monthCalendar.SetDate(value);
                _hour = value.Hour;
                _minute = value.Minute;
                txtHour.Text = _hour.ToString().PadLeft(2, '0');
                txtMin.Text = _minute.ToString().PadLeft(2, '0');
            }
        }

        #endregion 属性

        public MyCalanderTime()
        {
            InitializeComponent();
            txtHour.LostFocus += new EventHandler(txtHour_LostFocus);
            txtMin.LostFocus += new EventHandler(txtMin_LostFocus);

        }

        private void txtHour_MouseClick(object sender, MouseEventArgs e)
        {
            panHour.Visible = true;

            foreach (var lblHour in panHour.Controls)
            {
                if (lblHour is MyLabel)
                {
                    MyLabel label = lblHour as MyLabel;

                    if (!string.IsNullOrEmpty(txtHour.Text.Trim()) && int.Parse(txtHour.Text) == int.Parse(label.Text))
                    {
                        label.BackColor = _selectedTimeColor;
                    }
                    else
                    {
                        label.BackColor = Color.Transparent;
                    }
                }
            }

            txtHour.SelectAll();
        }

        private void txtHour_LostFocus(object sender, EventArgs e)
        {
            panHour.Visible = false;
        }

        private void txtMin_MouseClick(object sender, MouseEventArgs e)
        {
            panMin.Visible = true;

            foreach (var lblMin in panMin.Controls)
            {
                if (lblMin is MyLabel)
                {
                    MyLabel label = lblMin as MyLabel;

                    if (!string.IsNullOrEmpty(txtMin.Text.Trim()) && int.Parse(txtMin.Text) == int.Parse(label.Text))
                    {
                        label.BackColor = _selectedTimeColor;
                    }
                    else
                    {
                        label.BackColor = Color.Transparent;
                    }
                }
            }

            txtMin.SelectAll();
        }

        private void txtMin_LostFocus(object sender, EventArgs e)
        {
            panMin.Visible = false;
        }

        /// <summary>
        /// 选中小时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ibkHour_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (var lblHour in panHour.Controls)
            {
                if (lblHour is MyLabel)
                {
                    (lblHour as MyLabel).BackColor = Color.Transparent;
                }
            }

            if (sender is MyLabel)
            {
                MyLabel selLabel = sender as MyLabel;
                selLabel.BackColor = _selectedTimeColor;
                _hour = int.Parse(selLabel.Text);
                txtHour.Text = _hour.ToString().PadLeft(2, '0');
                panHour.Visible = false;
                txtHour.SelectAll();
            }
        }

        /// <summary>
        /// 选中分钟
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ibkMin_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (var lblMin in panMin.Controls)
            {
                if (lblMin is MyLabel)
                {
                    (lblMin as MyLabel).BackColor = Color.Transparent;
                }
            }

            if (sender is MyLabel)
            {
                MyLabel selLabel = sender as MyLabel;
                selLabel.BackColor = _selectedTimeColor;
                _minute = int.Parse(selLabel.Text);
                txtMin.Text = _minute.ToString().PadLeft(2, '0');
                panMin.Visible = false;
                txtMin.SelectAll();
            }
        }

        private void txtHour_TextChanged(object sender, EventArgs e)
        {
            _hour = 0;
            int.TryParse(txtHour.Text, out _hour);

            if (_hour < 0 || _hour > 23)
            {
                _hour = 0;
                txtHour.Text = _hour.ToString().PadLeft(2, '0');
            }
        }

        private void txtMin_TextChanged(object sender, EventArgs e)
        {
            _minute = 0;
            int.TryParse(txtMin.Text, out _minute);

            if (_minute < 0 || _minute > 59)
            {
                _minute = 0;
                txtMin.Text = _minute.ToString().PadLeft(2, '0');
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}
