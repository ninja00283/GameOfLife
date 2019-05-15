using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;

namespace GameOfLife
{

    class Program
    {
        static void Main(string[] args)
        {
            LifeLogic.StartConsoleLoop();
        }
    }


    public class GameBoardClass {
        public List<List<char>> GameBoardGrid { get; set; }

        public GameBoardClass() {
            GameBoardGrid = new List<List<char>> {
                new List<char>{ '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0','0', '0',},
                new List<char>{ '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0','0', '0',},
                new List<char>{ '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0','0', '0',},
                new List<char>{ '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0','0', '0',},
                new List<char>{ '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0','0', '0',},
                new List<char>{ '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0','0', '0',},
                new List<char>{ '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0','0', '0',},
                new List<char>{ '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0','0', '0',},
                new List<char>{ '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0','0', '0',},
                new List<char>{ '0', '0', '0', '0', '0', 'X', '0', 'X', '0', 'X', '0', '0', '0', '0','0', '0',},
                new List<char>{ '0', '0', '0', '0', '0', 'X', '0', '0', '0', 'X', '0', '0', '0', '0','0', '0',},
                new List<char>{ '0', '0', '0', '0', '0', 'X', '0', '0', '0', 'X', '0', '0', '0', '0','0', '0',},
                new List<char>{ '0', '0', '0', '0', '0', 'X', '0', '0', '0', 'X', '0', '0', '0', '0','0', '0',},
                new List<char>{ '0', '0', '0', '0', '0', 'X', '0', 'X', '0', 'X', '0', '0', '0', '0','0', '0',},
                new List<char>{ '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0','0', '0',},
                new List<char>{ '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0','0', '0',},
                new List<char>{ '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0','0', '0',},
                new List<char>{ '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0','0', '0',},
                new List<char>{ '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0','0', '0',},
                new List<char>{ '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0','0', '0',},
                new List<char>{ '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0','0', '0',},
                new List<char>{ '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0','0', '0',},
                new List<char>{ '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0','0', '0',},
                new List<char>{ '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0','0', '0',},
            };
        }
    }

    public class LifeLogic
    {

        //The list that will hold all the cell data
        public static List<List<char>> GameBoardGrid = new GameBoardClass().GameBoardGrid;


        /// <summary>
        /// 
        /// Initialize the game board that will hold the cells with a explorder pattern.
        /// 
        /// Then loop until there's nothing left to change updating every frame.
        /// 
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void StartConsoleLoop()
        {
            
            int Time = 0;
            bool Changed = true;
            while (Changed)
            {
                
                System.Threading.Thread.Sleep(250);
                PrintFinal(GameBoardGrid);
                List<List<char>> GameBoardGridToCheck = BeginCheckingCells(GameBoardGrid);
                int CheckedEqual = 0;
                for (int i = 0; i < GameBoardGridToCheck.Count; i++)
                {
                    for (int ii = 0; ii < GameBoardGridToCheck[i].Count; ii++)
                    {
                        if (GameBoardGridToCheck[i][ii] == GameBoardGrid[i][ii])
                        {
                            CheckedEqual++;
                        }
                    }
                }
                Changed = !(CheckedEqual == GameBoardGridToCheck.Count * GameBoardGridToCheck[0].Count);
                GameBoardGrid = GameBoardGridToCheck;
                Console.WriteLine(Time++);
            }   
        }

        /// <summary>
        ///  First deep clone the list for the exact size of the DataGrid.
        ///  Then empty the grid.
        ///  Then for each posistion in the grid check the number of neighbours and remove the cell, populate the cell or do nothing;
        ///  Then Return the updated grid.
        ///  
        /// </summary>
        /// <param name="GameBoardGrid"> The Grid with cell data </param>
        /// <returns></returns>
        public static List<List<char>> BeginCheckingCells(List<List<char>> GameBoardGrid) {

            List<List<char>> gameBoardGrid = GameBoardGrid.ConvertAll(GameBoard => new List<char>(GameBoard));

            for (int i = 0; i < gameBoardGrid.Count; i++)
            {
                for (int ii = 0; ii < gameBoardGrid[i].Count; ii++)
                {
                    gameBoardGrid[i][ii] = '0';
                }
            }

            for (int y = 0; y < GameBoardGrid.Count; y++)
            {
                for (int x = 0; x < GameBoardGrid[y].Count; x++)
                {
                    int numberOfNeighbours = checkcells(GameBoardGrid, x,y);
                    switch (numberOfNeighbours)
                    {
                        case int i when (i < 2):
                            gameBoardGrid = RemoveCurrentCell(x, y, gameBoardGrid);
                            break;
                        case 3:
                            gameBoardGrid = AddCurrentCell(x, y, gameBoardGrid);
                            break;
                        case int i when (i > 3):
                            gameBoardGrid = RemoveCurrentCell(x, y, gameBoardGrid);
                            break;
                        default:
                            gameBoardGrid[y][x] = GameBoardGrid[y][x];
                            break;
                    }
                }
            }
            return gameBoardGrid;
        }

        //clear the given cell
        public static List<List<char>> RemoveCurrentCell(int x, int y, List<List<char>> GameBoardGrid) {
            List<List<char>> gameBoardGrid = GameBoardGrid.ConvertAll(GameBoard => new List<char>(GameBoard));
            gameBoardGrid[y][x] = '0';
            return gameBoardGrid;
        }

        //populate the given cell
        public static List<List<char>> AddCurrentCell(int x, int y, List<List<char>> GameBoardGrid)
        {
            List<List<char>> gameBoardGrid = GameBoardGrid.ConvertAll(GameBoard => new List<char>(GameBoard));
            gameBoardGrid[y][x] = 'X';
            return gameBoardGrid;
        }


        //return the amount of neighbours
        public static int checkcells(List<List<char>> gameBoardGrid, int x, int y) {
            int numberOfNeighbours = 0;
            for (int i = x-1; i < x+2; i++)
            {
                for (int i1 = y-1; i1 < y+2; i1++)
                {
                    if (i1 < gameBoardGrid.Count && i1 > -1)
                    {
                        if (i < gameBoardGrid[i1].Count && i > -1)
                        {
                            if (i != x ||  i1 != y)
                            {
                                if (gameBoardGrid[i1][i] == 'X')
                                {
                                    numberOfNeighbours++;
                                }
                            }
                        }
                    }
                }
            }
            return numberOfNeighbours;
        }

        //print tthe whole grid and change the colour if the cell is full
        public static void PrintFinal(List<List<char>> gameBoardGrid)
        {
            Console.Clear();
            foreach (var xItem in gameBoardGrid)
            {
                foreach (var yItem in xItem)
                {
                    if (yItem == 'X')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else {
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.Write(yItem.ToString());
                }
                Console.Write("\n");
            }

        }
    }
}
