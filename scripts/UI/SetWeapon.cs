using Godot;
using System;

public partial class SetWeapon: Button
{
	[Export] public PackedScene		scene;

	public override void _Ready() {
		Pressed += OnPressed;
	}

	void OnPressed() {
		GetNode<GameState>("/root/GameState").weapon_scene = scene;
	}
}
