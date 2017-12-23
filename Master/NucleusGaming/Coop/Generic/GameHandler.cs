using Nucleus.Gaming.Coop.Generic.Cursor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Coop.Generic
{
    public class GameHandler
    {
        private UserGameInfo _userGame;
        private GameProfile _profile;
        private GenericHandlerData _handlerData;

        private CursorModule _cursorModule;

        private Dictionary<string, string> jsData;

        public bool Initialize(GenericHandlerData handlerData, UserGameInfo userGameInfo, GameProfile profile)
        {
            this._handlerData = handlerData;
            this._userGame = userGameInfo;
            this._profile = profile;

            if (this._handlerData.LockMouse)
            {
                _cursorModule = new CursorModule();
            }

            jsData = new Dictionary<string, string>();
            jsData.Add(Folder.Documents.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            jsData.Add(Folder.MainGameFolder.ToString(), Path.GetDirectoryName(userGameInfo.ExePath));
            jsData.Add(Folder.InstancedGameFolder.ToString(), Path.GetDirectoryName(userGameInfo.ExePath));

            return true;
        }

        public RequestResult<string> Play()
        {
            var result = new RequestResult<string>();



            return result;
        }
    }
}
