using Godot;
using System;

public partial class Player : CharacterBody2D
{
	private float				acceleration = 0.003f;
	private float				speed = 0.0f;
	private float				friction = 0.92f;
	private float				speed_limit = 0.07f;
	private float				horizontal_size;
	private float				sprite_half_size;
	private float				radius = 300f;         // adjust to taste
	public float				angle = Mathf.Pi / 2f; // start at bottom (90°)
	public static Vector2		circleCenter;

	public void GetInput()
	{
		if (Input.IsKeyPressed(Key.A))
			speed += acceleration;
		else if (Input.IsKeyPressed(Key.D))
			speed -= acceleration;
		else
			speed *= friction;
		if (Input.IsKeyPressed(Key.W))
			radius++;
		else if (Input.IsKeyPressed(Key.S))
			radius--;
		speed = Mathf.Clamp(speed, -speed_limit, speed_limit);
		angle += speed;
		angle = Mathf.Clamp(angle, 0f, Mathf.Pi); // lock to semicircle
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

		//SPRITE
		Sprite2D	sprite;

		sprite = GetNode<Sprite2D>("Sprite2D");
		Vector2 size = sprite.Texture.GetSize() * sprite.Scale;
		sprite_half_size = size.X / 2f;

		//START POS
		circleCenter.X = size_of_window.X / 2;
		circleCenter.Y = size_of_window.Y;

		//AREA ENTER
		GetNode<Area2D>("Area2D").AreaEntered += OnAreaEntered;
	}

	public override void _PhysicsProcess(double delta)
	{
		GetInput();
		MoveAndSlide();
		Position = circleCenter + new Vector2(Mathf.Cos(angle), -Mathf.Sin(angle)) * radius;
		Rotation = -angle + Mathf.Pi / 2f;
	}

	public override void _Process(double delta)
	{
	}

	private void OnAreaEntered(Area2D area)
	{
		if (area is Enemy)
		{
			QueueFree();
		}
	}
}
