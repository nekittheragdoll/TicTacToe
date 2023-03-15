using Godot;
using System;
using TicTacToeResources;
using System.Collections.Generic;

public partial class GC_Field : GridContainer
{
	FieldTile[,] Field = new FieldTile[5,5];
	// Called when the node enters the scene tree for the first time.
	bool isPlayer_x = true;
	public override void _Ready()
	{
		for (int i = 0; i < Field.GetLength(0); i++)
		{
			for (int j = 0; j < Field.GetLength(1); j++)
			{
				FieldTile ft = new FieldTile(){
				SizeFlagsHorizontal = SizeFlags.ExpandFill,
				SizeFlagsVertical = SizeFlags.ExpandFill,
				Scale = new Vector2(0.05f, 0.05f),
				Visible = true,
				TextureNormal = ResourceLoader.Load("res://btn_empty.png") as Texture2D,
				IgnoreTextureSize = true,
				StretchMode = TextureButton.StretchModeEnum.Scale,

				PosX = i,
				PosY = j,
			};
			ft.Pressed += () => TileClicked(ft);
			AddChild(ft);
			Field[i,j] = ft;
			}			
		}
	}

	private void TileClicked(FieldTile ft)
	{
		ft.PieceExists = true;
		ft.PlayerIsX = isPlayer_x;
		GD.Print(isPlayer_x);
		ft.ActivateTile();
		isPlayer_x = !isPlayer_x;
		CheckSurrounding(ft);
	}

	private void CheckSurrounding(FieldTile ftvar){

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
