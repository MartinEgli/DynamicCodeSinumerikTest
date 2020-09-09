using SinumerikWrapperInterfaces;

namespace StaticSinumerikWrapper
{
    public class StaticSinumerikWrapperProvider
    {
        public static StaticSinumerikWrapperProvider Instance { get; } = new StaticSinumerikWrapperProvider();

        public ISinumerikWrapper SinumerikWrapper { get; } = new SinumerikWrapper();
    }
}