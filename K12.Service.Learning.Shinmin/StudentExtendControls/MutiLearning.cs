using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using System.Xml;
using K12.Data;
using FISCA.LogAgent;
using FISCA.UDT;

namespace K12.Service.Learning.Shinmin
{
    public partial class MutiLearning : BaseForm
    {
        Dictionary<string, string> ReasonDic = new Dictionary<string, string>();

        StringBuilder sb = new StringBuilder();

        AccessHelper _accessHelper = new AccessHelper();

        BackgroundWorker BGW = new BackgroundWorker();

        List<DigitalCodeRecord> DCRecord { get; set; }

        List<string> OrganizersList { get; set; }

        /// <summary>
        /// 傳入獎勵或懲戒字串,以決定模式
        /// </summary>
        /// <param name="DemeritOrMerit"></param>
        public MutiLearning()
        {
            InitializeComponent();
        }

        //Load
        private void MutiLearning_Load(object sender, EventArgs e)
        {
            integerInput1.Text = School.DefaultSchoolYear;
            integerInput2.Text = School.DefaultSemester;
            dateTimeInput1.Text = DateTime.Today.ToShortDateString();
            dateTimeInput2.Text = DateTime.Today.ToShortDateString();

            BGW.DoWork += new DoWorkEventHandler(BGW_DoWork);
            BGW.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BGW_RunWorkerCompleted);
            BGW.RunWorkerAsync();

        }

        void BGW_DoWork(object sender, DoWorkEventArgs e)
        {
            //取得學生
            List<K12.Data.StudentRecord> StudentList = K12.Data.Student.SelectByIDs(K12.Presentation.NLDPanels.Student.SelectedSource);
            StudentList = SortClassIndex.K12Data_StudentRecord(StudentList); //sort
            e.Result = StudentList;

            //取得服務學習代碼表
            DCRecord = _accessHelper.Select<DigitalCodeRecord>();

            OrganizersList = tool.GetOrganizersTable();

        }

        void BGW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //加入畫面學生
            List<K12.Data.StudentRecord> StudentList = (List<K12.Data.StudentRecord>)e.Result;
            foreach (K12.Data.StudentRecord student in StudentList)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridViewX1);
                row.Tag = student;
                row.Cells[0].Value = student.Class != null ? student.Class.Name : "";
                row.Cells[1].Value = student.SeatNo.HasValue ? student.SeatNo.Value.ToString() : "";
                row.Cells[2].Value = student.StudentNumber;
                row.Cells[3].Value = student.Name;
                dataGridViewX1.Rows.Add(row);
            }

            comboBoxEx2.Items.Add("");
            foreach (string organizers in OrganizersList)
            {
                if (!string.IsNullOrEmpty(organizers))
                {
                    comboBoxEx2.Items.Add(organizers);
                }
            }

            KeyValuePair<string, string> fkvp = new KeyValuePair<string, string>("", "");
            comboBoxEx1.Items.Add(fkvp);
            //加入代碼表
            foreach (DigitalCodeRecord each in DCRecord)
            {
                string k = each.Code + "-" + each.Content;
                string v = each.Content;
                KeyValuePair<string, string> kvp = new KeyValuePair<string, string>(k, v);
                comboBoxEx1.Items.Add(kvp);

                if (!ReasonDic.ContainsKey(each.Code))
                {
                    ReasonDic.Add(each.Code, each.Content);
                }
            }

            comboBoxEx1.DisplayMember = "Key";
            comboBoxEx1.ValueMember = "Value";
            comboBoxEx1.SelectedIndex = 0;
        }

        //儲存
        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (!CheckIntError())
            {
                MsgBox.Show("輸入[時數]型態錯誤,請重新修正後再儲存!!");
                return;
            }

            if (CheckOrganizers())
            {
                DialogResult dr = MsgBox.Show("部份學生資料未輸入主辦單位,是否繼續儲存?", MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button1);

                if (dr == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }


            if (CheckReasonError())
            {
                DialogResult dr = MsgBox.Show("部份學生資料未輸入事由,是否繼續儲存?", MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button1);

                if (dr == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }

            List<SLRecord> MeritList = GetSLRList();
            try
            {
                _accessHelper.InsertValues(MeritList);
            }
            catch (Exception ex)
            {
                MsgBox.Show("新增服務學習記錄失敗\n" + ex.Message);
                return;
            }
            ApplicationLog.Log("服務學習多處室登錄", "多處室登錄", sb.ToString());
            MsgBox.Show("新增服務學習記錄成功!");

            K12.Service.Learning.Modules.LearningEvents.RaiseAssnChanged();

            this.Close();
        }

        //取得獎勵資料
        private List<SLRecord> GetSLRList()
        {
            sb.AppendLine("批次「服務學習記錄」快速登錄作業");
            sb.AppendLine("學年度「" + integerInput1.Value.ToString() + "」");
            sb.AppendLine("學期「" + integerInput2.Value.ToString() + "」");
            sb.AppendLine("發生日期：" + dateTimeInput1.Value.ToShortDateString());
            sb.AppendLine("登錄日期：" + dateTimeInput2.Value.ToShortDateString());
            sb.AppendLine("詳細資料：");

            List<SLRecord> SLRList = new List<SLRecord>();
            //每一位學生的獎懲資料
            foreach (DataGridViewRow row in dataGridViewX1.Rows)
            {
                K12.Data.StudentRecord student = (K12.Data.StudentRecord)row.Tag;
                SLRecord mr = new SLRecord();

                mr.RefStudentID = student.ID; //學生ID
                mr.SchoolYear = integerInput1.Value; //學年度
                mr.Semester = integerInput2.Value; //學期

                mr.Hours = decimal.Parse("" + row.Cells[4].Value); //時數
                mr.Reason = "" + row.Cells[5].Value; //事由

                mr.Organizers = "" + row.Cells[6].Value;  //主辦單位
                mr.Remark = "" + row.Cells[7].Value;  //備註

                mr.InternalOrExternal = "" + row.Cells[8].Value;  //校內外

                mr.OccurDate = dateTimeInput1.Value;
                mr.RegisterDate = dateTimeInput2.Value;

                SLRList.Add(mr);

                sb.AppendLine("學生「" + student.Name + "」"
                + "時數「" + row.Cells[4].Value + "」"
                + "事由「" + row.Cells[5].Value + "」"
                + "主辦單位「" + row.Cells[6].Value + "」"
                + "備註「" + row.Cells[7].Value + "」"
                  + "校內外「" + row.Cells[8].Value + "」");
            }

            return SLRList;
        }

        private bool CheckOrganizers()
        {
            bool returnTrue = false;

            foreach (DataGridViewRow row in dataGridViewX1.Rows)
            {
                if (("" + row.Cells[Column8.Index].Value).Trim() == "")
                {
                    returnTrue = true;
                }
            }
            return returnTrue;
        }

        /// <summary>
        /// 檢查事由是否輸入
        /// </summary>
        /// <returns></returns>
        private bool CheckReasonError()
        {
            bool returnTrue = false;

            foreach (DataGridViewRow row in dataGridViewX1.Rows)
            {
                if (("" + row.Cells[Column13.Index].Value).Trim() == "")
                {
                    returnTrue = true;
                }
            }
            return returnTrue;
        }

        /// <summary>
        /// 檢查時數是否為int型態
        /// </summary>
        /// <returns></returns>
        private bool CheckIntError()
        {
            bool returnTrue = false;

            foreach (DataGridViewRow row in dataGridViewX1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.ColumnIndex > 3 && cell.ColumnIndex < 5)
                    {
                        if (時數轉換器.IsDecimal("" + cell.Value))
                        {
                            if (時數轉換器.SentDecimalByString("" + cell.Value))
                            {
                                returnTrue = true;
                            }
                        }
                    }
                }
            }
            return returnTrue;
        }

        //離開
        private void buttonX3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {
            if (時數轉換器.IsDecimal(textBoxX1.Text))
            {
                if (時數轉換器.SentDecimalByString(textBoxX1.Text))
                {
                    foreach (DataGridViewRow row in dataGridViewX1.Rows)
                    {
                        row.Cells[4].Value = textBoxX1.Text.Trim();
                        row.Cells[4].ErrorText = "";
                    }
                    errorProvider1.Clear();
                }
                else
                {
                    foreach (DataGridViewRow row in dataGridViewX1.Rows)
                    {
                        row.Cells[4].Value = "";
                    }
                    errorProvider1.SetError(textBoxX1, "必須輸入整數或小數後兩位數!!");
                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridViewX1.Rows)
                {
                    row.Cells[4].Value = "";
                }
                errorProvider1.SetError(textBoxX1, "輸入內容非數字!\n(或時數並非整數!)");
            }
        }

        private void textBoxX3_TextChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewX1.Rows)
            {
                row.Cells[7].Value = textBoxX3.Text.Trim();
            }
        }

        private void dataGridViewX1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //不是標題列
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                //缺曠數量
                if (e.ColumnIndex == Column7.Index)
                {
                    DataGridViewCell cell = dataGridViewX1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    if (時數轉換器.IsDecimal("" + cell.Value))
                    {
                        if (時數轉換器.SentDecimalByString("" + cell.Value))
                        {
                            cell.ErrorText = "";
                        }
                        else
                        {
                            cell.ErrorText = "輸入內容必須為整數\n或小數後兩位!!";
                        }
                    }
                    else
                    {
                        cell.ErrorText = "內容非數字!!";
                    }
                }

                //事由替換
                if (e.ColumnIndex == Column13.Index)
                {
                    DataGridViewCell cell = dataGridViewX1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.Value = GetReason("" + cell.Value);
                }

                //校內外
                if (e.ColumnIndex == colIandOut.Index)
                {
                    DataGridViewCell cell = dataGridViewX1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    if ("" + cell.Value == "校內" || "" + cell.Value == "校外" || "" + cell.Value == "")
                    {
                        cell.ErrorText = "";
                    }
                    else
                    {
                        cell.ErrorText = "必須為校內或校外";
                    }
                }
            }
        }

        private void comboBoxEx1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboBoxEx1.Focus();

                string comText = comboBoxEx1.Text;
                comText = comText.Remove(0, comText.IndexOf('-') + 1);

                string reasonValue = GetReason(comText);

                foreach (DataGridViewRow row in dataGridViewX1.Rows)
                {
                    row.Cells[Column13.Index].Value = reasonValue;
                }
            }
        }

        private string GetReason(string comText)
        {
            string reasonValue = "";
            List<string> list = new List<string>();
            string[] reasonList = comText.Split(',');
            foreach (string each in reasonList)
            {
                string each1 = each.Replace("\r\n", "");
                if (ReasonDic.ContainsKey(each1))
                {
                    list.Add(ReasonDic[each1]);
                }
                else
                {
                    list.Add(each1);
                }
            }

            reasonValue = string.Join(",", list);
            return reasonValue;
        }

        private void comboBoxEx1_TextChanged(object sender, EventArgs e)
        {
            string comText = comboBoxEx1.Text;
            comText = comText.Remove(0, comText.IndexOf('-') + 1);

            string reasonValue = GetReason(comText);

            foreach (DataGridViewRow row in dataGridViewX1.Rows)
            {
                row.Cells[Column13.Index].Value = reasonValue;
            }
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            KeyValuePair<string, string> kvp = (KeyValuePair<string, string>)comboBoxEx1.SelectedItem;
            foreach (DataGridViewRow row in dataGridViewX1.Rows)
            {
                row.Cells[Column13.Index].Value = kvp.Value;
            }
        }

        private void comboBoxEx2_TextChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewX1.Rows)
            {
                row.Cells[6].Value = comboBoxEx2.Text.Trim();
            }
        }

        private void cbIAndE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbIAndE.SelectedIndex == 1)
            {
                foreach (DataGridViewRow row in dataGridViewX1.Rows)
                {
                    row.Cells[8].Value = "校內";
                }
            }
            else if (cbIAndE.SelectedIndex == 2)
            {
                foreach (DataGridViewRow row in dataGridViewX1.Rows)
                {
                    row.Cells[8].Value = "校外";
                }
            }
            else if (cbIAndE.SelectedIndex == 0)
            {
                foreach (DataGridViewRow row in dataGridViewX1.Rows)
                {
                    row.Cells[8].Value = "";
                }
            }

        }
    }
}
