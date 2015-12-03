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
    internal static class TravelStationDefinitionLoader
    {
        public static InfoDictionary<TravelStationDefinition> Load(
            InfoDictionary<DownloadableContentDefinition> downloadableContents)
        {
            try
            {
                var raws = LoaderHelper
                    .DeserializeJson<Dictionary<string, Raw.TravelStationDefinition>>("Travel Stations");
                var defs = new InfoDictionary<TravelStationDefinition>(
                    raws.ToDictionary(kv => kv.Key,
                                      kv => GetTravelStationDefinition(downloadableContents, kv)));
                foreach (var kv in raws.Where(kv => string.IsNullOrEmpty(kv.Value.PreviousStation) == false))
                {
                    if (defs.ContainsKey(kv.Value.PreviousStation) == false)
                    {
                        throw new KeyNotFoundException("could not find travel station '" + kv.Value.PreviousStation +
                                                       "'");
                    }

                    defs[kv.Key].PreviousStation = defs[kv.Value.PreviousStation];
                }
                return defs;
            }
            catch (Exception e)
            {
                throw new InfoLoadException("failed to load travel stations", e);
            }
        }

        private static TravelStationDefinition GetTravelStationDefinition(
            InfoDictionary<DownloadableContentDefinition> downloadableContents,
            KeyValuePair<string, Raw.TravelStationDefinition> kv)
        {
            DownloadableContentDefinition dlcExpansion = null;
            if (string.IsNullOrEmpty(kv.Value.DLCExpansion) == false)
            {
                if (downloadableContents.ContainsKey(kv.Value.DLCExpansion) == false)
                {
                    throw new KeyNotFoundException("could not find downloadable content '" + kv.Value.DLCExpansion + "'");
                }
                dlcExpansion = downloadableContents[kv.Value.DLCExpansion];
            }

            if (kv.Value is Raw.FastTravelStationDefinition)
            {
                return GetFastTravelStationDefinition(dlcExpansion, kv);
            }

            if (kv.Value is Raw.LevelTravelStationDefinition)
            {
                return GetLevelTravelStationDefinition(dlcExpansion, kv);
            }

            throw new InvalidOperationException();
        }

        private static TravelStationDefinition GetFastTravelStationDefinition(
            DownloadableContentDefinition dlcExpansion, KeyValuePair<string, Raw.TravelStationDefinition> kv)
        {
            var raw = (Raw.FastTravelStationDefinition)kv.Value;

            return new FastTravelStationDefinition()
            {
                ResourcePath = kv.Key,
                LevelName = raw.LevelName,
                DLCExpansion = dlcExpansion,
                DisplayName = raw.DisplayName,
                MissionDependencies = GetMissionStatusData(raw.MissionDependencies),
                InitiallyActive = raw.InitiallyActive,
                SendOnly = raw.SendOnly,
                Description = raw.Description,
                Sign = raw.Sign,
                InaccessibleObjective = raw.InaccessibleObjective,
                AccessibleObjective = raw.AccessibleObjective,
            };
        }

        private static TravelStationDefinition GetLevelTravelStationDefinition(
            DownloadableContentDefinition dlcExpansion, KeyValuePair<string, Raw.TravelStationDefinition> kv)
        {
            var raw = (Raw.LevelTravelStationDefinition)kv.Value;

            return new LevelTravelStationDefinition()
            {
                ResourcePath = kv.Key,
                LevelName = raw.LevelName,
                DLCExpansion = dlcExpansion,
                DisplayName = raw.DisplayName,
                MissionDependencies = GetMissionStatusData(kv.Value.MissionDependencies),
            };
        }

        private static List<MissionStatusData> GetMissionStatusData(List<Raw.MissionStatusData> raws)
        {
            if (raws == null)
            {
                return null;
            }

            return raws.Select(raw => new MissionStatusData()
            {
                MissionDefinition = raw.MissionDefinition,
                MissionStatus = raw.MissionStatus,
                IsObjectiveSpecific = raw.IsObjectiveSpecific,
                ObjectiveDefinition = raw.ObjectiveDefinition,
                ObjectiveStatus = raw.ObjectiveStatus,
            }).ToList();
        }
    }
}
