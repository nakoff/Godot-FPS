using Component;
using DefaultEcs;
using Godot;
using Service;
using View;

namespace System;

public class ArrowEntity : BaseEntity
{
    public Entity Create(Vector3 pos, Basis rot)
    {
        var entity = CreateEntity();

        var view = ViewService.CreateView<ArrowView>(ENTITY_VIEW.BULLET_ARROW);
        view.GlobalPosition = pos;
        // view.Rotation = rot;
        view.Basis = rot;
        entity.Set(new RigidBodyComponent(view));
        entity.Set(new BulletViewComponent(view));
        entity.Set(new PositionComponent());

        return entity;
    }
}
