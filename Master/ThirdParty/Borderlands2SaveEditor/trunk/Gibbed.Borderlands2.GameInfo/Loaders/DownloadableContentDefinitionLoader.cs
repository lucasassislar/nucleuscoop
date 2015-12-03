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
using System.Collections.Generic;
using System.Linq;

namespace Gibbed.Borderlands2.GameInfo.Loaders
{
    internal static class DownloadableContentDefinitionLoader
    {
        public static InfoDictionary<DownloadableContentDefinition> Load(
            InfoDictionary<DownloadablePackageDefinition> downloadablePackages)
        {
            try
            {
                var raws =
                    LoaderHelper.DeserializeJson<Dictionary<string, Raw.DownloadableContentDefinition>>(
                        "Downloadable Contents");
                return
                    new InfoDictionary<DownloadableContentDefinition>(raws.ToDictionary(kv => kv.Key,
                                                                                        kv =>
                                                                                        GetDownloadableContent(
                                                                                            downloadablePackages, kv)));
            }
            catch (Exception e)
            {
                throw new InfoLoadException("failed to load downloadable contents", e);
            }
        }

        private static DownloadableContentDefinition GetDownloadableContent(
            InfoDictionary<DownloadablePackageDefinition> downloadablePackages,
            KeyValuePair<string, Raw.DownloadableContentDefinition> kv)
        {
            DownloadablePackageDefinition package = null;
            if (string.IsNullOrEmpty(kv.Value.Package) == false)
            {
                if (downloadablePackages.ContainsKey(kv.Value.Package) == false)
                {
                    throw new KeyNotFoundException("could not find downloadable package " + kv.Value.Package);
                }
                package = downloadablePackages[kv.Value.Package];
            }

            return new DownloadableContentDefinition()
            {
                ResourcePath = kv.Key,
                Id = kv.Value.Id,
                Name = kv.Value.Name,
                Package = package,
                Type = kv.Value.Type,
            };
        }
    }
}
