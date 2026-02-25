using Godot;
using System;

public partial class Bullet : Area2D
{
	[Export] public float Speed = 800f;
	private Vector2 direction = Vector2.Right;

	public void Init(Vector2 dir)
	{
		direction = dir.Normalized();
	}

	public override void _PhysicsProcess(double delta)
	{
		Position += direction * Speed * (float)delta;
	}

	private void OnBodyEntered(Node body)
	{
		QueueFree();
	}
}
