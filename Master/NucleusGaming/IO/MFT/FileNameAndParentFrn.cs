#if UNSAFE

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.ComponentModel;

namespace Nucleus.Gaming
{

    public class FileNameAndParentFrn
    {
        #region Properties
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private UInt64 _parentFrn;
        public UInt64 ParentFrn
        {
            get { return _parentFrn; }
            set { _parentFrn = value; }
        }
        #endregion

        #region Constructor
        public FileNameAndParentFrn(string name, UInt64 parentFrn)
        {
            if (name != null && name.Length > 0)
            {
                _name = name;
            }
            else
            {
                throw new ArgumentException("Invalid argument: null or Length = zero", "name");
            }
            if (!(parentFrn < 0))
            {
                _parentFrn = parentFrn;
            }
            else
            {
                throw new ArgumentException("Invalid argument: less than zero", "parentFrn");
            }
        }
        #endregion

    }
}
#endif