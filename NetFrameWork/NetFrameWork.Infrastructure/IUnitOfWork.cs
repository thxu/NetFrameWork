using System;
using System.Data;

namespace NetFrameWork.Infrastructure
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 执行命令
        /// </summary>
        IDbCommand Command { get; }

        /// <summary>
        /// 提交
        /// </summary>
        void Complete();

        /// <summary>
        /// 回滚
        /// </summary>
        void Rollback();
    }
}
