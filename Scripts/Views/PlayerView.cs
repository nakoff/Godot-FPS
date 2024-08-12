using Godot;
using System;

namespace View;

public partial class PlayerView : CharacterBody3D {
	[Export] public Node3D BodyUp { get; private set; }

	private Vector2 bodyUpRotateLimits = new Vector2(-65, 45);

	// public Node3D BodyUp => bodyUp;
	public float BodyRotate {
		get => BodyUp.RotationDegrees.Z;
		set {
			var rot = BodyUp.RotationDegrees;
			rot.Z = Math.Clamp(value, bodyUpRotateLimits.X, bodyUpRotateLimits.Y);
			BodyUp.RotationDegrees = rot;
		}
	}
}
