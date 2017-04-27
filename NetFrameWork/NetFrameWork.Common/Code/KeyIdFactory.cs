using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFrameWork.Common.Code
{
    /// <summary>
    /// 主键工厂类
    /// </summary>
    public class KeyIdFactory
    {
        private static int _maxLen = 16;

        /// <summary>
        /// Fields
        /// </summary>
        private static long lastIdentity;

        /// <summary>
        /// locker
        /// </summary>
        private static readonly object locker = new object();

        /// <summary>
        /// 新的主键ID
        /// </summary>
        /// <returns>主键ID</returns>
        public static string NewKeyId()
        {
            return NewKeyId(_maxLen, null);
        }

        /// <summary>
        /// 新的主键ID
        /// </summary>
        /// <returns>主键ID</returns>
        public static string NewKeyId(DateTime time)
        {
            return NewKeyId(_maxLen, time);
        }

        /// <summary>
        /// 新的主键ID
        /// </summary>
        /// <param name="code">业务码</param>
        /// <returns>主键ID</returns>
        public static string NewKeyId(string code)
        {
            return NewKeyId(_maxLen, null) + code;
        }

        /// <summary>
        /// 新的主键ID
        /// </summary>
        /// <param name="length">ID长度(不能小于24)</param>
        /// <param name="time">创建时间</param>
        /// <returns>主键ID</returns>
        public static string NewKeyId(int length, DateTime? time)
        {
            if (length <= _maxLen)
            {
                length = _maxLen;
            }

            string strNum = string.Empty;
            for (int i = _maxLen; i <= length; i++)
            {
                strNum += "9";
            }

            lock (locker)
            {
                string str = lastIdentity.ToString().PadLeft(strNum.Length, '0');
                string str2 = time?.ToString("yyMMddHHmmssfff") ?? DateTime.Now.ToString("yyMMddHHmmssfff");

                lastIdentity++;

                if (lastIdentity > long.Parse(strNum))
                {
                    lastIdentity = 0;
                }
                Random random = new Random(Common.CreateRandomSeed());
                var r = random.Next(1000, 9999);
                return $"{str2}{r}{str}";
            }
        }

        /// <summary>
        /// 新的主键ID
        /// </summary>
        /// <param name="code">业务码</param>
        /// <param name="length">ID长度(不能小于24)</param>
        /// <returns>主键ID</returns>
        public static string NewKeyId(string code, int length)
        {
            if (length <= 24)
            {
                length = 24;
            }

            string strNum = string.Empty;
            for (int i = 24; i <= length; i++)
            {
                strNum += "9";
            }

            lock (locker)
            {
                string str = lastIdentity.ToString().PadLeft(strNum.Length, '0');
                string str2 = DateTime.Now.ToString("yyyyMMddHHmmssfff");

                lastIdentity++;

                if (lastIdentity > long.Parse(strNum))
                {
                    lastIdentity = 0;
                }
                Random random = new Random(Common.CreateRandomSeed());
                var r = random.Next(1000000, 9999999);
                return $"{str2}{r}{str}{code}";
            }
        }
    }
}
