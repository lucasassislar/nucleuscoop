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

namespace Gibbed.Borderlands2.GameInfo
{
    public sealed class InfoDictionary<TType>
    {
        private readonly Dictionary<string, TType> _Dictionary;

        public InfoDictionary(Dictionary<string, TType> dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }

            this._Dictionary = new Dictionary<string, TType>();
            foreach (var kv in dictionary)
            {
                this._Dictionary.Add(kv.Key, kv.Value);
            }
        }

        public bool ContainsKey(string key)
        {
            return this._Dictionary.ContainsKey(key);
        }

        public TType this[string key]
        {
            get
            {
                if (this._Dictionary.ContainsKey(key) == false)
                {
                    throw new KeyNotFoundException();
                }

                return this._Dictionary[key];
            }
        }

        public IEnumerable<KeyValuePair<string, TType>> Items
        {
            get { return this._Dictionary; }
        }
    }
}
