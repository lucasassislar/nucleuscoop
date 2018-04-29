using Newtonsoft.Json;
using Nucleus.Gaming.Coop.Interop;
using Nucleus.Gaming.Package;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Coop
{
    public class HandlerDataManager : IDisposable
    {
        private HandlerDataEngine jsEngine;
        private HandlerData handlerData;
        private GameHandlerMetadata handlerMetadata;
        private ContentManager content;
        private bool isDisposed;

        public HandlerData HandlerData
        {
            get { return handlerData; }
        }

        public HandlerDataEngine Engine
        {
            get { return jsEngine; }
        }

        public ContentManager Content
        {
            get { return content; }
        }

        public HandlerDataManager(GameHandlerMetadata metadata, string jsCode)
        {
            Initialize(metadata, jsCode);
        }

        public HandlerDataManager(GameHandlerMetadata metadata, Stream stream)
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                Initialize(metadata, reader.ReadToEnd());
            }
        }

        private void Initialize(GameHandlerMetadata metadata, string jsCode)
        {
            this.handlerMetadata = metadata;

            jsEngine = new HandlerDataEngine(metadata, jsCode);

            string handlerStr = jsEngine.Initialize();
            handlerData = JsonConvert.DeserializeObject<HandlerData>(handlerStr);

            // content manager is shared withing the same game
            content = new ContentManager(metadata, handlerData);
        }

        public void Dispose()
        {
            if (isDisposed)
            {
                return;
            }
            isDisposed = true;

            jsEngine.Dispose();

            content.Dispose();
        }

        public void Play(HandlerContext context, PlayerInfo player)
        {
            // ugly solution
            context.PackageFolder = content.PackageFolder;
            string contextData = Engine.Play(context, player);

            JsonConvert.PopulateObject(contextData, context);
        }
    }
}
