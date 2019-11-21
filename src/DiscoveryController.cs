
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using SwinGameSDK;

/// <summary>
/// The battle phase is handled by the DiscoveryController.
/// </summary>
static class DiscoveryController
{
	
	/// <summary>
	/// Handles input during the discovery phase of the game.
	/// </summary>
	/// <remarks>
	/// Escape opens the game menu. Clicking the mouse will
	/// attack a location.
	/// </remarks>
	public static void HandleDiscoveryInput ()
	{
		if (SwinGame.KeyTyped (KeyCode.vk_ESCAPE)) {
			GameController.AddNewState (GameState.ViewingGameMenu);
		}

		if (SwinGame.MouseClicked (MouseButton.LeftButton)) {
			DoAttack ();
		}
	}

	/// <summary>
	/// Attack the location that the mouse if over.
	/// </summary>
	private static void DoAttack ()
	{
		Point2D mouse = default (Point2D);

		mouse = SwinGame.MousePosition ();

		//Calculate the row/col clicked
		int row = 0;
		int col = 0;
		row = Convert.ToInt32 (Math.Floor ((mouse.Y - UtilityFunctions.FIELD_TOP) / (UtilityFunctions.CELL_HEIGHT + UtilityFunctions.CELL_GAP)));
		col = Convert.ToInt32 (Math.Floor ((mouse.X - UtilityFunctions.FIELD_LEFT) / (UtilityFunctions.CELL_WIDTH + UtilityFunctions.CELL_GAP)));

		//When the pause button is clicked
		if (row == -1 & col == 9) 
		{
			SwinGame.PauseTimer (GameController.Timer);
			GameController.AddNewState (GameState.ViewingGameMenu);
		}

		if (row >= 0 & row < GameController.HumanPlayer.EnemyGrid.Height) 
		{
			if (col >= 0 & col < GameController.HumanPlayer.EnemyGrid.Width) 
			{
				GameController.Attack (row, col);
			}
		}


	}

	/// <summary>
	/// Draws the game during the attack phase.
	/// </summary>s
	public static void DrawDiscovery ()
	{
		const int SCORES_LEFT = 172;
		const int SHOTS_TOP = 157;
		const int HITS_TOP = 206;
		const int SPLASH_TOP = 256;
		const int SCORE_TOP = 306;
		const int TIMER_LEFT = 440;
		const int TIMER_TOP = 85;

		//Show enemy ships 
		if ((SwinGame.KeyDown (KeyCode.vk_LCTRL) && SwinGame.KeyDown (KeyCode.vk_v))) 
		{
			UtilityFunctions.DrawField (GameController.HumanPlayer.EnemyGrid, GameController.ComputerPlayer, true);
		}

		//Terminate the game directly
		if ((SwinGame.KeyDown (KeyCode.vk_LCTRL) && SwinGame.KeyDown (KeyCode.vk_t))) 
		{
			GameController.SwitchState (GameState.EndingGame);
		}


		else 
		{
			UtilityFunctions.DrawField (GameController.HumanPlayer.EnemyGrid, GameController.ComputerPlayer, false);
		}

		UtilityFunctions.DrawSmallField (GameController.HumanPlayer.PlayerGrid, GameController.HumanPlayer);
		UtilityFunctions.DrawMessage ();

				        
		SwinGame.DrawBitmap (GameResources.GameImage("PauseButton"), 722, 72);

		SwinGame.DrawText (GameController.HumanPlayer.Shots.ToString (), Color.White, GameResources.GameFont ("Menu"), SCORES_LEFT, SHOTS_TOP);
		SwinGame.DrawText (GameController.HumanPlayer.Hits.ToString (), Color.White, GameResources.GameFont ("Menu"), SCORES_LEFT, HITS_TOP);
		SwinGame.DrawText (GameController.HumanPlayer.Missed.ToString (), Color.White, GameResources.GameFont ("Menu"), SCORES_LEFT, SPLASH_TOP);
		SwinGame.DrawText (GameController.HumanPlayer.Score.ToString (), Color.White, GameResources.GameFont ("Menu"), SCORES_LEFT, SCORE_TOP);
		SwinGame.DrawText ("Time left: ", Color.White, GameResources.GameFont ("Menu"), TIMER_LEFT, TIMER_TOP);
		SwinGame.DrawText (GameController.timeCount (480000), Color.White, GameResources.GameFont ("MenuMedium"), TIMER_LEFT + 135, TIMER_TOP - 4);

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
