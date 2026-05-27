using Godot;

[GlobalClass]
public partial class Basic : Weapon
{
	public override void _Ready()
	{
		magazine = 6;
		ammo = magazine;
		combo = "LLR";
		combo_input = "";
	}
}
