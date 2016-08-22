using System;

namespace DotaSharp
{
    public class Creep : Unit
    {
        #region Constructors

        internal Creep(IntPtr entityPointer) : base(entityPointer)
        {
        }

        public Creep()
        {
        }

        #endregion
    }
}