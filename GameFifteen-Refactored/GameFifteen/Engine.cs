using System;
using System.Linq;
using System.Text;
using System.IO;

namespace GameFifteen
{

    /// <summary>
    /// Class responsible for the main game logic
    /// </summary>
    public class Engine
    {
        /// <summary>
        /// Renderer used by the Engine to render given objects
        /// </summary>
        /// <param name="obj">Object to be rendered (usually some string expression)</param>
        public delegate void Render(object obj);

        /// <summary>
        /// The method for getting user input
        /// </summary>
        /// <returns>User input as string</returns>
        public delegate string InputReader();

        private Game game;
        private Render render;
        private InputReader inputReader;
        private bool play;

        /// <summary>
        /// Creates a new Engine instance
        /// </summary>
        /// <param name="stringRenderer">Renderer used by the Engine to render given objects(usually strings)</param>
        /// <param name="stringInputReader">The method for getting user input</param>
        public Engine(Render stringRenderer, InputReader stringInputReader)
        {
            this.game = new Game();
            this.render = stringRenderer;
            this.inputReader = stringInputReader;
            this.play = true;
        }

        /// <summary>
        /// Gets the game field cells
        /// </summary>
        public int[,] Cells
        {
            get
            {
                return this.game.Field.Cells;
            }
        }

        /// <summary>
        /// Gets the empty cell x coordinate
        /// </summary>
        public int EmptyCellX
        {
            get
            {
                return this.game.Field.EmptyX;
            }
        }
        /// <summary>
        /// Gets the empty cell y coordinate
        /// </summary>
        public int EmptyCellY
        {
            get
            {
                return this.game.Field.EmptyY;
            }
        }

        /// <summary>
        /// Starts the main game logic
        /// </summary>
        public void Start()
        {
            this.play = true;
            LoadScoresFromFile();

            while (this.play)
            {
                this.Restart();

                string currentCommand = "";
                while (!this.game.IsSolved() && this.play)
                {
                    currentCommand = ReadCommand();
                    ExecuteCommand(currentCommand);
                }

                if (this.play)
                {
                    this.render(this.GetGameWonScreen());
                    this.UpdateScoreBoard();
                    this.render(TopScores.GetScoreBoard());
                }
            }
        }

        private void LoadScoresFromFile()
        {
            try
            {
                TopScores.GetScoreBoard();
            }
            catch (FileLoadException ex)
            {
                this.render(ex.Message);
            }
        }

        private void UpdateScoreBoard()
        {
            this.game.Player.Name = this.inputReader() ?? "Unknown";
            TopScores.AddPlayerToScoreBoard(this.game.Player);
        }

        private string GetGameWonScreen()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(Messages.CongratulationMessage(this.game.Player.Moves));
            result.Append(Messages.NameDemand);
            return result.ToString();
        }

        private void ExecuteCommand(string currentCommand)
        {
            int cellValue;
            bool isNumber = int.TryParse(currentCommand, out cellValue);
            if (isNumber)
            {
                TryMove(cellValue);
                this.render(this.game.Field);
            }
            else
            {
                switch (currentCommand)
                {
                    case "exit":
                        this.play = false;
                        break;
                    case "restart":
                        this.Restart();
                        break;
                    case "top":
                        this.render(TopScores.GetScoreBoard());
                        this.render(this.game.Field);
                        break;
                    default:
                        this.render(Messages.IllegalCommand);
                        this.render(this.game.Field);
                        break;
                }
            }
        }

        private string ReadCommand()
        {
            this.render(Messages.InputDemand);
            return this.inputReader();
        }

        private void Restart()
        {
            this.render(Messages.Welcome);
            this.render(Messages.ComplexityDemand);
            int complexity = 0;
            while (!int.TryParse(this.inputReader(), out complexity) || complexity < 1)
            {
                this.render(Messages.InvalidComplexityValue);
                this.render(Messages.ComplexityDemand);
            }
            this.game.Complexity = complexity;
            this.game.Restart();
            while (this.game.IsSolved())
            {
                this.game.Restart();
            }
            this.render(this.game.Field);
        }

        private bool IsValidMove(int i, int j)
        {
            if (j == this.EmptyCellY && (i == this.EmptyCellX - 1 || i == this.EmptyCellX + 1))
            {
                return true;
            }
            if ((i == this.EmptyCellX) && (j == this.EmptyCellY - 1 || j == this.EmptyCellY + 1))
            {
                return true;
            }
            return false;
        }


        private void TryMove(int cellValue)
        {
            if (cellValue < 0 || cellValue > Field.MaxCellValue)
            {
                this.render(Messages.CellValueOutOfRange);
                return;
            }

            int x;
            int y;
            this.game.Field.GetCellCoordinates(cellValue, out x, out y);

            if (IsValidMove(x, y))
            {
                MakeMove(x,y);
            }
            else
            {
                this.render(Messages.IllegalMove);
            }
        }

        private void MakeMove(int newEmptyCellX, int newEmptyCellY)
        {
            this.game.Field.MoveEmptyCell(newEmptyCellX, newEmptyCellY);
            this.game.Player.Move();
        }
    }
}
