using Godot;

public partial class Minigun : Weapon
{
	private double	burst_timer = 0.0;
	private double	shoot_rate = 1;
	public override void _Ready()
	{
		bulletScene = GD.Load<PackedScene>("res://Scenes/Ammo/Bullet.tscn");
		magazine = 150;
		ammo = magazine;
		combo = "RDL";
		combo_input = "";
	}

	public override void _Process(double delta)
	{
		if (Input.IsKeyPressed(Key.Space))
		{
			burst_timer += delta;
			flag_shooting = true;
			if (flag_shooting && ammo > 0 && burst_timer >= shoot_rate) 
			{
				Player player = (Player)GetParent();
				float angle = player.angle;
				Shoot(player.Position, angle);
				ammo--;
				burst_timer = 0.0;
			}
		}
		else
		{
			flag_shooting = false;
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
		if (Input.IsKeyPressed(Key.J) && burst_timer > 0)


	}
}
