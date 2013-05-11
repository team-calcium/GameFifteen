using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFifteen
{
    class Program
    {
        static int[,] a = new int[4, 4] {{1,2,3,4}, {5,6,7,8}, {9,10,11,12}, {13,14,15,0}};
        static int x = 3, y = 3;
        static bool repeat = true;
        static int counter;       
        
        static string[] topCandidates = new string[5];
        static int topCount = 0;

        static void PrintTable()
        {
            Console.WriteLine(" -------------");
            for (int i = 0; i < 4; i++)
            {
                Console.Write("| ");
                for (int j = 0; j < 4; j++)
                {
                    Console.Write("{0,2} ", a[i,j] != 0 ? a[i,j].ToString() : " ");                    
                }
                Console.WriteLine("|");
            }
            Console.WriteLine(" -------------");
        }

        static void GenerateТаблица()
        {
            counter = 0;
            Random r = new Random();
            for (int i = 0; i < 1000; i++)
            {
                int n = r.Next(3);
                if (n == 0)
                {
                    int nx = x - 1;
                    int ny = y;
                    if (nx >= 0 && nx <= 3 && ny >= 0 && ny <= 3)
                    {
                        int temp = a[x, y];
                        a[x, y] = a[nx, ny];
                        a[nx, ny] = temp;
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
                        int temp = a[x, y];
                        a[x, y] = a[nx, ny];
                        a[nx, ny] = temp;
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
                        int temp = a[x, y];
                        a[x, y] = a[nx, ny];
                        a[nx, ny] = temp;
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
                        int temp = a[x, y];
                        a[x, y] = a[nx, ny];
                        a[nx, ny] = temp;
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

        static bool proverka(int i, int j)
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
            bool flag = true;
            for (int i = 0; i < 4; i++)
            {
                if (flag)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (a[i, j] == n)
                        {
                            k = i; l = j;
                            flag = false;
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }

            bool flag2 = proverka(k, l);
            if (!flag2)
            {
                Console.WriteLine("Illegal move!");
            }
            else
            {
                int temp = a[k, l];
                a[k, l] = a[x, y];
                a[x, y] = temp;
                x = k; 
                y = l;
                counter++;
                PrintTable();
            }
        }

        static bool Solved()
        {
            if (a[3, 3] == 0)
            {
                int n = 1;
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (n <= 15)
                        {
                            if (a[i, j] == n)
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
            GenerateТаблица();
            PrintTable();
        }

        static void AddAndSort(int i, string res)
        {
            if (i == 0)
            {
                topCandidates[i] = res;
            }
            for (int j = 0; j < i; j++)
            {
                topCandidates[j] = topCandidates[j + 1];
            }
            topCandidates[i] = res;
        }

        static void PrintTop()
        {
            Console.WriteLine("\nScoreboard:");
            if (topCount != 0)
            {
                for (int i = 5 - topCount; i < 5; i++)
                {
                    Console.WriteLine("{0}", topCandidates[i]);
                }
            }
            else
            {
                Console.WriteLine("-");
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            while (repeat)
            {
                GenerateТаблица();
                Console.WriteLine("Welcome to the game “15”. Please try to arrange the numbers sequentially. Use 'top' to view the top scoreboard, 'restart' to start a new game and 'exit' to quit the game.\n");
                PrintTable();

                bool flagSolved = Solved();
                while (!flagSolved)
                {
                    Console.Write("Enter a number to move: ");
                    string s = Console.ReadLine();
                    int n;
                    bool flag = int.TryParse(s, out n);
                    if (flag)
                    {
                        if (n >= 1 && n <= 15)
                        {
                            Move(n);
                        }
                        else
                        {
                            Console.WriteLine("Illegal move!");
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
                                    PrintTop();
                                }
                                else
                                {
                                    Console.WriteLine("Illegal command!");
                                }
                            }
                        }
                    }
                    flagSolved = Solved();
                }
                if (flagSolved)
                {
                    Console.WriteLine("Congratulations! You won the game in {0} moves.", counter);
                   
                    Console.Write("Please enter your name for the top scoreboard: ");
                   
                    string s1 = Console.ReadLine();
                  
                    string res = counter + " moves by " + s1;
                 
                    if (topCount < 5)
                    {
                        topCandidates[topCount] = res;
                 
                        topCount++;
                 
               
                        Array.Sort(topCandidates);
                    }



                    else


                    

                        for (int i = 4; i >= 0; i++)
                        

                            if (topCandidates[i].CompareTo(res) <= 0)
                            

                                AddAndSort(i, res);
                            

                        

                    
                    PrintTop();
                }
            }
        }
    }
}
