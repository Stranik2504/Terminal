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
            _theme = "Fallout";
            
            InitializeComponent();
            LoadTheme(_theme);
            ConfigManager.Load();
            
            DevicesManager.AddDisk += AddDisk;
            DevicesManager.RemoveDisk += RemoveDisk;
            /*DevicesManager.StartLisining();
            DevicesManager.StopLisining();*/
            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;
            AllowsTransparency = true;

            KeyDown += (obj, e) =>
            {
                if (e.Key == Key.Escape)
                    Close();
                
                if (e.Key == Key.R)
                    Frame.NavigationService.Content.To<TextViewPage>().Relaod();
            };

            Frame.NavigationService.Navigate(new TextViewPage(Addition.Local + "/Test.txt", _theme));
        }

        private void LoadTheme(string name)
        {
            Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(Addition.Themes + name + "/Background.png", UriKind.RelativeOrAbsolute)) };
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