using Godot;

public partial class Borders : Node2D
{
	[Export]
	public CharacterBody2D Player = default;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double _)
	{
		Position = new Vector2(Player.Position.X + 512, Position.Y);
	}
}
