using Godot;
using System;
using System.Collections.Generic;

public partial class Player : CharacterBody2D
{
	//Ship
	[Export] private float		acceleration = 0.003f;
	private float				speed = 0.0f;
	private float				speed_limit = 0.07f;
	public float				angle = Mathf.Pi / 2f; // start at bottom (90°)
	private Weapon				currentWeapon;
	public static Vector2		circleCenter;
	private Planeta				planet;

	//Graphics
	private float				horizontal_size;
	private float				sprite_half_size;
	private AnimatedSprite2D	left_animation;
	private AnimatedSprite2D	right_animation;

	public void GetInput()
	{
		if (Input.IsKeyPressed(Key.A))
		{
			right_animation.Play("Propulsor_on");
			left_animation.Play("Propulsor_idle");
			speed += acceleration;
		}
		else if (Input.IsKeyPressed(Key.D))
		{
			left_animation.Play("Propulsor_on");
			right_animation.Play("Propulsor_idle");
			speed -= acceleration;
		}
		else
		{
			if (right_animation.Animation == "Propulsor_on"
				|| left_animation.Animation == "Propulsor_on")
			{
				right_animation.Frame = 0;
				left_animation.Frame = 0;
				left_animation.Play("Propulsor_idle");
				right_animation.Play("Propulsor_idle");
			}
			speed *= planet.friction;
		}
		if (Input.IsKeyPressed(Key.W))
			planet.radius++;
		else if (Input.IsKeyPressed(Key.S))
			planet.radius--;
	}

	public void Equip(PackedScene weapon_scene)
	{
		if (currentWeapon != null)
			currentWeapon.QueueFree();
		currentWeapon = weapon_scene.Instantiate<Weapon>();
		AddChild(currentWeapon);
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

		//Set planet
		planet = GetNode<Planeta>("../Planeta");

		//SPRITE
		Sprite2D	sprite;

		sprite = GetNode<Sprite2D>("Sprite2D");
		Vector2 size = sprite.Texture.GetSize() * sprite.Scale;
		sprite_half_size = size.X / 2f;

		left_animation = GetNode<AnimatedSprite2D>("Propulsor_izq");
		right_animation = GetNode<AnimatedSprite2D>("Propulsor_der");

		//START POS
		circleCenter.X = size_of_window.X / 2;
		circleCenter.Y = size_of_window.Y;

		//AREA ENTER
		GetNode<Area2D>("Area2D").AreaEntered += OnAreaEntered;

		//Equip(new Basic());
		Equip(GetNode<GameState>("/root/GameState").weapon_scene);
	}

	public override void _PhysicsProcess(double delta)
	{
		if (!Input.IsKeyPressed(Key.R))
		{
			GetInput();	
		}
		speed = Mathf.Clamp(speed, -speed_limit, speed_limit);
		angle += speed;
		angle = Mathf.Clamp(angle, 0f, Mathf.Pi); // lock to semicircle
		MoveAndSlide();
		Position = circleCenter + new Vector2(Mathf.Cos(angle), -Mathf.Sin(angle)) * planet.radius;
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
