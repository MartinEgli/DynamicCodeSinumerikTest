using System;
using System.Reflection;

namespace DynamicSinumerikWrapper
{
    public static class InvokeHelper
    {

        public static ConstructorInfo GetConstructor(this Type type) => type.GetConstructor(Type.EmptyTypes);

        public static object Invoke(this ConstructorInfo info) => info.Invoke(EmptyParameters());

        public static T Invoke<T>(this ConstructorInfo info) where T : class => info.Invoke() as T;

        public static object[] EmptyParameters() => new object[] { };
    }
}