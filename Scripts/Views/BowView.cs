using Godot;
using System;

namespace View;

public partial class BowView : Node3D, IWeaponView
{
    [Export] private Node3D spawnNode;

    public void ShotStart()
    {
        new ArrowEntity().Create(spawnNode.GlobalPosition, spawnNode.GlobalTransform.Basis);
    }

    public void ShotEnd()
    {
        GD.Print("\nFIRE END!\n");
    }
}
