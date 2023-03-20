using Godot;
using System;

public partial class main_game : Node2D
{
	static Label lbl_turnIndic;

	public static string p1name = "Player1";
	public static string p2name = "Player2";
	
	public override void _Ready()
	{
		lbl_turnIndic = GetNode<Label>("lbl_turnIndic");		
		lbl_turnIndic.Text = (GC_Field.isPlayer_x) ? "it is " + p1name + "'s turn (X)" : "it is " + p2name + "'s turn (O)";
		
	}

	public static void ChangeLabel(bool playerx){

		lbl_turnIndic.Text = (playerx) ? "it is " + p1name + "'s turn (X)" : "it is " + p2name + "'s turn (O)";
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
