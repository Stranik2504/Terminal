using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Terminal.Classes;

namespace Terminal.Frames;

public partial class PictureViewPage : Page
{
    private string _filename;
    private string _theme;
    public PictureViewPage(string filename, string theme)
    {
        InitializeComponent();
        LoadTheme(theme);

        _filename = filename;
        _theme = theme;

        LoadImage(filename);
    }

    public void Reload()
    {
        LoadTheme(_theme);
        LoadImage(_filename);
    }

    private void LoadImage(string filename)
    {
        Picture.Source = new BitmapImage(new Uri(filename));
    }

    private void LoadTheme(string theme)
    {
        
    }
}