using System;
using DotaSharp.DotaSharpKernel.Misc;
using DotaSharp.DotaSharpKernel.Offsets;
using DotaSharp.Memory;

namespace DotaSharp
{
    public class Ability : Entity
    {
        #region Constructors

        internal Ability(IntPtr entityPointer) : base(entityPointer)
        {
        }

        public Ability()
        {
        }

        #endregion

        #region Public Methods

        public int CastRange() => MemoryManager.ReadInt(EntityPointer + (int) AblityOffsets.CastRange);

        public float CoolDown()
        {
            float cooldown = GetCooldown() - GameRules.Instance.GameTime();

            return cooldown > 0.0 ? cooldown : 0f;
        }

        public float CooldownLength() => MemoryManager.ReadFloat(EntityPointer + (int) AblityOffsets.CooldownLength);

        public int EnemyLevel() => MemoryManager.ReadInt(EntityPointer + (int) AblityOffsets.EnemyLevel);

        public int FrozenCooldown() => MemoryManager.ReadInt(EntityPointer + (int) AblityOffsets.FrozenCooldown);

        public bool InAbilityPhase() =>
            MemoryManager.ReadInt(EntityPointer + (int) AblityOffsets.InAbilityPhase).Equals(1);

        public int Level() => MemoryManager.ReadInt(EntityPointer + (int) AblityOffsets.Level);

        public float ManaCost() => MemoryManager.ReadFloat(EntityPointer + (int) AblityOffsets.ManaCost);

        #endregion

        #region Private Methods

        private float GetCooldown() => MemoryManager.ReadFloat(EntityPointer + (int) AblityOffsets.Cooldown);

        #endregion
    }
}