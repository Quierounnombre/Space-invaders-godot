using Godot;
using System;

public partial class Spawner : Node2D
{
	private PackedScene			enemyScene;
	private Timer				_timer;
	private Vector2				pos;
	private float				windowWidth;

	public override void _Ready()
	{
		enemyScene = GD.Load<PackedScene>("res://Enemy.tscn");
		_timer = GetNode<Timer>("Timer");
		_timer.WaitTime = 3.0f;
		_timer.Timeout += OnTick;
		_timer.Start();
		windowWidth = GetViewport().GetVisibleRect().Size.X;
	}

	private void OnTick()
	{
		pos.X = windowWidth * GD.Randf();
		pos.Y = Position.Y;
		Position = pos;
		Spawn_enemy();
	}
	
	private void Spawn_enemy()
	{
		var enemy = enemyScene.Instantiate<Enemy>();
		enemy.Position = new Vector2(Position.X, Position.Y - 200);
		enemy.SetTarget(Player.circleCenter);
		GetTree().CurrentScene.AddChild(enemy);
	}

	public override void _Process(double delta)
	{
	}
}
