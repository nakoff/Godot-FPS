
using Component;
using DefaultEcs;
using DefaultEcs.System;
using Godot;
using Service;
using View;

namespace System;

[With(typeof(PositionComponent))]
[WhenAdded(typeof(ProjectileComponent))]
public sealed class ProjectileInitSystem : AEntitySetSystem<float> {
    private World _world;

    public ProjectileInitSystem(World world) : base(world, false) {
        _world = world;
    }

    protected override void Update(float state, in Entity entity) {
        ref var projectile = ref entity.Get<ProjectileComponent>();

        try {
            ref var pos = ref entity.Get<PositionComponent>();
        } catch (System.Exception) {
        }
    }
}