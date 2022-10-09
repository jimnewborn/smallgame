using System;
using System.Collections.Generic;
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

namespace smallgame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    using System.Windows.Threading;
    public partial class MainWindow : Window
    {
        TextBlock lastTextBlockClicked;
        bool findingMatch;
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecobdsElapsed;
        int mathesFound;
        public MainWindow()
        {
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            InitializeComponent();
            SetupGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            match.Text = mathesFound.ToString();
            tenthsOfSecobdsElapsed++;
            timeTextBlock.Text = (tenthsOfSecobdsElapsed / 10F).ToString("0.0s");
            if(mathesFound == 8)
            {
                timer.Stop();
                restart.IsEnabled = true;
                timeTextBlock.Text = timeTextBlock.Text + "-Play Again?";
            }
        }

        private void SetupGame()
        {
            List<string> animalEmoji = new List<string>
            {
                "🦝","🦝",
                "🐒","🐒",
                "🦊","🦊",
                "🐼","🐼",
                "🐧","🐧",
                "🐳","🐳",
                "🦑","🦑",
                "🐌","🐌",
            };
            Random random = new Random();
            foreach(TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if(textBlock.Name != "timeTextBlock" && textBlock.Name != "match")
                {
                    int index = random.Next(animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    textBlock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                }
                
            }
            timer.Start();
            tenthsOfSecobdsElapsed = 0;
            mathesFound = 0;
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (findingMatch == false)
            {
                lastTextBlockClicked = textBlock;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = true;
            }
            else if(lastTextBlockClicked.Text == textBlock.Text)
            {
                mathesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(mathesFound == 8)
            {
                SetupGame();
            }
        }

        private void restart_Click(object sender, RoutedEventArgs e)
        {
            SetupGame();
        }
    }
}
