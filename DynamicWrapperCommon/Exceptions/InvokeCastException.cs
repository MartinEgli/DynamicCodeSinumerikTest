using System;
using System.Runtime.Serialization;

namespace DynamicWrapperCommon.Exceptions
{
    [Serializable]
    public sealed class InvokeCastException : InvokeHelperException
    {
        public string InvokeTypeName { get; }
        public string TargetTypeName { get; }
        
        public InvokeCastException(Type invokeType, Type targetType) : base($"Error on casting invoke type [{invokeType.FullName}] to target type [{targetType.FullName}].")
        {
            InvokeTypeName = invokeType.FullName;
            TargetTypeName = targetType.FullName;
        }

        private InvokeCastException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            if (info == null)
            {
                return;
            }
            InvokeTypeName = info.GetString(nameof(InvokeTypeName));
            TargetTypeName = info.GetString(nameof(TargetTypeName));
        }


        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(InvokeTypeName), InvokeTypeName);
            info.AddValue(nameof(TargetTypeName), TargetTypeName);
        }
    }
}