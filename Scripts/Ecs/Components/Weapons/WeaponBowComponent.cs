using DefaultEcs;

namespace Component;
public struct WeaponBowComponent : IWeaponComponent
{
    public bool Current { get; set; }
    public Entity Owner { get; set; }
}