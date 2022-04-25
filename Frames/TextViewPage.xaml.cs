using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Terminal.Classes;

namespace Terminal.Frames;

public partial class TextViewPage : Page
{
    private string _filename;
    private string _theme;

    public TextViewPage(string filename, string theme)
    {
        InitializeComponent();
        LoadTheme(theme);
        LoadParams();
        _filename = filename;
        _theme = theme;
        
        LoadText();
    }

    public void Relaod()
    {
        ConfigManager.Load();
        LoadParams();
        LoadTheme(_theme);
        LoadText();
    }

    private void LoadText()
    {
        new Thread(() =>
        {
            using var stream = File.OpenText(_filename);
            var text = stream.ReadToEnd();
                
            Addition.PrintLines(Output, Dispatcher, (text, ConfigManager.Config.UsingDelayFastOutput ? ConfigManager.Config.DelayFastOutput : 0));
        }).Start();
    }

    private void LoadTheme(string name)
    {
        Output.FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "Assets/Themes/Fallout/#Fallout Regular");
    }

    private void LoadParams()
    {
        Output.FontSize = ConfigManager.Config.FontSize;
        Output.Opacity = ConfigManager.Config.Opacity;
        Output.Foreground = (Brush)new BrushConverter().ConvertFromString(ConfigManager.Config.TerminalColor)!; 
    }
}