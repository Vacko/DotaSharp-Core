using System;
using DotaSharp.DotaSharpKernel.EntitySystem;
using DotaSharp.DotaSharpKernel.Offsets;
using DotaSharp.Memory;

namespace DotaSharp
{
    public class Item : Ability
    {
        #region Constructors

        internal Item(IntPtr entityPointer) : base(entityPointer)
        {
        }

        public Item()
        {
        }

        #endregion

        #region Properties

        public Hero Owner
        {
            get
            {
                int getHandle = MemoryManager.ReadInt(EntityPointer + (int) ItemOffsets.OldOwnerEntity);

                return getHandle == -1 ? null : new Hero(EntitySystem.GetEntityFromHandle(getHandle));
            }
        }

        public Hero Purchaser
        {
            get
            {
                int getHandle = MemoryManager.ReadInt(EntityPointer + (int) ItemOffsets.Purchaser);

                return getHandle == -1 ? null : new Hero(EntitySystem.GetEntityFromHandle(getHandle));
            }
        }

        #endregion

        #region Public Methods

        public int CurrentCharges() => MemoryManager.ReadInt(EntityPointer + (int) ItemOffsets.CurrentCharges);

        public int InitialCharges() => MemoryManager.ReadInt(EntityPointer + (int) ItemOffsets.InitialCharges);

        #endregion
    }
}