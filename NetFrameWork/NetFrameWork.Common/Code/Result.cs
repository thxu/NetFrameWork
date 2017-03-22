using System;
using System.Runtime.Serialization;

namespace NetFrameWork.Common.Code
{
    /// <summary>
    /// 结果
    /// </summary>
    [Serializable]
    [DataContract]
    public class Result
    {
        /// <summary>
        /// 成功/失败
        /// </summary>
        [DataMember]
        public bool IsSucceed { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        [DataMember]
        public string Message { get; set; }
    }
}
