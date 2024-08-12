
using Component;
using DefaultEcs;
using DefaultEcs.System;
using Godot;
using Service;

namespace System;

[With(typeof(PlayerViewComponent))]
[With(typeof(PositionComponent))]
public sealed class PlayerControllSystem : AEntitySetSystem<float> {
    private InputService inputService;

    public PlayerControllSystem(World world) : base(world, false) {
        inputService = new InputService();
    }

    protected override void Update(float state, in Entity entity) {
        ref var view = ref entity.Get<PlayerViewComponent>().View;
        entity.Remove<MoveComponent>();

        if (Input.IsActionJustPressed("Jump")) {
            if (!entity.Has<OnFloorMarker>()) return;
            entity.Set(new JumpComponent { Force = 10 });
        }

        // Movement
        Vector2 inputDir = Input.GetVector("Left", "Right", "Forward", "Backward");
        if (inputDir != Vector2.Zero) {
            entity.Set(new MoveComponent {
                DirX = Math.Sign(inputDir.X),
                DirZ = Math.Sign(inputDir.Y),
                Accel = 1.5f,
                Speed = 5,
            });
        }

        // Use Ability
        if (Input.IsActionJustPressed("Use")) {
            inputService.MouseCapture = true;
            entity.Set(new WeaponActivatedComponent());
        }

        if (Input.IsActionJustReleased("Use")) {
            inputService.MouseCapture = false;
            entity.Remove<WeaponActivatedComponent>();
            view.BodyRotate = 0;
        }
    }
}