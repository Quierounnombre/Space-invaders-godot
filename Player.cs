using Godot;
using System;

public partial class Player : CharacterBody2D
{
	private int					speed = 400;
	private float				dir_horizontal = 0;
	private float				horizontal_size;
	private float				sprite_half_size;
	private PackedScene			bulletScene;

	public void GetInput()
	{
		if (Input.IsKeyPressed(Key.A))
		{
			dir_horizontal = -1f;
			Velocity = new Vector2(dir_horizontal * speed, 0f);
		}
		else if (Input.IsKeyPressed(Key.D))
		{
			dir_horizontal = 1f;
			Velocity = new Vector2(dir_horizontal * speed, 0f);
		}
	}

	public override void _Ready()
	{
		Viewport	view;
		Rect2		windows;
		Vector2		size_of_window;

		view = GetViewport();
		windows = view.GetVisibleRect();
		size_of_window = windows.Size;
		horizontal_size = size_of_window.X;
		GD.Print(horizontal_size);

		//SPRITE
		Sprite2D	sprite;

		sprite = GetNode<Sprite2D>("Sprite2D");
		Vector2 size = sprite.Texture.GetSize() * sprite.Scale;
		sprite_half_size = size.X / 2f;

		//START POS
		Vector2 pos;

		pos.X = 0f;
		pos.Y = size_of_window.Y - (size.Y / 2);
		Position = pos;

		//LOAD SCENE
		bulletScene = GD.Load<PackedScene>("res://Bullet.tscn");
	}

	public override void _PhysicsProcess(double delta)
	{
		GetInput();
		MoveAndSlide();

		Vector2 pos = Position;
		pos.X = Mathf.Clamp(pos.X, sprite_half_size, horizontal_size - sprite_half_size);
		Position = pos;
		GD.Print(Position);
	}

	public override void _Process(double delta)
	{
		if (Input.IsKeyPressed(Key.Space))
		{
			var bullet = bulletScene.Instantiate<Bullet>();
			bullet.Position = Position;
			bullet.Init(Vector2.Right); // or your facing direction
			GetTree().CurrentScene.AddChild(bullet);
		}
	}
}
