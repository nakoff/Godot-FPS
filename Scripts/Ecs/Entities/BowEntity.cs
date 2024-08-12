using Component;
using DefaultEcs;
using Godot;
using Service;
using View;

namespace System;

public class BowEntity : BaseEntity
{
    public Entity Create(Node3D parent)
    {
        var entity = CreateEntity();

        var view = ViewService.CreateView<BowView>(ENTITY_VIEW.WEAPON_BOW, parent);
        entity.Set(new WeaponViewComponent(view));

        return entity;
    }
}
