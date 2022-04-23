using System;
using System.Windows.Controls;
using System.Windows.Media;
using Terminal.Classes;

namespace Terminal.Frames;

public partial class StartingPage : Page
{
    public StartingPage()
    {
        InitializeComponent();
        LoadTheme("Fallout");
        LoadParams();
        
    }

    private void LoadTheme(string name)
    {
        Output.FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "Assets/Themes/Fallout/#Fallout Regular");
    }

    private void LoadParams()
    {
        Output.FontSize = ConfigManager.Config.FontSize;
    }
}