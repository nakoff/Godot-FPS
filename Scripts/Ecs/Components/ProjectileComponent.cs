using DefaultEcs;
using View;

namespace Component;

public struct ProjectileComponent {
    public readonly IProjectileView View;
    public readonly Entity OwnEntity;
    public float Speed = 1f;
    public float Deaccel = 0f;
    public float Gravity = -10;

    public ProjectileComponent(Entity entity, IProjectileView view) {
        View = view;
        OwnEntity = entity;
    }
}