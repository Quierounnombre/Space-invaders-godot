using Godot;
using System;

public partial class Enemy : Area2D
{
	public float		Speed = 100f;
	private Vector2		direction = Vector2.Down;

	public override void _PhysicsProcess(double delta)
	{
		Position += direction * Speed * (float)delta;
	}

	public void SetTarget(Vector2 target)
	{
		direction = (target - Position).Normalized();
	}

	public override void _Ready()
	{
		AreaEntered += OnAreaEntered;
	}

	private void OnAreaEntered(Area2D area)
	{
		if (area is Bullet)
		{
			QueueFree();
		}
		if (area.GetParent() is Player)
		{
			QueueFree();
		}
	}

}
