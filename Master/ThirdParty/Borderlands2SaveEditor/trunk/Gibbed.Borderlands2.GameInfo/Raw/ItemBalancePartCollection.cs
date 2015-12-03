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
    internal sealed class ItemBalancePartCollection
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "mode")]
        public PartReplacementMode Mode { get; set; }

        [JsonProperty(PropertyName = "alpha")]
        public List<string> AlphaParts { get; set; }

        [JsonProperty(PropertyName = "beta")]
        public List<string> BetaParts { get; set; }

        [JsonProperty(PropertyName = "gamma")]
        public List<string> GammaParts { get; set; }

        [JsonProperty(PropertyName = "delta")]
        public List<string> DeltaParts { get; set; }

        [JsonProperty(PropertyName = "epsilon")]
        public List<string> EpsilonParts { get; set; }

        [JsonProperty(PropertyName = "zeta")]
        public List<string> ZetaParts { get; set; }

        [JsonProperty(PropertyName = "eta")]
        public List<string> EtaParts { get; set; }

        [JsonProperty(PropertyName = "theta")]
        public List<string> ThetaParts { get; set; }

        [JsonProperty(PropertyName = "material")]
        public List<string> MaterialParts { get; set; }
    }
}

#pragma warning restore 649
