using Godot;
using System;
using TicTacToeResources;
using System.Collections.Generic;

public partial class GC_Field : GridContainer
{
	FieldTile[] Field = new FieldTile[25];
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		for (int i = 0; i < Field.Length; i++)
		{
			FieldTile ft = new FieldTile(){
				TextureNormal = ResourceLoader.Load("res://o_sprite.png") as Texture2D,
			};
			AddChild(ft);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
