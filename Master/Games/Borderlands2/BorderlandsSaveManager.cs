using Gibbed.Borderlands2.FileFormats;
using Nucleus.Gaming;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Games.Borderlands
{
    public class BorderlandsSaveManager : UserControl, IUserInputForm
    {
        public BorderlandsSaveManager()
        {
        }

        public bool CanProceed
        {
            get { return true; }
        }

        public bool CanPlay
        {
            get { return true; }
        }

        public event Action Proceed;

        public string Title
        {
            get { return "Save Manager"; }
        }

        public void Initialize(UserGameInfo game, GameProfile profile)
        {
            string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string configFolder = Path.Combine(documents, @"My Games\Borderlands 2\WillowGame\SaveData");
            DirectoryInfo[] userDirs = new DirectoryInfo(configFolder).GetDirectories();

            for (int i = 0; i < userDirs.Length; i++)
            {
                DirectoryInfo user = userDirs[i];

                FileInfo[] saves = user.GetFiles("*.sav");
                for (int j = 0; j < saves.Length; j++)
                {
                    FileInfo save = saves[j];

                    BorderlandsSaveControl con = new BorderlandsSaveControl();
                    using (Stream s = save.OpenRead())
                    {
                        con.SaveFile = SaveFile.Deserialize(s, SaveFile.DeserializeSettings.None);
                    }

                    con.UserName = user.Name;
                    //con.SaveName = con.SaveFile.SaveGame.AppliedCustomizations;
                    this.flowLayoutPanel1.Controls.Add(con);
                }
            }
        }
    }
}
