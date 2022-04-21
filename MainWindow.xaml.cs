using System;
using System.Collections.Generic;
using System.IO;
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
            DevicesManager.AddDisk += Add;
            DevicesManager.RemoveDisk += rem;

            lstB.ContextMenu = new ContextMenu();

            DevicesManager.StartLisining();
        }
        private void Add(string text)
        {
            Log.Logger.Information("add: {0}", text);
            System.Diagnostics.Debug.WriteLine("add: " + text);

            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, () => lstB.Items.Add(text));
            
        }
        private void rem(string text)
        {
            System.Diagnostics.Debug.WriteLine("remove: " + text);
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, () => lstB.Items.Remove(text));
        }

        private void lstB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string path = (string)(lstB.SelectedItem);
            string[] allFiles = Directory.GetFiles(path);
            string temp = "";
            for (int i = 0; i < allFiles.Length; i++)
            {
                //t2 += allFiles[i] + "\n";

                string[] template = allFiles[i].Split('\\');
                string text = (template[template.Length - 1].Split('.'))[0];

                temp += text+"\n";
                //bmp = new Bitmap(fullpath);

            }
           
            MessageBox.Show(temp); 
        }
    }
}