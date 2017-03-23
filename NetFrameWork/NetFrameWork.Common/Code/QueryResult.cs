using System;
using System.Runtime.Serialization;

namespace NetFrameWork.Common.Code
{
    /// <summary>
    /// 查询结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    [DataContract]
    public class QueryResult<T>
    {
        /// <summary>
        /// 查询结果
        /// </summary>
        [DataMember]
        public Result Result { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        [DataMember]
        public T Data { get; set; }
    }
}
