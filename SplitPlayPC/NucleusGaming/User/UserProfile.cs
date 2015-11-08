using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming
{
    public class UserProfile
    {
        public List<UserGameInfo> Games;

        public UserProfile()
        {
            Games = new List<UserGameInfo>();
        }

        public static readonly float Version = 0.1f;

        public static UserProfile Read(BinaryReader reader)
        {
            UserProfile profile = new UserProfile();

            char[] nuke = reader.ReadChars(4);
            if (nuke[0] != 'N' || nuke[1] != 'U' ||
                nuke[2] != 'K' || nuke[3] != 'E')
            {
                throw new NotSupportedException();
            }

            char[] split = reader.ReadChars(5);
            // I'm freaking lazy and dont want to write the code for checking that

            float version = reader.ReadSingle();
            if (version > Version)
            {
                throw new NotSupportedException();
            }

            bool games = reader.ReadBoolean();
            if (games)
            {
                int count = reader.ReadInt32();
                profile.Games = new List<UserGameInfo>();
                for (int i = 0; i < count; i++)
                {
                    UserGameInfo game = UserGameInfo.Read(reader);
                    profile.Games.Add(game);
                }
            }

            return profile;
        }

        public static void Write(BinaryWriter writer, UserProfile info)
        {
            writer.Write(new char[] { 'N', 'U', 'K', 'E' });
            writer.Write(new char[] { 'S', 'P', 'L', 'I', 'T' });

            writer.Write(Version);

            var games = info.Games;
            writer.Write(games != null);
            if (games != null)
            {
                writer.Write(games.Count);
                for (int i = 0; i < games.Count; i++)
                {
                    var game = games[i];
                    UserGameInfo.Write(writer, game);
                }
            }
        }
    }
}
