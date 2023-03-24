using Godot;
using System;

public partial class main_menu : Control
{
	// Called when the node enters the scene tree for the first time.
	SpinBox sb_fieldSize;
	Button btn_setSize;
	Button btn_lastNames;
	LineEdit le_p1;
	LineEdit le_p2;
	public override void _Ready()
	{
		sb_fieldSize = GetNode<SpinBox>("sb_fieldSize");

		btn_setSize = GetNode<Button>("btn_setSize");
		btn_setSize.Pressed += () => startGame(); 

		le_p1 = GetNode<LineEdit>("le_p1");
		le_p2 = GetNode<LineEdit>("le_p2");

		btn_lastNames = GetNode<Button>("btn_lastNames");
		btn_lastNames.Pressed += () => loadLastNames();
	}

	public void startGame()
	{
		if(le_p1.Text == ""){
		main_game.p1name = "Player1";}
		else if(le_p2.Text == ""){
		main_game.p2name = "Player2";}
		else{
		GlobalParameters.PXname = le_p1.Text;
		GlobalParameters.POname = le_p2.Text;}
		GlobalParameters.SizeOfField = Convert.ToInt32(sb_fieldSize.Value);
		GetTree().ChangeSceneToFile("res://Scenes/main_game.tscn");
	}

	public void loadLastNames(){
		String[] names = System.IO.File.ReadAllLines("LastNames.txt");
		le_p1.Text = names[0];
		le_p2.Text = names[1];
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
