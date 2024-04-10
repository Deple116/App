using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace SmoreControlLibrary.SMForm
{
    public partial class SMDataGridViewShow : UserControl
    {
        private int m_pageSize = 0;
        private int m_nMax = 0;
        private int m_pageCount = 0;
        private int m_pageCurrent = 0;

        private DataTable m_dtInfo = null;
        private AccessOperate accessOperate = null;

        private string m_strSQL_Read = "Select * from tHistory order by ID desc";

        public SMDataGridViewShow()
        {
            InitializeComponent();
        }

        private void InitData()
        {
            m_pageSize = 50;
            m_nMax = 0;
            m_pageCount = 0;
            m_pageCurrent = 0;
            m_dtInfo = new DataTable();
            if (ShowBySql(m_strSQL_Read, ref m_dtInfo, ref m_pageCount, m_pageSize))
                LoadData(0, m_dtInfo);
        }

        public void InitData(DataTable dtInfo)
        {
            m_pageSize = 50;
            m_nMax = 0;
            m_pageCount = 0;
            m_pageCurrent = 0;
            m_dtInfo = dtInfo;
            if (ShowByDataTable(ref m_dtInfo, ref m_pageCount, m_pageSize))
                LoadData(0, m_dtInfo);
        }

        private void LoadData(int nCurrent, DataTable dtInfo)
        {
            int nStartPos = 0;
            int nEndPos = 0;
            DataTable dtTemp = dtInfo.Clone();

            m_pageCurrent = (nCurrent / m_pageSize) + 1;

            if (m_pageCurrent == m_pageCount)
            {
                nEndPos = m_nMax;
            }
            else
            {
                nEndPos = m_pageSize + nCurrent;
            }

            nStartPos = nCurrent;

            toolStripTextBox_CurrentPage.Text = m_pageCurrent.ToString();
            toolStripLabel_TotalPage.Text = m_pageCount.ToString();

            for (int i = nStartPos; i < nEndPos; i++)
            {
                dtTemp.ImportRow(dtInfo.Rows[i]);
            }

            bdsInfo.DataSource = dtTemp;
            bdnInfo.BindingSource = bdsInfo;
            dgvInfo.DataSource = bdsInfo;

            toolStripButton_PrePage.Enabled = (m_pageCurrent > 1);
            toolStripButton_FirstPage.Enabled = (m_pageCurrent > 1);

            toolStripButton_NextPage.Enabled = (m_pageCurrent < m_pageCount);
            toolStripButton_LastPage.Enabled = (m_pageCurrent < m_pageCount);

            tBtnRefreshDB.Enabled = true;
        }

        private void bdnInfo_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == toolStripButton_FirstPage)
            {
                LoadData(0, m_dtInfo);
            }
            else if (e.ClickedItem == toolStripButton_PrePage)
            {
                LoadData(m_pageSize * (m_pageCurrent - 2), m_dtInfo);
            }
            else if (e.ClickedItem == toolStripButton_NextPage)
            {
                LoadData(m_pageSize * (m_pageCurrent), m_dtInfo);
            }
            else if (e.ClickedItem == toolStripButton_LastPage)
            {
                LoadData(m_pageSize * (m_pageCount - 1), m_dtInfo);
            }
        }

        public bool ShowByDataTable(ref DataTable dtInfo, ref int pageCount, int pageSize)
        {
            try
            {
                m_nMax = dtInfo.Rows.Count;
                pageCount = m_nMax / pageSize;
                if ((m_nMax % pageSize) > 0)
                {
                    pageCount++;
                }
            }
            catch
            {
                return false;
            }

            return pageCount > 0;
        }

        public bool ShowBySql(string strSQL, ref DataTable dtInfo, ref int pageCount, int pageSize)
        {
            try
            {
                dtInfo = accessOperate.ReadData(strSQL);
                if (dtInfo == null)
                {
                    return false;
                }

                dtInfo.Columns.RemoveAt(0);

                m_nMax = dtInfo.Rows.Count;
                pageCount = m_nMax / pageSize;
                if ((m_nMax % pageSize) > 0)
                {
                    pageCount++;
                }
            }
            catch
            {
                return false;
            }

            return pageCount > 0;
        }

        private void tBtnRefreshDB_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection con = new OleDbConnection($"Provider=Microsoft.Jet.OleDb.4.0;Data Source={DatabasePara.m_strPath}");
                OleDbDataAdapter Adapter = new OleDbDataAdapter($"select * from tHistory", con);
                DataTable table = new DataTable();
                Adapter.Fill(table);
                dgvInfo.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"刷新数据库失败\r\n语句:{ex.ToString()}", "提示!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }
    }
}
