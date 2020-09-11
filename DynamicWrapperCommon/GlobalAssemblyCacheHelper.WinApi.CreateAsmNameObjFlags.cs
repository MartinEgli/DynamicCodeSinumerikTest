namespace DynamicWrapperCommon
{
    public static partial class GlobalAssemblyCacheHelper
    {
        public static partial class WinApi
        {
            public enum CreateAsmNameObjFlags : uint
            {
                ParseDisplayName = 0x1,
                SetDefaultValues = 0x2,
                VerifyFriendAssemblyName = 0x4,
                ParseFriendDisplayName = ParseDisplayName | VerifyFriendAssemblyName
            }
        }
    }
}