using Godot;

public partial class Laser_gun : Weapon
{
	private double			charge_power_timer = 0.0;
	private const double	CHARGE_MAX_TIME = 1.0;

	public override void _Ready()
	{
		magazine = 30;
		ammo = magazine;
		combo = "L";
		combo_input = "";
	}

	public override void _Process(double delta)
	{
		if (!Input.IsKeyPressed(Key.Space))
		{
			if (flag_shooting && ammo > 0){
				Player player = (Player)GetParent();
				float angle = player.angle;
				Shoot(player.Position, angle);
				ammo--;
			}
			charge_power_timer = 0.0;
			flag_shooting = false;
		}

		if (Input.IsKeyPressed(Key.Space))
		{
			charge_power_timer+=delta;
			if (charge_power_timer >= CHARGE_MAX_TIME)
			{
				flag_shooting = true;
			}
		} else
		{
			flag_shooting = false;
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
