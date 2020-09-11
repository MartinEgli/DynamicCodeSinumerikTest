namespace DynamicWrapperCommon
{
    public static partial class GlobalAssemblyCacheHelper
    {
        public static partial class WinApi
        {
            public enum CacheFlags : uint
            {
                AsmCacheZap = 0x01,
                AsmCacheGac = 0x02,
                AsmCacheDownload = 0x04,
                AsmCacheRoot = 0x08,
                AsmCacheRootEx = 0x80
            }
        }
    }
}