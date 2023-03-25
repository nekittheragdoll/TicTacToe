using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

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
		LoadLeaderBoard();
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

	public static void SaveLeaderBoard(){
		using var file = FileAccess.Open("res://LeaderBoard.dat", FileAccess.ModeFlags.Write);
		foreach (var item in LeaderBoardData)
		{
			if(item != null)file.StoreLine(String.Format("{0};{1}", item.Name, item.TurnCount));
		}
	}

	public static void LoadLeaderBoard(){
		if (!FileAccess.FileExists("res://LeaderBoard.dat")) return;
		using var file = FileAccess.Open("res://LeaderBoard.dat", FileAccess.ModeFlags.Read);
		for (int i = 0; i < LeaderBoardData.Length; i++)
		{
			if (file.GetPosition() >= file.GetLength()) break;
			string[] line = file.GetLine().Split(";");
			Int32.TryParse(line[1], System.Globalization.NumberStyles.Integer, new System.Globalization.CultureInfo("es-ES", false), out int tc);
			var name = line[0];
			LeaderBoardData[i] = new LB_Player(name, tc);
		}
	}

	public static void AddPlayerToLeaderBoard(bool pisx, int tc = 7)
	{
		var _arrayLast = LeaderBoardData.Length - 1;

		for (int i = 0; i < LeaderBoardData.Length; i++)
		{
			if (LeaderBoardData[i] == null || (i >= _arrayLast && LeaderBoardData[_arrayLast].TurnCount > tc))
			{
				LeaderBoardData[i] = new LB_Player(pisx ? PXname : POname, tc);
				break;
			}
		}
		LeaderBoardData = LeaderBoardData.Where(x => x != null).OrderBy(x => x.TurnCount).ToArray();
	}
}

public class LB_Player
{	
	public string Name { get; set;}
	public int TurnCount { get; set; }
	public LB_Player(string name, int t_count)
	{
		Name = name;
		TurnCount = t_count;
	}
}
