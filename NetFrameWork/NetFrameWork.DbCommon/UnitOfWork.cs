using System.Data;
using NetFrameWork.Infrastructure;

namespace NetFrameWork.DbCommon
{
    /// <summary>
    /// UnitOfWork
    /// </summary>	
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// 设置事务隔离级别
        /// </summary>
        /// <param name="isolationLevel">事务级别</param>
        /// <param name="name">链接名称</param>
        public UnitOfWork(IsolationLevel? isolationLevel, string name)
        {
            this.Conn = DbFactory.GetConnection(name);
            this.Command = this.Conn.CreateCommand();
            this.Transaction = isolationLevel.HasValue ? this.Conn.BeginTransaction(isolationLevel.Value) : this.Conn.BeginTransaction();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="name">链接名称</param>
        public UnitOfWork(string name)
            : this(null, name)
        {
        }

        /// <summary>
        /// IDbConnection
        /// </summary>
        private IDbConnection Conn { get; set; }

        /// <summary>
        /// IDbCommand
        /// </summary>
        public IDbCommand Command { get; private set; }

        /// <summary>
        /// IDbTransaction
        /// </summary>
        private IDbTransaction Transaction { get; set; }

        /// <summary>
        /// 提交事物
        /// </summary>
        public void Complete()
        {
            this.Transaction.Commit();
        }

        /// <summary>
        /// 回滚事物
        /// </summary>
        public void Rollback()
        {
            this.Transaction.Rollback();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (this.Transaction != null)
            {
                this.Transaction.Dispose();
                this.Transaction = null;
            }

            if (this.Command != null)
            {
                this.Command.Dispose();
                this.Command = null;
            }

            if (this.Conn != null)
            {
                this.Conn.Close();
                this.Conn.Dispose();
                this.Conn = null;
            }
        }
    }
}
