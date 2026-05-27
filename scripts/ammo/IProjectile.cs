using Godot;
using System;

public partial interface IProjectile
{
	public Vector2	Direction { get; set; }
	public Vector2	Position { get; set; }
	public float	Rotation { get; set; }
}
