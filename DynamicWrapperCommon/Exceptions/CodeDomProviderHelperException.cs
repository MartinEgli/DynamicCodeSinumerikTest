using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace DynamicWrapperCommon.Exceptions
{
    [Serializable]
    public abstract class CodeDomProviderHelperException : Exception 
    {
        protected CodeDomProviderHelperException(){}

        protected CodeDomProviderHelperException(string message):base(message) { }


        protected CodeDomProviderHelperException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

    }

    [Serializable]
    public sealed class CompiledAssemblyException : CodeDomProviderHelperException
    {
        public CompiledAssemblyException() : base($"Compiled assembly is null.")
        {
        }

        private CompiledAssemblyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }


    [Serializable]
    public sealed class GetTypeFromCompiledAssemblyException : CodeDomProviderHelperException
    {
        public string TargetTypeName { get; }
        public string CompiledAssemblyName { get; }

        public GetTypeFromCompiledAssemblyException(string targetTypeName, Assembly compiledAssembly) : base($"Error on getting type [{targetTypeName}] from compiled assembly [{compiledAssembly.FullName}].")
        {
            TargetTypeName = targetTypeName;
            CompiledAssemblyName = compiledAssembly.FullName;
        }

        private GetTypeFromCompiledAssemblyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            if (info == null)
            {
                return;
            }
            this.TargetTypeName = info.GetString(nameof(TargetTypeName));
            this.CompiledAssemblyName = info.GetString(nameof(CompiledAssemblyName));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(TargetTypeName), TargetTypeName);
            info.AddValue(nameof(CompiledAssemblyName), CompiledAssemblyName);
        }
    }
}