using Godot;

public partial class Misile_launcher : Weapon
{
	public override void _Ready()
	{
		magazine = 4;
		ammo = magazine;
		combo = "UUR";
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
		}
		else
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
}
