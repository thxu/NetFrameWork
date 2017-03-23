using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NetFrameWork.Common.Code
{
    [Serializable]
    [DataContract]
    public class PagingResult<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryResult{T}"/> class.
        /// </summary>
        /// <param name="paging">
        /// The paging.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        public PagingResult(Paging paging, IList<T> data)
        {
            this.Paging = paging;
            this.Data = data;
        }

        /// <summary>
        /// 分页信息
        /// </summary>
        [DataMember]
        public Paging Paging { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        [DataMember]
        public IList<T> Data { get; set; }
    }
}
