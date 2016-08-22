using System;

namespace DotaSharp
{
    public class ChangeGameUiStateArgs : EventArgs
    {
        #region Constructors

        internal ChangeGameUiStateArgs(UiState state) => State = state;

        #endregion

        #region Properties

        public UiState State { get; }

        #endregion
    }
}