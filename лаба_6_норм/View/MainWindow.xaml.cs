using System.Windows;
using лаба_6_норм.ViewModels;

namespace лаба_6_норм.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}