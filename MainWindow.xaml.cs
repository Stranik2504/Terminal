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
using Terminal.Classes;
using Terminal.Frames;

namespace Terminal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _theme;
        
        public MainWindow()
        {
            InitializeComponent();
            ConfigManager.Load();
            
            _theme = ConfigManager.Config.Theme;
            
            LoadTheme(_theme);
            LoadParams();

            Frame.NavigationService.Navigate(new PictureViewPage(@"C:\Users\rund2\Documents\Programming\C#\Terminal\bin\Debug\net6.0-windows\Local\Test.jpg", _theme));
        }

        private void LoadTheme(string name)
        {
            Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(Addition.Themes + name + "/Background.png", UriKind.RelativeOrAbsolute)) };
        }

        private void LoadParams()
        {
            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;
            ResizeMode = ResizeMode.NoResize;
            AllowsTransparency = true;
            
            DevicesManager.AddDisk += AddDisk;
            DevicesManager.RemoveDisk += RemoveDisk;
            
            KeyDown += (obj, e) =>
            {
                if (e.Key == Key.Escape)
                    Close();
                
                if (e.Key == Key.R)
                    Frame.NavigationService.Content.To<PictureViewPage>().Reload();
            };
        }
        
        private void AddDisk(string text)
        {
            Log.Logger.Information("add: {text}", text);
        }
        
        private void RemoveDisk(string text)
        {
            Log.Logger.Information("remove: {text}", text);
        }
    }
}