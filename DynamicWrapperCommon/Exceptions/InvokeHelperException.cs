using System;
using System.Runtime.Serialization;

namespace DynamicWrapperCommon.Exceptions
{
    [Serializable]
    public abstract class InvokeHelperException : Exception 
    {
        protected InvokeHelperException(){}

        protected InvokeHelperException(string message):base(message) { }


        protected InvokeHelperException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

    }
}