
using Component;
using DefaultEcs;
using DefaultEcs.System;
using Godot;
using Service;
using View;

namespace System;

[With(typeof(PlayerViewComponent))]
[With(typeof(WeaponBowComponent))]
[WhenRemoved(typeof(WeaponActivatedComponent))]
public sealed class WeaponDeActivateSystem : AEntitySetSystem<float> {
    private World _world;
    private InputService inputService;

    public WeaponDeActivateSystem(World world) : base(world, false) {
        inputService = new InputService();
        _world = world;
    }

    protected override void Update(float state, in Entity entity) {
        ref var curWeapon = ref entity.Get<WeaponBowComponent>();

        var bowEntity = curWeapon.Owner;
        if (bowEntity == default) {
            GD.PrintErr("Wrong bow entity");
            return;
        }

        if (!bowEntity.Has<WeaponViewComponent>()) {
            GD.PrintErr("No WeaponViewComponent");
            return;
        }

        ref var weapon = ref bowEntity.Get<WeaponViewComponent>();
        if (weapon.View is BowView bowView) {
            bowView.QueueFree();
        }

        curWeapon.Owner = default;
        bowEntity.Dispose();
    }
}