using Service;
using Component;
using DefaultEcs;
using DefaultEcs.System;

namespace System;

[WhenAdded(typeof(PlayerViewComponent))]
// [With(typeof(PositionComponent))]
// [With(typeof(CharacterBodyComponent))]
public sealed class PlayerInitSystem : AEntitySetSystem<float> {
    public PlayerInitSystem(World world) : base(world, false) { }

    protected override void Update(float state, in Entity entity) {
        Godot.GD.Print("Player Created!");
        ref var view = ref entity.Get<PlayerViewComponent>().View;
        try {
            ref var pos = ref entity.Get<PositionComponent>();
            ref var body = ref entity.Get<CharacterBodyComponent>().Body;

            body.GlobalPosition = pos.CurPosition;
            var cameraService = new CameraService();
            cameraService.CreateCamera(view, new Godot.Vector2(5, 5));
        } catch (System.Exception) {
            Godot.GD.PrintErr("Can't init player");
        }
    }
}