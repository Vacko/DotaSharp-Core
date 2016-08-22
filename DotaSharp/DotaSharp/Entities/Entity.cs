using System;
using DotaSharp.DotaSharp.Enumerations;
using DotaSharp.DotaSharpKernel.EntitySystem;
using DotaSharp.DotaSharpKernel.Misc;
using DotaSharp.DotaSharpKernel.Offsets;
using DotaSharp.Memory;

namespace DotaSharp
{
    public class Entity : BaseEntity
    {
        #region Constructors

        internal Entity(IntPtr entityPointer) : base(entityPointer)
        {
        }

        public Entity()
        {
        }

        #endregion

        #region Properties

        public Vector3 NetworkOrigin => new Vector3(PossitionX(), PossitionY(), PossitionZ());

        #endregion

        #region Public Methods

        public ClassId ClassId() =>
            (ClassId) Enum.Parse(typeof(ClassId), ReadObj.ReadClassFromPointer(EntityPointer), true);

        public string ClassIdName() => ReadObj.ReadClassFromPointer(EntityPointer);

        public LifeState GetLifeState() =>
            (LifeState) MemoryManager.ReadInt(EntityPointer + (int) EntityOffsets.LifeState);

        public Team GetTeam() => (Team) MemoryManager.ReadInt(EntityPointer + (int) EntityOffsets.TeamNum);

        public int Health() => MemoryManager.ReadInt(EntityPointer + (int) EntityOffsets.Health);

        public int Index() => EntitySystem.GetEntityIndex(EntityPointer);

        public int MaxHealth() => MemoryManager.ReadInt(EntityPointer + (int) EntityOffsets.MaxHealth);

        public string Name() => ReadObj.ReadNameFromPointer(EntityPointer);

        public string NameDesigned()
        {
            IntPtr pEntity = MemoryManager.ReadIntPtr(EntityPointer + (int) EntityOffsets.Entity);

            return MemoryManager.ReadString(pEntity + (int) EntityOffsets.Name);
        }

        #endregion

        #region Internal Methods

        internal IntPtr BasePointer() => EntityPointer;

        internal float PossitionX() => MemoryManager.ReadFloat(EntityPointer + (int) EntityOffsets.VecNetworkOrigin);

        internal float PossitionY() =>
            MemoryManager.ReadFloat(EntityPointer + (int) EntityOffsets.VecNetworkOrigin + 0x04);

        internal float PossitionZ() =>
            MemoryManager.ReadFloat(EntityPointer + (int) EntityOffsets.VecNetworkOrigin + 0x08);

        #endregion
    }
}