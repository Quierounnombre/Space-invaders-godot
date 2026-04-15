using Godot;
using System;

public partial class Weapon : Node2D
{
	private int					magazine;
	private int					ammo;
	private string				charge_formula;
	private bool				flag_shooting = true;
	private PackedScene			bulletScene;
	private string				combo; 
	private string				combo_input; 
	public override void _Ready()
	{
		bulletScene = GD.Load<PackedScene>("res://Bullet.tscn");
		magazine = 6;
		ammo = magazine;
		combo = "LLD";
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
				var bullet = bulletScene.Instantiate<Bullet>();
				bullet.Position = player.Position;
				bullet.Direction = new Vector2(Mathf.Cos(angle), -Mathf.Sin(angle));
				bullet.Rotation = -angle + Mathf.Pi / 2f;
				GetTree().CurrentScene.AddChild(bullet);
				flag_shooting = false;
				ammo--;
			}
		} else
		{
			flag_shooting = true;
		}
		if (Input.IsKeyPressed(Key.R))
		{
			GetCombo();
		}
		else
		{
			if (IsComboOK())
				ammo = magazine;
			combo_input = "";			
		}
	}

// < < >+!R
	private bool IsComboOK()
	{
		if (combo_input == combo)
			return (true);
		return (false);
	}

	private void GetCombo()
	{
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
