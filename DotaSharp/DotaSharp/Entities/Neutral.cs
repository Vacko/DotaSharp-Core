using System;

namespace DotaSharp
{
    public class Neutral : Unit
    {
        #region Constructors

        internal Neutral(IntPtr entityPointer) : base(entityPointer)
        {
        }

        public Neutral()
        {
        }

        #endregion
    }
}