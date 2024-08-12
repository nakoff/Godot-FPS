
using Component;
using DefaultEcs;
using DefaultEcs.System;
using Godot;
using Service;
using View;

namespace System;

[WhenAdded(typeof(BulletViewComponent))]
public sealed class BulletInitSystem : AEntitySetSystem<float> {
    private World _world;

    public BulletInitSystem(World world) : base(world, false) {
        _world = world;
    }

    protected override void Update(float state, in Entity entity) {
        if (!entity.Has<RigidBodyComponent>()) {
            GD.Print("No RigidBody component");
            return;
        }

        var body = entity.Get<RigidBodyComponent>().Body;
        var vel = body.Transform.Basis * new Vector3(20, 0, 0);
        body.LinearVelocity = vel;

        // body.BodyEntered += OnBodyEntered;
        // body.BodyEntered += (Node node) => {
        //     OnBodyEntered(node);
        // };
    }

    private void OnBodyEntered(Node body)
    {
    }
}