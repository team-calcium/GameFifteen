using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GameFifteen
{
    class Engine
    {
        public delegate void Render(object obj);
        public delegate string InputReader();

        private Game game;
        private Render render;
        private InputReader inputReader;
        private bool play;

        public Engine(Render stringRenderer, InputReader stringInputReader)
        {
            this.game = new Game();
            this.render = stringRenderer;
            this.inputReader = stringInputReader;
            this.play = true;
        }

        public int[,] Cells
        {
            get
            {
                return this.game.Field.Cells;
            }
        }

        public int EmptyCellX
        {
            get
            {
                return this.game.Field.EmptyX;
            }
            set
            {
                this.game.Field.EmptyX = value;
            }
        }

        public int EmptyCellY
        {
            get
            {
                return this.game.Field.EmptyY;
            }
            set
            {
                this.game.Field.EmptyY = value;
            }
        }

        public void Start()
        {
            this.play = true;
            try
            {
                TopScores.GetScoreBoard();
            }
            catch (FileLoadException ex)
            {
                this.render(ex.Message);
            }

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

        private void UpdateScoreBoard()
        {
            this.game.Player.Name = this.inputReader() ?? "Unknown";
            TopScores.Update(this.game.Player);
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


        public void TryMove(int cellValue)
        {
            if (cellValue < 0 || cellValue > Field.MaxValue)
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

        private void MakeMove(int startX, int startY)
        {
            this.Cells[EmptyCellX, EmptyCellY] = this.Cells[startX, startY];
            this.Cells[startX, startY] = 0;
            EmptyCellX = startX;
            EmptyCellY = startY;

            this.game.Player.Move();
        }
    }
}
