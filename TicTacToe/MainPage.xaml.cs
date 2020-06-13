using System;
using Xamarin.Forms;

// Name     : Steve Schrader
// Student# : J294862
// Project  : TicTacToe game
// Started  : Wed 11th March, 2020
// Finished : Wed 25th March, 2020
//
// This application reproduces the game of Tic Tac Toe, with the main purpose of this class being to
// update the View Model as the game progresses.
//
// The first of three methods, PlayerMovePlaced(), uses a series of if-then statements to determine 
// the playerSquareSelected value. This is used to set the human player's move on the game grid. If the
// current game is not new, then the selected move is  also passed to the main Game class method, 
// GameController(), which then calls the Computer class method, ChooseMove(), and returns the computer
// player's selected move to MainPage. The computer's move is then passed to the second method in this
// class, ComputerMovePlaced(), to be drawn on the game grid. Control is then passed to the Game class
// method, CheckForWinner(), to finalize the turn.
//
// The third method of this class, ResetGameGrid(), is triggered by a MessagingCenter object. The first
// job of this method is to display the current AI difficulty level on screen, and to update the player 
// scores (both human and computer).
// 
// An array of integers containing the current board positions that has been passed to this method is
// used in a series of Switch statements to either clear the game grid (if this is a new game), or to 
// position the moves which have been loaded from the SavedGameFile.
// 
// As well as the "Reset Board" MessagingCenter object referred to above, a number of other objects are
// subscribed to in MainPage(). The first two ("Loading ..." and "Sleeping...") are used by App.xaml.cs 
// on start-up and close of the application. The remainder display pop-ups to provide information to the
// user. The last three of these ("Play Again ??", "Difficulty ??" and "Bye") await their own MessagingCenter
// objects to send user responses back to subscribers in the Game class.
// (See Stack Overflow link in Game.cs.)
//
namespace TicTacToe
{
    //
    // TO-DO LIST
    // 'Bye' pop-up does not display on forced close
    //

    public partial class MainPage : ContentPage
    {
        Game myGame = new Game();

        public MainPage()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<object>(this, "Loading ...", (sender) =>
            {
                myGame.LoadSavedGameAsync();
            });

            MessagingCenter.Subscribe<object>(this, "Sleeping ...", (sender) =>
            {
                myGame.WriteSavedGameAsync();
            });

            MessagingCenter.Subscribe<object>(this, "Welcome", (sender) =>
            {
                DisplayAlert("Welcome to Tic Tac Toe", "I can see that this is your first game, so I'll play easy for starters ....", "OK");
            });

            MessagingCenter.Subscribe<object>(this, "Returning", (sender) =>
            {
                DisplayAlert("Hello again", "Let's pick-up from where we were ...", "OK");
            });

            MessagingCenter.Subscribe<object>(this, "Player has won !!", (sender) =>
            {
                playerScore.Text = Player.playerScore.ToString();
                DisplayAlert("You won !!", "You have beaten the computer !!!", "OK");
            });

            MessagingCenter.Subscribe<object>(this, "Computer has won !!", (sender) =>
            {
                computerScore.Text = Computer.playerScore.ToString();
                DisplayAlert("You lost !!", "Sorry. But the computer was just too good for you ... this time.", "OK");
            });

            MessagingCenter.Subscribe<object>(this, "Game is a Draw", (sender) =>
            {
                DisplayAlert("The game is a draw !!", "No-one has won this game.", "OK");
            });

            MessagingCenter.Subscribe<object>(this, "Play again ??", async (sender) =>
            {
                Boolean playAgainResult = await DisplayAlert("Play again ??", "Would you like to play another game ??",
                                "Yes",
                                "No");

                MessagingCenter.Send<object, Boolean>(this, "Play Again Response", playAgainResult);
            });

            MessagingCenter.Subscribe<object>(this, "Difficulty ??", async (sender) =>
            {
                String difficultyResultString = await DisplayActionSheet("Change AI Difficulty ??",
                                "Level 4",                             
                                "Level 3",
                                "Level 1",
                                "Level 2");
                MessagingCenter.Send<object, String>(this, "Difficulty Response", difficultyResultString);
            });

            MessagingCenter.Subscribe<object, int[]>(this, "Reset Board", (sender, gridBoardPositions) =>
            {
                ResetGameGrid(gridBoardPositions);
            });

            MessagingCenter.Subscribe<object>(this, "Bye", async (sender) =>
            {
                await DisplayAlert("Goodbye", "Come back again soon.", "OK");
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            });
        }

        private void PlayerMovePlaced(object sender, EventArgs e)
        {
            int blank = 0;
            int computerMove;
            int playerSquareSelected = -1;
            Button squareSelected = (Button)sender;

            if (squareSelected.Equals(square1))
            {
                playerSquareSelected = 0;
            }

            if (squareSelected.Equals(square2))
            {
                playerSquareSelected = 1;
            }

            if (squareSelected.Equals(square3))
            {
                playerSquareSelected = 2;
            }

            if (squareSelected.Equals(square4))
            {
                playerSquareSelected = 3;
            }

            if (squareSelected.Equals(square5))
            {
                playerSquareSelected = 4;
            }

            if (squareSelected.Equals(square6))
            {
                playerSquareSelected = 5;
            }

            if (squareSelected.Equals(square7))
            {
                playerSquareSelected = 6;
            }

            if (squareSelected.Equals(square8))
            {
                playerSquareSelected = 7;
            }

            if (squareSelected.Equals(square9))
            {
                playerSquareSelected = 8;
            }

            if (myGame.boardPositionArray[playerSquareSelected] == blank)
            {
                squareSelected.Text = "O";
                squareSelected.TextColor = Color.Red;
                computerMove = myGame.GameController(playerSquareSelected);

                if (myGame.newGameFlag == false) { 
                ComputerMovePlaced(computerMove);
            }
                myGame.CheckForWinner(Computer.playerFlag);
            }
        }

        public void ComputerMovePlaced(int computerMove)
        {
            
            switch (computerMove + 1)
            {
                case 1:
                    square1.Text = "X";
                    square1.TextColor = Color.Blue;
                    break;

                case 2:
                    square2.Text = "X";
                    square2.TextColor = Color.Blue;
                    break;

                case 3:
                    square3.Text = "X";
                    square3.TextColor = Color.Blue;
                    break;

                case 4:
                    square4.Text = "X";
                    square4.TextColor = Color.Blue;
                    break;

                case 5:
                    square5.Text = "X";
                    square5.TextColor = Color.Blue;
                    break;

                case 6:
                    square6.Text = "X";
                    square6.TextColor = Color.Blue;
                    break;

                case 7:
                    square7.Text = "X";
                    square7.TextColor = Color.Blue;
                    break;

                case 8:
                    square8.Text = "X";
                    square8.TextColor = Color.Blue;
                    break;

                case 9:
                    square9.Text = "X";
                    square9.TextColor = Color.Blue;
                    break;
            }
        }

        public void ResetGameGrid(int[] gridBoardPositions)
        {
            playerScore.Text = Player.playerScore.ToString();
            computerScore.Text = Computer.playerScore.ToString();

            switch (myGame.myComputer.GetDifficultyLevel())
            {
                case 1:
                    difficultyLevel.Text = "Easy";
                    break;

                case 2:
                    difficultyLevel.Text = "Challenging";
                    break;

                case 3:
                    difficultyLevel.Text = "Very Hard";
                    break;

                case 4:
                    difficultyLevel.Text = "Impossible";
                    break;
            }
            
            for (int counter = 0; counter < Game.numberOfSquares; ++counter)
            {
                myGame.boardPositionArray[counter] = gridBoardPositions[counter];

                
                if (gridBoardPositions[counter] == 0)
                {
                    switch (counter + 1)
                    {
                        case 1:
                            square1.Text = "";
                            break;

                        case 2:
                            square2.Text = "";
                            break;

                        case 3:
                            square3.Text = "";
                            break;

                        case 4:
                            square4.Text = "";
                            break;

                        case 5:
                            square5.Text = "";
                            break;

                        case 6:
                            square6.Text = "";
                            break;

                        case 7:
                            square7.Text = "";
                            break;

                        case 8:
                            square8.Text = "";
                            break;

                        case 9:
                            square9.Text = "";
                            break;
                    }
                }

                if (gridBoardPositions[counter] == 1)
                {
                    switch (counter + 1)
                    {
                        case 1:
                            square1.Text = "O";
                            square1.TextColor = Color.Red;
                            break;

                        case 2:
                            square2.Text = "O";
                            square2.TextColor = Color.Red;
                            break;

                        case 3:
                            square3.Text = "O";
                            square3.TextColor = Color.Red;
                            break;

                        case 4:
                            square4.Text = "O";
                            square4.TextColor = Color.Red;
                            break;

                        case 5:
                            square5.Text = "O";
                            square5.TextColor = Color.Red;
                            break;

                        case 6:
                            square6.Text = "O";
                            square6.TextColor = Color.Red;
                            break;

                        case 7:
                            square7.Text = "O";
                            square7.TextColor = Color.Red;
                            break;

                        case 8:
                            square8.Text = "O";
                            square8.TextColor = Color.Red;
                            break;

                        case 9:
                            square9.Text = "O";
                            square9.TextColor = Color.Red;
                            break;
                    }
                }

                if (gridBoardPositions[counter] == 2)
                {
                    
                    switch (counter + 1)
                    {
                        case 1:
                            square1.Text = "X";
                            square1.TextColor = Color.Blue;
                            break;

                        case 2:
                            square2.Text = "X";
                            square2.TextColor = Color.Blue;
                            break;

                        case 3:
                            square3.Text = "X";
                            square3.TextColor = Color.Blue;
                            break;

                        case 4:
                            square4.Text = "X";
                            square4.TextColor = Color.Blue;
                            break;

                        case 5:
                            square5.Text = "X";
                            square5.TextColor = Color.Blue;
                            break;

                        case 6:
                            square6.Text = "X";
                            square6.TextColor = Color.Blue;
                            break;

                        case 7:
                            square7.Text = "X";
                            square8.TextColor = Color.Blue;
                            break;

                        case 8:
                            square8.Text = "X";
                            square8.TextColor = Color.Blue;
                            break;

                        case 9:
                            square9.Text = "X";
                            square9.TextColor = Color.Blue;
                            break;
                    }
                }
            }
        }
    }
}
