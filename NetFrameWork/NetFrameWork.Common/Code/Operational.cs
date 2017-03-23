using System;
using System.Runtime.Serialization;

namespace NetFrameWork.Common.Code
{
    /// <summary>
    /// 操作信息
    /// </summary>
    [DataContract]
    public class Operational
    {
        /// <summary>
        /// 操作人
        /// </summary>
        [DataMember]
        public string Operator { get; set; }

        /// <summary>
        /// 操作内容
        /// </summary>
        [DataMember]
        public string OperationContent { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        [DataMember]
        public DateTime OperationDateTime { get; set; }
    }
}
