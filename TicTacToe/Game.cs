using System;
using Xamarin.Forms;
using System.IO;
using PCLStorage;

// The Game class holds the variables and methods used by the application. As well as 
// the folder and filename string variables required by the two file-handling methods,
// LoadSavedGameAsync() and WriteSavedGameAsync(), an integer constant, named 'draw', 
// is declared and initialized to zero. This is purely to make the code more readable 
// (and assist with de-bugging).  
// 
// A Boolean (newGameFlag) is also declared and initialized to 'false'. This is used by 
// LoadSavedGameAsync() and passed to the ResetBoardArray() to determine whether this
// is a new game, or a saved game that has been re-loaded. 
//
// A constant integer, numberOfSquares, is used to define the size of an array of integers
// (boardPositionArray[]) that holds values to indicate which squares are held by each 
// player (playerFlag), and which squares currently remain unselected (blank).
//
// As well as the two methods that conduct file saving and loading operations, there are 
// four other methods that are held by this class. GameController() is where the majority
// of the game is conducted. This method is called from MainPage() when the player has 
// made a turn selection. That selection is passed to GameController() which updates the 
// board position array with the player's selection. 
// 
// Next, the player's flag is passed to the second method, CheckForWinner(), which checks
// each of the eight lines (three across, three down, and the two diagonals) which would 
// win the game for either player (if they hold all three of the squares in any line.)
//
// The value returned by CheckForWinner() will be one of four possible values. Either a 1 or
// 2 to indicate that one of the players has won the match, a value of 0 to show that the 
// game is a draw, or a value of -1 if blank spaces remain and there is no winner (the game
// is not over yet).
//
// If the CheckForWinner() value is -1, then GameController() sends the board position array
// to the Computer class method, ChooseMove() to decide where to place its' piece ('X'). The
// returned value from this call, computerMove, is added to the board position array, and the
// computer player's flag is then passed to CheckForWinner() to determine if the computer has
// now won the match.
//
// Again, a returned value of -1 progresses GameController(), which then calls the method,
// WriteSavedGameAsync() to update the game-state variables, before it returns computerMove to
// the MainPage method which originally called GameController().

// If the value returned by CheckForWinner() is not equal to -1, then GameController() calls 
// the third method of this class, WeHaveAWinner(), passing the winning playerFlag value from
// the CheckForWinner() call to it. This method updates the winning player score, calls the
// WriteSavedGameAsync() method again, and then sends the relevant MessagingCenter object 
// (subscribed to on MainPage()) to display one of three pop-up alerts to inform the user of 
// the match outcome.
//
// https://stackoverflow.com/questions/27864635/how-do-you-return-a-value-from-a-messagingcenter-call
// Using a suggestion from the Stack Overflow website, the next two pop-up alerts each have an 
// embedded "subscribe" message to collect user responses asking, firstly, if the player wishes
// to play another game and then (assuming the Boolean reply to that question is true) if the
// player wishes to change the computer difficulty setting. 
// (If, however, the playAgain reply is negative, a "Bye" message is sent which displays a closing
// display alert, and the application is kill()ed.)
//
// WeHaveAWinner() sets the new difficulty level by passing an integer to the SetDifficultyLevel() 
// method (Computer class), saves the game state variables again to record the new difficulty 
// level, sets the newGameFlag to 'true' and then passes that flag to the last method in
// this class, ResetBoardArray(). 
// 
// ResetBoardArray() can be called from either LoadSavedGameAsync(), or, as above, from WeHaveAWinner().
// If the Boolean flag passed to this method is 'true', a blank boardPositionArray[] is created. A
// MessagingCenter object then "sends" the array (either as it was if the flag is 'false', or newly 
// zeroed) to the message subscriber on MainPage, which then passes it in a call to ResetBoardArray().
//
// FOOTNOTE: If a saved game file is not found during LoadSavedGameAsync(), then a DisplayAlert 
// greeting pop-up is presented, and the variables that would otherwise be loaded from the saved game  
// file are all initialized to zero.
//

namespace TicTacToe
{
    class Game
    {
		String fileName = "SavedGameFile.bin";
		String folderName = "TicTacToeSavedGame";
		IFolder folder = FileSystem.Current.LocalStorage;
		const int draw = 0;
		public Boolean newGameFlag = false;
		public const int numberOfSquares = 9;
		public int[] boardPositionArray = new int[numberOfSquares];

		public Computer myComputer = new Computer();

		public async void LoadSavedGameAsync()
		{
			int bufferPosition = 0;
			try
			{
				folder = await folder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
				IFile file = await folder.GetFileAsync(fileName);

				using (Stream gameStream = await file.OpenAsync(PCLStorage.FileAccess.Read))
				{
					long savedGameFileLength = gameStream.Length;
					byte[] gameDataBuffer = new byte[savedGameFileLength];
					gameStream.Read(gameDataBuffer, 0, (int)savedGameFileLength);

					for (int counter = 0; counter < numberOfSquares; ++counter)
					{
						boardPositionArray[counter] = BitConverter.ToInt32(gameDataBuffer, bufferPosition);
						bufferPosition += 4;
					}

					Player.playerScore = BitConverter.ToInt32(gameDataBuffer, bufferPosition);
					bufferPosition += 4;

					Computer.playerScore = BitConverter.ToInt32(gameDataBuffer, bufferPosition);
					bufferPosition += 4;

					myComputer.SetDifficultyLevel(BitConverter.ToInt32(gameDataBuffer, bufferPosition));
					newGameFlag = false;
					ResetBoardArray(newGameFlag);
				}
			}
		
			catch (Exception)
			{
				// Display pop-up message to welcome human player to the game.
				MessagingCenter.Send<object>(this, "Welcome");

				for (int counter = 0; counter < numberOfSquares; ++counter)
				{
					boardPositionArray[counter] = 0;
				}

				Player.playerScore = 0;
				Computer.playerScore = 0;
				myComputer.SetDifficultyLevel(1);
				newGameFlag = true;
				ResetBoardArray(newGameFlag);
			}
		}

		public async void WriteSavedGameAsync()
		{
			try
			{
				IFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
				using (Stream gameStream = await file.OpenAsync(PCLStorage.FileAccess.ReadAndWrite))
				{
					for (int counter = 0; counter < numberOfSquares; ++counter)
					{
						byte[] squareSelectedData = BitConverter.GetBytes(boardPositionArray[counter]);
						gameStream.Write(squareSelectedData, 0, squareSelectedData.Length);
					}

					byte[] playerScoreData = BitConverter.GetBytes(Player.playerScore);
					gameStream.Write(playerScoreData, 0, playerScoreData.Length);

					byte[] computerScoreData = BitConverter.GetBytes(Computer.playerScore);
					gameStream.Write(computerScoreData, 0, computerScoreData.Length);

					byte[] levelData = BitConverter.GetBytes(myComputer.GetDifficultyLevel());
					gameStream.Write(levelData, 0, levelData.Length);
					gameStream.Dispose();
				}
			}

			catch (Exception error)
			{
				System.Diagnostics.Debug.WriteLine("********** COULD NOT SAVE FILE **********");
				System.Diagnostics.Debug.WriteLine(error);
			}

			MessagingCenter.Send<object>(this, "Saving ...");
		}

		public int GameController(int playerMove)
		{
			int winner;
			int computerMove = -1;

			boardPositionArray[playerMove] = Player.playerFlag;

			winner = CheckForWinner(Player.playerFlag);

			if (winner != -1)	
			{
				WeHaveAWinner(winner);
			}
			else
			{
				computerMove = myComputer.ChooseMove(boardPositionArray);
				boardPositionArray[computerMove] = Computer.playerFlag;

				winner = CheckForWinner(Computer.playerFlag);
				if (winner != -1) 	
				{
					WeHaveAWinner(winner);
				}

				WriteSavedGameAsync();
			}

			return computerMove;
		}

		public int CheckForWinner(int player)
		{
			int winner = -1;

			if (((boardPositionArray[0] == player) && (boardPositionArray[1] == player) && (boardPositionArray[2] == player)) ||
				((boardPositionArray[3] == player) && (boardPositionArray[4] == player) && (boardPositionArray[5] == player)) ||
				((boardPositionArray[6] == player) && (boardPositionArray[7] == player) && (boardPositionArray[8] == player)) ||
				((boardPositionArray[0] == player) && (boardPositionArray[3] == player) && (boardPositionArray[6] == player)) ||
				((boardPositionArray[1] == player) && (boardPositionArray[4] == player) && (boardPositionArray[7] == player)) ||
				((boardPositionArray[2] == player) && (boardPositionArray[5] == player) && (boardPositionArray[8] == player)) ||
				((boardPositionArray[0] == player) && (boardPositionArray[4] == player) && (boardPositionArray[8] == player)) ||
				((boardPositionArray[2] == player) && (boardPositionArray[4] == player) && (boardPositionArray[6] == player)))
			{
				winner = player;
			}
			else
			{
				winner = 0;

				for (int counter = 0; counter < numberOfSquares; ++counter)
				{
					if (boardPositionArray[counter] == 0)
					{
						winner = -1;
					}
				}
			}

			newGameFlag = (winner != -1);
			return winner;
		}

		public void WeHaveAWinner(int winner)
		{
			if (winner == Player.playerFlag)
			{
				++Player.playerScore;
				MessagingCenter.Send<object>(this, "Player has won !!");
			}

			else if (winner == Computer.playerFlag)
			{
				++Computer.playerScore;
				MessagingCenter.Send<object>(this, "Computer has won !!");
			}

			else
			{
				MessagingCenter.Send<object>(this, "Game is a Draw");
			}

			ResetBoardArray(true);
			WriteSavedGameAsync();
			MessagingCenter.Send<object>(this, "Play again ??");
			MessagingCenter.Subscribe<object, Boolean>(this, "Play Again Response", (sender, playAgainReply) =>
			{
				if (playAgainReply == false)
				{
					WriteSavedGameAsync();
					MessagingCenter.Send<object>(this, "Bye");
				}
				else
				{
					MessagingCenter.Send<object>(this, "Difficulty ??");
					MessagingCenter.Subscribe<object, String>(this, "Difficulty Response", (sender2, nextGameLevelReply) =>
						{
							if (nextGameLevelReply == "Level 1")
							{
								myComputer.SetDifficultyLevel(1);
							}

							if (nextGameLevelReply == "Level 2")
							{
								myComputer.SetDifficultyLevel(2);
							}

							if (nextGameLevelReply == "Level 3")
							{
								myComputer.SetDifficultyLevel(3);
							}

							if (nextGameLevelReply == "Level 4")
							{
								myComputer.SetDifficultyLevel(4);
							}

							WriteSavedGameAsync();
							newGameFlag = true;
							ResetBoardArray(newGameFlag);
						});
				}
			});
		}

		public void ResetBoardArray(Boolean newGame)
		{
			if (newGame)
			{
				for (int counter = 0; counter < numberOfSquares; ++counter)
				{
					boardPositionArray[counter] = 0;			
				}
			}
			else
			{
				MessagingCenter.Send<object>(this, "Returning");
			}

			MessagingCenter.Send<object, int[]>(this, "Reset Board", boardPositionArray);
		}
	}
}
