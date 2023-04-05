using Godot;
using System;
using System.IO;

public partial class main_game : Node2D
{
	Button btn_rst;
	Button btn_quit;
	
	public override void _Ready()
	{
		btn_quit = GetNode<Button>("btn_quit");
		btn_quit.Pressed += () => saveQuit();
	}

	public void saveQuit()
	{
		GlobalParameters.SaveNames();

		GetTree().Quit();
	}


}
