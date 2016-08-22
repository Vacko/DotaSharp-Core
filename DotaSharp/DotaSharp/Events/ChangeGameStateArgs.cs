using System;

namespace DotaSharp
{
    public class ChangeGameStateArgs : EventArgs
    {
        #region Constructors

        internal ChangeGameStateArgs(GameState state) => State = state;

        #endregion

        #region Properties

        public GameState State { get; }

        #endregion
    }
}