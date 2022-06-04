using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace MatchTheSymbol
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new();
        int tenthOfSecondsElapsed = 0;
        int pairsMatched = 0;
        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthOfSecondsElapsed++;
            timeTextBlock.Text = (tenthOfSecondsElapsed / 10F).ToString("0.0s");
            if (pairsMatched == 10)
            {
                timer.Stop();
                PlayAgainButton.Visibility = Visibility.Visible;
                PlayAgainButton.IsEnabled = true;
            }
        }

        private void SetUpGame()
        {
            PlayAgainButton.IsEnabled = false;
            PlayAgainButton.Visibility = Visibility.Hidden;

            List<string> emoji = new()
            {
                "💡", "💡",
                "✨", "✨",
                "🔅", "🔅",
                "🔆","🔆",
                "❌","❌",
                "♾️","♾️",
                "🙂","🙂",
                "🕶","🕶",
                "🎵","🎵",
                "🎶","🎶"
            };
            Random random = new();
            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name == "timeTextBlock")
                    continue;
                textBlock.Visibility = Visibility.Visible;
                int index = random.Next(emoji.Count);
                textBlock.Text = emoji[index];
                emoji.RemoveAt(index);
            }
            timer.Start();
        }

        TextBlock lastClickedBlock;
        bool isMatching = false;
        private void TextBlock_MouseDown(object sender, MouseEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            if (isMatching == false)
            {
                isMatching = true;
                textBlock.Visibility = Visibility.Hidden;
                lastClickedBlock = textBlock;
            }
            else if (lastClickedBlock.Text == textBlock.Text)
            {
                isMatching = false;
                pairsMatched++;
                textBlock.Visibility = Visibility.Hidden;
            }
            else
            {
                lastClickedBlock.Visibility = Visibility.Visible;
                isMatching = false;
            }
        }

        /*
        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (pairsMatched == 10)
            {
                timeTextBlock.Text = "0.0s";
                tenthOfSecondsElapsed = 0;
                pairsMatched = 0;
                SetUpGame();
            }
        }
        */
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (pairsMatched == 10)
            {
                PlayAgainButton.Visibility = Visibility.Hidden;
                tenthOfSecondsElapsed = 0;
                pairsMatched = 0;
                SetUpGame();
            }
        }
    }
}
