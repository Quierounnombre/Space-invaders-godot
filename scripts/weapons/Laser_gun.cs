using Godot;

public partial class Laser_gun : Weapon
{
	private double					charge_power_timer = 0.0;
	private const double			CHARGE_MAX_TIME = 1.0;
	private AnimatedSprite2D		animation;

	public override void _Ready()
	{
		magazine = 1;
		ammo = magazine;
		combo = "RLRL";
		combo_input = "";
		animation = GetNode<AnimatedSprite2D>("Animacion");
		animation.SpeedScale = 0.2f;
	}

	public override void _Process(double delta)
	{
		if (Input.IsKeyPressed(Key.Space))
		{
			if (!animation.IsPlaying() && ammo > 0)
			{
				animation.Play();
			}
			charge_power_timer += delta;
			if (charge_power_timer >= CHARGE_MAX_TIME)
			{
				if (ammo > 0)
				{
					Player player = (Player)GetParent();
					float angle = player.angle;
					Shoot();
					ammo--;
					animation.Stop();
				}
				charge_power_timer = 0.0;
			}
		}
		else
		{
			animation.Stop();
			charge_power_timer = 0.0f;
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

	public void Shoot()
	{
		Player parent = (Player)GetParent();
		var bullet = (Laser_ray)bulletScene.Instantiate();
		bullet.Source = parent;
		GetTree().CurrentScene.AddChild(bullet);
	}
}
