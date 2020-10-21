using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace MatchGame
{
    using System.Windows.Threading;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tentsOfSecondsElapsed;
        int matchesFound;
        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += Timer_Tick;

            SetupGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tentsOfSecondsElapsed++;
            timeTextBlock.Text = (tentsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
            }
        }

        private void SetupGame()
        {
            List<string> animalEmoji = new List<string>()
            {
                "🤣","🤣",
                "😊","😊",
                "😂","😂",
                "❤","❤",
                "😍","😍",
                "👌","👌",
                "🙌","🙌",
                "🎂","🎂"

            };
            Random random = new Random();
            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "timeTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    textBlock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);

                }
                timer.Start();
                tentsOfSecondsElapsed = 0;
                matchesFound = 0;
            }

        }

        TextBlock lastTextBlockClicket;
        bool findMatch = false;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (findMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicket = textBlock;
                findMatch = true;
            }
            else if (textBlock.Text == lastTextBlockClicket.Text)
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findMatch = false;
            }
            else
            {
                lastTextBlockClicket.Visibility = Visibility.Visible;
                findMatch = false;
            }

        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetupGame();
            }
        }
    }
}
