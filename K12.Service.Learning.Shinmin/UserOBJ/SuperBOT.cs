using K12.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace K12.Service.Learning.Shinmin
{
    class SuperBOT
    {

        public SuperBOT(List<SLRecord> list)
        {

            StudentDic = tool.GetStudent(list);
            ClassDic = new Dictionary<string, SLRClass>();
            OrganizersDic = new Dictionary<string, SLROrganizers>();

            #region 學生統計

            foreach (SLRecord each in list)
            {
                if (each.InternalOrExternal == "校內")
                {
                    全校服務總時數_校內 += each.Hours;
                    全校服務總人次_校內 += 1;
                }
                else if (each.InternalOrExternal == "校外")
                {
                    全校服務總時數_校外 += each.Hours;
                    全校服務總人次_校外 += 1;
                }

                if (StudentDic.ContainsKey(each.RefStudentID))
                {
                    StudentDic[each.RefStudentID].SLRList.Add(each);
                }
            }

            #endregion

            #region 班級統計

            Dictionary<string, SLRClass> c_t_Dic = new Dictionary<string, SLRClass>();
            foreach (string each in StudentDic.Keys)
            {
                StudentOBJ obj = StudentDic[each];

                foreach (SLRecord slr in obj.SLRList)
                {
                    if (!c_t_Dic.ContainsKey(obj.ref_class_id))
                    {
                        SLRClass slrC = new SLRClass();
                        slrC._class = obj.class_name;
                        slrC._teacher = obj.TeacherName;
                        slrC._班級人數 = obj.class_count;

                        c_t_Dic.Add(obj.ref_class_id, slrC);
                    }

                    if (slr.InternalOrExternal == "校內")
                    {
                        //記錄班級校內時數
                        c_t_Dic[obj.ref_class_id].班級校內時數 += slr.Hours;
                    }
                    else if (slr.InternalOrExternal == "校外")
                    {
                        //記錄班級校外時數
                        c_t_Dic[obj.ref_class_id].班級校外時數 += slr.Hours;
                    }

                    //記錄班級的總時數
                    c_t_Dic[obj.ref_class_id].班級服務總時數 += slr.Hours;
                }
            }

            List<string> classIDList = c_t_Dic.Keys.ToList();
            List<ClassRecord> classList = K12.Data.Class.SelectByIDs(classIDList);
            classList = SortClassIndex.K12Data_ClassRecord(classList);
            foreach (ClassRecord each in classList)
            {
                ClassDic.Add(each.ID, c_t_Dic[each.ID]);

            }
            #endregion

            #region 依主辦單位統計

            Dictionary<string, SLROrganizers> dic = new Dictionary<string, SLROrganizers>();

            foreach (SLRecord each in list)
            {
                string name = each.InternalOrExternal + "_" + each.Organizers;
                if (!dic.ContainsKey(name))
                {
                    dic.Add(name, new SLROrganizers(each));
                }

                dic[name]._AllCount += each.Hours;
            }

            List<string> OrganizersList = dic.Keys.ToList();
            OrganizersList.Sort();
            foreach (string each in OrganizersList)
            {
                OrganizersDic.Add(each, dic[each]);
            }

            #endregion


            decimal Count校內 = 0;
            decimal Count校外 = 0;

            int Count校內人數 = 0;
            int Count校外人數 = 0;

            foreach (string each in StudentDic.Keys)
            {
                foreach (SLRecord slr in StudentDic[each].SLRList)
                {
                    if (slr.InternalOrExternal == "校內")
                    {
                        Count校內 += slr.Hours;
                        Count校內人數 += 1;
                    }
                    else if (slr.InternalOrExternal == "校外")
                    {
                        Count校外 += slr.Hours;
                        Count校外人數 += 1;
                    }
                }
            }

            if (Count校內 != 0)
                個人平均時數_校內 = tool.GetAwayZero(Count校內 / Count校內人數);

            if (Count校外 != 0)
                個人平均時數_校外 = tool.GetAwayZero(Count校外 / Count校外人數);
        }

        public decimal 個人平均時數_校內 { get; set; }
        public int 全校服務總人次_校內 { get; set; }
        public decimal 全校服務總時數_校內 { get; set; }

        public decimal 個人平均時數_校外 { get; set; }
        public int 全校服務總人次_校外 { get; set; }
        public decimal 全校服務總時數_校外 { get; set; }

        /// <summary>
        /// 學生清單 學生ID + 學生
        /// </summary>
        public Dictionary<string, StudentOBJ> StudentDic { get; set; }

        /// <summary>
        /// 依班級 班級ID + 班級
        /// </summary>
        public Dictionary<string, SLRClass> ClassDic { get; set; }

        /// <summary>
        /// 依主辦單位 校內外_主辦單位 + 校內外
        /// </summary>
        public Dictionary<string, SLROrganizers> OrganizersDic { get; set; }
    }
}
