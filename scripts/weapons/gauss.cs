using Godot;

public partial class Gauss : Weapon
{
	private double			shoot_timer = 0.0;
	private double			burst_timer = 0.0;
	private const double	shoot_delay = 1.5;
	private const double	shoot_rate = 0.1;

	public override void _Ready()
	{
		bulletScene = GD.Load<PackedScene>("res://Scenes/Ammo/Gauss_basic.tscn");
		magazine = 27;
		ammo = magazine;
		combo = "UDL";
		combo_input = "";
	}

	public override void _Process(double delta)
	{
		if (Input.IsKeyPressed(Key.Space))
		{
			shoot_timer += delta;
			burst_timer += delta;
			if (shoot_timer >= shoot_delay)
				flag_shooting = true;
			if (flag_shooting && ammo > 0 && burst_timer >= shoot_rate) 
			{
				Player player = (Player)GetParent();
				float angle = player.angle;
				angle += (float)GD.RandRange(-15.0, 15.0) * (Mathf.Pi / 180.0f);
				Shoot(player.Position, angle);
				ammo--;
				burst_timer = 0.0;
			}
		} else
		{
			flag_shooting = false;
			shoot_timer = 0.0;
			burst_timer = 0.0;
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
