using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.Data;
using System.Data;
using FISCA.UDT;

namespace K12.Service.Learning.Shinmin
{
    static public class tool
    {
        static public QueryHelper _Q = new QueryHelper();

        static public AccessHelper _A = new AccessHelper();


        static public string TableName = "\"$K12.Service.Learning.Record\"";

        /// <summary>
        /// 取得小數點後2位
        /// </summary>
        public static decimal GetAwayZero(decimal x)
        {
            return Math.Round(x, 2, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// 取得系統內所有主辦單位的交集資料
        /// </summary>
        static public List<string> GetOrganizersTable()
        {
            DataTable dt = _Q.Select("select organizers from " + TableName.ToLower() + " group by organizers ORDER by organizers");

            List<string> list = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                string organizers = "" + row["organizers"];
                if (!string.IsNullOrEmpty(organizers))
                {
                    list.Add(organizers);
                }
            }
            return list;
        }

        /// <summary>
        /// 依據服務學習資料
        /// 取得學生資料集合
        /// </summary>
        static public Dictionary<string, StudentOBJ> GetStudent(List<SLRecord> list)
        {
            #region 取得學生

            Dictionary<string, StudentOBJ> dic = new Dictionary<string, StudentOBJ>();
            List<string> StudentIDList = new List<string>();

            foreach (SLRecord each in list)
            {
                if (!StudentIDList.Contains(each.RefStudentID))
                    StudentIDList.Add(each.RefStudentID);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("select student.id,student.name,student.seat_no,student.student_number,class.class_name,class.id as ref_class_id,teacher.teacher_name,teacher.nickname,cls.studentcount from student ");
            sb.Append("left join class on class.id=student.ref_class_id ");
            sb.Append("left join teacher on teacher.id=class.ref_teacher_id ");
            sb.Append("left join (select ref_class_id,count(*) studentcount from student where status in ('1','2') group by ref_class_id) cls on student.ref_class_id=cls.ref_class_id ");
            sb.Append("where student.status in ('1','2') ");
            sb.Append(string.Format("and student.id in ('{0}')", string.Join("','", StudentIDList)));
            DataTable dt = tool._Q.Select(sb.ToString());

            dic = new Dictionary<string, StudentOBJ>();

            foreach (DataRow row in dt.Rows)
            {
                StudentOBJ stud = new StudentOBJ(row);

                if (!dic.ContainsKey(stud.id))
                {
                    dic.Add(stud.id, stud);
                }
            }

            return dic;

            #endregion
        }
    }
}
