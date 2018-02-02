using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NetFrameWork.Common.Code
{
    /// <summary>
    /// 按月分表
    /// </summary>
    public class MonthlyTable
    {
        /// <summary>
        /// 获取交易分表时间
        /// </summary>
        /// <returns>交易分表时间</returns>
        private static DateTime GetTradeTableDefaultDateTime()
        {
            return new DateTime(2016, 01, 09, 00, 30, 59, 999); // 这个之前的订单不进行分表
        }

        /// <summary>
        /// 根据外部订单号获取表名
        /// </summary>
        /// <param name="tableName">原表名（没有进行分表时的表名)</param>
        /// <param name="orderNumber">外部订单号</param>
        /// <returns>表名后缀</returns>
        public static string GetTableName(string tableName, string orderNumber)
        {
            var defaultDateTime = GetTradeTableDefaultDateTime(); // 这个之前的订单不进行分表
            var currentDataTime = DateTime.Now; // 当前时间
            var thisYearDataTime = currentDataTime.ToString("yyyy").Substring(0, 2);  // 今年
            DateTime orderDateTime; // 订单日期
            string tableNameExtension; // 表扩展名称
            if (Regex.IsMatch(orderNumber, "^[A-Za-z]"))
            {
                if (orderNumber.Substring(1, 2) == thisYearDataTime)
                {
                    tableNameExtension = orderNumber.Substring(1, 6);
                    var year = orderNumber.Substring(1, 4);
                    var month = orderNumber.Substring(5, 2);
                    var day = orderNumber.Substring(7, 2);
                    var hour = orderNumber.Substring(9, 2);
                    var minutes = orderNumber.Substring(11, 2);
                    var seconds = orderNumber.Substring(13, 2);
                    orderDateTime = Convert.ToDateTime($"{year}-{month}-{day} {hour}:{minutes}:{seconds}");
                }
                else
                {
                    tableNameExtension = thisYearDataTime + orderNumber.Substring(1, 4);
                    var year = thisYearDataTime + orderNumber.Substring(1, 2);
                    var month = orderNumber.Substring(3, 2);
                    var day = orderNumber.Substring(5, 2);
                    var hour = orderNumber.Substring(7, 2);
                    var minutes = orderNumber.Substring(9, 2);
                    var seconds = orderNumber.Substring(11, 2);
                    orderDateTime = Convert.ToDateTime($"{year}-{month}-{day} {hour}:{minutes}:{seconds}");
                }
            }
            else
            {
                if (orderNumber.Substring(0, 2) == thisYearDataTime)
                {
                    tableNameExtension = orderNumber.Substring(0, 6);
                    var year = orderNumber.Substring(0, 4);
                    var month = orderNumber.Substring(4, 2);
                    var day = orderNumber.Substring(6, 2);
                    var hour = orderNumber.Substring(8, 2);
                    var minutes = orderNumber.Substring(10, 2);
                    var seconds = orderNumber.Substring(12, 2);
                    orderDateTime = Convert.ToDateTime($"{year}-{month}-{day} {hour}:{minutes}:{seconds}");
                }
                else
                {
                    tableNameExtension = thisYearDataTime + orderNumber.Substring(0, 4);
                    var year = thisYearDataTime + orderNumber.Substring(0, 2);
                    var month = orderNumber.Substring(2, 2);
                    var day = orderNumber.Substring(4, 2);
                    var hour = orderNumber.Substring(6, 2);
                    if (Convert.ToInt32(hour) > 24)
                    {
                        hour = "00";
                    }
                    var minutes = orderNumber.Substring(8, 2);
                    if (Convert.ToInt32(minutes) > 60)
                    {
                        minutes = "00";
                    }
                    var seconds = orderNumber.Substring(10, 2);
                    if (Convert.ToInt32(seconds) > 60)
                    {
                        seconds = "00";
                    }
                    orderDateTime = Convert.ToDateTime($"{year}-{month}-{day} {hour}:{minutes}:{seconds}");
                }
            }
            if (orderDateTime >= defaultDateTime)
            {
                return $"{tableName}_{tableNameExtension}";
            }
            else
            {
                return tableName;
            }
        }

        /// <summary>
        /// 根据时间获取表名(交易版本)
        /// </summary>
        /// <param name="tableName">原表名（没有进行分表时的表名)</param>
        /// <param name="dateTime">时间</param>
        /// <returns>表名后缀</returns>
        public static string GetTableName(string tableName, DateTime dateTime)
        {
            var defaultDateTime = GetTradeTableDefaultDateTime(); // 这个之前的订单不进行分表
            if (dateTime >= defaultDateTime)
            {
                return $"{tableName}_{dateTime:yyyyMM}";
            }
            return tableName;
        }

        /// <summary>
        /// 根据起止日期,获取分表后的表名列表
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="start">The start date.</param>
        /// <param name="end">The end date.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        public static List<string> GetTableName(string tableName, DateTime start, DateTime end)
        {
            List<string> result = new List<string>();
            var currDate = DateTime.Today;
            if (end > currDate)
            {
                end = currDate;
            }
            var between = end.Year * 12 + end.Month - (start.Year * 12 + start.Month);
            for (int i = 0; i <= between; i++)
            {
                var temp = start.AddMonths(i);
                result.Add(tableName + "_" + temp.ToString("yyyyMM"));
            }
            return result;
        }
    }
}
