using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K12.Service.Learning.Shinmin
{
    class SLROrganizers
    {
        public SLROrganizers(SLRecord name)
        {
            _Organizers = name.Organizers;
            _InternalExternal = name.InternalOrExternal;

            //_AllCount += name.Hours;
        }


        /// <summary>
        /// 依主辦單位
        /// </summary>
        public string _Organizers { get; set; }

        /// <summary>
        /// 校內/校外
        /// </summary>
        public string _InternalExternal { get; set; }

        /// <summary>
        /// 總時數
        /// </summary>
        public decimal _AllCount { get; set; }
    }
}
