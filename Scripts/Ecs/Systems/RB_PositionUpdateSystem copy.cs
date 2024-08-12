using Component;
using DefaultEcs;
using DefaultEcs.System;

namespace System;

[With(typeof(RigidBodyComponent))]
[With(typeof(PositionComponent))]
public sealed class RB_PositionUpdateSystem : AEntitySetSystem<float> {

    public RB_PositionUpdateSystem(World world) : base(world, false) { }

    protected override void Update(float state, in Entity entity) {
        var body = entity.Get<RigidBodyComponent>().Body;
        ref var pos = ref entity.Get<PositionComponent>();

        pos.CurPosition = body.GlobalPosition;
    }
}