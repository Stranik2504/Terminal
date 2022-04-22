using System.Windows.Controls;

namespace Terminal.Frames;

public partial class StartingPage : Page
{
    public StartingPage()
    {
        InitializeComponent();

        LoadTheme("Fallout");
    }

    private void LoadTheme(string name)
    {
        //TODO: Load spechial font for page
    }
}