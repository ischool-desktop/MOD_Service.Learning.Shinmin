using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;

namespace K12.Service.Learning.Shinmin
{
    [TableName("K12.Service.Learning.DigitalCodeRecord")]
    class DigitalCodeRecord : ActiveRecord
    {
        /// <summary>
        /// 事由代碼
        /// </summary>
        [Field(Field = "code", Indexed = false)]
        public string Code { get; set; }

        /// <summary>
        /// 事由
        /// </summary>
        [Field(Field = "content", Indexed = false)]
        public string Content { get; set; }

    }
}
