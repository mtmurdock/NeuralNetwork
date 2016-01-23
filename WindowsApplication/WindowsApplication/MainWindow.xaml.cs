using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
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

namespace NeuralNetwork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        
        Canvas canvas;

        Perceptron perceptron;
        Trainer[] trainers = new Trainer[2000];
        
        public MainWindow()
        {
            InitializeComponent();

            setup();

            canvas = new Canvas();
            this.Content = canvas;






            // Create a StackPanel to contain the shape.
            //stackPanel = new StackPanel();
            //background = new Rectangle();

            //this.Content = stackPanel;

            //SolidColorBrush solidColorBrush = new SolidColorBrush();
            //solidColorBrush.Color = Color.FromArgb(255, 255, 255, 255);

            //background.Fill = solidColorBrush;
            //background.Width = Width;
            //background.Height = Height;

            //stackPanel.Children.Add(background);


            //// Create a red Ellipse.
            //myEllipse = new Ellipse();

            //// Create a SolidColorBrush with a red color to fill the 
            //// Ellipse with.
            //SolidColorBrush mySolidColorBrush = new SolidColorBrush();

            //// Describes the brush's color using RGB values. 
            //// Each value has a range of 0-255.
            //mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 0);
            //myEllipse.Fill = mySolidColorBrush;
            //myEllipse.StrokeThickness = 2;
            //myEllipse.Stroke = Brushes.Black;

            //// Set the width and height of the Ellipse.
            //myEllipse.Width = 200;
            //myEllipse.Height = 100;

            //// Add the Ellipse to the StackPanel.
            //myStackPanel.Children.Add(myEllipse);

            

            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Tick += new EventHandler(onTimerTick);
            timer.Start();
        }

        void setup()
        {
            perceptron = new Perceptron(3);

            // generate trainers
            Random random = new Random();
            for (int i = 0; i < trainers.Count(); i++)
            {
                float x = (float)((this.Width * random.NextDouble()) - (this.Width / 2));
                float y = (float)((this.Height * random.NextDouble()) - (this.Height / 2));

                int answer = 1;
                if (y < lineFunction(x))
                    answer = -1;

                trainers[i] = new Trainer(x, y, answer);
            }
        }

        float lineFunction(float x)
        {
            return 2 * x + 1;
        }

        private int count = 0;
        public void onTimerTick(object o, EventArgs sender)
        {
            perceptron.train(trainers[count].inputs, trainers[count].answer);
            count = (count + 1) % trainers.Count();

            canvas.Children.Clear();

            Rectangle rectangle = new Rectangle();
            rectangle.Width = Width;
            rectangle.Height = Height;
            rectangle.Fill = Brushes.Green;

            canvas.Children.Add(rectangle);

            for (int i = 0; i < count; i++)
            {
                int guess = perceptron.feedForward(trainers[i].inputs);

                Ellipse point = new Ellipse();
                point.Height = 8;
                point.Width = 8;

                if (guess > 0)
                {
                    point.Fill = Brushes.Black;
                }
                else
                {
                    point.Fill = Brushes.White;
                }
                
                Canvas.SetLeft(point, (Width/2) + trainers[i].inputs[0]);
                Canvas.SetTop(point, (Height/2) + trainers[i].inputs[1]);

                canvas.Children.Add(point);
            }
        }
    }
}
