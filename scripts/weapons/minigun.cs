using Godot;

public partial class Minigun : Weapon
{
	public override void _Ready()
	{
		bulletScene = GD.Load<PackedScene>("res://Scenes/Ammo/Bullet.tscn");
		magazine = 6;
		ammo = magazine;
		combo = "LLR";
		combo_input = "";
	}
}
