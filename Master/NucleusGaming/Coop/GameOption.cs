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
        public object DefaultValue { get; set; }
        private string name;
        private string description;
        private object value;
        private string key;
        private IList list;

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

        public IList List { get { return list; } }
        public bool Hidden { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="desc"></param>
        /// <param name="value"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        public GameOption(string name, string desc, string key, object value)
        {
            this.name = name;
            this.description = desc;
            this.value = value;
            this.key = key;
            if (value is IList)
            {
                this.list = (IList)value;
                this.value = 0;
            }
        }

        public GameOption(string name, string desc, string key, object value, object defaultValue)
        {
            DefaultValue = defaultValue;
            this.name = name;
            this.description = desc;
            this.value = value;
            this.key = key;
            if (value is IList)
            {
                this.list = (IList)value;
                this.value = 0;
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
    }
}
