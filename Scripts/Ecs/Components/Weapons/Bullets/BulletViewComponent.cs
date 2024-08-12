using View;

namespace Component;

public struct BulletViewComponent {
    public IBulletView View;

    public BulletViewComponent(IBulletView view) {
        this.View = view;
    }
}