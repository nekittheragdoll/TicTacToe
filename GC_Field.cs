using Godot;
using System;
using TicTacToeResources;
using System.Collections.Generic;

public partial class GC_Field : GridContainer
{
	public static int SizeOfField = 5;
	FieldTile[,] Field = new FieldTile[SizeOfField,SizeOfField];
	
	bool isPlayer_x = Convert.ToBoolean((new Random()).Next(0,2));
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
		if (DidYouDoIt(ft) != null)
		{
			GD.Print(ft.PlayerIsX ? "Player X Won" : "Player O Won");
		}
		;
	}

	public FieldTile[] DidYouDoIt(FieldTile ftvar){
		int streak = 0;

		for (int i = 0; i < SizeOfField; i++)
		{
			if ((Field[ftvar.PosY, i].PlayerIsX == ftvar.PlayerIsX) && Field[ftvar.PosY, i].PieceExists)
			{
				streak++;
			}
			else streak = 0;
			if (streak >= 5) { return new FieldTile[2] { Field[ftvar.PosY, (i - streak + 1)], Field[ftvar.PosY, i] }; }
		}
		streak = 0;
		for (int i = 0; i < SizeOfField; i++)
		{

			if ((Field[i, ftvar.PosX].PlayerIsX == ftvar.PlayerIsX) && Field[i, ftvar.PosX].PieceExists) streak++;
			else streak = 0;
			if (streak >= 5) { return new FieldTile[2] { Field[i - streak + 1, ftvar.PosX], Field[i, ftvar.PosX] }; }
		}


		int[] diagoffset = {0, 0};
		streak = 0;

		if (ftvar.PosY > ftvar.PosX)
		{
			diagoffset[0] = ftvar.PosY - ftvar.PosX;
			diagoffset[1] = 0;
		}
		else
		{
			diagoffset[0] = 0;
			diagoffset[1] = ftvar.PosX - ftvar.PosY;
		}


		for (int i = 0; i + diagoffset[0] + diagoffset[1] < SizeOfField; i++)
		{

			if ((Field[i + diagoffset[0], i + diagoffset[1]].PlayerIsX == ftvar.PlayerIsX) && Field[i + diagoffset[0], i + diagoffset[1]].PieceExists) streak++;
			else streak = 0;
			if (streak >= 5) { return new FieldTile[2] { Field[i + diagoffset[0] - streak + 1, i + diagoffset[1] - streak + 1], Field[i + diagoffset[0], i + diagoffset[1]] }; }
		}

		if (SizeOfField-1 - ftvar.PosY > ftvar.PosX)
		{
			diagoffset[0] = SizeOfField-1 - ftvar.PosY - ftvar.PosX;
			diagoffset[1] = 0;
		}
		else
		{
			diagoffset[0] = 0;
			diagoffset[1] = ftvar.PosX - (SizeOfField-1 - ftvar.PosY);
		}

		int offset = Math.Abs(ftvar.PosX - ftvar.PosY);
		streak = 0;
		string dbgforY = "Y series: ";
		string dbgforX = "X series: ";
		for (int i = 0, j = SizeOfField-1; i + diagoffset[1] < SizeOfField && j - diagoffset[0] >= 0 ; i++, j--)
		{
			if ((Field[j- diagoffset[0], i + diagoffset[1]].PlayerIsX == ftvar.PlayerIsX) && Field[j- diagoffset[0], i + diagoffset[1]].PieceExists) streak++;
			else streak = 0;
			if (streak >= 5) { return new FieldTile[2] { Field[j - diagoffset[0] + streak - 1, i + diagoffset[1] - streak + 1], Field[j- diagoffset[0], i + diagoffset[1]] }; }

			dbgforX += (i).ToString();
			dbgforY += (j- diagoffset[0]).ToString();

		}

		GD.Print(dbgforX);
		GD.Print(dbgforY);


		return null;
	}		

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	// public override void _Process(double delta)
	// {
	// }
}
