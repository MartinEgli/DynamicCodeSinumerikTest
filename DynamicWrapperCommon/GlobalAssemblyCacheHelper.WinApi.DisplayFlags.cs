namespace DynamicWrapperCommon
{
    public static partial class GlobalAssemblyCacheHelper
    {
        public static partial class WinApi
        {
            /// <summary>
            /// https://docs.microsoft.com/en-us/windows/win32/api/winsxs/ne-winsxs-asm_display_flags
            /// </summary>
            public enum DisplayFlags : uint
            {
                Version = 0x1,
                Culture = 0x2,
                PublicKeyToken = 0x4,
                PublicKey = 0x8,
                Custom = 0x10,
                ProcessorArchitecture = 0x20,
                LanguageId = 0x40,
                Retarget = 0x80,
                ConfigMask = 0x100,
                Mvid = 0x200,
                ContentType = 0x400,
                Full = Version | Culture | PublicKeyToken | Retarget | ProcessorArchitecture | ContentType
            }
        }
    }
}