namespace NetFrameWork.Common.Extension
{
    /// <summary>
    /// 字符串相关扩展方法
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 字符串是否为空
        /// </summary>
        /// <param name="val">要判断的字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsNullOrEmpty(this string val)
        {
            return string.IsNullOrEmpty(val);
        }

        /// <summary>
        /// 字符串是否为空或者空白字符串
        /// </summary>
        /// <param name="val">要判断的字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsNullOrWhiteSpace(this string val)
        {
            return string.IsNullOrWhiteSpace(val);
        }
    }
}
