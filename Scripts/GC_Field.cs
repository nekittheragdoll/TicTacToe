using Godot;
using System;
using TicTacToeResources;
using System.Collections.Generic;
using System.Linq;

public partial class GC_Field : GridContainer
{
	[Signal] public delegate void BoardStatusChangedEventHandler(int state);	// 1 - player X playing ; 2 - player O playing; 3 - player X won; 4 - player O Won
	FieldTile[,] Field = new FieldTile[GlobalParameters.SizeOfField, GlobalParameters.SizeOfField];
	int[] io;


	//My TTT dream does not include the state, which looks like a bubblebum, and tastes like a bubblebum
	enum GameState { XisPlaying, OisPlaying, GameOver }
	GameState CurrentState = GameState.GameOver;


	public override void _Ready()
	{
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
				TextureNormal = ResourceLoader.Load("res://Images/btn_empty.png") as Texture2D,
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
		bool rndTrue = (new Random()).Next(1,10) > 5;
		CurrentState = rndTrue ? GameState.XisPlaying : GameState.OisPlaying;
		EmitSignal(SignalName.BoardStatusChanged, rndTrue ? 1 : 2);
		//CurrentState = (GlobalParameters.isPlayer_x) ? GameState.XisPlaying : GameState.OisPlaying;
	}

	private void TileClicked(FieldTile ft)
	{
		if(ft.PieceExists || CurrentState == GameState.GameOver) return;
		//ft.PieceExists = true;
		//ft.PlayerIsX = GlobalParameters.isPlayer_x;
		if (CurrentState == GameState.XisPlaying)
		{
			ft.ActivateTile(true);
			//GlobalParameters.isPlayer_x = true;
			EmitSignal(SignalName.BoardStatusChanged, 2);
			CurrentState = GameState.OisPlaying;
		}
		else
		{
			ft.ActivateTile(false);
			//GlobalParameters.isPlayer_x = false;
			EmitSignal(SignalName.BoardStatusChanged, 1);
			CurrentState = GameState.XisPlaying;
		}
		//GlobalParameters.isPlayer_x = !GlobalParameters.isPlayer_x;
		FieldTile[] twopoints = DidYouDoIt(ft);		// 'twopoints' are starting and ending points of five consecutive marks
		if (twopoints != null)
		{
			GD.Print(ft.PlayerIsX ? "Player X Won" : "Player O Won");	//#debug func#
			GD.Print(String.Format("Positions: [{0},{1}] to [{2},{3}]", twopoints[0].PosX, twopoints[0].PosY, twopoints[1].PosX, twopoints[1].PosY )); //#debug func#

			EmitSignal(SignalName.BoardStatusChanged, ft.PlayerIsX ? 3 : 4);
			//GlobalParameters.winnerIndic = ft.PlayerIsX ? 1 : 2;
			//---
			int cnt = 0;
			foreach (var item in Field) { if (item.PieceExists && item.PlayerIsX == ft.PlayerIsX) cnt++; }
			GlobalParameters.AddPlayerToLeaderBoard(ft.PlayerIsX, cnt);
			GlobalParameters.SaveLeaderBoard();
			//---
			
			CurrentState = GameState.GameOver;
			
		}
		//main_game.ChangeTurnLabel(GlobalParameters.isPlayer_x, GlobalParameters.winnerIndic);
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




	// Called every frame. 'delta' is the elapsed time since the previous frame.
	// public override void _Process(double delta)
	// {
	// }
}
