using Godot;
using System;

public partial class Salida: Button
{
	public override void _Ready() {
		Pressed += OnPressed;
	}

	void OnPressed() {
		GetNode<GameState>("/root/GameState").GoBack();
	}
}
