using Godot;
using System;

/*
Fuel -> more fuel = delta V(for changing course)
 */

[GlobalClass]
public partial class FuelResource : Resource
{
	[Export] public double Fuel;
	[Export] public double density;
	[Export] public double Energy_per_unit;
	[Export] public double Burn_rate;

	public double Propulsate(Missile missile, double delta) {
		double e;
		double expected_burn;
		double propulsion_percentage;

		if (Fuel == 0.0)
			return (0.0);
		expected_burn = Burn_rate * delta;
		if (Fuel > expected_burn)
			e = Energy_per_unit * Burn_rate * delta;
		else {
			propulsion_percentage = Fuel / expected_burn;
			expected_burn = Fuel;
			e = Energy_per_unit * Burn_rate * delta * propulsion_percentage;
		}
		Fuel -= expected_burn;
		missile.Mass -= Fuel * density;
		return (e);
	}
}
