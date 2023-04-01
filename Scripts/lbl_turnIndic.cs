using Godot;
using System;

public partial class lbl_turnIndic : Label
{
	enum GameState { DRAW, P1PLAYING, P2PLAYING, P1WON, P2WON }
	private void _on_gc_field_board_status_changed(long state)
	{
		var currentState = (GameState)state;
		string p1 = GlobalParameters.PXname;
		string p2 = GlobalParameters.POname;
		switch (currentState)
		{
			case GameState.DRAW:
				this.Text = String.Format("Draw!", p1);
				break;
			case GameState.P1PLAYING:
				this.Text = String.Format("Player {0}(X) is playing!", p1);
				break;

			case GameState.P2PLAYING:
				this.Text = String.Format("Player {0}(O) is playing!", p2);
				break;

			case GameState.P1WON:
				this.Text = String.Format("Player {0}(X) has won!", p1);
				break;

			case GameState.P2WON:
				this.Text = String.Format("Player {0}(O) has won!", p2);
				break;

			default:
				this.Text = String.Format("<Undefined State>");
				break;
		}
	}

}


