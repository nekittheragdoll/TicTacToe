using Godot;
using System;

public partial class LB_List : ItemList
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach (var item in GlobalParameters.LeaderBoardData)
		{
			if (item != null)
			{
				AddItem(item.Index.ToString());
				AddItem(item.Name);
				AddItem(item.TurnCount.ToString());
			}
		}
	}
}
