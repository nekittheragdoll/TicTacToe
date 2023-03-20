using Godot;
using System;

public partial class main_menu : Control
{
	// Called when the node enters the scene tree for the first time.
	SpinBox sb_fieldSize;
	Button btn_setSize;

	LineEdit le_p1;
	LineEdit le_p2;
	public override void _Ready()
	{
		sb_fieldSize = GetNode<SpinBox>("sb_fieldSize");
		btn_setSize = GetNode<Button>("btn_setSize");
		le_p1 = GetNode<LineEdit>("le_p1");
		le_p2 = GetNode<LineEdit>("le_p2");
		btn_setSize.Pressed += () => startGame(); 
	}

	public void startGame()
	{
		main_game.p1name = le_p1.Text;
		main_game.p2name = le_p2.Text;
		GC_Field.SizeOfField = Convert.ToInt32(sb_fieldSize.Value);
		GetTree().ChangeSceneToFile("res://main_game.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
