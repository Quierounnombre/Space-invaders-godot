using Godot;
using System;

/*
Payload in TNT kilos
*/

[GlobalClass]
public partial class PayloadResource : Resource
{
	private const float TNT_ENERGY_RATE = 4198f;

	[Export] public float Kilos_of_TNT;
	[Export] public float Area_of_impact;

	public double Direct_impact(double m, double v)
	{
		return (Detonation() + Kinetic_energy(m, v));
	}

	private double Detonation()
	{
		return (Kilos_of_TNT * TNT_ENERGY_RATE);
	}

	private double Kinetic_energy(double m, double v)
	{
		return (0.5 * m * v * v);
	}

	//Indirect impact mechanic23
}
