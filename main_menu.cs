using Godot;
using System;

public partial class main_menu : Control
{
	// Called when the node enters the scene tree for the first time.
	SpinBox sb_fieldSize;
	Button btn_setSize;
	public override void _Ready()
	{
		sb_fieldSize = GetNode<SpinBox>("sb_fieldSize");
		btn_setSize = GetNode<Button>("btn_setSize");
		btn_setSize.Pressed += () => startGame(); 
	}

	public void startGame()
	{
		GC_Field.SizeOfField = Convert.ToInt32(sb_fieldSize.Value);
		GetTree().ChangeSceneToFile("res://main_game.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
