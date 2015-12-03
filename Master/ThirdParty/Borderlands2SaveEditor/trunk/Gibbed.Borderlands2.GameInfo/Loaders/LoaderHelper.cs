/* Copyright (c) 2012 Rick (rick 'at' gibbed 'dot' us)
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 * 
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 * 
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace Gibbed.Borderlands2.GameInfo.Loaders
{
    internal static class LoaderHelper
    {
        public static UnmanagedMemoryStream GetUnmanagedMemoryStream(string embeddedResourceName)
        {
            if (embeddedResourceName == null)
            {
                throw new ArgumentNullException();
            }

            var path = "Gibbed.Borderlands2.GameInfo.Resources." + embeddedResourceName + ".json";

            var assembly = Assembly.GetExecutingAssembly();
            var stream = (UnmanagedMemoryStream)assembly.GetManifestResourceStream(path);
            if (stream == null)
            {
                throw new ArgumentException("The specified embedded resource could not be found.",
                                            "embeddedResourceName");
            }
            return stream;
        }

        public static TType DeserializeJson<TType>(string embeddedResourceName)
        {
            var settings = new JsonSerializerSettings()
            {
                MissingMemberHandling = MissingMemberHandling.Error,
                TypeNameHandling = TypeNameHandling.Auto,
                Binder =
                    new TypeNameSerializationBinder("Gibbed.Borderlands2.GameInfo.Raw.{0}, Gibbed.Borderlands2.GameInfo")
            };
            settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

            try
            {
                var serializer = JsonSerializer.Create(settings);

                using (var input = GetUnmanagedMemoryStream(embeddedResourceName))
                using (var textReader = new StreamReader(input))
                using (var jsonReader = new JsonTextReader(textReader))
                {
                    return serializer.Deserialize<TType>(jsonReader);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
