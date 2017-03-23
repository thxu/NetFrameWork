using System;

namespace NetFrameWork.Infrastructure
{
    /// <summary>
    /// IQueryRepository{根据主键查询接口}.
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    public interface IQueryRepository<out T> : IDisposable
    {
        /// <summary>
        /// Query
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>返回查询单条数据</returns>
        T Query(long id);
    }
}
