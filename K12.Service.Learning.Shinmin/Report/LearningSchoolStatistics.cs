using Aspose.Words;
using FISCA.Presentation.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace K12.Service.Learning.Shinmin
{
    public partial class LearningSchoolStatistics : BaseForm
    {
        BackgroundWorker BGW = new BackgroundWorker();

        string AnnouncementSingleConfig = "K12.Service.Learning.Shinmin.LearningSchoolStatistics_0918";
        string _school_year { get; set; }
        string _semester { get; set; }

        SuperBOT bot { get; set; }

        //移動使用
        private Run _run;

        public LearningSchoolStatistics()
        {
            InitializeComponent();

            BGW.RunWorkerCompleted += BGW_RunWorkerCompleted;
            BGW.DoWork += BGW_DoWork;

            intSchoolyear.Value = int.Parse(K12.Data.School.DefaultSchoolYear);
            intSemester.Value = int.Parse(K12.Data.School.DefaultSemester);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (!BGW.IsBusy)
            {
                btnPrint.Enabled = false;
                _school_year = intSchoolyear.Value.ToString();
                _semester = intSemester.Value.ToString();

                BGW.RunWorkerAsync();

            }
            else
            {
                MsgBox.Show("系統忙碌中,請稍後再試!!");
            }
        }

        void BGW_DoWork(object sender, DoWorkEventArgs e)
        {

            //取得選擇 學年期 學校服務學習資料

            List<SLRecord> slrList = tool._A.Select<SLRecord>(string.Format("school_year={0} and semester={1}", _school_year, _semester));

            if (slrList.Count > 0)
            {
                bot = new SuperBOT(slrList);

                #region 範本

                //整理取得報表範本
                Campus.Report.ReportConfiguration ConfigurationInCadre = new Campus.Report.ReportConfiguration(AnnouncementSingleConfig);
                Aspose.Words.Document Template;

                if (ConfigurationInCadre.Template == null)
                {
                    //如果範本為空,則建立一個預設範本
                    Campus.Report.ReportConfiguration ConfigurationInCadre_1 = new Campus.Report.ReportConfiguration(AnnouncementSingleConfig);
                    ConfigurationInCadre_1.Template = new Campus.Report.ReportTemplate(Properties.Resources.全校服務學習時數總表, Campus.Report.TemplateType.Word);
                    Template = ConfigurationInCadre_1.Template.ToDocument();
                }
                else
                {
                    //如果已有範本,則取得樣板
                    Template = ConfigurationInCadre.Template.ToDocument();
                }

                #endregion

                #region 列印報表

                //填資料部份
                DataTable table = new DataTable();

                table.Columns.Add("學年度");
                table.Columns.Add("學期");
                table.Columns.Add("學校名稱");

                table.Columns.Add("校內個人平均時數");
                table.Columns.Add("校內全校服務總人次");
                table.Columns.Add("校內全校服務總時數");
                table.Columns.Add("校外個人平均時數");
                table.Columns.Add("校外全校服務總人次");
                table.Columns.Add("校外全校服務總時數");

                table.Columns.Add("班級資料");
                table.Columns.Add("主辦單位資料");

                DataRow row = table.NewRow();

                row["學年度"] = _school_year;
                row["學期"] = _semester;
                row["學校名稱"] = K12.Data.School.ChineseName;

                row["校內個人平均時數"] = bot.個人平均時數_校內;
                row["校內全校服務總人次"] = bot.全校服務總人次_校內;
                row["校內全校服務總時數"] = bot.全校服務總時數_校內;

                row["校外個人平均時數"] = bot.個人平均時數_校外;
                row["校外全校服務總人次"] = bot.全校服務總人次_校外;
                row["校外全校服務總時數"] = bot.全校服務總時數_校外;

                row["班級資料"] = "";
                row["主辦單位資料"] = "";

                table.Rows.Add(row);

                Document PageOne = (Document)Template.Clone(true);
                PageOne.MailMerge.MergeField += new Aspose.Words.Reporting.MergeFieldEventHandler(MailMerge_MergeField);
                PageOne.MailMerge.Execute(table);
                PageOne.MailMerge.DeleteFields();
                e.Result = PageOne;
                #endregion

            }
            else
                e.Cancel = true;

        }

        void MailMerge_MergeField(object sender, Aspose.Words.Reporting.MergeFieldEventArgs e)
        {
            if (e.FieldName == "班級資料")
            {
                Document PageOne = e.Document; // (Document)_template.Clone(true);
                _run = new Run(PageOne);
                DocumentBuilder builder = new DocumentBuilder(PageOne);
                builder.MoveToMergeField("班級資料");
                ////取得目前Cell
                Cell cell = (Cell)builder.CurrentParagraph.ParentNode;
                ////取得目前Row
                Row row = (Row)builder.CurrentParagraph.ParentNode.ParentNode;

                //建立新行
                for (int x = 1; x < bot.ClassDic.Count; x++)
                {
                    (cell.ParentNode.ParentNode as Table).InsertAfter(row.Clone(true), cell.ParentNode);
                }

                foreach (string each in bot.ClassDic.Keys)
                {
                    Write(cell, bot.ClassDic[each]._class);
                    if (cell.NextSibling != null) //是否最後一格
                        cell = cell.NextSibling as Cell; //下一格

                    Write(cell, bot.ClassDic[each]._班級人數);
                    if (cell.NextSibling != null) //是否最後一格
                        cell = cell.NextSibling as Cell; //下一格

                    Write(cell, bot.ClassDic[each]._teacher);
                    if (cell.NextSibling != null) //是否最後一格
                        cell = cell.NextSibling as Cell; //下一格

                    Write(cell, tool.GetAwayZero(bot.ClassDic[each].班級校內時數).ToString());
                    if (cell.NextSibling != null) //是否最後一格
                        cell = cell.NextSibling as Cell; //下一格

                    Write(cell, tool.GetAwayZero(bot.ClassDic[each].班級校外時數).ToString());
                    if (cell.NextSibling != null) //是否最後一格
                        cell = cell.NextSibling as Cell; //下一格

                    Write(cell, tool.GetAwayZero(bot.ClassDic[each].班級服務總時數).ToString());
                    if (cell.NextSibling != null) //是否最後一格
                        cell = cell.NextSibling as Cell; //下一格

                    Row Nextrow = cell.ParentRow.NextSibling as Row; //取得下一個Row
                    if (Nextrow == null)
                        break;
                    cell = Nextrow.FirstCell; //第一格Cell  
                }
            }
            else if (e.FieldName == "主辦單位資料")
            {
                Document PageOne = e.Document; // (Document)_template.Clone(true);
                _run = new Run(PageOne);
                DocumentBuilder builder = new DocumentBuilder(PageOne);
                builder.MoveToMergeField("主辦單位資料");
                ////取得目前Cell
                Cell cell = (Cell)builder.CurrentParagraph.ParentNode;
                ////取得目前Row
                Row row = (Row)builder.CurrentParagraph.ParentNode.ParentNode;

                //建立新行
                for (int x = 1; x < bot.OrganizersDic.Count; x++)
                {
                    (cell.ParentNode.ParentNode as Table).InsertAfter(row.Clone(true), cell.ParentNode);
                }

                foreach (string each in bot.OrganizersDic.Keys)
                {
                    Write(cell, bot.OrganizersDic[each]._Organizers);
                    if (cell.NextSibling != null) //是否最後一格
                        cell = cell.NextSibling as Cell; //下一格

                    Write(cell, bot.OrganizersDic[each]._InternalExternal);
                    if (cell.NextSibling != null) //是否最後一格
                        cell = cell.NextSibling as Cell; //下一格

                    Write(cell, tool.GetAwayZero(bot.OrganizersDic[each]._AllCount).ToString());
                    if (cell.NextSibling != null) //是否最後一格
                        cell = cell.NextSibling as Cell; //下一格

                    Row Nextrow = cell.ParentRow.NextSibling as Row; //取得下一個Row
                    if (Nextrow == null)
                        break;
                    cell = Nextrow.FirstCell; //第一格Cell  
                }
            }

        }

        void BGW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnPrint.Enabled = true;

            if (!e.Cancelled)
            {
                if (e.Error == null)
                {
                    Document inResult = (Document)e.Result;

                    try
                    {
                        SaveFileDialog SaveFileDialog1 = new SaveFileDialog();

                        SaveFileDialog1.Filter = "Word (*.doc)|*.doc|所有檔案 (*.*)|*.*";
                        SaveFileDialog1.FileName = "校園服務時數統計表";

                        if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            inResult.Save(SaveFileDialog1.FileName);
                            Process.Start(SaveFileDialog1.FileName);
                        }
                        else
                        {
                            FISCA.Presentation.Controls.MsgBox.Show("檔案未儲存");
                            return;
                        }
                    }
                    catch
                    {
                        FISCA.Presentation.Controls.MsgBox.Show("檔案儲存錯誤,請檢查檔案是否開啟中!!");
                        return;
                    }

                    this.Close();

                }
                else
                {
                    MsgBox.Show("列印發生錯誤!!\n" + e.Error.Message);
                }
            }
            else
            {
                MsgBox.Show("當學期無任何時數記錄!!");
            }
        }

        /// <summary>
        /// 寫入資料
        /// </summary>
        private void Write(Cell cell, string text)
        {
            if (cell.FirstParagraph == null)
                cell.Paragraphs.Add(new Paragraph(cell.Document));
            cell.FirstParagraph.Runs.Clear();
            _run.Text = text;
            _run.Font.Size = 12;
            _run.Font.Name = "標楷體";
            cell.FirstParagraph.Runs.Add(_run.Clone(true));
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lbTempAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "另存新檔";
            sfd.FileName = "校園服務時數統計表_功能變數表.doc";
            sfd.Filter = "Word檔案 (*.doc)|*.doc|所有檔案 (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                    fs.Write(Properties.Resources.服務學習總表_功能變數表, 0, Properties.Resources.服務學習總表_功能變數表.Length);
                    fs.Close();
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch
                {
                    FISCA.Presentation.Controls.MsgBox.Show("指定路徑無法存取。", "另存檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //取得設定檔
            Campus.Report.ReportConfiguration ConfigurationInCadre = new Campus.Report.ReportConfiguration(AnnouncementSingleConfig);
            //畫面內容(範本內容,預設樣式
            Campus.Report.TemplateSettingForm TemplateForm;
            if (ConfigurationInCadre.Template != null)
            {
                TemplateForm = new Campus.Report.TemplateSettingForm(ConfigurationInCadre.Template, new Campus.Report.ReportTemplate(Properties.Resources.全校服務學習時數總表, Campus.Report.TemplateType.Word));
            }
            else
            {
                ConfigurationInCadre.Template = new Campus.Report.ReportTemplate(Properties.Resources.全校服務學習時數總表, Campus.Report.TemplateType.Word);
                TemplateForm = new Campus.Report.TemplateSettingForm(ConfigurationInCadre.Template, new Campus.Report.ReportTemplate(Properties.Resources.全校服務學習時數總表, Campus.Report.TemplateType.Word));
            }

            //預設名稱
            TemplateForm.DefaultFileName = "全校服務學習時數總表(範本)";
            //如果回傳為OK
            if (TemplateForm.ShowDialog() == DialogResult.OK)
            {
                //設定後樣試,回傳
                ConfigurationInCadre.Template = TemplateForm.Template;
                //儲存
                ConfigurationInCadre.Save();
            }
        }
    }
}
