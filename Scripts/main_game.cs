using Godot;
using System;
using System.IO;

public partial class main_game : Node2D
{
	static Label lbl_turnIndic;

	public static string p1name = "Player1";
	public static string p2name = "Player2";

	Button btn_rst;
	Button btn_quit;
	
	public override void _Ready()
	{
		// lbl_turnIndic = GetNode<Label>("lbl_turnIndic");		
		// lbl_turnIndic.Text = (GlobalParameters.isPlayer_x) ? "it is " + p1name + "'s turn (X)" : 
		// 											 "it is " + p2name + "'s turn (O)";
		
		// btn_rst = GetNode<Button>("btn_rst");
		// btn_rst.Pressed += () => resetScene();//=> GC_Field.resetField();

		btn_quit = GetNode<Button>("btn_quit");
		btn_quit.Pressed += () => saveQuit();
	}

	public void saveQuit()
	{
		String[] names = {p1name, p2name};
		File.WriteAllLinesAsync("LastNames.txt", names);
		GlobalParameters.SaveNames();
		GetTree().Quit();
	}


}
