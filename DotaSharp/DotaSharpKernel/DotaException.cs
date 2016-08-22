using System;
using DotaSharp.Utils;

namespace DotaSharp.DotaSharpKernel
{
    internal sealed class DotaException : ApplicationException
    {
        #region Constructors

        internal DotaException(string msg) : base(msg)
        {
            Logging.Write(true, $"CreateInterface")(Message);
        }

        #endregion
    }
}