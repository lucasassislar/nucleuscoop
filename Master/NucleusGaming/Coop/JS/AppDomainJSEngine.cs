using Jint;
using Jint.Runtime.Interop;
using Newtonsoft.Json;
using Nucleus.Gaming.Package;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Coop.JS
{
    [Serializable]
    public class AppDomainJSEngine
    {
        private Engine engine;
        private HandlerData hData;

        private GameHandlerMetadata gameMetadata;

        public AppDomainJSEngine()
        {

        }

        public void Import(string path)
        {
            // TODO: IO Manager
            string folderPath = PackageManager.GetAssetsFolder(gameMetadata);
            string fullPath = Path.Combine(folderPath, path);

            string jsCode = File.ReadAllText(fullPath);
            engine.Execute(jsCode);
        }

        public string Initialize(string metadata, string metadataRootfolder, string jsCode)
        {
            hData = new HandlerData();

            engine = new Engine();

            gameMetadata = JsonConvert.DeserializeObject<GameHandlerMetadata>(metadata);
            gameMetadata.RootDirectory = metadataRootfolder;

            engine.SetValue("SaveType", TypeReference.CreateTypeReference(engine, typeof(SaveType)));
            engine.SetValue("DPIHandling", TypeReference.CreateTypeReference(engine, typeof(DPIHandling)));
            engine.SetValue("Folder", TypeReference.CreateTypeReference(engine, typeof(Folder)));
            engine.SetValue("SaveType", TypeReference.CreateTypeReference(engine, typeof(SaveType)));

            engine.SetValue("Game", hData);
            engine.SetValue("Import", (Action<string>)Import);

            engine.Execute(jsCode);

            return JsonConvert.SerializeObject(hData);
        }

        public string Play(string contextData, string playerInfo)
        {
            HandlerContext context = JsonConvert.DeserializeObject<HandlerContext>(contextData);
            PlayerInfo player = JsonConvert.DeserializeObject<PlayerInfo>(playerInfo);

            engine.SetValue("Context", context);
            engine.SetValue("Player", player);
            engine.SetValue("Game", hData);

            hData.OnPlay.Invoke();

            return JsonConvert.SerializeObject(context);
        }
    }
}
