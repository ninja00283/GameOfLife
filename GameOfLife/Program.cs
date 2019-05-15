using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GameOfLife
{

    class Program
    {

        private static List<List<char>> GameBoardGrid;

        

        static void Main(string[] args)
        {
            GameBoardGrid = new List<List<char>> {
                new List<char>{ '0','0','0','0','0' },
                new List<char>{ '0','0','X','0','0' },
                new List<char>{ '0','0','X','0','0' },
                new List<char>{ '0','0','X','0','0' },
                new List<char>{ '0','0','0','0','0' },
            };
            int i = 0;
            while (true)
            {
                
                System.Threading.Thread.Sleep(500);
                PrintFinal(GameBoardGrid);
                GameBoardGrid = BeginCheckingCells(GameBoardGrid);
                Console.WriteLine(i++);
            }   
        }


        public static List<List<char>> BeginCheckingCells(List<List<char>> gameBoardGrid) {
            for (int y = 0; y < gameBoardGrid.Count; y++)
            {
                for (int x = 0; x < gameBoardGrid[y].Count; x++)
                {
                    int numberOfNeighbours = checkcells(gameBoardGrid,y,x);
                    switch (numberOfNeighbours)
                    {
                        case int i when (i < 2):
                            gameBoardGrid = RemoveCurrentCell(x, y, gameBoardGrid);
                            Debug.WriteLine("<2");
                            break;
                        case 3:
                            gameBoardGrid = AddCurrentCell(x, y, gameBoardGrid);
                            Debug.WriteLine("3");
                            break;
                        case int i when (i > 3):
                            gameBoardGrid = RemoveCurrentCell(x, y, gameBoardGrid);
                            Debug.WriteLine(">3");
                            break;
                        default:
                            Debug.WriteLine("<2");
                            break;
                    }
                }
            }
            return gameBoardGrid;
        }

        public static List<List<char>> RemoveCurrentCell(int x, int y, List<List<char>> GameBoardGrid) {
            List<List<char>> gameBoardGrid = GameBoardGrid.ConvertAll(GameBoard => new List<char>(GameBoard));

            gameBoardGrid[x][y] = '0';
            return gameBoardGrid;
        }

        public static List<List<char>> AddCurrentCell(int x, int y, List<List<char>> GameBoardGrid)
        {
            List<List<char>> gameBoardGrid = GameBoardGrid.ConvertAll(GameBoard => new List<char>(GameBoard));
            gameBoardGrid[x][y] = 'X';
            return gameBoardGrid;
        }

        public static int checkcells(List<List<char>> gameBoardGrid, int x, int y) {
            //return the amount of neighbours
            int numberOfNeighbours = 0;
            for (int i = x-1; i < x+2; i++)
            {
                for (int i1 = y-1; i1 < y+2; i1++)
                {
                    if (i1 < gameBoardGrid.Count && i1 > -1) {
                        if (i < gameBoardGrid[i1].Count && i > -1) {
                            if (i != x ||  i1 != y)
                            {
                                if (gameBoardGrid[i][i1] == 'X')
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

        public static void PrintFinal(List<List<char>> gameBoardGrid)
        {
            Console.Clear();
            foreach (var xItem in gameBoardGrid)
            {
                foreach (var yItem in xItem)
                {
                    Console.Write(yItem.ToString());
                }
                Console.Write("\n");
            }

        }
    }
}
