using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFifteen
{
    class OldUnusedClass
    {
        static int[,] field = new int[4, 4] {{1,2,3,4}, {5,6,7,8}, {9,10,11,12}, {13,14,15,0}};
        static int x = 3, y = 3;
        static bool repeat = true;
        static int counter;
        
        static string[] topScorers = new string[5];
        static int topCount = 0;

        static void PrintTable()
        {
            //Console.WriteLine(game.Field);
            Console.WriteLine(" -------------");
            for (int i = 0; i < 4; i++)
            {
                Console.Write("| ");
                for (int j = 0; j < 4; j++)
                {
                    Console.Write("{0,2} ", field[i,j] != 0 ? field[i,j].ToString() : " ");                    
                }
                Console.WriteLine("|");
            }
            Console.WriteLine(" -------------");
        }

        static void GenerateRandomTable()
        {
            counter = 0;
            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                int n = random.Next(3);
                if (n == 0)
                {
                    int nx = x - 1;
                    int ny = y;
                    if (nx >= 0 && nx <= 3 && ny >= 0 && ny <= 3)
                    {
                        int temp = field[x, y];
                        field[x, y] = field[nx, ny];
                        field[nx, ny] = temp;
                        x = nx;
                        y = ny;
                    }
                    else
                    {
                        n++;
                        i--;
                    }
                }
                if (n == 1)
                {
                    int nx = x;
                    int ny = y + 1;
                    if (nx >= 0 && nx <= 3 && ny >= 0 && ny <= 3)
                    {
                        int temp = field[x, y];
                        field[x, y] = field[nx, ny];
                        field[nx, ny] = temp;
                        x = nx;
                        y = ny;
                    }
                    else
                    {
                        n++;
                        i--;
                    }
                }
                if (n == 2)
                {
                    int nx = x + 1;
                    int ny = y;
                    if (nx >= 0 && nx <= 3 && ny >= 0 && ny <= 3)
                    {
                        int temp = field[x, y];
                        field[x, y] = field[nx, ny];
                        field[nx, ny] = temp;
                        x = nx;
                        y = ny;
                    }
                    else
                    {
                        n++;
                        i--;
                    }
                }
                if (n == 3)
                {
                    int nx = x;
                    int ny = y - 1;
                    if (nx >= 0 && nx <= 3 && ny >= 0 && ny <= 3)
                    {
                        int temp = field[x, y];
                        field[x, y] = field[nx, ny];
                        field[nx, ny] = temp;
                        x = nx;
                        y = ny;
                    }
                    else
                    {
                        i--;
                    }
                }
            }
        }

        static bool IsValidMove(int i, int j)
        {
            if ((i == x - 1 || i == x + 1) && j == y)
            {
                return true;
            }
            if ((i == x) && (j == y - 1 || j == y + 1))
            {
                return true;
            }
            return false;
        }

        static void Move(int n)
        {
            int k = x, l = y;
            bool searchCell = true;
            for (int i = 0; i < 4; i++)
            {
                if (searchCell)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (field[i, j] == n)
                        {
                            k = i; l = j;
                            searchCell = false;
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }

            bool legalMove = IsValidMove(k, l);
            if (!legalMove)
            {
                Console.WriteLine(Messages.IllegalMove);
            }
            else
            {
                int temp = field[k, l];
                field[k, l] = field[x, y];
                field[x, y] = temp;
                x = k; 
                y = l;
                counter++;
                PrintTable();
            }
        }

        static bool Solved()
        {
            if (field[3, 3] == 0)
            {
                int n = 1;
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (n <= 15)
                        {
                            if (field[i, j] == n)
                            {
                                n++;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        static void RestartGame()
        {
            GenerateRandomTable();
            PrintTable();
        }

        static void AddAndSort(int i, string res)
        {
            if (i == 0)
            {
                topScorers[i] = res;
            }
            for (int j = 0; j < i; j++)
            {
                topScorers[j] = topScorers[j + 1];
            }
            topScorers[i] = res;
        }

        static void PrintTopScorers()
        {
            Console.WriteLine("\nScoreboard:");
            if (topCount != 0)
            {
                for (int i = 5 - topCount; i < 5; i++)
                {
                    Console.WriteLine("{0}", topScorers[i]);
                }
            }
            else
            {
                Console.WriteLine("-");
            }
            Console.WriteLine();
        }

        static void OldMain(string[] args)
        {
            while (repeat)
            {
                GenerateRandomTable();
                Console.WriteLine(Messages.Welcome);
                PrintTable();

                bool flagSolved = Solved();
                while (!flagSolved)
                {
                    Console.Write(Messages.InputDemand);
                    string s = Console.ReadLine();
                    int n;
                    bool isNumberInput = int.TryParse(s, out n);
                    if (isNumberInput)
                    {
                        if (n >= 1 && n <= 15)
                        {
                            Move(n);
                        }
                        else
                        {
                            Console.WriteLine(Messages.IllegalMove);
                        }
                    }
                    else
                    {
                        if (s == "exit")
                        {
                            Console.WriteLine("Good bye!");
                            repeat = false;
                            break;
                        }
                        else
                        {
                            if (s == "restart")
                            {
                                RestartGame();
                            }
                            else
                            {
                                if (s == "top")
                                {
                                    PrintTopScorers();
                                }
                                else
                                {
                                    Console.WriteLine(Messages.IllegalCommand);
                                }
                            }
                        }
                    }
                    flagSolved = Solved();
                }
                if (flagSolved)
                {
                    Console.WriteLine(Messages.CongratulationMessage(counter));
                   
                    Console.Write(Messages.NameDemand);
                   
                    string currentName = Console.ReadLine();
                  
                    string res = counter + " moves by " + currentName;

                    if (topCount < 5)
                    {
                        topScorers[topCount] = res;
                        topCount++;
                        Array.Sort(topScorers);
                    }
                    else
                    {
                        for (int i = 4; i >= 0; i++)
                        {
                            if (topScorers[i].CompareTo(res) <= 0)
                            {
                                AddAndSort(i, res);
                            }
                        }
                    }
                    PrintTopScorers();
                }
            }
        }
    }
}
