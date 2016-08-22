using System;

namespace DotaSharp
{
    public class Tower : Building
    {
        #region Constructors

        internal Tower(IntPtr entityPointer) : base(entityPointer)
        {
        }

        public Tower()
        {
        }

        #endregion
    }
}