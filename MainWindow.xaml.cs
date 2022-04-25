using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
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
using Terminal.Pages;
using System.ComponentModel;

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
            Background = new ImageBrush(new BitmapImage(new Uri("Assets/Themes/Fallout/Background.png", UriKind.Relative)));

            qwe.NavigationService.Navigate(new Uri("Pages/LoadingPage.xaml", UriKind.Relative));
            Closing += (object? sender, CancelEventArgs e) => DevicesManager.StopLisining();
        }
    }
}