using Godot;
using System;

public partial class main_game : Node2D
{
	static Label lbl_turnIndic;

	public static string p1name = "Player1";
	public static string p2name = "Player2";

	Button btn_rst;
	
	public override void _Ready()
	{
		lbl_turnIndic = GetNode<Label>("lbl_turnIndic");		
		lbl_turnIndic.Text = (GC_Field.isPlayer_x) ? "it is " + p1name + "'s turn (X)" : "it is " + p2name + "'s turn (O)";
		
		btn_rst = GetNode<Button>("btn_rst");
		btn_rst.Pressed += () => GC_Field.resetField();
	}

	public static void ChangeTurnLabel(bool playerx, int winner){
		if (winner == 0)
		lbl_turnIndic.Text = (playerx) ? "it is " + p1name + "'s turn (X)" : "it is " + p2name + "'s turn (O)";
		else if(winner == 1)
		lbl_turnIndic.Text = p1name + " (X) won!";
		else if(winner == 2)
		lbl_turnIndic.Text = p2name + " (O) won!";
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
