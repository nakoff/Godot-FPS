using View;

namespace Component;

public struct WeaponViewComponent {
    public IWeaponView View;

    public WeaponViewComponent(IWeaponView view) {
        this.View = view;
    }
}