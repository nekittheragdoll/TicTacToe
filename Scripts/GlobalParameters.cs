using Godot;
using System;

public partial class GlobalParameters : Node
{
	public static int SizeOfField = 5;	//5 is default and minium value

	public static string PXname = "<placeholder for X>";
	public static string POname = "<placeholder for O>";

	public static bool isPlayer_x = true;
	public static int winnerIndic = 0; // 0 --> no winner, 1 --> X won, 2 --> O won

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{		
		DisplayServer.WindowSetSize(new Vector2I(854, 480));
		isPlayer_x = Convert.ToBoolean((new Random()).Next(0,2));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	
}
