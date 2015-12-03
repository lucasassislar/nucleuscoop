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

#pragma warning disable 649

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Gibbed.Borderlands2.GameInfo.Raw
{
    [JsonObject(MemberSerialization.OptIn)]
    internal sealed class WeaponBalancePartCollection
    {
        internal WeaponBalancePartCollection()
        {
            this.Mode = PartReplacementMode.Unknown;
            this.BodyParts = null;
            this.GripParts = null;
            this.BarrelParts = null;
            this.SightParts = null;
            this.StockParts = null;
            this.ElementalParts = null;
            this.Accessory1Parts = null;
            this.Accessory2Parts = null;
            this.MaterialParts = null;
        }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "mode")]
        public PartReplacementMode Mode { get; set; }

        [JsonProperty(PropertyName = "body")]
        public List<string> BodyParts { get; set; }

        [JsonProperty(PropertyName = "grip")]
        public List<string> GripParts { get; set; }

        [JsonProperty(PropertyName = "barrel")]
        public List<string> BarrelParts { get; set; }

        [JsonProperty(PropertyName = "sight")]
        public List<string> SightParts { get; set; }

        [JsonProperty(PropertyName = "stock")]
        public List<string> StockParts { get; set; }

        [JsonProperty(PropertyName = "elemental")]
        public List<string> ElementalParts { get; set; }

        [JsonProperty(PropertyName = "accessory1")]
        public List<string> Accessory1Parts { get; set; }

        [JsonProperty(PropertyName = "accessory2")]
        public List<string> Accessory2Parts { get; set; }

        [JsonProperty(PropertyName = "material")]
        public List<string> MaterialParts { get; set; }
    }
}

#pragma warning restore 649
