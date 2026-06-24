using Godot;
using System;

public partial class Planeta : Area2D
{
	[Export] public float				HP;
	[Export] public float				friction;
	[Export] public float				radius;
	[Export] public float				rotationSpeed;

	public override void _Process(double delta)
	{
		Rotation += rotationSpeed * (float)delta;
	}

	public override void _Ready()
	{
		AreaEntered += OnAreaEntered;
	}

	private void OnAreaEntered(Area2D area)
	{
		if (area is Enemy)
		{
			Enemy enemy = area as Enemy;
			HP -= enemy.DealDamage();
			if (HP <= 0.0f)
			{
				QueueFree();
			}
		}
	}
}
