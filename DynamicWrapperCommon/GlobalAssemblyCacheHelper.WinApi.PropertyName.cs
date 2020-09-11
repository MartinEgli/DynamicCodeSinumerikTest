namespace DynamicWrapperCommon
{
    public static partial class GlobalAssemblyCacheHelper
    {
        public static partial class WinApi
        {
            /// <summary>
            /// https://docs.microsoft.com/en-us/windows/win32/api/winsxs/ne-winsxs-asm_name
            /// </summary>
            public enum PropertyName : uint
            {
                /// <summary>
                ///     Property ID for the assembly's public key. The value is a byte array.
                /// </summary>
                PublicKey = 0,

                /// <summary>
                ///     Property ID for the assembly's public key token. The value is a byte array.
                /// </summary>
                PublicKeyToken,

                /// <summary>
                ///     Property ID for a reserved name-value pair. The value is a byte array.
                /// </summary>
                HashValue,

                /// <summary>
                ///     Property ID for the assembly's simple name. The value is a string value.
                /// </summary>
                Name,

                /// <summary>
                ///     Property ID for the assembly's major version. The value is a WORD value.
                /// </summary>
                MajorVersion,

                /// <summary>
                ///     Property ID for the assembly's minor version. The value is a WORD value.
                /// </summary>
                MinorVersion,

                /// <summary>
                ///     Property ID for the assembly's build version. The value is a WORD value.
                /// </summary>
                BuildNumber,

                /// <summary>
                ///     Property ID for the assembly's revision version. The value is a WORD value.
                /// </summary>
                RevisionNumber,

                /// <summary>
                ///     Property ID for the assembly's culture. The value is a string value.
                /// </summary>
                Culture,

                /// <summary>
                ///     Property ID for a reserved name-value pair.
                /// </summary>
                ProcessorIdArray,

                /// <summary>
                ///     Property ID for a reserved name-value pair.
                /// </summary>
                OsInfoArray,

                /// <summary>
                ///     Property ID for a reserved name-value pair. The value is a DWORD value.
                /// </summary>
                HashAlgId,

                /// <summary>
                ///     Property ID for a reserved name-value pair.
                /// </summary>
                Alias,

                /// <summary>
                ///     Property ID for a reserved name-value pair.
                /// </summary>
                CodebaseUrl,

                /// <summary>
                ///     Property ID for a reserved name-value pair. The value is a FILETIME structure.
                /// </summary>
                CodeBaseLastMod,

                /// <summary>
                ///     Property ID for the assembly as a simply named assembly that does not have a public key.
                /// </summary>
                NullPublicKey,

                /// <summary>
                ///     Property ID for the assembly as a simply named assembly that does not have a public key token.
                /// </summary>
                NullPublicKeyToken,

                /// <summary>
                ///     Property ID for a reserved name-value pair. The value is a string value.
                /// </summary>
                Custom,

                /// <summary>
                ///     Property ID for a reserved name-value pair.
                /// </summary>
                NullCustom,

                /// <summary>
                ///     Property ID for a reserved name-value pair.
                /// </summary>
                MvId,

                /// <summary>
                ///     Reserved.
                /// </summary>
                MaxParams
            }
        }
    }
}