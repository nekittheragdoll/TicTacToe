using Godot;
using System;

public partial class lbl_turnIndic : Label
{
	private void _on_gc_field_board_status_changed(long state)
	{
		string p1 = GlobalParameters.PXname;
		string p2 = GlobalParameters.POname;
		switch (state)
		{
			case 1:
			this.Text = String.Format("Player {0}(X) is playing!", p1);
			break;

			case 2:
			this.Text = String.Format("Player {0}(O) is playing!", p2);
			break;

			case 3:
			this.Text = String.Format("Player {0}(X) has won!", p1);
			break;

			case 4:
			this.Text = String.Format("Player {0}(O) has won!", p2);
			break;

			default:
			this.Text = String.Format("This was not suppose to happen!");
			break;
		}
	}

}


