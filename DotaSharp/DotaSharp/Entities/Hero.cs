using System;
using DotaSharp.DotaSharpKernel.Offsets;
using DotaSharp.Memory;

namespace DotaSharp
{
    public class Hero : Unit
    {
        #region Constructors

        internal Hero(IntPtr entityPointer) : base(entityPointer)
        {
        }

        public Hero()
        {
        }

        #endregion

        #region Public Methods

        public int AbilityPoints() => MemoryManager.ReadInt(EntityPointer + (int) HeroOffsets.AbilityPoints);

        public float Agility() => MemoryManager.ReadFloat(EntityPointer + (int) HeroOffsets.Agility);

        public float AgilityTotal() => MemoryManager.ReadFloat(EntityPointer + (int) HeroOffsets.AgilityTotal);

        public bool IsBuybackDisabled() =>
            MemoryManager.ReadInt(EntityPointer + (int) HeroOffsets.BuybackDisabled).Equals(1);

        public bool IsIllusion() =>
            MemoryManager.ReadInt(EntityPointer + (int) HeroOffsets.ReplicatingOtherHeroModel) != -1;

        public float LIntellect() => MemoryManager.ReadFloat(EntityPointer + (int) HeroOffsets.Intellect);

        public float LIntellectTotal() => MemoryManager.ReadFloat(EntityPointer + (int) HeroOffsets.IntellectTotal);

        public float Strength() => MemoryManager.ReadFloat(EntityPointer + (int) HeroOffsets.Strength);

        public float StrengthTotal() => MemoryManager.ReadFloat(EntityPointer + (int) HeroOffsets.StrengthTotal);

        #endregion
    }
}