using Component;
using Godot;
using Service;
using View;

namespace System;

public class PlayerEntity : BaseEntity
{
    public void Create(Vector3 pos)
    {
        var entity = CreateEntity();
        entity.Set(new PositionComponent(pos));

        var view = ViewService.CreateView<PlayerView>(ENTITY_VIEW.PLAYER);
        entity.Set(new PlayerViewComponent(view));
        entity.Set(new CharacterBodyComponent(view));
        entity.Set(new VelocityComponent());

        // entity.Set(new WeaponBowComponent());
    }
}
