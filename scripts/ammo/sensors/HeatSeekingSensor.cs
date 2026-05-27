using Godot;
using System;

[GlobalClass]
public partial class HeatSeekingSensor : SensorResource
{
	[Export] public float	TurnRate;

	public override void Guide(Missile missile, double delta)
	{
		Enemy		target;
		Vector2		desired;
		float		dot;

		target = GetHeatTarget(missile);
		if (target == null)
			return ;
		desired = (target.GlobalPosition - missile.GlobalPosition).Normalized();
		missile.Direction = missile.Direction.Lerp(desired, TurnRate * (float)delta).Normalized();
		dot = missile.Direction.Dot(desired);
		missile.Speed *= 0.97 + 0.03 * dot;
	}

	private Enemy GetHeatTarget(Missile missile)
	{
		Enemy	target;
		double	distance;
		double	Heat_value;
		double	biggest_heat_value;

		target = null;
		biggest_heat_value = 0;
		foreach (Node node in missile.GetTree().GetNodesInGroup("enemies"))
		{
			Enemy enemy = node as Enemy;
			if (enemy != null)
			{
				distance = missile.GlobalPosition.DistanceTo(enemy.GlobalPosition);
				Heat_value = enemy.HeatSignature / (distance * distance);
				if (Heat_value > biggest_heat_value)
				{
					biggest_heat_value = Heat_value;
					target = enemy;
				}
			}
		}
		return (target);
	}
}
