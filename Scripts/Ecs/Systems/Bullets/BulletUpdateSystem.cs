
using Component;
using DefaultEcs;
using DefaultEcs.System;
using Godot;
using Service;
using View;

namespace System;

[With(typeof(BulletViewComponent))]
[With(typeof(RigidBodyComponent))]
[With(typeof(PositionComponent))]
public sealed class BulletUpdateSystem : AEntitySetSystem<float> {
    private World _world;

    public BulletUpdateSystem(World world) : base(world, false) {
        _world = world;
    }

    protected override void Update(float state, in Entity entity) {
        var body = entity.Get<RigidBodyComponent>().Body;
        ref var pos = ref entity.Get<PositionComponent>();

        body.LookAt(pos.CurPosition + body.LinearVelocity.Normalized(), Vector3.Forward);
        body.RotateZ(80); //TODO fix it
    }
}