using Component;
using DefaultEcs;
using DefaultEcs.System;

namespace System;

// [With(typeof(RigidBodyComponent))]
[With(typeof(CharacterBodyComponent))]
[With(typeof(PositionComponent))]
public sealed class CB_PositionUpdateSystem : AEntitySetSystem<float> {

    public CB_PositionUpdateSystem(World world) : base(world, false) { }

    protected override void Update(float state, in Entity entity) {
        // var body = entity.Get<RigidBodyComponent>().Body;
        var body = entity.Get<CharacterBodyComponent>().Body;
        ref var pos = ref entity.Get<PositionComponent>();

        pos.CurPosition = body.GlobalPosition;
    }
}