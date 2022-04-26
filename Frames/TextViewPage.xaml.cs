using System;
using System.Diagnostics;
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
    private bool _update;
    private Thread _printText;
    private Mutex _mutex = new();

    public TextViewPage(string filename, string theme)
    {
        InitializeComponent();
        LoadTheme(theme);
        LoadParams();
        
        _filename = filename;
        _theme = theme;
        Output.Text = ConfigManager.Config.SpecialSymbol;
        
        Unloaded += (obj, e) => { _update = false; _printText.Interrupt(); };

        LoadText();
        Scroller.Focus();
    }

    public void Reload()
    {
        ConfigManager.Load();
        LoadParams();
        LoadTheme(_theme);
        
        _update = false;
        
        _mutex?.WaitOne();
        _printText.Interrupt();
        Output.Text = ConfigManager.Config.SpecialSymbol;
        _mutex?.ReleaseMutex();

        LoadText();
        Scroller.Focus();
    }

    private void LoadText()
    {
        try
        {
            _printText = new Thread(() =>
            {
                using var stream = File.OpenText(_filename);
                var text = stream.ReadToEnd();
                
                Addition.PrintLines(Output, Dispatcher, _mutex, (text, ConfigManager.Config.UsingDelayFastOutput ? ConfigManager.Config.DelayFastOutput : 0));
                UpdateCarriage();
            });
        
            _printText.Start();
        }
        catch {}
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

    private void UpdateCarriage()
    {
        _update = true;
        
        new Thread(() =>
        {
            while (_update)
            {
                _mutex?.WaitOne();
                
                Dispatcher.Invoke(() =>
                {
                    if (Output.Text.Length > 0 && Output.Text[^1].ToString() == ConfigManager.Config.SpecialSymbol)
                        Output.Text = Output.Text.Remove(Output.Text.Length - 1);
                    else
                        Output.Text += ConfigManager.Config.SpecialSymbol;
                }, DispatcherPriority.Background);
                
                _mutex?.ReleaseMutex();
                
                Thread.Sleep((int)ConfigManager.Config.DelayUpdateCarriage);
            }
        }).Start();
    }
}