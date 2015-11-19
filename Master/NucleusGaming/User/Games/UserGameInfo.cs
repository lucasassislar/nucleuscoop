using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming
{
    public class UserGameInfo
    {
        public string GameName;
        public string ExecutablePath;
        public List<UserGameProfile> Profiles;
        public string GameGuid;

        public static UserGameInfo Read(BinaryReader reader)
        {
            UserGameInfo prof = new UserGameInfo();

            prof.GameName = reader.ReadString();
            prof.ExecutablePath = reader.ReadString();
            prof.GameGuid = reader.ReadString();

            bool profiles = reader.ReadBoolean();
            if (profiles)
            {
                int count = reader.ReadInt32();
                prof.Profiles = new List<UserGameProfile>();
                for (int i = 0; i < count; i++)
                {
                    UserGameProfile profile = UserGameProfile.Read(reader);
                    prof.Profiles.Add(profile);
                }
            }

            return prof;
        }

        public override string ToString()
        {
            return GameName;
        }

        public static void Write(BinaryWriter writer, UserGameInfo info)
        {
            writer.Write(info.GameName);
            writer.Write(info.ExecutablePath);
            writer.Write(info.GameGuid);

            var profiles = info.Profiles;
            writer.Write(profiles != null);
            if (profiles != null)
            {
                writer.Write(profiles.Count);
                for (int i = 0; i < profiles.Count; i++)
                {
                    var prof = profiles[i];
                    UserGameProfile.Write(writer, prof);
                }
            }
        }
    }
}
