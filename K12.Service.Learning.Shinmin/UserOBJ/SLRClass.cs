using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K12.Service.Learning.Shinmin
{
    class SLRClass
    {
        /// <summary>
        /// 班級
        /// </summary>
        public string _class { get; set; }

        /// <summary>
        /// 老師
        /// </summary>
        public string _teacher { get; set; }


        /// <summary>
        /// 班級人數
        /// </summary>
        public string _班級人數 { get; set; }

        /// <summary>
        /// 校內時數
        /// </summary>
        public decimal 班級校內時數 { get; set; }

        /// <summary>
        /// 校外時數
        /// </summary>
        public decimal 班級校外時數 { get; set; }

        /// <summary>
        /// 總時數
        /// </summary>
        public decimal 班級服務總時數 { get; set; }
    }
}
