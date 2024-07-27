using Godot;
public partial class PlayerMovement : CharacterBody2D
{
	[Export]
	public int Speed { get; set; } = 400;

	[Export]
	public float RotationSpeed { get; set; } = 1.5f;

	private float _rotationDirection;

	public void GetInput()
	{
		_rotationDirection = Input.GetAxis("Left", "Right");
		Velocity = Transform.X * Input.GetAxis("Brake", "Forward") * Speed;
	}

	public override void _PhysicsProcess(double delta)
	{
		GetInput();
		Rotation += _rotationDirection * RotationSpeed * (float)delta;
		MoveAndSlide();
	}
}
