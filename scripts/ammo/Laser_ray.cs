using Godot;
using System;
using System.Threading.Tasks;

public partial class Laser_ray : Area2D, IProjectile
{
	[Export] public float		Duration;
	[Export] public float		Damage;
	private Timer				_timer;
	public Player				Source;

	public Vector2 Direction { get; set; } = Vector2.Up;

	public override void _Ready()
	{
		_timer = GetNode<Timer>("Timer");
		_timer.Timeout += OnTick;
		_timer.Start();
		AreaEntered += OnAreaEntered;
	}

	private void OnTick()
	{
		QueueFree();
	}

	private void OnAreaEntered(Area2D area)
	{
		if (area is Enemy enemy)
		{
			enemy.TakeDamage(Damage);
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		if (Source != null)
		{
			GlobalPosition = Source.GlobalPosition;
			Rotation = Source.Rotation;
		}
	}
}
