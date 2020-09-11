using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DynamicWrapperCommon
{
    public static class SourceLoaderHelper
    {
        public const string CSharpFileExtension = ".cs";

        public static string LoadEmbeddedSource(this object self, string file)
        {
            return LoadEmbeddedSource(file, self.GetType().Assembly);
        }

        public static string LoadEmbeddedSource(string file, Assembly assembly)
        {
            var resourceName = assembly.GetName().Name + "." + file;

            string resource;
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    return null;
                }

                var reader = new StreamReader(stream);
                resource = reader.ReadToEnd();
            }
            return resource;
        }
        
        public static IEnumerable<string> LoadEmbeddedSources(this object self)
        {
            if (self == null)
            {
                throw new ArgumentNullException(nameof(self));
            }
            return LoadEmbeddedSources(CSharpFileExtension, self.GetType().Assembly);
        }

        public static IEnumerable<string> LoadEmbeddedSources(this object self, string extension)
        {
            if (self == null)
            {
                throw new ArgumentNullException(nameof(self));
            }
            return LoadEmbeddedSources(extension, self.GetType().Assembly);
        }

        public static IEnumerable<string> LoadEmbeddedSources(string extension, Assembly assembly)
        {
            var resourceNames = assembly.GetManifestResourceNames()
                .Where(n => n.EndsWith(extension))
                .ToList();

            var results = new List<string>();
            foreach (var resourceName in resourceNames)
            {
                string resource;
                using (var stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                    {
                        continue;
                    }

                    var reader = new StreamReader(stream);
                    resource = reader.ReadToEnd();
                }
                results.Add(resource);
            }

            return results;
        }
    }
}