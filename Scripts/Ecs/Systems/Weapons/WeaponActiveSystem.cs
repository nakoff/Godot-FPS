
using Component;
using DefaultEcs;
using DefaultEcs.System;
using Godot;
using Service;

namespace System;

[With(typeof(PlayerViewComponent))]
[With(typeof(WeaponActivatedComponent))]
public sealed class WeaponActiveSystem : AEntitySetSystem<float> {
    private InputService inputService;
    public WeaponActiveSystem(World world) : base(world, false) {
        inputService = new InputService();
    }

    protected override void Update(float state, in Entity entity) {
        ref var view = ref entity.Get<PlayerViewComponent>().View;

        view.BodyRotate += -inputService.MouseRelative.Y * state * 10;

        if (inputService.IsActionPressed(InputService.ACTION.FIRE)) {
            if (!entity.Has<WeaponBowComponent>()) return;
            ref var bow = ref entity.Get<WeaponBowComponent>();

            if (bow.Owner == default) return;
            if (!bow.Owner.Has<WeaponViewComponent>()) return;
            var weaponView = bow.Owner.Get<WeaponViewComponent>().View;

            weaponView.ShotStart();
        }
    }
}