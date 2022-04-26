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
using System.Windows.Controls.Primitives;

namespace Terminal.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoadingPage.xaml
    /// </summary>
    public partial class LoadingPage : Page
    {
        private string directory = "";
        public LoadingPage()
        {
            InitializeComponent();
            // G:/Coding/MyProjects/Terminal/Resources
            // ../../../Resources
            DevicesManager.AddDisk += Add;
            DevicesManager.RemoveDisk += rem;

            //LoadingPage l = new LoadingPage();

            lstB.SelectionMode = SelectionMode.Single;
            lstB.ContextMenu = new ContextMenu();

            lstB.SelectedIndex = 0;

            lstB.PreviewKeyDown += AdditionalKeys;

            DevicesManager.StartLisining();
        }
        private void Add(string text)
        {
            Log.Logger.Information("add: {0}", text);
            System.Diagnostics.Debug.WriteLine("add: " + text);

            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, () => lstB.Items.Add(new ListBoxItem()
            {
                Tag = "📂",
                Content = text,
                Style = (Style)Resources["123"],
            }));

        }
        private void rem(string text)
        {
            System.Diagnostics.Debug.WriteLine("remove: " + text);
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, () => lstB.Items.Remove(text));
        }

        private void lstB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show(lstB.SelectedItem.ToString());
            string path = (string)(((ListBoxItem)(lstB.SelectedItem)).Content);
            directory += path + "\\";
            OpenFolder();

            //MessageBox.Show(directory);
        }
        private void OpenFolder()
        {
            
            string[] allFiles;
            try
            {
                allFiles = Directory.GetFiles(directory);
            }
            catch (Exception)
            {
                allFiles = new string[0];
            }
            lstB.Items.Clear();
            for (int i = 0; i < allFiles.Length; i++)
            {
                string[] template = allFiles[i].Split('\\');
                string text = (template[template.Length - 1].Split('.'))[0];
                string format = (template[template.Length - 1].Split('.'))[1].ToLower();
                //  🖹🖻🖺
                ListBoxItem lstBI = new ListBoxItem()
                {
                    Content = text,
                    Style = (Style)Resources["123"],
                };
                switch (format)
                {
                    case "txt":
                        lstBI.Tag = "🖹";
                        break;
                    case "png":
                        lstBI.Tag = "🖼";
                        break;
                    case "jpg":
                        lstBI.Tag = "🖼";
                        break;
                    case "bmp":
                        lstBI.Tag = "🖼";
                        break;
                    default:
                        break;
                }
                lstB.Items.Add(lstBI);
            }
            string[] allDirectories;
            try
            {
                allDirectories = Directory.GetDirectories(directory);
            }
            catch (Exception)
            {
                allDirectories = new string[0];
            }
            for (int i = 0; i < allDirectories.Length; i++)
            {
                string[] template = allDirectories[i].Split('\\');
                string text = (template[template.Length - 1].Split('.'))[0];
                if (text == "System Volume Information")
                {
                    continue;
                }
                ListBoxItem lstBI = new ListBoxItem()
                {
                    Tag = "📂",
                    Content = text,
                    Style = (Style)Resources["123"],
                };

                lstB.Items.Add(lstBI);
            }
            lstB.SelectedIndex = 0;
        }
        private void AdditionalKeys(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    lstB_MouseDoubleClick(null, null);
                    break;
                case Key.Escape:
                    //lstB_MouseDoubleClick(null, null); //       e/adsadad/asdsadas/
                    //directory.Split("/")
                    directory = directory.Remove(directory.LastIndexOf("\\"));
                    directory = directory.Remove(directory.LastIndexOf("\\"));
                    directory += "\\";
                    //MessageBox.Show(directory);
                    OpenFolder();
                    break;
                default:
                    break;
            }
        }
    }
}
