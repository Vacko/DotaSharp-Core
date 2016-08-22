using System;

namespace DotaSharp
{
    public class Building : Unit
    {
        #region Constructors

        internal Building(IntPtr entityPointer) : base(entityPointer)
        {
        }

        public Building()
        {
        }

        #endregion
    }
}