using Godot;
using System;

[GlobalClass]
public partial class LidarSensor : SensorResource
{
	[Export] public float		TurnRate;
	[Export] public float		TimeForActivation;
	[Export] public Shape2D		DetectionShape;
	private double				time_since_launch = 0f;

	public override void Guide(Missile missile, double delta)
	{
		Enemy		target;
		Vector2		desired;
		float		dot;

		time_since_launch += delta;
		if (TimeForActivation > time_since_launch)
			return ;
		target = GetTarget(missile);
		if (target == null)
			return ;
		desired = (target.GlobalPosition - missile.GlobalPosition).Normalized();
		missile.Direction = missile.Direction.Lerp(desired, TurnRate * (float)delta).Normalized();
		dot = missile.Direction.Dot(desired);
		missile.Speed *= 0.97 + 0.03 * dot;
	}

	private Enemy GetTarget(Missile missile)
	{
		Area2D	_area;
		Enemy	target;
		double	distance;
		double	min_distance;

		target = null;
		min_distance = 1000;
		distance = 0;
		_area = GetOrCreateArea(missile);
		foreach (var overlap in _area.GetOverlappingAreas())
		{
			var enemy = overlap as Enemy;
			if (enemy == null)
				continue ;
			distance = missile.GlobalPosition.DistanceTo(enemy.GlobalPosition);
			if (distance < min_distance)
			{
				target = enemy;
				min_distance = distance;
			}
		}
		return target;
	}

	private Area2D GetOrCreateArea(Missile missile)
	{
		if (missile.SensorArea.GetChildCount() == 0)
		{
			var shape = new CollisionShape2D();
			shape.Shape = DetectionShape ?? new CircleShape2D { Radius = 200 };
			missile.SensorArea.AddChild(shape);
		}
		return missile.SensorArea;
	}
}
