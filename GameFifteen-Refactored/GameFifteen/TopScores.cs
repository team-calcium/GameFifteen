﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GameFifteen
{
    /// <summary>
    /// Static class for managing, saving and loading top scores
    /// </summary>
    public static class TopScores
    {
        private const int NumberOfPlayersToSave = 10;
        private const string SaveScoresPath = "scores.txt";
        private static List<Player> topPlayers;

        /// <summary>
        /// Static constructor for loading topscorers from file
        /// </summary>
        static TopScores()
        {
            topPlayers = new List<Player>();
            if (!File.Exists(SaveScoresPath))
            {
                StreamWriter writer = new StreamWriter(SaveScoresPath, false);
                using (writer)
                {
                    writer.WriteLine("");
                }
            }
            else
            {
                GetTopScoresFromFile();
            }
        }

        /// <summary>
        /// Getting the top scores board
        /// </summary>
        /// <returns>The Scoreboard as string</returns>
        public static string GetScoreBoard()
        {
            StringBuilder scoreBoard = new StringBuilder();
            scoreBoard.AppendLine();
            scoreBoard.AppendLine("-----------------------");
            scoreBoard.Append("Top scores:");
            scoreBoard.AppendLine();
            int savedPlayersCount = topPlayers.Count;
            if (savedPlayersCount > 0)
            {
                for (int i = 0; i < savedPlayersCount; i++)
                {
                    scoreBoard.AppendLine(topPlayers[i].ToString());
                }
            }
            else
            {
                scoreBoard.AppendLine("<the scoreboard is currently empty!> ");
            }
            scoreBoard.AppendLine("-----------------------");

            return scoreBoard.ToString();
        }

        /// <summary>
        /// Adds a player to top scores (only a fixed number of scores saved).
        /// </summary>
        /// <param name="player">The player to add</param>
        public static void AddPlayerToScoreBoard(Player player)
        {
            topPlayers.Add(player);
            topPlayers.Sort((x, y) => x.Moves.CompareTo(y.Moves));
            if (topPlayers.Count > TopScores.NumberOfPlayersToSave)
            {
                topPlayers.RemoveAt(NumberOfPlayersToSave - 1);
            }
            SaveTopScoresToFile();
        }

        private static void GetTopScoresFromFile()
        {
            StreamReader reader = new StreamReader(TopScores.SaveScoresPath);
            using (reader)
            {
                char[] splitters = {'\n', '\r'};
                string[] scores = reader.ReadToEnd().Split(splitters, StringSplitOptions.RemoveEmptyEntries);

                int count = scores.Length;
                string name;
                int moves;
                string score;
                bool successfulParse = true;
                for (int i = 0; i < count; i++)
                {
                    score = scores[i];
                    name = score.Substring(0, score.LastIndexOf(':'));
                    score = score.Substring(score.LastIndexOf(':') + 1);
                    score = score.Substring(0, score.LastIndexOf('m')).Trim();
                    successfulParse = int.TryParse(score, out moves);
                    if (successfulParse)
                    {
                        TopScores.topPlayers.Add(new Player(name, moves));
                    }
                    else
                    {
                        throw new FileLoadException("Error occured while loading scores.txt file!");
                    }
                }

            }
        }

        private static void SaveTopScoresToFile()
        {
            StreamWriter writer = new StreamWriter(SaveScoresPath, false);
            using (writer)
            {
                int savedPlayersCount = topPlayers.Count;
                for (int i = 0; i < savedPlayersCount; i++)
                {
                    writer.WriteLine(topPlayers[i].ToString());
                }
            }
        }

    }
}
