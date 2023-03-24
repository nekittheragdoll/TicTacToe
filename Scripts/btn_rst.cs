using Godot;
using System;

public partial class btn_rst : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Pressed += () => GetTree().ChangeSceneToFile("res://Scenes/main_game.tscn");
	}
}
