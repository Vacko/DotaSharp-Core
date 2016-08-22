using System;
using System.Collections.Generic;
using System.Linq;
using DotaSharp.DotaSharpKernel.EntitySystem;
using DotaSharp.DotaSharpKernel.Misc;

namespace DotaSharp
{
    public static class ObjectMgr
    {
        #region Properties

        public static Hero LocalHero
        {
            get
            {
                IntPtr localPlayer = EntitySystem.GetLocalPlayer();

                if (localPlayer != IntPtr.Zero) return (Hero) CacheObject.Instance.CreateGet(localPlayer);

                return null;
            }
        }

        public static Player LocalPlayer
        {
            get
            {
                IntPtr localPlayer = EntitySystem.GetLocalPlayer();

                return localPlayer != IntPtr.Zero
                    ? GetEntities<Player>().First(x => x.AssignedHero() == LocalHero)
                    : null;
            }
        }

        #endregion

        #region Static Methods

        public static IEnumerable<TObjectType> GetEntities<TObjectType>() where TObjectType : Entity, new()
        {
            List<TObjectType> list = new List<TObjectType>();

            IntPtr entityPtr = IntPtr.Zero;

            do
            {
                entityPtr = EntitySystem.NextEnt(entityPtr);

                if (entityPtr == IntPtr.Zero) continue;

                Entity getEntity = CacheObject.Instance.CreateGet(entityPtr);

                if (getEntity is TObjectType type)
                    list.Add(type);
            } while (entityPtr != IntPtr.Zero);

            return list;
        }


        internal static Entity CreateEntityFromPointer(IntPtr entityPointer)
        {
            string getClassId = ReadObj.ReadClassFromPointer(entityPointer);

            if (getClassId.Contains("DOTA_Ability") || getClassId.Contains("BaseAbility"))
                return new Ability(entityPointer);
            if (getClassId.Contains("DOTA_Unit_Hero"))
                return new Hero(entityPointer);
            if (getClassId.Contains("DOTAPlayer"))
                return new Player(entityPointer);
            if (getClassId.Contains("DOTA_Item"))
                return new Item(entityPointer);
            if (getClassId.Contains("BaseNPC_Creep_Lane"))
                return new Creep(entityPointer);
            if (getClassId.Contains("BaseNPC_Creep_Neutral"))
                return new Neutral(entityPointer);
            if (getClassId.Contains("BaseNPC_Shop"))
                return new Shop(entityPointer);
            if (getClassId.Contains("BaseNPC_Tower"))
                return new Tower(entityPointer);
            if (getClassId.Contains("DOTA_Unit_Courier"))
                return new Courier(entityPointer);
            if (getClassId.Contains("BaseNPC_Building"))
                return new Building(entityPointer);
            return new Entity(entityPointer);
        }

        #endregion
    }
}