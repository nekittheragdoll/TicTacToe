using Godot;
using System;
using TicTacToeResources;
using System.Collections.Generic;

public partial class GC_Field : GridContainer
{
	FieldTile[] Field = new FieldTile[25];
	// Called when the node enters the scene tree for the first time.
	bool isPlayer_x = true;
	public override void _Ready()
	{
		for (int i = 0; i < Field.Length; i++)
		{
			FieldTile ft = new FieldTile(){
				SizeFlagsHorizontal = SizeFlags.ExpandFill,
				Visible = true,
			};
			ft.Pressed += () => TileClicked(ft);
			AddChild(ft);
		}
	}

	private void TileClicked(FieldTile ft)
	{
		ft.PlayerIsX = isPlayer_x;
		GD.Print(isPlayer_x);
		ft.ActivateTile();
		isPlayer_x = !isPlayer_x;
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
