using System;

namespace ApplicationFrameworkSDK.CustomExceptions
{
    /// <summary>
    /// Wrapper class for <code>Exceptions</code> thrown by the DB classes
    /// </summary>
    [Serializable()]
    public class DBException : Exception
    {
        /// <summary>
        /// Creates an instance of the DBException class
        /// </summary>
        public DBException() : base() { }

        /// <summary>
        /// Creates an instance of the DBException class
        /// </summary>
        /// <param name="message"></param>
        public DBException(string message) : base(message) { }

        /// <summary>
        /// Creates an instance of the DBException class
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public DBException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Creates an instance of the DBException class
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected DBException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
}
