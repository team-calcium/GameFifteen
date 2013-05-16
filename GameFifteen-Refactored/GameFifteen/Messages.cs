using System;
using System.Linq;

namespace GameFifteen
{
    /// <summary>
    /// Static class containing dialog messages used by the Game Engine
    /// </summary>
    public static class Messages
    {
        public const string Welcome = "\r\nWelcome to the game “15”. \r\nPlease try to arrange the numbers sequentially. \r\nUse: \r\n'top' to view the top scoreboard, \r\n'restart' to start a new game, \r\n'exit' to quit the game.\r\n";
        public const string IllegalMove = "Illegal move!\r\n";
        public const string IllegalCommand = "Illegal command!\r\n";
        public static readonly string CellValueOutOfRange = String.Format("Cell value must be in range [1; {0}] !", Field.MaxCellValue);
        public const string InputDemand = "Enter a number to move: ";
        public const string NameDemand = "Please enter your name for the top scoreboard: ";
        public const string ComplexityDemand = "Input the level of complexity (integer value bigger than 1):";
        public const string InvalidComplexityValue = "Invalid complexity value!";

        public static string CongratulationMessage(int movesCount)
        {
            return String.Format("\r\nCongratulations! You won the game in {0} moves.", movesCount);
        }
    }
}
