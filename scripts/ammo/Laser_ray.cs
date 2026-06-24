using Godot;
using System;
using System.Threading.Tasks;

public partial class Laser_ray : Area2D, IProjectile
{
	public Vector2 Direction { get; set; } = Vector2.Up;

	public override void _Ready()
	{
		AreaEntered += OnAreaEntered;
		autoDelete();
	}

	private async Task autoDelete()
	{
		await ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout);
		QueueFree();
	}

	private void OnAreaEntered(Area2D area)
	{
		if (area is Enemy enemy)
		{
			enemy.TakeDamage(100);
		}
	}
}
