using System;
using System.Runtime.Serialization;

namespace DynamicWrapperCommon.Exceptions
{
    [Serializable]
    public abstract class GlobalAssemblyCacheHelperException : Exception 
    {
        protected GlobalAssemblyCacheHelperException(){}

        protected GlobalAssemblyCacheHelperException(string message):base(message) { }


        protected GlobalAssemblyCacheHelperException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

    }
}