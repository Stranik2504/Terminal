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

    public static void PrintLines<T>(T element, Dispatcher dispatcher, params (string Text, uint Delay)[] TextArray) where T : TextBlock
    {
        new Thread(() =>
        {
            dispatcher.Invoke(() =>
            {
                element.Text = ConfigManager.Config.SpecialSymbol;
            });

            foreach ((string Text, uint Delay) in TextArray)
            {
                foreach (var symbol in Text)
                {
                    dispatcher.Invoke(() =>
                    {
                        element.Text = element.Text.Insert(element.Text.Length - 1, symbol.ToString());
                    });

                    if (Delay > 0)
                        Thread.Sleep((int)Delay);
                }
            }
            
            dispatcher.Invoke(() =>
            {
                element.Text = element.Text.Remove(element.Text.Length - 1);
            });
        }).Start();
    }
}