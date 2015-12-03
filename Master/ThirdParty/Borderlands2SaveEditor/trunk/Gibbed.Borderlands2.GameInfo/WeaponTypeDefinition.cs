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
    public sealed class WeaponTypeDefinition
    {
        internal WeaponTypeDefinition()
        {
        }

        public string ResourcePath { get; internal set; }
        public WeaponType Type { get; internal set; }
        public string Name { get; internal set; }
        public List<string> Titles { get; internal set; }
        public List<string> Prefixes { get; internal set; }
        public List<string> BodyParts { get; internal set; }
        public List<string> GripParts { get; internal set; }
        public List<string> BarrelParts { get; internal set; }
        public List<string> SightParts { get; internal set; }
        public List<string> StockParts { get; internal set; }
        public List<string> ElementalParts { get; internal set; }
        public List<string> Accessory1Parts { get; internal set; }
        public List<string> Accessory2Parts { get; internal set; }
        public List<string> MaterialParts { get; internal set; }
    }
}
