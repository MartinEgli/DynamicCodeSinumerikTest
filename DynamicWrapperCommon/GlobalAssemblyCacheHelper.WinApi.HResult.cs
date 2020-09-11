namespace DynamicWrapperCommon
{
    public static partial class GlobalAssemblyCacheHelper
    {
        public static partial class WinApi
        {
            public enum HResult
            {
                SOk = 0,
                SFalse = 1,
                ENoInterface = unchecked((int)0x80004002),
                ENotImplemented = unchecked((int)0x80004001),
                EFail = unchecked((int)0x80004005)
            }
        }
    }
}