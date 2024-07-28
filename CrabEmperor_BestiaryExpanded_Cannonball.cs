using System;

#nullable disable
namespace XRL.World.Parts
{
  [Serializable]
  public class Cannonball : IAmmo
  {
    public string ProjectileObject;

    public override bool SameAs(IPart p)
    {
      return !((p as Cannonball).ProjectileObject != this.ProjectileObject) && base.SameAs(p);
    }

    public override bool WantEvent(int ID, int cascade)
    {
      return base.WantEvent(ID, cascade) || ID == PooledEvent<GetProjectileObjectEvent>.ID || ID == QueryEquippableListEvent.ID;
    }

    public override bool HandleEvent(QueryEquippableListEvent E)
    {
      if (E.SlotType.Contains(nameof (Cannonball)) && !E.List.Contains(this.ParentObject))
        E.List.Add(this.ParentObject);
      return base.HandleEvent(E);
    }

    public override bool HandleEvent(GetProjectileObjectEvent E)
    {
      if (string.IsNullOrEmpty(this.ProjectileObject))
        return base.HandleEvent(E);
      E.Projectile = GameObject.Create(this.ProjectileObject);
      return false;
    }

    public override bool AllowStaticRegistration() => true;
  }
}