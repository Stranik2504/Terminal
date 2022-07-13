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
using Terminal.Windows;

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
            DevicesManager.AddDisk += Add;
            DevicesManager.RemoveDisk += rem;
            DevicesManager.StartLisining();
            new HackWindow().Show();
        }
        private void Add(string text)
        {
           System.Diagnostics.Debug.WriteLine("add: " + text);
        }
        private void rem(string text)
        {
            System.Diagnostics.Debug.WriteLine("remove: " + text);
        }
    }
}