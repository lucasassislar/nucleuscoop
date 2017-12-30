using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Coop
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
        private IList dataSet; // should we create a new GameOption for Collection options?
        private object defaultValue;

        /// <summary>
        /// The value to default to when no value was set by the user
        /// </summary>
        public object DefaultValue
        {
            get { return defaultValue; }
            set { defaultValue = value; }
        }

        /// <summary>
        /// The name of the option
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// The description of the option
        /// </summary>
        public string Description
        {
            get { return description; }
        }

        /// <summary>
        /// The current value of option
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

        /// <summary>
        /// If this option should be shown to the user
        /// </summary>
        public bool Hidden { get; set; }

        /// <summary>
        /// Initializes a new instance of GameOption, with no default value
        /// </summary>
        /// <param name="name"></param>
        /// <param name="desc"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public GameOption(string name, string desc, string key, object value)
            : this(name, desc, key, value, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of GameOption
        /// </summary>
        /// <param name="name">The name of the option (what will be shown to the user)</param>
        /// <param name="desc">The description of the option (what will be shown to the user)</param>
        /// <param name="key">The key for accesing this game option</param>
        /// <param name="value">The value </param>
        /// <param name="defaultValue"></param>
        public GameOption(string name, string desc, string key, object value, object defaultValue)
        {
            this.name = name;
            this.description = desc;
            this.value = value;
            this.key = key;

            if (value is IList)
            {
                this.dataSet = (IList)value;
                this.value = 0;
            }
            else
            {
                this.defaultValue = defaultValue;
            }
        }

        public GameOption Instantiate()
        {
            return new GameOption(this.Name, this.Description, this.Key, this.Value);
        }

        public override string ToString()
        {
            return string.Format("{0} : {1}", Key, Value);
        }

        public bool IsCollection()
        {
            return dataSet != null;
        }

        public IList GetCollection()
        {
            return dataSet;
        }

        public T GetValue<T>()
        {
            return (T)value;
        }
    }
}
