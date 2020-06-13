using System;

// This class is the computer player for the Tic Tac Toe application. 
// 
// Computer() has the properties, playerScore and playerFlag, inherited from its' parent class
// (Player.cs). Computer() also has the property, difficultyLevelVariable, as well as its' own
// methods, GetDifficultyLevel(), SetDifficultyLevel() and ChooseMove(). The first two are basic
// get and set methods for the difficultyLevelVariable.
//
// When it is the computer player's turn, the Game class method, GameController(), sends an array
// of current board positions to the ChooseMove() method. This method then uses the current value
// of the difficultyLevelVariable to determine which AI routine the computer player will use to 
// decide where to place its' piece (X). Comments have been included in each AI routine to show 
// how the decision-making process is done.
//
// The computer's decision is then checked against the array to ensure that the selected square is
// unoccupied. If so, ChooseMove() is called again. This can, on occasion, create a recursive call, 
// however, only a small number of iterations would ever be necessary to select a vacant square.
// When ChooseMove has a valid selection, that value is returned to GameController().
//
// I have attempted to make the AI code more readable by using the integer variables, 'human', 
// 'computer' and 'blank' to represent the values held in the positionArray[].
//
namespace TicTacToe
{
    class Computer : Player
    {
        new public static int playerScore;
        new public static int playerFlag = 2;
        public int difficultyLevelVariable;

        public int GetDifficultyLevel()
        {
            return difficultyLevelVariable;
        }
        
        public void SetDifficultyLevel(int difficultyLevel)
        {
            difficultyLevelVariable = difficultyLevel;
        }

        public int ChooseMove(int[] positionArray)
        {
            int blank = 0;
            int computer = playerFlag;
            int human = Player.playerFlag;
            int moveRating = 0;
            int computerMove = -1;
            int computerMoveStrength = 0;

            switch (difficultyLevelVariable)
            {   
                case 1:
                    // The first AI routine selects a random number for the computer move. By making the 
                    // random number 0 to 11 (and not 0 to 8, as would be expected), there is a slightly 
                    // weighted chance that the computer will select the centre square). The computer choice 
                    // is then checked against the positionArray[] to see if it has already been 
                    // selected. If so, then ChooseMove() is called again to select another number.

                    Random aRandom = new Random();
                    computerMove = aRandom.Next(0, 11);
                    if (computerMove > 8)
                    {
                        computerMove = 4;
                    }

                    break;

                case 2:
                    // The second level of AI plays a bit smarter than just selecting a random square. Firstly, the AI will 
                    // check if the human player has two squares on any line and will select the third square for the block. 
                    if ((positionArray[0] == blank) && (((positionArray[1] == human) && (positionArray[2] == human))
                        || ((positionArray[3] == human) && (positionArray[6] == human))
                        || ((positionArray[4] == human) && (positionArray[8] == human))))
                    {
                        moveRating = 8;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 0;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[1] == blank) && (((positionArray[0] == human) && (positionArray[2] == human))
                        || ((positionArray[4] == human) && (positionArray[7] == human))))
                    {
                        moveRating = 8;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 1;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[2] == blank) && (((positionArray[0] == human) && (positionArray[1] == human))
                        || ((positionArray[4] == human) && (positionArray[6] == human))
                        || ((positionArray[5] == human) && (positionArray[8] == human))))
                    {
                        moveRating = 8;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 2;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[3] == blank) && (((positionArray[0] == human) && (positionArray[6] == human))
                        || ((positionArray[4] == human) && (positionArray[5] == human))))
                    {
                        moveRating = 8;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 3;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[4] == blank) && (((positionArray[0] == human) && (positionArray[8] == human))
                        || ((positionArray[1] == human) && (positionArray[7] == human))
                        || ((positionArray[2] == human) && (positionArray[6] == human))
                        || ((positionArray[3] == human) && (positionArray[5] == human))))
                    {
                        moveRating = 8;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 4;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[5] == blank) && (((positionArray[2] == human) && (positionArray[8] == human))
                        || ((positionArray[3] == human) && (positionArray[4] == human))))
                    {
                        moveRating = 8;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 5;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[6] == blank) && (((positionArray[0] == human) && (positionArray[3] == human))
                        || ((positionArray[2] == human) && (positionArray[4] == human))
                        || ((positionArray[7] == human) && (positionArray[8] == human))))
                    {
                        moveRating = 8;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 6;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[7] == blank) && (((positionArray[1] == human) && (positionArray[4] == human))
                        || ((positionArray[6] == human) && (positionArray[8] == human))))
                    {
                        moveRating = 8;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 7;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[8] == blank) && (((positionArray[0] == human) && (positionArray[4] == human))
                        || ((positionArray[2] == human) && (positionArray[5] == human))
                        || ((positionArray[6] == human) && (positionArray[7] == human))))
                    {
                        moveRating = 8;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 8;
                            computerMoveStrength = moveRating;
                        }
                    }

                    // Next, if the centre square is vacant, the AI will select that square
                    if (positionArray[4] == blank)
                    {
                        moveRating = 6;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 4;
                            computerMoveStrength = moveRating;
                        }
                    }

                    // If there is a vacant square next to another X, it will select that square.
                    if ((positionArray[0] == blank) && ((positionArray[1] == computer) || (positionArray[3] == computer)
                        || (positionArray[4] == computer)))
                    {
                        moveRating = 4;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 0;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[1] == blank) && ((positionArray[0] == computer) || (positionArray[2] == computer)
                        || (positionArray[3] == computer) || (positionArray[4] == computer)
                        || (positionArray[5] == computer)))
                    {
                        moveRating = 4;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 1;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[2] == blank) && ((positionArray[1] == computer) || (positionArray[4] == computer)
                        || (positionArray[5] == computer)))
                    {
                        moveRating = 4;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 2;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[3] == blank) && ((positionArray[0] == computer) || (positionArray[1] == computer)
                        || (positionArray[4] == computer) || (positionArray[6] == computer)
                        || (positionArray[7] == computer)))
                    {
                        moveRating = 4;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 3;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[5] == blank) && ((positionArray[1] == computer) || (positionArray[2] == computer)
                        || (positionArray[4] == computer) || (positionArray[7] == computer)
                        || (positionArray[8] == computer)))
                    {
                        moveRating = 4;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 5;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[6] == blank) && ((positionArray[3] == computer) || (positionArray[4] == computer)
                        || (positionArray[7] == computer)))
                    {
                        moveRating = 4;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 6;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[7] == blank) && ((positionArray[3] == computer) || (positionArray[4] == computer)
                        || (positionArray[5] == computer) || (positionArray[6] == computer)
                        || (positionArray[8] == computer)))
                    {
                        moveRating = 4;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 7;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[8] == blank) && ((positionArray[4] == computer) || (positionArray[5] == computer)
                        || (positionArray[7] == computer)))
                    {
                        moveRating = 4;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 8;
                            computerMoveStrength = moveRating;
                        }
                    }

                    // If no other options remain, the AI will select a random number for it's move
                    if (computerMove == -1)
                    {
                        moveRating = 2;

                        if (computerMoveStrength < moveRating)
                        {
                            Random anotherRandom = new Random();
                            computerMove = anotherRandom.Next(0, 9);
                        }
                    }

                    break;

                case 3:
                    // The third level of AI plays very smart. Firstly, if the computer occupies two squares in any row and the third
                    // square is vacant, then the AI will select that square for the win.
                    if ((positionArray[0] == blank) && (((positionArray[1] == computer) && (positionArray[2] == computer))
                        || ((positionArray[3] == computer) && (positionArray[6] == computer))
                        || ((positionArray[4] == computer) && (positionArray[8] == computer))))
                    {
                        moveRating = 10;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 0;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[1] == blank) && (((positionArray[0] == computer) && (positionArray[2] == computer))
                        || ((positionArray[4] == computer) && (positionArray[7] == computer))))
                    {
                        moveRating = 10;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 1;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[2] == blank) && (((positionArray[0] == computer) && (positionArray[1] == computer))
                        || ((positionArray[4] == computer) && (positionArray[6] == computer))
                        || ((positionArray[5] == computer) && (positionArray[8] == computer))))
                    {
                        moveRating = 10;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 2;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[3] == blank) && (((positionArray[0] == computer) && (positionArray[6] == computer))
                        || ((positionArray[4] == computer) && (positionArray[5] == computer))))
                    {
                        moveRating = 10;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 3;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if (( positionArray[4] == blank) && (((positionArray[0] == computer) && (positionArray[8] == computer))
                        || ((positionArray[1] == computer) && (positionArray[7] == computer))
                        || ((positionArray[2] == computer) && (positionArray[6] == computer))
                        || ((positionArray[3] == computer) && (positionArray[5] == computer))))
                    {
                        moveRating = 10;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 4;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[5] == blank) && (((positionArray[2] == computer) && (positionArray[8] == computer))
                        || ((positionArray[3] == computer) && (positionArray[4] == computer))))
                    {
                        moveRating = 10;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 5;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[6] == blank) && (((positionArray[0] == computer) && (positionArray[3] == computer))
                        || ((positionArray[2] == computer) && (positionArray[4] == computer))
                        || ((positionArray[7] == computer) && (positionArray[8] == computer))))
                    {
                        moveRating = 10;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 6;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[7] == blank) && (((positionArray[1] == computer) && (positionArray[4] == computer))
                        || ((positionArray[6] == computer) && (positionArray[8] == computer))))
                    {
                        moveRating = 10;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 7;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[8] == blank) && (((positionArray[0] == computer) && (positionArray[4] == computer))
                        || ((positionArray[2] == computer) && (positionArray[5] == computer))
                        || ((positionArray[6] == computer) && (positionArray[7] == computer))))
                    {
                        moveRating = 10;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 8;
                            computerMoveStrength = moveRating;
                        }
                    }

                    // Next, the AI will check if the human player has two squares on any line and will select the third square for the block. 
                    if ((positionArray[0] == blank) && (((positionArray[1] == human) && (positionArray[2] == human))
                        || ((positionArray[3] == human) && (positionArray[6] == human))
                        || ((positionArray[4] == human) && (positionArray[8] == human))))
                    {
                        moveRating = 8;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 0;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[1] == blank) && (((positionArray[0] == human) && (positionArray[2] == human))
                        || ((positionArray[4] == human) && (positionArray[7] == human))))
                    {
                        moveRating = 8;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 1;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[2] == blank) && (((positionArray[0] == human) && (positionArray[1] == human))
                        || ((positionArray[4] == human) && (positionArray[6] == human))
                        || ((positionArray[5] == human) && (positionArray[8] == human))))
                    {
                        moveRating = 8;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 2;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[3] == blank) && (((positionArray[0] == human) && (positionArray[6] == human))
                        || ((positionArray[4] == human) && (positionArray[5] == human))))
                    {
                        moveRating = 8;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 3;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[4] == blank) && (((positionArray[0] == human) && (positionArray[8] == human))
                        || ((positionArray[1] == human) && (positionArray[7] == human))
                        || ((positionArray[2] == human) && (positionArray[6] == human))
                        || ((positionArray[3] == human) && (positionArray[5] == human))))
                    {
                        moveRating = 8;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 4;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[5] == blank) && (((positionArray[2] == human) && (positionArray[8] == human))
                        || ((positionArray[3] == human) && (positionArray[4] == human))))
                    {
                        moveRating = 8;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 5;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[6] == blank) && (((positionArray[0] == human) && (positionArray[3] == human))
                        || ((positionArray[2] == human) && (positionArray[4] == human))
                        || ((positionArray[7] == human) && (positionArray[8] == human))))
                    {
                        moveRating = 8;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 6;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[7] == blank) && (((positionArray[1] == human) && (positionArray[4] == human))
                        || ((positionArray[6] == human) && (positionArray[8] == human))))
                    {
                        moveRating = 8;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 7;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[8] == blank) && (((positionArray[0] == human) && (positionArray[4] == human))
                        || ((positionArray[2] == human) && (positionArray[5] == human))
                        || ((positionArray[6] == human) && (positionArray[7] == human))))
                    {
                        moveRating = 8;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 8;
                            computerMoveStrength = moveRating;
                        }
                    }

                    // Next, if the centre square is vacant, the AI will select that square
                    if (positionArray[4] == blank)
                    {
                        moveRating = 6;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 4;
                            computerMoveStrength = moveRating;
                        }
                    }

                    // If a corner square is available, the AI will select a random one 
                    moveRating = 5;

                    if (computerMoveStrength < moveRating)
                    {
                        Random anotherRandom = new Random();
                        computerMove = anotherRandom.Next(0, 3);
                        switch (computerMove)
                        {
                            case 0:
                                if (positionArray[0] == blank)
                                {
                                    computerMove = 0;
                                }
                                break;

                            case 1:
                                if (positionArray[2] == blank)
                                {
                                    computerMove = 2;
                                }
                                break;

                            case 2:
                                if (positionArray[6] == blank)
                                {
                                    computerMove = 6;
                                }
                                break;

                            case 3:
                                if (positionArray[8] == blank)
                                {
                                    computerMove = 8;
                                }
                                break;
                        }
                    }

                    // If there is a vacant square next to another X, it will select that square.
                    if ((positionArray[0] == blank) &&((positionArray[1] == computer) || (positionArray[3] == computer)
                        || (positionArray[4] == computer)))
                    {
                        moveRating = 4;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 0;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[1] == blank) && ((positionArray[0] == computer) || (positionArray[2] == computer)
                        || (positionArray[3] == computer) || (positionArray[4] == computer)
                        || (positionArray[5] == computer)))
                    {
                        moveRating = 4;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 1;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[2] == blank) && ((positionArray[1] == computer) || (positionArray[4] == computer)
                        || (positionArray[5] == computer)))
                    {
                        moveRating = 4;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 2;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[3] == blank) && ((positionArray[0] == computer) || (positionArray[1] == computer)
                        || (positionArray[4] == computer) || (positionArray[6] == computer)
                        || (positionArray[7] == computer)))
                    {
                        moveRating = 4;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 3;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[5] == blank) && ((positionArray[1] == computer) || (positionArray[2] == computer)
                        || (positionArray[4] == computer) || (positionArray[7] == computer)
                        || (positionArray[8] == computer)))
                    {
                        moveRating = 4;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 5;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[6] == blank) && ((positionArray[2] == computer) || (positionArray[4] == computer)
                        || (positionArray[7] == computer)))
                    {
                        moveRating = 4;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 6;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[7] == blank) && ((positionArray[3] == computer) || (positionArray[4] == computer)
                        || (positionArray[5] == computer) || (positionArray[6] == computer)
                        || (positionArray[8] == computer)))
                    {
                        moveRating = 4;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 7;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[8] == blank) && ((positionArray[4] == computer) || (positionArray[5] == computer)
                        || (positionArray[7] == computer)))
                    {
                        moveRating = 4;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 8;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if (computerMove == -1)
                    {
                        moveRating = 2;

                        if (computerMoveStrength < moveRating)
                        {
                            Random anotherRandom = new Random();
                            computerMove = anotherRandom.Next(0, 9);
                        }
                    }

                    break;

                case 4:
                    // The fourth level of AI is almost impossible to beat. Firstly, the AI will check if the human player 
                    // has two squares on any line and will select the third square for the block. 
                    if ((positionArray[0] == blank) && (((positionArray[1] == human) && (positionArray[2] == human))
                        || ((positionArray[3] == human) && (positionArray[6] == human))
                        || ((positionArray[4] == human) && (positionArray[8] == human))))
                    {
                        moveRating = 10;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 0;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[1] == blank) && (((positionArray[0] == human) && (positionArray[2] == human))
                        || ((positionArray[4] == human) && (positionArray[7] == human))))
                    {
                        moveRating = 10;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 1;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[2] == blank) && (((positionArray[0] == human) && (positionArray[1] == human))
                        || ((positionArray[4] == human) && (positionArray[6] == human))
                        || ((positionArray[5] == human) && (positionArray[8] == human))))
                    {
                        moveRating = 10;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 2;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[3] == blank) && (((positionArray[0] == human) && (positionArray[6] == human))
                        || ((positionArray[4] == human) && (positionArray[5] == human))))
                    {
                        moveRating = 10;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 3;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[4] == blank) && (((positionArray[0] == human) && (positionArray[8] == human))
                        || ((positionArray[1] == human) && (positionArray[7] == human))
                        || ((positionArray[2] == human) && (positionArray[6] == human))
                        || ((positionArray[3] == human) && (positionArray[5] == human))))
                    {
                        moveRating = 10;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 4;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[5] == blank) && (((positionArray[2] == human) && (positionArray[8] == human))
                        || ((positionArray[3] == human) && (positionArray[4] == human))))
                    {
                        moveRating = 10;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 5;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[6] == blank) && (((positionArray[0] == human) && (positionArray[3] == human))
                        || ((positionArray[2] == human) && (positionArray[4] == human))
                        || ((positionArray[7] == human) && (positionArray[8] == human))))
                    {
                        moveRating = 10;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 6;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[7] == blank) && (((positionArray[1] == human) && (positionArray[4] == human))
                        || ((positionArray[6] == human) && (positionArray[8] == human))))
                    {
                        moveRating = 10;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 7;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[8] == blank) && (((positionArray[0] == human) && (positionArray[4] == human))
                        || ((positionArray[2] == human) && (positionArray[5] == human))
                        || ((positionArray[6] == human) && (positionArray[7] == human))))
                    {
                        moveRating = 10;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 8;
                            computerMoveStrength = moveRating;
                        }
                    }

                    // Next, if the computer occupies two squares in any row and the third square is vacant,
                    // then the AI will select that square for the win.
                    if ((positionArray[0] == blank) && (((positionArray[1] == computer) && (positionArray[2] == computer))
                        || ((positionArray[3] == computer) && (positionArray[6] == computer))
                        || ((positionArray[4] == computer) && (positionArray[8] == computer))))
                    {
                        moveRating = 8;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 0;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[1] == blank) && (((positionArray[0] == computer) && (positionArray[2] == computer))
                        || ((positionArray[4] == computer) && (positionArray[7] == computer))))
                    {
                        moveRating = 8;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 1;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[2] == blank) && (((positionArray[0] == computer) && (positionArray[1] == computer))
                        || ((positionArray[4] == computer) && (positionArray[6] == computer))
                        || ((positionArray[5] == computer) && (positionArray[8] == computer))))
                    {
                        moveRating = 8;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 2;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[3] == blank) && (((positionArray[0] == computer) && (positionArray[6] == computer))
                        || ((positionArray[4] == computer) && (positionArray[5] == computer))))
                    {
                        moveRating = 8;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 3;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[4] == blank) && (((positionArray[0] == computer) && (positionArray[8] == computer))
                        || ((positionArray[1] == computer) && (positionArray[7] == computer))
                        || ((positionArray[2] == computer) && (positionArray[6] == computer))
                        || ((positionArray[3] == computer) && (positionArray[5] == computer))))
                    {
                        moveRating = 8;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 4;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[5] == blank) && (((positionArray[2] == computer) && (positionArray[8] == computer))
                        || ((positionArray[3] == computer) && (positionArray[4] == computer))))
                    {
                        moveRating = 8;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 5;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[6] == blank) && (((positionArray[0] == computer) && (positionArray[3] == computer))
                        || ((positionArray[2] == computer) && (positionArray[4] == computer))
                        || ((positionArray[7] == computer) && (positionArray[8] == computer))))
                    {
                        moveRating = 8;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 6;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[7] == blank) && (((positionArray[1] == computer) && (positionArray[4] == computer))
                        || ((positionArray[6] == computer) && (positionArray[8] == computer))))
                    {
                        moveRating = 8;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 7;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[8] == blank) && (((positionArray[0] == computer) && (positionArray[4] == computer))
                        || ((positionArray[2] == computer) && (positionArray[5] == computer))
                        || ((positionArray[6] == computer) && (positionArray[7] == computer))))
                    {
                        moveRating = 8;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 8;
                            computerMoveStrength = moveRating;
                        }
                    }

                    // Next, if the human player holds two squares either side of a corner, the computer will take that corner
                    if ((positionArray[0] == blank) && ((positionArray[1] == human) && (positionArray[3] == human)))
                    {
                        moveRating = 7;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 0;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[2] == blank) && ((positionArray[1] == human) && (positionArray[5] == human)))
                    {
                        moveRating = 7;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 2;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[6] == blank) && ((positionArray[3] == human) && (positionArray[7] == human)))
                    {
                        moveRating = 7;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 0;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[8] == blank) && ((positionArray[5] == human) && (positionArray[7] == human)))
                    {
                        moveRating = 7;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 8;
                            computerMoveStrength = moveRating;
                        }
                    }

                    // Next, if the centre square is vacant, the AI will select that square
                    if (positionArray[4] == blank)
                    {
                        moveRating = 6;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 4;
                            computerMoveStrength = moveRating;
                        }
                    }

                    // If a corner square is available, the AI will select a random one 
                    moveRating = 5;

                    if (computerMoveStrength < moveRating)
                    {
                        Random anotherRandom = new Random();
                        computerMove = anotherRandom.Next(0, 3);
                        switch (computerMove)
                        {
                            case 0:
                                if (positionArray[0] == blank)
                                {
                                    computerMove = 0;
                                }
                                break;

                            case 1:
                                if (positionArray[2] == blank)
                                {
                                    computerMove = 2;
                                }
                                break;

                            case 2:
                                if (positionArray[6] == blank)
                                {
                                    computerMove = 6;
                                }
                                break;

                            case 3:
                                if (positionArray[8] == blank)
                                {
                                    computerMove = 8;
                                }
                                break;
                        }
                    }

                    // If there is a vacant square next to another X, it will select that square.
                    if ((positionArray[0] == blank) && ((positionArray[1] == computer) || (positionArray[3] == computer)
                        || (positionArray[4] == computer)))
                    {
                        moveRating = 4;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 0;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[1] == blank) && ((positionArray[0] == computer) || (positionArray[2] == computer)
                        || (positionArray[3] == computer) || (positionArray[4] == computer)
                        || (positionArray[5] == computer)))
                    {
                        moveRating = 4;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 1;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[2] == blank) && ((positionArray[1] == computer) || (positionArray[4] == computer)
                        || (positionArray[5] == computer)))
                    {
                        moveRating = 4;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 2;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[3] == blank) && ((positionArray[0] == computer) || (positionArray[1] == computer)
                        || (positionArray[4] == computer) || (positionArray[6] == computer)
                        || (positionArray[7] == computer)))
                    {
                        moveRating = 4;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 3;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[5] == blank) && ((positionArray[1] == computer) || (positionArray[2] == computer)
                        || (positionArray[4] == computer) || (positionArray[7] == computer)
                        || (positionArray[8] == computer)))
                    {
                        moveRating = 4;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 5;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[6] == blank) && ((positionArray[2] == computer) || (positionArray[4] == computer)
                        || (positionArray[7] == computer)))
                    {
                        moveRating = 4;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 6;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[7] == blank) && ((positionArray[3] == computer) || (positionArray[4] == computer)
                        || (positionArray[5] == computer) || (positionArray[6] == computer)
                        || (positionArray[8] == computer)))
                    {
                        moveRating = 4;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 7;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if ((positionArray[8] == blank) && ((positionArray[4] == computer) || (positionArray[5] == computer)
                        || (positionArray[7] == computer)))
                    {
                        moveRating = 4;

                        if (computerMoveStrength < moveRating)
                        {
                            computerMove = 8;
                            computerMoveStrength = moveRating;
                        }
                    }

                    if (computerMove == -1)
                    {
                        moveRating = 2;

                        if (computerMoveStrength < moveRating)
                        {
                            Random anotherRandom = new Random();
                            computerMove = anotherRandom.Next(0, 9);
                        }
                    }

                    break;
            }

            if (positionArray[computerMove] != blank)
            {
                computerMove = ChooseMove(positionArray);
            }

            return computerMove;
        }
    }
}
