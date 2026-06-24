using Godot;
using System;

public partial class Enemy : Area2D
{
	[Export] public float	HeatSignature;
	[Export] public float	Speed;
	[Export] public float	damage;
	[Export] public double	Life;
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
		if (area.GetParent() is Player)
		{
			QueueFree();
		}
		if (area is Planeta)
		{
			QueueFree();
		}
	}

	public void TakeDamage(double e)
	{
		Life -= e;
		if (Life <= 0)
		{
			QueueFree();
		}
	}

	public float DealDamage()
	{
		return (damage);
	}
}
