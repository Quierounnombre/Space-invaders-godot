using Godot;
using System;

public partial class DebugCheat : Node
{

	public void GetInput()
	{
		if (Input.IsKeyPressed(Key.Escape))
		{
			foreach (Enemy enemy in GetTree().GetNodesInGroup("enemies"))
			{
				enemy.Speed = 0f;
			}
		}
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GetInput();
	}
}
