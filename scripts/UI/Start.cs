using Godot;
using System;

public partial class Start: Button
{
	public override void _Ready() {
		Pressed += OnPressed;
	}

	void OnPressed() {
		GetTree().ChangeSceneToFile("res://level_1.tscn");
	}
}
