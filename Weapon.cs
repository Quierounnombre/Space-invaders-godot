using Godot;
using System;

public partial class Weapon : Node2D
{
	private int					magazine;
	private int					ammo;
	private string				charge_formula;
	private bool				flag_shooting = true;
	private PackedScene			bulletScene;

	public override void _Ready()
	{
		bulletScene = GD.Load<PackedScene>("res://Bullet.tscn");
	}

	public override void _Process(double delta)
	{
		if (Input.IsKeyPressed(Key.Space))
		{
			if (flag_shooting) 
			{
				Player player = (Player)GetParent();
				float angle = player.angle;
				var bullet = bulletScene.Instantiate<Bullet>();
				bullet.Position = player.Position;
				bullet.Direction = new Vector2(Mathf.Cos(angle), -Mathf.Sin(angle));
				bullet.Rotation = -angle + Mathf.Pi / 2f;
				GetTree().CurrentScene.AddChild(bullet);
				flag_shooting = false;
			}
		} else
		{
			flag_shooting = true;
		}
	}
}
