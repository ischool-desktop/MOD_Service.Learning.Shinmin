using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K12.Service.Learning.Shinmin
{
    public static class 時數轉換器
    {
        /// <summary>
        /// 是否為Decimal
        /// </summary>
        public static bool IsDecimal(string name)
        {
            decimal a1;
            return decimal.TryParse(name, out a1);
        }

        /// <summary>
        /// 是否為小數點後一位
        /// 是否為整數
        /// 是否大於0
        /// </summary>
        public static bool SentDecimalByString(string name)
        {
            if (IsDecimal(name))
            {
                decimal DEC = decimal.Parse(name);
                if (DEC > 0)
                {
                    decimal decRound = Math.Round(DEC, 2);

                    if (DEC == decRound)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else //小於等於0
                {
                    return false;
                }


            }
            else //不是數字
            {
                return false;
            }
        }
    }
}
