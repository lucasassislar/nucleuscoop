using Newtonsoft.Json;

namespace Nucleus.Gaming.Package {
    /// <summary>
    /// Metadata included in the package file
    /// </summary>
    public class GameHandlerMetadata : GameHandlerBaseMetadata {
        /// <summary>
        /// The context needed to find the executable of the game
        /// </summary>
        public string[] ExeContext { get; set; }

        public string GameTitle { get; set; }

        /// <summary>
        /// A description for the game handler
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// A list of paths to screenshots inside the game package
        /// </summary>
        public string[] Screenshots { get; set; }

        public string DevDescription { get; set; }

        /// <summary>
        /// Used so we dont have to compute the path to the *.js handler,
        /// as this could just be a debug file and we shouldnt enforce installation
        /// </summary>
        [JsonIgnore]
        public string RootDirectory { get; set; }


        public static int CompareGameTitle(GameHandlerMetadata x, GameHandlerMetadata y) {
            return x.GameTitle.CompareTo(y.GameTitle);
        }
        public static int CompareHandlerTitle(GameHandlerMetadata x, GameHandlerMetadata y) {
            return x.Title.CompareTo(y.Title);
        }
    }
}
