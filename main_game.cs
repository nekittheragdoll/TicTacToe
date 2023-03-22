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
		lbl_turnIndic = GetNode<Label>("lbl_turnIndic");		
		lbl_turnIndic.Text = (GC_Field.isPlayer_x) ? "it is " + p1name + "'s turn (X)" : 
													 "it is " + p2name + "'s turn (O)";
		
		btn_rst = GetNode<Button>("btn_rst");
		btn_rst.Pressed += () => resetScene();//=> GC_Field.resetField();

		btn_quit = GetNode<Button>("btn_quit");
		btn_quit.Pressed += () => saveQuit();
	}

	public void resetScene(){
		GetTree().ChangeSceneToFile("res://main_game.tscn");
		GC_Field.winnerIndic = 0;
	}

	public void saveQuit()
	{
		String[] names = {p1name, p2name};
		File.WriteAllLinesAsync("LastNames.txt", names);
		GetTree().Quit();
	}

	public static void ChangeTurnLabel(bool playerx, int winner){
		if (winner == 0)
		lbl_turnIndic.Text = (playerx) ? "it is " + p1name + "'s turn (X)" : 
										 "it is " + p2name + "'s turn (O)";
		else
		lbl_turnIndic.Text = (winner == 1) ? p1name + " (X) won!" : p2name + " (O) won!";
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
