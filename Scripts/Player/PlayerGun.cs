using Godot;
using System;

public partial class PlayerGun : Node2D
{
	[Export] 
	private PackedScene BulletScene;

	[Export]
	private CharacterBody2D Player = default;


	[Export]
	private double FireDelaySeconds = 0.2;
	[Export]
	private int BulletSpeed = 600;

	private double _timeSinceLastShot = 0;
	private double _shotTimer = 0;
	public override void _Process(double delta)
	{
		_shotTimer += delta;
		if(!Input.IsActionPressed("Fire"))
		{
			return;
		}

		if(_shotTimer >= _timeSinceLastShot + FireDelaySeconds)
		{
			var bullet = BulletScene.Instantiate<RigidBody2D>();
			bullet.Rotation = GlobalRotation;
			bullet.GlobalPosition = GlobalPosition;
			bullet.LinearVelocity = bullet.Transform.X * BulletSpeed;

			GetTree().Root.AddChild(bullet);
			_timeSinceLastShot = _shotTimer;
		}
	}
}
