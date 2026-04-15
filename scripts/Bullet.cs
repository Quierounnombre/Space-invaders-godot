using Godot;
using System;

public partial class Bullet : Area2D
{
	public float		Speed = 800f;
	public Vector2		Direction = Vector2.Up;

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
			QueueFree();
	}
}
