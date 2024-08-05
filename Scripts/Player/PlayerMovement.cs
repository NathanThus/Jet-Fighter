using System;
using Godot;
public partial class PlayerMovement : CharacterBody2D
{
	[Export]
	public int Acceleration { get; set; } = 100;

	[Export]
	public int MaxSpeed {get; set;} = 1200;

	[Export]
	public int Drag {get; set;} = 100;

	[Export]
	public float RotationSpeed { get; set; } = 3f;

	[Export]
	public RigidBody2D Rigidbody;

	private float _rotationDirection;
	private int _currentSpeed = 0;
	private Vector2 _gravity = (Vector2)ProjectSettings.GetSetting("physics/2d/default_gravity_vector");

	public void GetInput()
	{
		_rotationDirection = Input.GetAxis("Left", "Right");
		Velocity = CalculateAcceleration();
		Velocity += _gravity * Rigidbody.GravityScale;
	}

	private Vector2 CalculateAcceleration()
	{
		if(!Input.IsActionPressed("Forward")) 
		{
			_currentSpeed -= Drag;
			if(_currentSpeed < 0)
			{
				_currentSpeed = 0 ;
			}
			return Velocity;
		}

		_currentSpeed += Acceleration;

		if(_currentSpeed + Acceleration > MaxSpeed)
		{
			_currentSpeed = MaxSpeed;
			return Transform.X * MaxSpeed;
		}

		return Transform.X * _currentSpeed;
	}

	public override void _PhysicsProcess(double delta)
	{
		GetInput();
		Rotation += _rotationDirection * RotationSpeed * (float)delta;
		MoveAndSlide();
	}
}
