using Godot;
using System;

public partial class Spawner : Node2D
{
	private PackedScene			enemyScene;
	private Timer				_timer;
	private float				windowWidth;
	private float				radius = 1000f;
	private float				angle = Mathf.Pi / 2f;

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
		angle = (float)GD.RandRange(0.0, Mathf.Pi);
		Spawn_enemy();
	}
	
	private void Spawn_enemy()
	{
		var enemy = enemyScene.Instantiate<Enemy>();
		enemy.Position = Player.circleCenter + new Vector2(Mathf.Cos(angle), -Mathf.Sin(angle)) * radius;
		enemy.SetTarget(Player.circleCenter);
		GetTree().CurrentScene.AddChild(enemy);
	}

	public override void _Process(double delta)
	{
	}
}
