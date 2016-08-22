using System;
using System.Collections.Generic;
using DotaSharp.DotaSharpKernel.EntitySystem;
using DotaSharp.Memory;

namespace DotaSharp
{
    public class Inventory
    {
        #region Fields

        private readonly IntPtr m_heroInventory;

        #endregion

        #region Constructors

        internal Inventory(IntPtr pointer) => m_heroInventory = pointer;

        #endregion

        #region Properties

        public IEnumerable<Item> Items
        {
            get
            {
                List<Item> list = new List<Item>();

                if (m_heroInventory == IntPtr.Zero) return list;

                for (int i = (int) ItemSlot.InventorySlot1; i <= (int) ItemSlot.InventorySlot6; i++)
                {
                    Item get = GetItem((ItemSlot) i);

                    if (get != null)
                        list.Add(get);
                }

                return list;
            }
        }

        public IEnumerable<Item> StashItems
        {
            get
            {
                List<Item> list = new List<Item>();

                if (m_heroInventory == IntPtr.Zero) return list;

                for (int i = (int) ItemSlot.StashSlot1; i <= (int) ItemSlot.StashSlot6; i++)
                {
                    Item get = GetItem((ItemSlot) i);

                    if (get != null)
                        list.Add(get);
                }

                return list;
            }
        }

        #endregion

        #region Public Methods

        public Item GetItem(ItemSlot slot)
        {
            int readHandle = MemoryManager.ReadInt(m_heroInventory + (int) slot * 4);

            if (readHandle == -1)
                return null;


            IntPtr itemPointer = EntitySystem.GetEntityFromHandle(readHandle);

            Entity getEntity = CacheObject.Instance.CreateGet(itemPointer);

            if (getEntity is Item item)
                return item;
            return null;
        }

        #endregion
    }
}