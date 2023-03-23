using Godot;
using System;

public partial class GlobalParameters : Node
{
	public static int SizeOfField = 5;

	public static bool isPlayer_x = true;//Convert.ToBoolean((new Random()).Next(0,2));
	public static int winnerIndic = 0; // 0 --> no winner, 1 --> X won, 2 --> O won


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	
}
