using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K12.Service.Learning.Shinmin
{
    class Permissions
    {
        public static string 服務學習_多處室 { get { return "K12.Service.Learning.Shinmin.LearningItem.cs.0916"; } }
        public static bool 服務學習_多處室權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[服務學習_多處室].Executable;
            }
        }

        public static string 校園服務時數統計表 { get { return "K12.Service.Learning.Shinmin.LearningSchoolStatistics.cs"; } }
        public static bool 校園服務時數統計表權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[校園服務時數統計表].Executable;
            }
        }
    }
}
