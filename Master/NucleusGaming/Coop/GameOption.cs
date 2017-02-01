namespace Nucleus.Gaming
{
    /// <summary>
    /// A custom game option that can be modified by the user
    /// </summary>
    public class GameOption
    {
        private string name;
        private string description;
        private object value;
        private string key;

        /// <summary>
        /// The name of the variable
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// The description of the variable
        /// </summary>
        public string Description
        {
            get { return description; }
        }

        /// <summary>
        /// The value of the variable
        /// </summary>
        public object Value
        {
            get { return value; }
            set { this.value = value; }
        }

        /// <summary>
        /// The key to this variable
        /// </summary>
        public string Key
        {
            get { return key; }
        }

        public GameOption(string name, string desc, object value, string key)
        {
            this.name = name;
            this.description = desc;
            this.value = value;
            this.key = key;
        }

        public GameOption Instantiate()
        {
            return new GameOption(this.Name, this.Description, this.Value, this.Key);
        }

        public override string ToString()
        {
            return string.Format("{0} : {1}", Key, Value);
        }
    }
}
