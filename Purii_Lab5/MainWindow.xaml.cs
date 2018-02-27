using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace Purii_Lab5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Point> points;
        bool saved;

        public MainWindow()
        {
            InitializeComponent();
            points = new List<Point>();
            saved = true;
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            saved = false;

            Point last = e.GetPosition(this);

            if (points.Count > 0)
            {
                Point prev = points.Last();

                Line line = new Line();
                line.Stroke = System.Windows.Media.Brushes.Black;
                line.StrokeThickness = 2;
                line.HorizontalAlignment = HorizontalAlignment.Left;
                line.VerticalAlignment = VerticalAlignment.Center;
                line.X1 = last.X;
                line.Y1 = last.Y;
                line.X2 = prev.X;
                line.Y2 = prev.Y;

                Canvas.Children.Add(line);
            }

            points.Add(last);
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            Open();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private bool Save()
        {
            Stream fileStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "PTS files (*.pts)|*.pts";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == true)
            {
                if ((fileStream = saveFileDialog1.OpenFile()) != null)
                {
                    XmlSerializer XMLSer = new XmlSerializer(typeof(List<Point>));
                    XMLSer.Serialize(fileStream, points);
                    fileStream.Close();
                    saved = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool Open()
        {
            if (!saved)
            {
                Save();
            }

            Stream fileStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "PTS files (*.pts)|*.pts";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == true)
            {
                try
                {
                    if ((fileStream = openFileDialog1.OpenFile()) != null)
                    {
                        XmlSerializer XMLSer = new XmlSerializer(typeof(List<Point>));
                        points = (List<Point>)XMLSer.Deserialize(fileStream);
                        fileStream.Close();
                        Redraw();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void Redraw()
        {
            if (points.Count <= 1)
            {
                return;
            }

            Canvas.Children.Clear();

            Point prev = points[0];
            for(int i = 1; i < points.Count; i++)
            {
                Point next = points[i];
                Line line = new Line();
                line.Stroke = System.Windows.Media.Brushes.Black;
                line.StrokeThickness = 2;
                line.HorizontalAlignment = HorizontalAlignment.Left;
                line.VerticalAlignment = VerticalAlignment.Center;
                line.X1 = prev.X;
                line.Y1 = prev.Y;
                line.X2 = next.X;
                line.Y2 = next.Y;
                Canvas.Children.Add(line);
                prev = next;
            }
        }

        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            Canvas.Children.RemoveAt(Canvas.Children.Count - 1);
            points.RemoveAt(points.Count - 1);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Canvas.Children.Clear();
            points.Clear();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!saved)
            {
                Save();
            }
        }
    }
}
