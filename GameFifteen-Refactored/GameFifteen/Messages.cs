using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFifteen
{
    static class Messages
    {
        public const string Welcome = "\n\rWelcome to the game “15”. \n\rPlease try to arrange the numbers sequentially. \n\rUse: \n\r'top' to view the top scoreboard, \n\r'restart' to start a new game, \n\r'exit' to quit the game.\n\r";
        public const string IllegalMove = "Illegal move!\n";
        public const string IllegalCommand = "Illegal command!\n";
        public static readonly string CellValueOutOfRange = String.Format("Cell value must be in range [1; {0}] !", Field.MaxValue);
        public const string InputDemand = "Enter a number to move: ";
        public const string NameDemand = "Please enter your name for the top scoreboard: ";
        public const string ComplexityDemand = "Input the level of complexity (integer value bigger than 1):";
        public const string InvalidComplexityValue = "Invalid complexity value!";

        public static string CongratulationMessage(int movesCount)
        {
            return String.Format("\n\rCongratulations! You won the game in {0} moves.", movesCount);
        }
    }
}
