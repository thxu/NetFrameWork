﻿using System;

namespace NetFrameWork.Infrastructure
{
    /// <summary>
    ///  IUpdateRepository{更新接口}
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    public interface IUpdateRepository<in T> : IDisposable
    {
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>返回结果</returns>
        int Update(T entity);
    }
}
