using Godot;

public partial class Basic : Weapon
{
	public override void _Ready()
	{
		bulletScene = GD.Load<PackedScene>("res://Bullet.tscn");
		magazine = 6;
		ammo = magazine;
		combo = "LLR";
		combo_input = "";
	}
}
