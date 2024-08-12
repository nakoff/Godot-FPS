using DefaultEcs;

namespace Component;
public interface IWeaponComponent {
    public bool Current { get; set; }
    public Entity Owner { get; set; }
}