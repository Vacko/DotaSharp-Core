using System;
using System.Collections.Generic;
using DotaSharp.DotaSharpKernel.EntitySystem;
using DotaSharp.Memory;

namespace DotaSharp
{
    public class Spellbook
    {
        #region Fields

        private readonly IntPtr m_spellBook;

        #endregion

        #region Constructors

        internal Spellbook(IntPtr pointer) => m_spellBook = pointer;

        #endregion

        #region Properties

        public IEnumerable<Ability> Spells
        {
            get
            {
                List<Ability> list = new List<Ability>();

                if (m_spellBook != IntPtr.Zero)
                    for (int i = (int) AbilitySlot.SpellQ; i <= (int) AbilitySlot.Spell6; i++)
                    {
                        Ability get = GetAbility((AbilitySlot) i);

                        if (get != null)
                            list.Add(get);
                    }

                return list;
            }
        }

        #endregion

        #region Internal Methods

        internal Ability GetAbility(AbilitySlot slot)
        {
            int readHandle = MemoryManager.ReadInt(m_spellBook + (int) slot * 4);

            if (readHandle == -1)
                return null;

            IntPtr abilityPointer = EntitySystem.GetEntityFromHandle(readHandle);

            Entity getEntity = CacheObject.Instance.CreateGet(abilityPointer);

            if (getEntity is Ability ability)
                return ability;
            return null;
        }

        #endregion
    }
}