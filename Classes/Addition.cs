using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Terminal.Classes;

public static class Addition
{
    public const string Themes = "Assets/Themes/";
    public const string Assets = "Assets";
    public const string Local = "Local";

    public static T To<T>(this object obj)
    {
        if (obj is T robj)
            return robj;

        return default;
    }

    public static void PrintLines<T>(T element, Dispatcher dispatcher,
        params (string Text, uint Delay)[] TextArray) where T : TextBlock
        => PrintLines(element, dispatcher, default, TextArray);

    public static void PrintLines<T>(T element, Dispatcher dispatcher, Mutex mutex = default, params (string Text, uint Delay)[] TextArray) where T : TextBlock
    {
        foreach ((string Text, uint Delay) in TextArray)
        {
            foreach (var symbol in Text)
            {
                mutex?.WaitOne();
                    
                dispatcher.Invoke(() =>
                {
                    if (element.Text.Length > 0 && element.Text[^1].ToString() == ConfigManager.Config.SpecialSymbol)
                        element.Text = element.Text.Insert(element.Text.Length - 1, symbol.ToString());
                    else
                        element.Text += symbol.ToString();
                }, DispatcherPriority.Background);
                    
                mutex?.ReleaseMutex();

                if (Delay > 0)
                    Thread.Sleep((int)Delay);
            }
        }
    }
}