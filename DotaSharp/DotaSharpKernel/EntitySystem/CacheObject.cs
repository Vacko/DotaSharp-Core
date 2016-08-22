using System;
using System.Collections.Generic;

namespace DotaSharp.DotaSharpKernel.EntitySystem
{
    internal class CacheObject : ICacheService<Entity>
    {
        #region Fields

        private static CacheObject m_instance;
        private static readonly object Padlock = new object();
        private readonly Dictionary<IntPtr, Entity> m_cache = new Dictionary<IntPtr, Entity>();

        #endregion

        #region Properties

        public static CacheObject Instance
        {
            get
            {
                lock (Padlock)
                {
                    return m_instance ?? (m_instance = new CacheObject());
                }
            }
        }

        #endregion

        #region Implementation of ICacheService<Entity>

        public Entity CreateGet(IntPtr baseEntity)
        {
            if (baseEntity == IntPtr.Zero)
                return null;

            if (m_cache.TryGetValue(baseEntity, out Entity entity) && entity != null)
                return entity;

            Entity entityFromPointer = ObjectMgr.CreateEntityFromPointer(baseEntity);

            if (entityFromPointer != null)
                m_cache.Add(baseEntity, entityFromPointer);

            return entityFromPointer;
        }


        public void Remove(IntPtr pointer) => m_cache.Remove(pointer);

        #endregion
    }
}