using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming
{
    public class UserGameProfile
    {
        public List<PlayerInfo> Preset;
        public Dictionary<string, GameOption> Options;

        public static UserGameProfile Read(BinaryReader reader)
        {
            UserGameProfile prof = new UserGameProfile();
            int presets = reader.ReadInt32();

            prof.Preset = new List<PlayerInfo>();
            int players = reader.ReadInt32();
            for (int j = 0; j < players; j++)
            {
                PlayerInfo info = new PlayerInfo();
                prof.Preset.Add(info);

                info.Player = reader.ReadInt32();
                info.ScreenIndex = reader.ReadInt32();
                info.ScreenType = (ScreenType)reader.ReadInt32();
                info.Size.Width = reader.ReadInt32();
                info.Size.Height = reader.ReadInt32();
            }

            bool options = reader.ReadBoolean();
            if (options)
            {
                int count = reader.ReadInt32();

                prof.Options = new Dictionary<string, GameOption>();
                for (int i = 0; i < count; i++)
                {
                    string key = reader.ReadString();
                    string value = reader.ReadString();
                    GameOption opt = new GameOption("", "", value);
                    prof.Options.Add(key, opt);
                }
            }


            return prof;
        }

        public static void Write(BinaryWriter writer, UserGameProfile prof)
        {
            List<PlayerInfo> players = prof.Preset;
            writer.Write(players.Count);
            for (int j = 0; j < players.Count; j++)
            {
                PlayerInfo info = players[j];
                writer.Write(info.Player);
                writer.Write(info.ScreenIndex);
                writer.Write((int)info.ScreenType);
                writer.Write(info.Size.Width);
                writer.Write(info.Size.Height);
            }

            var options = prof.Options;
            writer.Write(options != null);
            if (options != null)
            {
                writer.Write(options.Count);

                foreach (var p in options)
                {
                    writer.Write(p.Key);
                    object ob = p.Value.Value;
                    // I Sure as hell know this ain't no way to serialize this type of object,
                    // but currently I cannot insert my Game Engine to this project, as we did not launch it yet.
                    // When we do launch, this will work perfeclty as I will be using it's system for serializating
                    writer.Write(ob.ToString());
                }
            }
        }
    }
}
