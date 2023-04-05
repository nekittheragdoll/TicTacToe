using Godot;
using System;

public partial class main_menu : Control
{
	SpinBox sb_fieldSize;
	Button btn_setSize;
	Button btn_lastNames;
	Button btn_showLB;
	LineEdit le_p1;
	LineEdit le_p2;
	PackedScene LeaderBoard = (PackedScene)GD.Load("res://Scenes/LeaderBoard.tscn");
	public override void _Ready()
	{
		sb_fieldSize = GetNode<SpinBox>("sb_fieldSize");

		btn_setSize = GetNode<Button>("btn_setSize");
		btn_setSize.Pressed += () => startGame(); 

		le_p1 = GetNode<LineEdit>("le_p1");
		le_p2 = GetNode<LineEdit>("le_p2");

		btn_lastNames = GetNode<Button>("btn_lastNames");
		btn_lastNames.Pressed += () => loadLastNames();

		btn_showLB = GetNode<Button>("btn_ShowLeaderBoard");
		btn_showLB.Pressed += () => AddChild(LeaderBoard.Instantiate());
	}

	public void startGame()
	{
		if(le_p1.Text == ""){
		GlobalParameters.PXname = "Player1";}
		else if(le_p2.Text == ""){
		GlobalParameters.POname = "Player2";}
		else{
		GlobalParameters.PXname = le_p1.Text;
		GlobalParameters.POname = le_p2.Text;}
		GlobalParameters.SizeOfField = Convert.ToInt32(sb_fieldSize.Value);
		GetTree().ChangeSceneToFile("res://Scenes/main_game.tscn");
	}

	public void loadLastNames(){
		if (!FileAccess.FileExists("res://LastNames.dat"))
		{
			return; // Error! We don't have a save to load.
		}

		using var file = FileAccess.Open("res://LastNames.dat", FileAccess.ModeFlags.Read);
		le_p1.Text = file.GetLine();
		le_p2.Text = file.GetLine();
	}


}
