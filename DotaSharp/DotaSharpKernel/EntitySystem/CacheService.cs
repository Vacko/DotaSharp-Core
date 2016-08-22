using System;

namespace DotaSharp.DotaSharpKernel.EntitySystem
{
    public interface ICacheService<out TObjectType>
    {
        #region Public Methods

        TObjectType CreateGet(IntPtr baseEntity);

        void Remove(IntPtr pointer);

        #endregion
    }
}