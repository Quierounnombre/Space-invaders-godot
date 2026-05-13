using Godot;
using System;

public partial class Bullet : Area2D
{
	[Export] public float		Speed;
	[Export] public float		Mass;
	public Vector2				Direction = Vector2.Up;

	public override void _PhysicsProcess(double delta)
	{
		Position += Direction * Speed * (float)delta;
	}

	public override void _Ready()
	{
		AreaEntered += OnAreaEntered;
		GetNode<VisibleOnScreenNotifier2D>("VisibleOnScreenNotifier2D").ScreenExited += ScreenLeave;
	}

	private void ScreenLeave()
	{
		QueueFree();
	}

	private void OnAreaEntered(Area2D area)
	{
		if (area is Enemy)
		{
			Calc_damage(area)
			QueueFree();
		}
	}
	
	private void Calc_damage(Enemy target)
	{
		double e;

		e = Kinetic_energy(Mass, Speed);
		target.TakeDamage(e);
	}

	private double Kinetic_energy(double m, double v)
	{
		return (0.5 * m * v * v);
	}
}
