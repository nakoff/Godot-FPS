
using Component;
using DefaultEcs;
using DefaultEcs.System;
using Godot;
using Service;
using View;

namespace System;

[With(typeof(PlayerViewComponent))]
[With(typeof(WeaponBowComponent))]
[WhenAdded(typeof(WeaponActivatedComponent))]
public sealed class WeaponActivateSystem : AEntitySetSystem<float>
{
    private World _world;
    private InputService inputService;

    public WeaponActivateSystem(World world) : base(world, false)
    {
        inputService = new InputService();
        _world = world;
    }

    protected override void Update(float state, in Entity entity)
    {
        ref var view = ref entity.Get<PlayerViewComponent>().View;
        ref var curWeapon = ref entity.Get<WeaponBowComponent>();

        var bowEntity = new BowEntity().Create(view.BodyUp);
        curWeapon.Owner = bowEntity;

        if (!bowEntity.Has<WeaponViewComponent>()) return;

        ref var weaponView = ref bowEntity.Get<WeaponViewComponent>();
        if (weaponView.View is BowView bowView)
        {
            bowView.Position = new Vector3(1f, 0.5f, 0f);
        }
    }
}
