using Godot;
using System;

public partial class GameState : Node
{
	public PackedScene			weapon_scene;
	public string				PreviousScene;

	public void GoBack()
	{
		GetTree().ChangeSceneToFile(PreviousScene);
	}

	public void GoTo(string scene)
	{
		PreviousScene = GetTree().CurrentScene.SceneFilePath;
		GetTree().ChangeSceneToFile(scene);
	}
}
