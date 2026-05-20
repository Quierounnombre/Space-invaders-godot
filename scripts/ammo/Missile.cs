using Godot;
using System;

/*
Sistema de misiles
Los jugadores pueden configurar su:
Fuel -> more fuel = delta V(for changing course)
Payload -> From nuclear bombs tu tungsten cores, each one has a different OnDetonation() formula that renders a set of E
Sensors -> Did the missile have a way to direct itself or being control remoted by the ship?
 */

public partial class Missile : Area2D
{
	[Export] public double				Speed;
	[Export] public double				Mass;
	[Export] public FuelResource		Fuel;
	[Export] public PayloadResource		Payload;

	public Vector2						Direction = Vector2.Up;

	public override void _PhysicsProcess(double delta)
	{
		double		delta_v;
		double		energy;

		energy = Fuel.Propulsate(this, delta);
		delta_v = energy / Mass;
		Speed += delta_v * delta;
		Position += Direction * (float)Speed;
	}

	public override void _Ready()
	{
		AreaEntered += OnAreaEntered;
		GetNode<VisibleOnScreenNotifier2D>("VisibleOnScreenNotifier2D").ScreenExited += ScreenLeave;
	}

	private void ScreenLeave()
	{
		QueueFree();
	}

	private void OnAreaEntered(Area2D area)
	{
		if (area is Enemy enemy)
		{
			Calc_damage(enemy);
			QueueFree();
		}
	}
	
	private void Calc_damage(Enemy target)
	{
		double e;

		e = Payload.Direct_impact(Mass, Speed);
		target.TakeDamage(e);
	}
}
