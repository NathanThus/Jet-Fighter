using Godot;

public partial class BulletTimer : RigidBody2D
{
	[Export] 
	Godot.Timer timer;

	public override void _Ready()
	{
		timer.Timeout += () => QueueFree();
	}

}
