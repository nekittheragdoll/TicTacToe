using Godot;
using System;
using System.IO;

public partial class main_game : Node2D
{
	public static string p1name = "Player1";
	public static string p2name = "Player2";

	Button btn_rst;
	Button btn_quit;
	
	public override void _Ready()
	{		
		// btn_rst = GetNode<Button>("btn_rst");
		// btn_rst.Pressed += () => resetScene();//=> GC_Field.resetField();

		btn_quit = GetNode<Button>("btn_quit");
		btn_quit.Pressed += () => saveQuit();
	}

	public void saveQuit()
	{
		String[] names = { p1name, p2name };
		//File.WriteAllLinesAsync("LastNames.txt", names);
		GlobalParameters.SaveNames();

		GetTree().Quit();

	}


}
