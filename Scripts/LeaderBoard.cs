using Godot;
using System;

public partial class LeaderBoard : Window
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.CloseRequested += QueueFree;
	}
}
