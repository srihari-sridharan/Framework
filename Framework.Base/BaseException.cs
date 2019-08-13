using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;

namespace Framework.Base
{
    /// <summary>
    ///     Base class for exceptions
    /// </summary>
    [Serializable]
    public class BaseException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseException" /> class.
        /// </summary>
        public BaseException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public BaseException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The exception.</param>
        public BaseException(string message, Exception ex)
            : base(message, ex)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The exception.</param>
        /// <param name="data">The data.</param>
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification =
            "Framework design.")]
        public BaseException(string message, Exception ex, params object[] data)
            : base(message, ex)
        {
            Data.Add("data", data.ToArray());
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseException" /> class.
        /// </summary>
        /// <param name="serializationInfo">The serialization information.</param>
        /// <param name="streamingContext">The streaming context.</param>
        protected BaseException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}