using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace K12.Service.Learning.Shinmin
{
    public class StudentOBJ
    {
        public StudentOBJ(DataRow row)
        {
            SLRList = new List<SLRecord>();


            id = "" + row["id"];
            name = "" + row["name"];
            student_number = "" + row["student_number"];
            seat_no = "" + row["seat_no"];

            ref_class_id = "" + row["ref_class_id"];
            class_name = "" + row["class_name"];

            teacher = "" + row["teacher_name"];
            nickname = "" + row["nickname"];

            class_count = "" + row["studentcount"];
        }


        /// <summary>
        /// 系統編號
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 學生學號
        /// </summary>
        public string student_number { get; set; }

        /// <summary>
        /// 座號
        /// </summary>
        public string seat_no { get; set; }

        /// <summary>
        /// 班級ID
        /// </summary>
        public string ref_class_id { get; set; }


        /// <summary>
        /// 班級
        /// </summary>
        public string class_name { get; set; }

        /// <summary>
        /// 人數
        /// </summary>
        public string class_count { get; set; }

        /// <summary>
        /// 老師
        /// </summary>
        private string teacher { get; set; }

        /// <summary>
        /// 老師暱稱
        /// </summary>
        private string nickname { get; set; }

        /// <summary>
        /// 老師全名
        /// </summary>
        public string TeacherName
        {
            get
            {
                if (!string.IsNullOrEmpty(nickname))
                {
                    return teacher + "(" + nickname + ")";
                }
                else
                {
                    return teacher;
                }
            }


        }

        public List<SLRecord> SLRList { get; set; }
    }
}
