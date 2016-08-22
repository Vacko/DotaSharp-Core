using System;
using DotaSharp.DotaSharpKernel.EntitySystem;
using DotaSharp.DotaSharpKernel.Misc;
using DotaSharp.DotaSharpKernel.Offsets;
using DotaSharp.Memory;

namespace DotaSharp
{
    public class Player : Entity
    {
        #region Constructors

        internal Player(IntPtr entityPointer) : base(entityPointer)
        {
        }

        public Player()
        {
        }

        #endregion

        #region Public Methods

        public void Ability(Ability usedAbility) => PlayerOrder.PrepareUnitOrders(EntityPointer, (int) Order.Ability, 0,
            0, 0, 0, usedAbility.Index(), OrderIssuer.DotaOrderIssuerPassedUnitOnly, AssignedHero(), false, true);

        public void AbilityLocation(float x, float y, float z, Ability usedAbility) => PlayerOrder.PrepareUnitOrders(
            EntityPointer, (int) Order.AbilityTarget, 0, x, y, z, usedAbility.Index(),
            OrderIssuer.DotaOrderIssuerPassedUnitOnly, AssignedHero(), false, true);

        public void AbilityTarget(Entity targetEntity, Ability usedAbility) => PlayerOrder.PrepareUnitOrders(
            EntityPointer, (int) Order.AbilityTarget, targetEntity.Index(), 0, 0, 0, usedAbility.Index(),
            OrderIssuer.DotaOrderIssuerPassedUnitOnly, AssignedHero(), false, true);

        public Hero AssignedHero()
        {
            int getHandle = MemoryManager.ReadInt(EntityPointer + (int) PlayerOffsets.AssignedHero);

            if (getHandle == -1)
                return null;

            IntPtr heroPointer = EntitySystem.GetEntityFromHandle(getHandle);

            Entity getEntity = CacheObject.Instance.CreateGet(heroPointer);

            if (getEntity is Hero hero)
                return hero;

            return null;
        }

        public int AssignedHeroIndex()
        {
            int getHandle = MemoryManager.ReadInt(EntityPointer + (int) PlayerOffsets.AssignedHero);

            return getHandle == -1 ? 0 : EntitySystem.HandleToIndex(getHandle);
        }

        public void AttackPossition(float x, float y, float z) => PlayerOrder.PrepareUnitOrders(EntityPointer,
            (int) Order.AttackLocation, 0, x, y, z, 0, OrderIssuer.DotaOrderIssuerSelectedUnits, null, false, true);

        public void AttackTarget(Entity targetEntity) => PlayerOrder.PrepareUnitOrders(EntityPointer,
            (int) Order.AttackTarget, targetEntity.Index(), 0, 0, 0, 0, OrderIssuer.DotaOrderIssuerSelectedUnits, null,
            false, true);

        public void HoldPosition() => PlayerOrder.PrepareUnitOrders(EntityPointer, (int) Order.Hold, 0, 0, 0, 0, 0,
            OrderIssuer.DotaOrderIssuerSelectedUnits, null, false, true);

        public void MoveToPosition(int x, int y, int z) => PlayerOrder.PrepareUnitOrders(EntityPointer,
            (int) Order.AttackTarget, 0, x, y, z, 0, OrderIssuer.DotaOrderIssuerSelectedUnits, null, false, true);

        public void MoveToTarget(Entity targetEntity) => PlayerOrder.PrepareUnitOrders(EntityPointer,
            (int) Order.MoveTarget, targetEntity.Index(), 0, 0, 0, 0, OrderIssuer.DotaOrderIssuerSelectedUnits, null,
            false, true);

        public void SelectAllOtherUnits() => Console.ExecuteComman_Unrestricted("dota_select_all_others");

        public void SelectAllUnits() => Console.ExecuteComman_Unrestricted("dota_select_all");

        public void SelectCourier() => Console.ExecuteComman_Unrestricted("dota_select_courier");

        public void SelectHero()
        {
            Console.ExecuteComman_Unrestricted("+dota_camera_follow");
            Console.ExecuteComman_Unrestricted("+dota_camera_follow");
            Console.ExecuteComman_Unrestricted("-dota_camera_follow");
        }

        public void Stop() => PlayerOrder.PrepareUnitOrders(EntityPointer, (int) Order.Stop, 0, 0, 0, 0, 0,
            OrderIssuer.DotaOrderIssuerSelectedUnits, null, false, true);

        public void UseBuyback() => PlayerOrder.PrepareUnitOrders(EntityPointer, (int) Order.Buyback, 0, 0, 0, 0, 0,
            OrderIssuer.DotaOrderIssuerHeroOnly, null, false, true);

        public void UseGlyph() => PlayerOrder.PrepareUnitOrders(EntityPointer, (int) Order.GlyphOfFortification, 0, 0,
            0, 0, 0, OrderIssuer.DotaOrderIssuerHeroOnly, null, false, true);

        #endregion
    }
}