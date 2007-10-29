using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using As3c.Swf.Utils;
using As3c.Swf.Types;
using System.Collections;

namespace As3c.Swf.Abc
{
    public class ClassInfo : IExternalizeable, IHasTraits
    {
        protected U30 _cinit;
        protected ArrayList _traits;

        public U30 CInit
        {
            get { return _cinit; }
            set { _cinit = value; }
        }

        public ArrayList Traits
        {
            get { return _traits; }
            set { _traits = value; }
        }

        #region IExternalizeable Members

        public void ReadExternal(BinaryReader input)
        {
            _cinit = Primitives.ReadU30(input);

            uint n = Primitives.ReadU30(input).Value;

            _traits = new ArrayList(Capacity.Max(n));

            for (uint i = 0; i < n; ++i)
            {
                TraitInfo ti = new TraitInfo();
                ti.ReadExternal(input);

                _traits.Add(ti);
            }   
        }

        public void WriteExternal(BinaryWriter output)
        {
            Primitives.WriteU30(output, _cinit);

            int n = _traits.Count;

            Primitives.WriteU30(output, (uint)n);

            for (int i = 0; i < n; ++i)
            {
                ((TraitInfo)_traits[i]).WriteExternal(output);
            }
        }

        #endregion
    }
}
