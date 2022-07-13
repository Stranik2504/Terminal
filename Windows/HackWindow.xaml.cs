using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;

namespace Terminal.Windows
{
    /// <summary>
    /// Логика взаимодействия для HackWindow.xaml
    /// </summary> 

    public enum Direction { Left, Right, Up, Down, JustNext }

    public partial class HackWindow : Window
    {
        private string[] words = { "я", "в", "своем", "познании", "настолько", "преисполнился", "лох" };
        private string symbols = "~!@#$%^&*()_-=+{}|?/\"\';:<>";
        private int prevSpanPosRow = 0;
        private int prevSpanPosColumn = 1;
        private string rightWord = "лох";
        private int lineNumber = 0;

        InlineCollection inlines;
        IEnumerator<Inline> enumerator;
        List<List<Span>> spans = new List<List<Span>>();

        public HackWindow()
        {
            InitializeComponent();
            AddToField();
            this.PreviewKeyDown += new KeyEventHandler(HandleButton);
            Initialize();
        }

        private void Initialize()
        {
            inlines = leftP.Inlines;
            enumerator = inlines.GetEnumerator();
            List<Span> oneSpan = new List<Span>();
            for (int i = 0; i < inlines.Count; i++)
            {
                bool b = enumerator.MoveNext();
                if (b && enumerator.Current is LineBreak)
                {
                    spans.Add(oneSpan);
                    oneSpan = new List<Span>();
                }
                if (b && enumerator.Current is Span)
                {
                    oneSpan.Add(enumerator.Current as Span);
                }
            }
            spans.Add(oneSpan);

            spans[prevSpanPosColumn][prevSpanPosRow].Background = new SolidColorBrush(Colors.DarkGreen);
            spans[prevSpanPosColumn][prevSpanPosRow].Focus();
            Run run = (Run)spans[prevSpanPosColumn][prevSpanPosRow].Inlines.FirstInline;
            string text = run.Text;
            password.Text = "Password: " + text;
        }

        private void HandleButton(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
            if (e.Key == Key.Right)
                HigliteWord(Direction.Right);
            if (e.Key == Key.Left)
                HigliteWord(Direction.Left);
            if (e.Key == Key.Down)
                HigliteWord(Direction.Down);
            if (e.Key == Key.Up)
                HigliteWord(Direction.Up);
            if (e.Key == Key.Tab)
                HigliteWord(Direction.JustNext);
            if (e.Key == Key.Enter)
                FillConsole();
        }

        private void FillConsole()
        {
            AddTextToConsole(CheckTheWord());
        }

        private string CheckTheWord()
        {
            if (progressBar.Value <= 0)
            {

            }
            if (password.Text == "Password: " + rightWord)
                return ">ACESS";
            else
            {
                progressBar.Value -= 1;
                return ">" + password.Text.Substring(password.Text.IndexOf(' ')) + "\n" + ">DENIED" + "\n" + ">" +
                    HowManyCorrectSymbols(password.Text.Substring(password.Text.IndexOf(' '))).ToString() + "/" + rightWord.Length.ToString();
            }
        }

        private void AddTextToConsole(string mes)
        {
            Dispatcher.InvokeAsync(() =>
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(55);
                rightConsole.RowDefinitions.Add(row);
                TextBlock text = new TextBlock();
                Border border = new Border();

                text.FontSize = 14;
                text.Margin = new Thickness(5, 0, 5, 0);
                text.VerticalAlignment = VerticalAlignment.Top;

                border.CornerRadius = new CornerRadius(5);
                border.BorderThickness = new Thickness(1);

                text.Text = mes;

                Grid.SetRow(border, lineNumber);
                Grid.SetRow(text, lineNumber);
                lineNumber++;
                border.Child = text;
                rightConsole.Children.Add(border);
            });
        }

        private int HowManyCorrectSymbols(string word)
        {
            HashSet<Char> set = new HashSet<Char>();
            for (int i = 0; i < word.Length; i++)
            {
                for (int j = 0; j < rightWord.Length; j++)
                {
                    if (rightWord[j] == word[i])
                        set.Add(word[i]);
                }
            }
            return set.Count;
        }

        private void HigliteWord(Direction direction)
        {
            ClearBgSpans(spans);

            bool isItArrow = true;

            if (direction == Direction.Left)
                prevSpanPosRow -= 1;
            if (direction == Direction.Right)
                prevSpanPosRow += 1;
            if (direction == Direction.Up)
                prevSpanPosColumn -= 1;
            if (direction == Direction.Down)
                prevSpanPosColumn += 1;
            if (direction == Direction.JustNext)
            {
                prevSpanPosRow += 1;
                isItArrow = false;
            }

            CorrectSpanPos(spans, isItArrow);

            spans[prevSpanPosColumn][prevSpanPosRow].Background = new SolidColorBrush(Colors.DarkGreen);
            spans[prevSpanPosColumn][prevSpanPosRow].Focus();
            Run run = (Run)spans[prevSpanPosColumn][prevSpanPosRow].Inlines.FirstInline;
            string text = run.Text;
            password.Text = "Password: " + text;
        }

        private void CorrectSpanPos(List<List<Span>> spans, bool isItArrow)
        {
            if (isItArrow)
            {
                if (prevSpanPosColumn >= spans.Count)
                    prevSpanPosColumn = spans.Count - 1;
                if (prevSpanPosColumn <= 0)
                    prevSpanPosColumn = 1;
                for (int i = 0; i < spans[prevSpanPosColumn].Count; i++)
                {
                    if (prevSpanPosRow >= spans[prevSpanPosColumn].Count)
                        prevSpanPosRow = spans[prevSpanPosColumn].Count - 1;
                    if (prevSpanPosRow <= 0)
                        prevSpanPosRow = 0;
                }
            }
            else
            {
                if (prevSpanPosRow >= spans[prevSpanPosColumn].Count)
                {
                    prevSpanPosColumn += 1;
                    prevSpanPosRow = 0;
                }
                if (prevSpanPosColumn >= spans.Count)
                {
                    prevSpanPosColumn = 1;
                    prevSpanPosRow = 0;
                }
            }
        }

        private void ClearBgSpans(List<List<Span>> spans)
        {
            for (int i = 0; i < spans.Count; i++)
            {
                for (int j = 0; j < spans[i].Count; j++)
                {
                    spans[i][j].Background = new SolidColorBrush(Colors.White);
                }
            }
        }

        private string GenerateRandomString(double length)
        {
            string resString = "";
            do
            {
                Random random = new Random();
                string prevSymb = "";
                resString = "";
                for (int i = 0; i < length; i++)
                {
                    string symb = "";
                    if (words.Contains(prevSymb))
                        symb = symbols[random.Next(symbols.Length)].ToString();
                    else
                        symb = random.Next(0, 15) == 1 ? words[random.Next(words.Length)] : symbols[random.Next(symbols.Length)].ToString();
                    if ((resString + symb).Length <= length)
                    {
                        resString += symb;
                        prevSymb = symb;
                    }
                }
            }
            while (!resString.Contains(rightWord));

            return resString;
        }

        private void AddToField()
        {
            string txt = GenerateRandomString(500);

            MessageBox.Show(txt.Length.ToString());

            string word = "";

            for (int i = 0; i < txt.Length; i++)
            {
                if (i % 50 == 0)
                {
                    LineBreak br = new LineBreak();
                    leftP.Inlines.Add(br);
                }
                if (char.IsLetter(txt[i]))
                {
                    word += txt[i].ToString();
                }
                else if (word != "" && !char.IsLetter(txt[i + 1]))
                {
                    Run run = new Run(word);
                    run.FontSize = 32;
                    Span span = new Span(run);
                    leftP.Inlines.Add(span);
                    word = "";
                }
                else
                {
                    Run run = new Run(txt[i].ToString());
                    run.FontSize = 32;
                    leftP.Inlines.Add(run);
                }

            }
            leftRTB.Focus();
        }
    }
}
