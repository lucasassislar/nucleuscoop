using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SplitScreenMe {
    public interface IGameOption {
        /// <summary>
        /// The name of the variable
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The description of the variable
        /// </summary>
        string Description { get; }

        /// <summary>
        /// The value of the variable
        /// </summary>
        object Value { get; set; }

        /// <summary>
        /// The key to this variable
        /// </summary>
        string Key { get; }

        IList List { get; }
        bool Hidden { get; set; }
    }
}
