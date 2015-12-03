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

using System.Collections.Generic;
using System.Linq;
using Gibbed.Borderlands2.GameInfo;

namespace Gibbed.Borderlands2.SaveEdit
{
    internal static class AssetCache
    {
        private static readonly object _AssetCacheLock;
        private static readonly Dictionary<int, AssetLibraryCache> _Libraries;

        static AssetCache()
        {
            _AssetCacheLock = new object();
            _Libraries = new Dictionary<int, AssetLibraryCache>();
        }

        public static AssetLibraryCache Get(int setId)
        {
            lock (_AssetCacheLock)
            {
                if (_Libraries.ContainsKey(setId) == false)
                {
                    return _Libraries[setId] = new AssetLibraryCache(setId);
                }

                return _Libraries[setId];
            }
        }

        public class AssetLibraryCache
        {
            private readonly int _SetId;
            private readonly object _AssetLibraryCacheLock;
            private readonly Dictionary<AssetGroup, string[]> _Assets = new Dictionary<AssetGroup, string[]>();

            public AssetLibraryCache(int setId)
            {
                this._AssetLibraryCacheLock = new object();
                this._SetId = setId;
            }

            private static string[] GetMergedAssets(AssetGroup group, int setId)
            {
                IEnumerable<string> assets;

                assets = InfoManager.AssetLibraryManager.GetSet(0).Libraries[group].GetAssets();
                if (setId != 0)
                {
                    assets = assets.Concat(InfoManager.AssetLibraryManager.GetSet(setId).Libraries[group].GetAssets());
                }

                return assets.Distinct().OrderBy(p => p).ToArray();
            }

            public string[] this[AssetGroup group]
            {
                get
                {
                    lock (this._AssetLibraryCacheLock)
                    {
                        if (this._Assets.ContainsKey(group) == false)
                        {
                            return this._Assets[group] = GetMergedAssets(group, this._SetId);
                        }

                        return this._Assets[group];
                    }
                }
            }
        }
    }
}
