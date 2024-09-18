using Godot;
using System;

public partial class TargetHealth : RigidBody2D
{
	#region Exported Fields
	// Health
	[Export]
	public int MaxHealth { get; private set; }

	private int currentHealth;

	// Children
	[Export]
	private PackedScene ChildScene;
	[Export]
	public int ChildCount {get; private set;}
	[Export]
	public int SpawnOffset {get; private set;}

	#endregion

	RandomNumberGenerator randomNumberGenerator = new();

	public void Hit(int damage)
	{
		if (damage >= currentHealth) Die();

		currentHealth -= damage;
	}

	private void Die()
	{
		if(ChildScene != null)
		{
			for (int i = 0; i < ChildCount; i++)
			{
				var child = ChildScene.Instantiate<RigidBody2D>();
				child.Rotation = GlobalRotation;
				child.GlobalPosition = new Vector2(Position.X + randomNumberGenerator.RandfRange(-SpawnOffset, SpawnOffset),
									   				Position.Y + randomNumberGenerator.RandfRange(-SpawnOffset, SpawnOffset));
				child.LinearVelocity = new Vector2(randomNumberGenerator.RandfRange(-1,1),randomNumberGenerator.RandfRange(-1,1));
				GetTree().Root.AddChild(child);
			}
		}
		QueueFree();
	}

	public void Heal(int hitpoints)
	{
		if (hitpoints + currentHealth >= MaxHealth)
		{
			currentHealth = MaxHealth;
		}
		currentHealth += hitpoints;
	}
	
	private void _on_body_entered(Node body)
	{
		if(!body.IsInGroup("PlayerBullet")) return;
		Hit(10);
		body.QueueFree();
	}
}
