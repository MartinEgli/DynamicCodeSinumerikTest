using System;
using System.Runtime.Serialization;

namespace DynamicWrapperCommon.Exceptions
{
    [Serializable]
    public sealed class QueryAssemblyInfoException : GlobalAssemblyCacheHelperException
    {
        public string AssemblyName { get; }
        public int Result { get; }

        public QueryAssemblyInfoException(string assemblyName, int result) : base($"Error on query assembly cache. HResult: [{result}]")
        {
            AssemblyName = assemblyName;
            Result = result;
        }

        private QueryAssemblyInfoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            if (info == null)
            {
                return;
            }
            AssemblyName = info.GetString(nameof(AssemblyName));
            Result = info.GetInt32(nameof(Result));

        }


        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(Result), Result);
            info.AddValue(nameof(AssemblyName), AssemblyName);
        }
    }
}