using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace DynamicWrapperCommon.Exceptions
{
    [Serializable]
    public sealed class GetDefaultConstructorException : GlobalAssemblyCacheHelperException
    {
        public string TypeName { get; }

        public GetDefaultConstructorException([CanBeNull] Type type) : base($"Error on getting default constructor for type [{type?.FullName}].")
        {
            TypeName = type?.FullName;
        }

        private GetDefaultConstructorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            if (info == null)
            {
                return;
            }
            TypeName = info.GetString(nameof(TypeName));
        }


        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(TypeName), TypeName);
        }
    }
}