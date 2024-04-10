using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SmoreControlLibrary.SystemConfig;
using static SmoreControlLibrary.SystemConfig.JsonFileParse;

namespace SmoreControlLibrary.SMForm
{
    public partial class SMProductInfoSet : UserControl
    {
        private List<SMProductInfo> sMProductInfosList = new List<SMProductInfo>();
        private List<bool> checkedStatus = new List<bool>();
        private List<FileInfo> jsonFileName = new List<FileInfo>();

        public static string JsonRootDir = $@"{AppDomain.CurrentDomain.BaseDirectory}" + "Formula\\";

        private JsonFileParse jsonFileParse = null;
        private ParametricRecord parametricRecord = null;
        private SMProductInfo sMProductInfo = null;

        public SMProductInfoSet()
        {
            InitializeComponent();
        }

        private void SMProductInfoSet_Load(object sender, EventArgs e)
        {
            jsonFileParse = new JsonFileParse();
            parametricRecord = new ParametricRecord();

            smProductInfoSetTitle2.lbl1.Text = "面积最小值";
            smProductInfoSetTitle2.lbl2.Text = "面积最大值";
            //smProductInfoSetTitle2.lbl3.Text = "曝光时间";
            InitProductInfo();
        }

        public void InitProductInfo()
        {
            try
            {
                jsonFileName = getFile(JsonRootDir, ".json");
                foreach (var item in jsonFileName)
                {
                    string jsonFilePath = JsonRootDir + item;
                    jsonFileParse.ReadJsonFile(jsonFilePath, ref parametricRecord);
                    sMProductInfo = new SMProductInfo();
                    sMProductInfo.Dock = DockStyle.Top;
                    sMProductInfo.Width = plProductSetHome.Width;
                    sMProductInfo.Checked = smProductInfoSetTitle2.Checked;
                    sMProductInfo.ID = parametricRecord.ID;
                    sMProductInfo.CellContent1 =parametricRecord.Items.Value1;
                    sMProductInfo.CellContent2 =parametricRecord.Items.Value2;
                    sMProductInfo.CellContent3 = parametricRecord.Items.Value3;
                    plProductSetHome.Controls.Add(sMProductInfo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.ToString()}", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void smBtnAdd_BtnClick(object sender, EventArgs e)
        {
            if (plProductSetHome.Controls.Count >= 20)
            {
                MessageBox.Show("超出配方限制!", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            SMProductInfo sMProductInfo = new SMProductInfo();
            sMProductInfo.Dock = DockStyle.Top ;
            sMProductInfo.Width = plProductSetHome.Width;
            sMProductInfo.Checked = smProductInfoSetTitle2.Checked;
            plProductSetHome.Controls.Add(sMProductInfo);
        }

        private void smBtnEdit_BtnClick(object sender, EventArgs e)
        {
            MessageBox.Show("请选择需要编辑的内容!", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void smBtnSave_BtnClick(object sender, EventArgs e)
        {
            checkedStatus.Clear();
            foreach (SMProductInfo item in plProductSetHome.Controls)
            {
                checkedStatus.Add(item.Checked);
            }

            if (!checkedStatus.Contains(true))
            {
                MessageBox.Show("请选择要保存的内容!", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if (!File.Exists(JsonRootDir)) Directory.CreateDirectory(JsonRootDir);

                foreach (SMProductInfo item in plProductSetHome.Controls)
                {
                    if (item.Checked)
                    {
                        item.parametricRecord.ID = item.ID;
                        item.parametricRecord.Items = new ParametricItem();

                        string error = "";

                        if (float.Parse(item.CellContent1) >= float.Parse(item.CellContent2))
                        {
                            item.CellColor1 = Color.Red;
                            item.CellColor2 = Color.Red;
                            error = item.ID + "上限（最大值）必须大于下限（最小值）";
                            MessageBox.Show(error, "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            continue;
                        }


                        if (float.Parse(item.CellContent1) > 0.0)
                        {
                            item.parametricRecord.Items.Value1 = item.CellContent1;
                        }
                        else
                        {
                            item.parametricRecord.Items.Value1 = "1.0";
                            MessageBox.Show($"内圈缩放比例{float.Parse(item.CellContent1)},为负数,重置为1.0");
                        }

                        if (float.Parse(item.CellContent2) > 0.0)
                        {
                            item.parametricRecord.Items.Value2 = item.CellContent2;
                        }
                        else
                        {
                            item.parametricRecord.Items.Value2 = "1.0";
                            MessageBox.Show($"内圈缩放比例{float.Parse(item.CellContent2)},为负数,重置为1.0");
                        }


                        


                        if (float.Parse(item.CellContent3) > 0.0)
                        {
                            item.parametricRecord.Items.Value3 = item.CellContent3;
                        }
                        else
                        {
                            item.parametricRecord.Items.Value3 = "1000";
                            MessageBox.Show($"曝光时间:{float.Parse(item.CellContent3)},为负数,重置为1000");
                        }
                        //if (float.Parse(item.CellContent2) > 0.0) item.parametricRecord.Items.Value2 = item.CellContent2;
                        //item.parametricRecord.Items.Value3 = item.CellContent3;
                        jsonFileParse.WriteJsonFile(item.parametricRecord, JsonRootDir+ $"{item.ID}.json");
                    }
                }
            }

            MessageBox.Show("保存成功!", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void smBtnDelete_BtnClick(object sender, EventArgs e)
        {
            sMProductInfosList.Clear();
            checkedStatus.Clear();
            foreach (SMProductInfo item in plProductSetHome.Controls)
            {
                sMProductInfosList.Add((SMProductInfo)item);
                checkedStatus.Add(item.Checked);
            }

            if (!checkedStatus.Contains(true))
            {
                MessageBox.Show("请选择要删除的内容!", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (var item in sMProductInfosList)
            {
                if (item.Checked)
                {
                    Delete(JsonRootDir + item.ID + ".json");
                    plProductSetHome.Controls.Remove(item);
                }
            }
        }

        private void smProductInfoSetTitle2_CheckedChangedEvent(object sender, EventArgs e)
        {
            sMProductInfosList.Clear();
            foreach (SMProductInfo item in plProductSetHome.Controls)
            {
                if (item is SMProductInfo)
                {
                    sMProductInfosList.Add(item);
                }
            }

            foreach (var item in sMProductInfosList)
            {
                item.Checked = smProductInfoSetTitle2.Checked;
            }
        }

        public static List<FileInfo> getFile(string path, string extName)
        {
            try
            {
                List<FileInfo> lst = new List<FileInfo>();
                string[] dir = Directory.GetDirectories(path); 
                DirectoryInfo fdir = new DirectoryInfo(path);
                FileInfo[] file = fdir.GetFiles(); 
                if (file.Length != 0 || dir.Length != 0)                 
                {
                    foreach (FileInfo f in file)  
                    {
                        if (extName.ToLower().IndexOf(f.Extension.ToLower()) >= 0)
                        {
                            lst.Add(f);
                        }
                    }
                    foreach (string d in dir)
                    {
                        getFile(d, extName);
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.ToString()}!", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
        }

        public void Delete(string fileFullPath)
        {
            try
            {
                if (File.Exists(fileFullPath))
                {
                    FileAttributes attr = File.GetAttributes(fileFullPath);
                    if (attr == FileAttributes.Directory)
                    {
                        Directory.Delete(fileFullPath, true);
                    }
                    else
                    {
                        File.Delete(fileFullPath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.ToString()}!", "提示!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
