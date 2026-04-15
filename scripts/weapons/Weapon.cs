using Godot;
using System;

public abstract partial class Weapon : Node2D
{
	protected int					magazine;
	protected int					ammo;
	protected bool					flag_shooting = true;
	protected bool					flag_combo = true;
	protected PackedScene			bulletScene;
	protected string				combo; 
	protected string				combo_input; 

	public override void _Ready()
	{
		bulletScene = GD.Load<PackedScene>("res://scripts/Bullet.tscn");
		magazine = 6;
		ammo = magazine;
		combo = "LLR";
		combo_input = "";
	}

	public override void _Process(double delta)
	{
		if (Input.IsKeyPressed(Key.Space))
		{
			if (flag_shooting && ammo > 0) 
			{
				Player player = (Player)GetParent();
				float angle = player.angle;
				Shoot(player.Position, angle);
				flag_shooting = false;
				ammo--;
			}
		} else
		{
			flag_shooting = true;
		}
		if (Input.IsKeyPressed(Key.R))
		{
			GD.Print(combo_input);
			if (flag_combo)
				GetCombo();
// TODO proper edge detection
			if (
				!Input.IsKeyPressed(Key.Left) &&
				!Input.IsKeyPressed(Key.Right) &&
				!Input.IsKeyPressed(Key.Up) &&
				!Input.IsKeyPressed(Key.Down)
				)
				flag_combo = true;
		}
		else
		{
			if (IsComboOK())
				ammo = magazine;
			combo_input = "";
		}
	}

	protected void Shoot(Vector2 position, float angle)
	{
		var bullet = bulletScene.Instantiate<Bullet>();
		bullet.Position = position;
		bullet.Direction = new Vector2(Mathf.Cos(angle), -Mathf.Sin(angle));
		bullet.Rotation = -angle + Mathf.Pi / 2f;
		GetTree().CurrentScene.AddChild(bullet);
	}

	protected bool IsComboOK()
	{
		if (combo_input == combo)
			return (true);
		return (false);
	}

	protected void GetCombo()
	{
		flag_combo = false;
		if (Input.IsKeyPressed(Key.Left))
			combo_input += "L";
		if (Input.IsKeyPressed(Key.Right))
			combo_input += "R";
		if (Input.IsKeyPressed(Key.Up))
			combo_input += "U";
		if (Input.IsKeyPressed(Key.Down))
			combo_input += "D";
	}
}
