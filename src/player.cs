using Godot;
using System;

public partial class Player : Area2D
{
	[Export]
	public int Speed { get; set; } = 400; //Speed of the player in pixels per second
	
	public Vector2 ScreenSize; //Size of the game window
	public override void _Process(double delta)
	{
	}
}
