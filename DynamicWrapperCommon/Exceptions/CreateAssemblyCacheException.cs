using System;
using System.Runtime.Serialization;

namespace DynamicWrapperCommon.Exceptions
{
    [Serializable]
    public sealed class CreateAssemblyCacheException : GlobalAssemblyCacheHelperException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CreateAssemblyCacheException" /> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public CreateAssemblyCacheException(int result) : base($"Error on creating assembly cache. HResult: [{result}]")
        {
            Result = result;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CreateAssemblyCacheException" /> class.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        private CreateAssemblyCacheException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            if (info != null)
            {
                this.Result = info.GetInt32(nameof(Result));
            }
        }

        /// <summary>
        ///     Gets the result.
        /// </summary>
        /// <value>
        ///     The result.
        /// </value>
        public int Result { get; }

        /// <summary>
        ///     Gets the object data.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(Result), Result);
        }
    }
}