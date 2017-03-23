using System;

namespace NetFrameWork.Common.Code
{
    /// <summary>
    /// 自定义异常
    /// </summary>
    [Serializable]
    public class CustomException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomException"/> class.
        /// </summary>
        public CustomException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public CustomException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="inner">
        /// The inner.
        /// </param>
        public CustomException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
