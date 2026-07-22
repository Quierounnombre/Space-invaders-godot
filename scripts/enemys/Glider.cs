using Godot;

public partial class Glider : Enemy
{
	[Export] private PackedScene	bulletScene;
	[Export] private float			outerMargin;
	[Export] private float			arcSpan;
	[Export] private float			arcSpeed;
	[Export] private float			fireCooldown;

	private Planeta		planet;
	private RayCast2D	sightRay;
	private float		angle;
	private float		baseAngle;
	private float		glideTimer = 0f;
	private float		cooldownLeft = 0f;

	public override void _Ready()
	{
		base._Ready();
		planet = GetNode<Planeta>("../Planeta");
		sightRay = GetNode<RayCast2D>("RayCast2D");
		baseAngle = Mathf.Atan2(-(Position.Y - Player.circleCenter.Y), Position.X - Player.circleCenter.X);
	}

	public override void _PhysicsProcess(double delta)
	{
		glideTimer += (float)delta;
		angle = baseAngle + Mathf.Sin(glideTimer * arcSpeed) * arcSpan;
		float radius = planet.radius + outerMargin;
		Position = Player.circleCenter + new Vector2(Mathf.Cos(angle), -Mathf.Sin(angle)) * radius;

		cooldownLeft -= (float)delta;
		if (cooldownLeft <= 0f)
		{
			Shoot();
			cooldownLeft = fireCooldown;
		}
	}

	private void Shoot()
	{
		var bullet = bulletScene.Instantiate<IProjectile>();
		bullet.Position = Position;
		((Node2D)bullet).ZIndex = -1;
		bullet.Direction = new Vector2(Mathf.Cos(angle), -Mathf.Sin(angle));
		bullet.Rotation = -angle + Mathf.Pi / 2f;
		GetTree().CurrentScene.AddChild((Node)bullet);
	}
}
