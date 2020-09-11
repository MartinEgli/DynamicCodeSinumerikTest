using System;
using System.Reflection;
using DynamicWrapperCommon.Exceptions;
using JetBrains.Annotations;

namespace DynamicWrapperCommon
{
    public static class InvokeHelper
    {

        /// <summary>
        /// Gets the default constructor.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// The ConstructorInfo of the default constructor.
        /// </returns>
        /// <exception cref="ArgumentNullException">Type is null.</exception>
        /// <exception cref="GetDefaultConstructorException"></exception>
        [NotNull]
        public static ConstructorInfo GetConstructor([NotNull] this Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            return type.GetConstructor(Type.EmptyTypes) ?? throw new GetDefaultConstructorException(type);
        }

        /// <summary>
        /// Gets a instance.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">type</exception>
        public static object GetInstance([NotNull] this Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            var ctor = type.GetConstructor();
            return ctor.Invoke();
        }

        /// <summary>
        /// Gets a instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">type</exception>
        [NotNull]
        public static T GetInstance<T>([NotNull] this Type type) where T : class
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            var ctor = type.GetConstructor();
            return ctor.Invoke<T>();
        }

        /// <summary>
        /// Invokes the default ctor.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <returns>
        /// The instance of the constructor.
        /// </returns>
        /// <exception cref="ArgumentNullException">info</exception>
        [NotNull] 
        public static object Invoke([NotNull] this ConstructorInfo info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            return info.Invoke(EmptyParameters());
        }

        /// <summary>
        /// Invokes the default ctor and cast it to the generic type.
        /// </summary>
        /// <typeparam name="T">Target type.</typeparam>
        /// <param name="info">The information.</param>
        /// <returns>
        /// The instance of the constructor as type of T.
        /// </returns>
        /// <exception cref="ArgumentNullException">info</exception>
        [NotNull]
        public static T Invoke<T>([NotNull] this ConstructorInfo info) where T : class
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            return info.Invoke(EmptyParameters()) as T ?? throw new InvokeCastException(info.DeclaringType, typeof(T)); 
        }

        /// <summary>
        /// Empty parameters.
        /// </summary>
        /// <returns></returns>
        public static object[] EmptyParameters() => new object[] { };
    }
}