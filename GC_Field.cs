using Godot;
using System;
using TicTacToeResources;
using System.Collections.Generic;

public partial class GC_Field : GridContainer
{
	public static int SizeOfField = 5;
	FieldTile[,] Field = new FieldTile[SizeOfField,SizeOfField];
	
	public static bool isPlayer_x = Convert.ToBoolean((new Random()).Next(0,2));
	public override void _Ready()
	{
		this.Columns = SizeOfField;
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

				PosX = j,
				PosY = i,
			};
			ft.Pressed += () => TileClicked(ft);
			AddChild(ft);
			Field[i,j] = ft;
			}			
		}
	}

	private void TileClicked(FieldTile ft)
	{
		if(ft.PieceExists) {return;}
		ft.PieceExists = true;
		ft.PlayerIsX = isPlayer_x;
		ft.ActivateTile();
		isPlayer_x = !isPlayer_x;
		FieldTile[] twopoints = DidYouDoIt(ft);
		if (twopoints != null)
		{
			GD.Print(ft.PlayerIsX ? "Player X Won" : "Player O Won");
			GD.Print(String.Format("Positions: [{0},{1}] to [{2},{3}]", twopoints[0].PosX, twopoints[0].PosY, twopoints[1].PosX, twopoints[1].PosY ));
		}
		;
		main_game.ChangeLabel(isPlayer_x);
	}

	public FieldTile[] DidYouDoIt(FieldTile ftvar){

		bool PlayerX = ftvar.PlayerIsX;
		int Y = ftvar.PosY;
		int X = ftvar.PosX; 
		int[] streak = {0,0,0,0};
		FieldTile[] f_span = new FieldTile[4]; //remembers last tile in a streak

		for (int i = 0; i < 10; i++)
		{
			//sizeoffield => sizeofX
			if (X - 4 + i < SizeOfField && X - 4 + i >= 0)
			{
				if (Field[Y, X - 4 + i].PieceExists && Field[Y, X - 4 + i].PlayerIsX == PlayerX)
				{ streak[0]++; f_span[0] = Field[Y, X - 4 + i]; }
				else streak[0] = 0;
			}
			if (streak[0] >= 5)
			{
				return new FieldTile[] { Field[Y, X - 4 + i - streak[0] + 1], Field[Y, X - 4 + i] };
			}
			//sizeoffield => sizeofY
			if (Y - 4 + i < SizeOfField && Y - 4 + i >= 0)
			{
				if (Field[Y - 4 + i, X].PieceExists && Field[Y - 4 + i, X].PlayerIsX == PlayerX)
				{ streak[1]++; f_span[1] = Field[Y - 4 + i, X]; }
				else streak[1] = 0;
			}
			if (streak[1] >= 5) { return new FieldTile[] { Field[Y - 4 + i - streak[1] + 1, X], Field[Y - 4 + i, X] }; }
			//sizeoffield => sizeofY for Y and sizeofX for X
			if (Y - 4 + i < SizeOfField && Y - 4 + i >= 0 && X - 4 + i < SizeOfField && X - 4 + i >= 0)
			{
				if (Field[Y - 4 + i, X - 4 + i].PieceExists && Field[Y - 4 + i, X - 4 + i].PlayerIsX == PlayerX)
				{ streak[2]++; f_span[2] = Field[Y - 4 + i, X - 4 + i]; }
				else streak[2] = 0;
			}
			if (streak[2] >= 5) { return new FieldTile[] { Field[Y - 4 + i - streak[2] + 1, X - 4 + i - streak[2] + 1], Field[Y - 4 + i, X - 4 + i] }; }
			//sizeoffield => sizeofY for Y and sizeofX for X
			if (Y + 4 - i < SizeOfField && Y + 4 - i >= 0 && X - 4 + i < SizeOfField && X - 4 + i >= 0)
			{
				if (Field[Y + 4 - i, X - 4 + i].PieceExists && Field[Y + 4 - i, X - 4 + i].PlayerIsX == PlayerX)
				{ streak[3]++; f_span[3] = Field[Y + 4 - i, X - 4 + i]; }
				else streak[3] = 0;
			}
			if (streak[3] >= 5) { return new FieldTile[] { Field[Y + 4 - i + streak[3] - 1, X - 4 + i - streak[3] + 1], Field[Y + 4 - i, X - 4 + i] }; }
		}

		return null;
	}		

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	// public override void _Process(double delta)
	// {
	// }
}
