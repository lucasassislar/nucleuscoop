using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Coop.Js
{
    public class JsHandlerEngine
    {
        private Dictionary<string, string> jsData;

        public JsHandlerEngine()
        {
        }

        public void Initialize(GenericHandlerData handlerData, UserGameInfo game, GameProfile profile)
        {
            jsData = new Dictionary<string, string>();
            jsData.Add(Folder.Documents.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            jsData.Add(Folder.MainGameFolder.ToString(), Path.GetDirectoryName(game.ExePath));
            jsData.Add(Folder.InstancedGameFolder.ToString(), Path.GetDirectoryName(game.ExePath));
        }
    }

}
