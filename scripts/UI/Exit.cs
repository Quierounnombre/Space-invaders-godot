using Godot;
using System;

public partial class Exit: Button
{
	public override void _Ready() {
		Pressed += OnPressed;
	}

	void OnPressed() {
		GetTree().Quit();
	}
}
