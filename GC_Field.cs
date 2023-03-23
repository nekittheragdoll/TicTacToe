using Godot;
using System;
using TicTacToeResources;
using System.Collections.Generic;

public partial class GC_Field : GridContainer
{
	public static int SizeOfField = 5;
	FieldTile[,] Field = new FieldTile[GlobalParameters.SizeOfField, GlobalParameters.SizeOfField];
	
	//public static bool isPlayer_x = GlobalParameters.StartingPlayerIsX;
	
	public static int winnerIndic = 0; // 0 --> no winner, 1 --> X won, 2 --> O won
	public override void _Ready()
	{
		GlobalParameters G = GetNode<GlobalParameters>("/root/GlobalParameters");
		this.Columns = GlobalParameters.SizeOfField;
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
		if(ft.PieceExists || GlobalParameters.winnerIndic != 0) {return;}
		ft.PieceExists = true;
		ft.PlayerIsX = GlobalParameters.isPlayer_x;
		ft.ActivateTile();
		GlobalParameters.isPlayer_x = !GlobalParameters.isPlayer_x;
		FieldTile[] twopoints = DidYouDoIt(ft);		// 'twopoints' are starting and ending points of five consecutive marks
		if (twopoints != null)
		{
			GD.Print(ft.PlayerIsX ? "Player X Won" : "Player O Won");
			GD.Print(String.Format("Positions: [{0},{1}] to [{2},{3}]", twopoints[0].PosX, twopoints[0].PosY, twopoints[1].PosX, twopoints[1].PosY ));
		
			GlobalParameters.winnerIndic = ft.PlayerIsX ? 1 : 2;
		}
		;
		main_game.ChangeTurnLabel(GlobalParameters.isPlayer_x, GlobalParameters.winnerIndic);
	}

	public FieldTile[] DidYouDoIt(FieldTile ftvar){

		bool PlayerX = ftvar.PlayerIsX;
		int Y = ftvar.PosY;
		int X = ftvar.PosX; 
		int[] streak = {0,0,0,0};
		FieldTile[] f_span = new FieldTile[4]; //remembers last tile in a streak

		for (int i = 0; i < 10; i++)
		{
			//GlobalParameters.SizeOfField => sizeofX
			if (X - 4 + i < GlobalParameters.SizeOfField && X - 4 + i >= 0)
			{
				if (Field[Y, X - 4 + i].PieceExists && Field[Y, X - 4 + i].PlayerIsX == PlayerX)
				{ streak[0]++; f_span[0] = Field[Y, X - 4 + i]; }
				else streak[0] = 0;
			}
			if (streak[0] >= 5)
			{
				return new FieldTile[] { Field[Y, X - 4 + i - streak[0] + 1], Field[Y, X - 4 + i] };
			}
			//GlobalParameters.SizeOfField => sizeofY
			if (Y - 4 + i < GlobalParameters.SizeOfField && Y - 4 + i >= 0)
			{
				if (Field[Y - 4 + i, X].PieceExists && Field[Y - 4 + i, X].PlayerIsX == PlayerX)
				{ streak[1]++; f_span[1] = Field[Y - 4 + i, X]; }
				else streak[1] = 0;
			}
			if (streak[1] >= 5) { return new FieldTile[] { Field[Y - 4 + i - streak[1] + 1, X], Field[Y - 4 + i, X] }; }
			//GlobalParameters.SizeOfField => sizeofY for Y and sizeofX for X
			if (Y - 4 + i < GlobalParameters.SizeOfField && Y - 4 + i >= 0 && X - 4 + i < GlobalParameters.SizeOfField && X - 4 + i >= 0)
			{
				if (Field[Y - 4 + i, X - 4 + i].PieceExists && Field[Y - 4 + i, X - 4 + i].PlayerIsX == PlayerX)
				{ streak[2]++; f_span[2] = Field[Y - 4 + i, X - 4 + i]; }
				else streak[2] = 0;
			}
			if (streak[2] >= 5) { return new FieldTile[] { Field[Y - 4 + i - streak[2] + 1, X - 4 + i - streak[2] + 1], Field[Y - 4 + i, X - 4 + i] }; }
			//GlobalParameters.SizeOfField => sizeofY for Y and sizeofX for X
			if (Y + 4 - i < GlobalParameters.SizeOfField && Y + 4 - i >= 0 && X - 4 + i < GlobalParameters.SizeOfField && X - 4 + i >= 0)
			{
				if (Field[Y + 4 - i, X - 4 + i].PieceExists && Field[Y + 4 - i, X - 4 + i].PlayerIsX == PlayerX)
				{ streak[3]++; f_span[3] = Field[Y + 4 - i, X - 4 + i]; }
				else streak[3] = 0;
			}
			if (streak[3] >= 5) { return new FieldTile[] { Field[Y + 4 - i + streak[3] - 1, X - 4 + i - streak[3] + 1], Field[Y + 4 - i, X - 4 + i] }; }
		}

		return null;
	}		

	public static void resetField()
	{
		for (int i = 0; i < GlobalParameters.SizeOfField; i++)
		{
			for (int j = 0; j < GlobalParameters.SizeOfField; j++)
			{
				//Field[i,j].PieceExists = false;
				//Field[i,j].PlayerIsX = false;				
				//Field[i,j].TextureNormal = ResourceLoader.Load("res://btn_empty.png") as Texture2D;
			}
		}

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	// public override void _Process(double delta)
	// {
	// }
}
