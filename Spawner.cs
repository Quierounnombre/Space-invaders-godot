using Godot;
using System;

public partial class Spawner : Node2D
{
	private PackedScene			enemyScene;
	private Timer				_timer;
	private Vector2				pos;

	public override void _Ready()
	{
		enemyScene = GD.Load<PackedScene>("res://Enemy.tscn");
		_timer = GetNode<Timer>("Timer");
		_timer.WaitTime = 10.0f;
		_timer.Timeout += OnTick;
		_timer.Start();
	}

	private void OnTick()
	{
		GD.Print("HEY");
		Spawn_enemy();
		pos = Position;
		pos.X += 50;
		Position = pos;
	}
	
	private void Spawn_enemy()
	{
		var enemy = enemyScene.Instantiate<Enemy>();
		enemy.Position = new Vector2(Position.X, Position.Y - 200);
		GetTree().CurrentScene.AddChild(enemy);
	}

	public override void _Process(double delta)
	{
	}
}
