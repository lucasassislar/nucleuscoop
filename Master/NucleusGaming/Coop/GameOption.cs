using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming
{
    /// <summary>
    /// A custom game option that can be modified by the user
    /// </summary>
    public class GameOption
    {
        /// <summary>
        /// The name of the variable
        /// </summary>
        public string Name;

        /// <summary>
        /// The description of the variable
        /// </summary>
        public string Description;

        /// <summary>
        /// The value of the variable
        /// </summary>
        public object Value;

        public GameOption(string name, string desc, object value)
        {
            this.Name = name;
            this.Description = desc;
            this.Value = value;
        }

        public GameOption Instantiate()
        {
            return new GameOption(this.Name, this.Description, this.Value);
        }
    }
}
