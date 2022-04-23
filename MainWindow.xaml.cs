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

namespace Terminal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadTheme("Fallout");
            ConfigManager.Load();
            
            DevicesManager.AddDisk += AddDisk;
            DevicesManager.RemoveDisk += RemoveDisk;
            DevicesManager.StartLisining();
            
            Frame.NavigationService.Navigate(new Uri("Frames/TextViewPage.xaml", UriKind.Relative));
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