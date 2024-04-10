using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DateSVN.Controls
{
    /// <summary>
    /// IBK Label
    /// </summary>
    public class MyLabel : Label
    {
        /// <summary>
        /// Label Key
        /// </summary>
        public string LabelKey
        {
            get;
            set;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MyLabel()
        {
            AutoSize = true;
            Margin = new Padding(0);
            Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            ForeColor = System.Drawing.Color.White;

            //if (!this.DesignMode && !string.IsNullOrEmpty(LabelKey))
            //{
            //    this.Text = LabelUtil.GetTextByKey(LabelKey);
            //}
        }
    }
}
