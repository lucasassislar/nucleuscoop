using Newtonsoft.Json;
using SplitScreenMe.Core.Interop;
using Nucleus.Gaming.Package;
using System;
using System.IO;

namespace SplitScreenMe.Core {
    public class HandlerDataManager : IDisposable {
        private GameHandlerMetadata handlerMetadata;
        private bool isDisposed;

        public HandlerData HandlerData { get; private set; }

        public HandlerDataEngine Engine { get; private set; }

        public ContentManager Content { get; private set; }

        public HandlerDataManager(GameHandlerMetadata metadata, string jsCode) {
            Initialize(metadata, jsCode);
        }

        public HandlerDataManager(GameHandlerMetadata metadata, Stream stream) {
            using (StreamReader reader = new StreamReader(stream)) {
                Initialize(metadata, reader.ReadToEnd());
            }
        }

        private void Initialize(GameHandlerMetadata metadata, string jsCode) {
            this.handlerMetadata = metadata;

            Engine = new HandlerDataEngine(metadata, jsCode);

            string handlerStr = Engine.Initialize();
            HandlerData = JsonConvert.DeserializeObject<HandlerData>(handlerStr);

            // content manager is shared within the same game
            Content = new ContentManager(metadata, HandlerData);
        }

        public void Dispose() {
            if (isDisposed) {
                return;
            }
            isDisposed = true;

            Engine.Dispose();

            Content.Dispose();
        }

        public void Play(HandlerContext context, PlayerInfo player) {
            // ugly solution
            context.PackageFolder = Content.PackageFolder;
            string contextData = Engine.Play(context, player);

            JsonConvert.PopulateObject(contextData, context);
        }
    }
}
