using System;

namespace DotaSharp
{
    public class Courier : Unit
    {
        #region Constructors

        internal Courier(IntPtr entityPointer) : base(entityPointer)
        {
        }

        public Courier()
        {
        }

        #endregion
    }
}