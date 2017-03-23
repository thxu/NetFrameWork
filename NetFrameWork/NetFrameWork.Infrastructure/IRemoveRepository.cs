using System;

namespace NetFrameWork.Infrastructure
{
    /// <summary>
    /// IRepository{删除接口}
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    public interface IRemoveRepository<in T> : IDisposable
    {
        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>返回结果</returns>
        int Remove(T entity);
    }
}
