using Godot;
using System;

public partial class LoadScene: Button
{
	[Export] public PackedScene		Scene;

	public override void _Ready() {
		Pressed += OnPressed;
	}

	void OnPressed() {
		GetTree().ChangeSceneToPacked(Scene);
	}
}
