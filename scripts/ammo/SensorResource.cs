using Godot;
using System;

/*
Payload in TNT kilos
*/

[GlobalClass]
public abstract partial class SensorResource : Resource
{
	public abstract void Guide(Missile missile, double delta);
}
