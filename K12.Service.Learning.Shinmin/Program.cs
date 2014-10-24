using FISCA;
using FISCA.Permission;
using FISCA.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K12.Service.Learning.Shinmin
{
    public class Program
    {
        [MainMethod()]
        static public void Main()
        {

            //轉移功能
            //服務學習快速登錄

            RibbonBarItem insert = MotherForm.RibbonBarItems["學生", "學務"];

            insert["服務學習多處室登錄"].Image = Properties.Resources.flip_vertical_clock_64;
            insert["服務學習多處室登錄"].Size = RibbonBarButton.MenuButtonSize.Medium;
            insert["服務學習多處室登錄"].Enable = false;
            insert["服務學習多處室登錄"].Click += delegate
            {
                MutiLearning acf = new MutiLearning();
                acf.ShowDialog();
            };

            K12.Presentation.NLDPanels.Student.ListPaneContexMenu["服務學習多處室登錄"].Image = Properties.Resources.flip_vertical_clock_64;
            K12.Presentation.NLDPanels.Student.ListPaneContexMenu["服務學習多處室登錄"].Enable = false;
            K12.Presentation.NLDPanels.Student.ListPaneContexMenu["服務學習多處室登錄"].Click += delegate
            {
                MutiLearning acf = new MutiLearning();
                acf.ShowDialog();
            };

            RibbonBarItem new_jj = MotherForm.RibbonBarItems["學務作業", "資料統計"];
            new_jj["報表"]["校園服務時數統計表"].Enable = Permissions.校園服務時數統計表權限;
            new_jj["報表"]["校園服務時數統計表"].Click += delegate
            {
                LearningSchoolStatistics acf = new LearningSchoolStatistics();
                acf.ShowDialog();
            };

            K12.Presentation.NLDPanels.Student.SelectedSourceChanged += delegate
            {
                if (Permissions.服務學習_多處室權限)
                {
                    bool a = K12.Presentation.NLDPanels.Student.SelectedSource.Count > 0;

                    insert["服務學習多處室登錄"].Enable = a;
                    K12.Presentation.NLDPanels.Student.ListPaneContexMenu["服務學習多處室登錄"].Enable = a;
                }
            };

            Catalog ribbon = RoleAclSource.Instance["學生"]["功能按鈕"];
            ribbon.Add(new RibbonFeature(Permissions.服務學習_多處室, "服務學習多處室登錄"));

            ribbon = RoleAclSource.Instance["學務作業"]["功能按鈕"];
            ribbon.Add(new RibbonFeature(Permissions.校園服務時數統計表, "校園服務時數統計表"));
        }
    }
}
