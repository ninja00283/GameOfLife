using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GameOfLife;

namespace GameOfLifeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int size = 50;

        List<List<Rectangle>> rectangles;

        public  MainWindow()
        {
            Debug.WriteLine("loop");
            InitializeComponent();

            buttonupdate.Click += UpdateGrid;


            rectangles = GenerateCellGrid.StartGenerating(size, size, 4.5f);
        }



        private void UpdateGrid(object sender, RoutedEventArgs e)
        {

                Debug.WriteLine("keydown");
                ReadAndWriteGrid readGrid = new ReadAndWriteGrid();
                List<List<char>> CellGrid = LifeLogic.BeginCheckingCells(readGrid.ReadWholeGrid(size, rectangles));
                readGrid.WriteGrid(CellGrid, rectangles);
        }
        
    }

    class ReadAndWriteGrid
    {
        public List<List<char>> ReadWholeGrid(int size, List<List<Rectangle>> rectangles) {
            List<List<char>> CellGrid = new List<List<char>>();
            for (int y = 0; y < size; y++)
            {
                CellGrid.Add(new List<char>());
                for (int x = 0; x < size; x++)
                {
                    Debug.WriteLine("loop");
                    if (rectangles[y][x].Fill == Brushes.Orange)
                    {
                        CellGrid[y].Add('X');
                    }
                    else
                    {
                        CellGrid[y].Add('0');
                    }

                }
            }
            return CellGrid;
        }

        public void WriteGrid(List<List<char>>  CellGrid, List<List<Rectangle>> rectangles) {
            for (int y = 0; y < CellGrid.Count; y++)
            {
                List<char> yItem = CellGrid[y];
                for (int x = 0; x < yItem.Count; x++)
                {
                    char xItem = yItem[x];
                    Rectangle rectangle = rectangles[y][x];
                    if (xItem == 'X')
                    {
                        rectangle.Fill = Brushes.Orange;
                    }
                    else
                    {
                        rectangle.Fill = Brushes.LightGray;
                    }
                }
            }
        }
    }
}


