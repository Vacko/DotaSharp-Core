using System;
using System.Linq;
using DotaSharp.DotaSharpKernel.Offsets;
using DotaSharp.Memory;

namespace DotaSharp.DotaSharpKernel.Misc
{
    internal class GameRules
    {
        #region Fields

        private static GameRules m_instance;
        private static readonly object Padlock = new object();
        private readonly IntPtr m_pGameRules;

        #endregion

        #region Constructors

        internal GameRules(IntPtr pointer) => m_pGameRules = pointer;

        #endregion

        #region Properties

        public static GameRules Instance
        {
            get
            {
                lock (Padlock)
                {
                    if (m_instance == null)
                    {
                        Entity pRules = ObjectMgr.GetEntities<Entity>().First(x => x.Name() == "C_DOTAGamerulesProxy");

                        if (pRules != null)
                            m_instance = new GameRules(MemoryManager.ReadIntPtr(pRules.BasePointer() + 0x2f0));
                    }

                    return m_instance;
                }
            }
        }

        #endregion

        #region Public Methods

        public float GameTime() => MemoryManager.ReadFloat(m_pGameRules + (int) GameRulesOffsets.FGameTime);

        public bool IsPaused() => MemoryManager.ReadInt(m_pGameRules + (int) GameRulesOffsets.BGamePaused).Equals(1);

        #endregion

        #region Internal Methods

        internal void Restart() => m_instance = null;

        #endregion
    }
}