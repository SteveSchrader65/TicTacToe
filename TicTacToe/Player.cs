// The Player class holds the information about the human player for the TicTacToe
// application. There are only two variables in the class. playerScore holds the 
// running total of wins by the player, while playerFlag indicates game squares
// that the player has occupied.
//
// A value of 1 is set for the human player, with a value of 2 for the computer 
// opponent. These flags are used throughout to indicate which player has made a 
// move, or which is being checked to determine if they have won the current game.
//
// The Player class is the parent to the Computer class.
//
namespace TicTacToe
{
    class Player
    {
        public static int playerScore;
        public static int playerFlag = 1;
    }
}
