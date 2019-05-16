using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;

namespace GameOfLifeWPF
{
    class GenerateCellGrid
    {



        public static List<List<Rectangle>> StartGenerating(int width, int height,float size) {
            List<List<Rectangle>> rectangles = new List<List<Rectangle>>();
            for (int y = 0; y < height; y++)
            {
                rectangles.Add(new List<Rectangle>());
                ((MainWindow)Application.Current.MainWindow).CellGrid.RowDefinitions.Add(new RowDefinition());
                ((MainWindow)Application.Current.MainWindow).CellGrid.RowDefinitions[y].Height = new GridLength(1, GridUnitType.Star);
                for (int x = 0; x < width; x++)
                {
                    if (y == 0) {
                        ((MainWindow)Application.Current.MainWindow).CellGrid.ColumnDefinitions.Add(new ColumnDefinition());
                        ((MainWindow)Application.Current.MainWindow).CellGrid.ColumnDefinitions[x].Width = new GridLength(1, GridUnitType.Star);
                    }

                    Rectangle rectangle = GenerateRectangle(x,y, size);
                    rectangle.MouseDown += new MouseButtonEventHandler(CellClick);
                    Grid.SetRow(rectangle, y+1);
                    Grid.SetColumn(rectangle, x);
                    ((MainWindow)Application.Current.MainWindow).CellGrid.Children.Add(rectangle);
                    rectangles[y].Add(rectangle);
                }
            }
            return rectangles;
        }

        public static Rectangle GenerateRectangle(int x, int y, float size)
        {

            return new Rectangle()
            {
                Height = Double.NaN,
                Width = Double.NaN,
                Fill = Brushes.LightGray,
                Stroke = Brushes.LightGray,
                Name = "x" + x.ToString() + "y" + y.ToString() + "Rectangle",
                Focusable = false

            };
        }


        public static void CellClick(object sender, EventArgs e)
        {
            if ((sender as Rectangle).Fill == Brushes.Orange)
            {
                (sender as Rectangle).Fill = Brushes.LightGray;
            }
            else
            {
                (sender as Rectangle).Fill = Brushes.Orange;
            }
        }
    }
}
