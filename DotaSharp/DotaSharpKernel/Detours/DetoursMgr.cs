using DotaSharp.DotaSharpKernel.Detours.DetouredFunctions;
using DotaSharp.Hooking;

namespace DotaSharp.DotaSharpKernel.Detours
{
    internal static class DetoursMgr
    {
        #region Fields

        private static bool m_initialized;
        private static readonly DetourManager Detours = new DetourManager();

        #endregion

        #region Properties

        internal static DetourManager GetDetours => Detours;

        #endregion

        #region Static Methods

        internal static void Initialize()
        {
            if (m_initialized) return;

            m_initialized = true;

            ChangeGameRulesState.CreateHook();
            ChangeGameUiState.CreateHook();
            PaintTraverse.CreateHook();

            Detours.ApplyAll();
        }

        #endregion
    }
}