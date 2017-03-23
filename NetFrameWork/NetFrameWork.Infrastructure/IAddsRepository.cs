using System;
using System.Collections.Generic;

namespace NetFrameWork.Infrastructure
{
    /// <summary>
    /// IAddsRepository
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    public interface IAddsRepository<T> : IDisposable
    {
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>返回结果</returns>
        int Add(IList<T> entity);
    }
}