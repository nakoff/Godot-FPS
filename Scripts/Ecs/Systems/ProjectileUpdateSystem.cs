
using Component;
using DefaultEcs;
using DefaultEcs.System;
using Godot;
using Service;
using View;

namespace System;

[With(typeof(PositionComponent))]
[With(typeof(ProjectileComponent))]
public sealed class ProjectileUpdateSystem : AEntitySetSystem<float> {
    private World _world;

    public ProjectileUpdateSystem(World world) : base(world, false) {
        _world = world;
    }

    protected override void Update(float state, in Entity entity) {
        ref var projectile = ref entity.Get<ProjectileComponent>();
        ref var pos = ref entity.Get<PositionComponent>();
    }
}