using Godot;

public partial class Minigun : Weapon
{
	private double					burst_timer = 0.0;
	private AnimatedSprite2D		animation;
	public double					shoot_rate = 1.0;

	public override void _Ready()
	{
		magazine = 150;
		ammo = magazine;
		combo = "RDL";
		combo_input = "";
		animation = GetNode<AnimatedSprite2D>("Animacion");
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
			shoot_rate = 1;
			animation.SpeedScale = 0.2f;
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
		if (Input.IsKeyPressed(Key.Space) && flag_shooting == true)
		{
			if (shoot_rate > 0.05)
			{
				shoot_rate -= delta / 2;
				animation.SpeedScale = (float)(1.0 / shoot_rate);
			}
		}
		else if (flag_shooting == true)
		{
			if (shoot_rate < 1)
			{
				shoot_rate += delta / 2;
				animation.SpeedScale = (float)(0.001 / shoot_rate);
			}
		}
	}
}
