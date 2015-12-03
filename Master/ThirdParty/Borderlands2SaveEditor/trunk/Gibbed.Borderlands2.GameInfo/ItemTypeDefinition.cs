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

namespace Gibbed.Borderlands2.GameInfo
{
    public sealed class ItemTypeDefinition
    {
        internal ItemTypeDefinition()
        {
        }

        public string ResourcePath { get; internal set; }
        public ItemType Type { get; internal set; }
        public string Name { get; internal set; }
        public List<string> Titles { get; internal set; }
        public List<string> Prefixes { get; internal set; }
        public List<string> AlphaParts { get; internal set; }
        public List<string> BetaParts { get; internal set; }
        public List<string> GammaParts { get; internal set; }
        public List<string> DeltaParts { get; internal set; }
        public List<string> EpsilonParts { get; internal set; }
        public List<string> ZetaParts { get; internal set; }
        public List<string> EtaParts { get; internal set; }
        public List<string> ThetaParts { get; internal set; }
        public List<string> MaterialParts { get; internal set; }
    }
}
