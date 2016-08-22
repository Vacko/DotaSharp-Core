using System;
using DotaSharp.DotaSharpKernel.Offsets;
using DotaSharp.Memory;

namespace DotaSharp
{
    public class Unit : Entity
    {
        #region Fields

        private Inventory m_inventory;
        private Spellbook m_spellbook;

        #endregion

        #region Constructors

        internal Unit(IntPtr entityPointer) : base(entityPointer)
        {
        }

        public Unit()
        {
        }

        #endregion

        #region Properties

        public Inventory Inventory =>
            m_inventory ?? (m_inventory = new Inventory(EntityPointer + (int) UnitOffsets.Inventory + 0x14));

        public Spellbook Spellbook =>
            m_spellbook ?? (m_spellbook = new Spellbook(EntityPointer + (int) UnitOffsets.Abilities));

        #endregion

        #region Public Methods

        public AttackCapability AttackCapabilities() =>
            (AttackCapability) MemoryManager.ReadInt(EntityPointer + (int) UnitOffsets.AttackCapabilities);

        public int AttackRange() => MemoryManager.ReadInt(EntityPointer + (int) UnitOffsets.AttackRange);

        public int BkbChargesUsed() => MemoryManager.ReadInt(EntityPointer + (int) UnitOffsets.BkbChargesUsed);

        public int BonusDamage() => MemoryManager.ReadInt(EntityPointer + (int) UnitOffsets.DamageBonus);

        public int CurrentLevel() => MemoryManager.ReadInt(EntityPointer + (int) UnitOffsets.CurrentLevel);

        public int DayVisionRange() => MemoryManager.ReadInt(EntityPointer + (int) UnitOffsets.DayTimeVisionRange);

        public bool HasInventory() => MemoryManager.ReadInt(EntityPointer + (int) UnitOffsets.HasInventory).Equals(1);

        public float HealthRegen() => MemoryManager.ReadFloat(EntityPointer + (int) UnitOffsets.HealthThinkRegen);

        public bool IsEnemy() => GetTeam() != ObjectMgr.LocalHero.GetTeam();

        public bool IsMoving() => MemoryManager.ReadInt(EntityPointer + (int) UnitOffsets.IsMoving).Equals(1);

        public bool IsNeutral() => MemoryManager.ReadInt(EntityPointer + (int) UnitOffsets.IsNeutralUnitType).Equals(1);

        public float MagicalResistance() =>
            MemoryManager.ReadFloat(EntityPointer + (int) UnitOffsets.MagicalResistance);

        public float Mana() => MemoryManager.ReadFloat(EntityPointer + (int) UnitOffsets.Mana);

        public float ManaRegen() => MemoryManager.ReadFloat(EntityPointer + (int) UnitOffsets.ManaThinkRegen);

        public int MaxDamage() => MemoryManager.ReadInt(EntityPointer + (int) UnitOffsets.DamageMax);

        public float MaxMana() => MemoryManager.ReadFloat(EntityPointer + (int) UnitOffsets.MaxMana);

        public int MinDamage() => MemoryManager.ReadInt(EntityPointer + (int) UnitOffsets.DamageMin);

        public int NightVisionRange() => MemoryManager.ReadInt(EntityPointer + (int) UnitOffsets.NightTimeVisionRange);

        public string UnitName() => MemoryManager.ReadString(EntityPointer + (int) UnitOffsets.UnitName);

        public int UnitNameIndex() => MemoryManager.ReadInt(EntityPointer + (int) UnitOffsets.UnitNameIndex);

        public UnitState UnitState() =>
            (UnitState) MemoryManager.ReadInt(EntityPointer + (int) UnitOffsets.UnitState, 8);

        public int UnitType() => MemoryManager.ReadInt(EntityPointer + (int) UnitOffsets.UnitType);

        #endregion
    }
}