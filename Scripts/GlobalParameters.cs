using Godot;
using System;
using System.Collections.Generic;

public partial class GlobalParameters : Node
{
	public static int SizeOfField = 5;	//5 is default and minium value
	public static LB_Player[] LeaderBoardData = new	LB_Player[10]; 

	public static string PXname = "<X_default_name>";
	public static string POname = "<O_default_name>";


	//public static bool isPlayer_x = true;
	//public static int winnerIndic = 0; // 0 --> no winner, 1 --> X won, 2 --> O won

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{		
		DisplayServer.WindowSetSize(new Vector2I(854, 480));
		//isPlayer_x = Convert.ToBoolean((new Random()).Next(0,2));
	}

	public static void SaveNames(){
		using var saveGame = FileAccess.Open("res://LastNames.dat", FileAccess.ModeFlags.Write);
		saveGame.StoreLine(PXname);
		saveGame.StoreLine(POname);
	}

	public static void LoadNames()
	{
		if (!FileAccess.FileExists("res://LastNames.dat"))
		{
			return; // Error! We don't have a save to load.
		}

		using var file = FileAccess.Open("res://LastNames.dat", FileAccess.ModeFlags.Read);
		PXname = file.GetLine();
		POname = file.GetLine();
	}

}

	public partial class LB_Player: Node{
		public int TurnCount {get; set;}
		LB_Player(string name, int t_count){
			Name = name;
			TurnCount = t_count;
		}
	}
