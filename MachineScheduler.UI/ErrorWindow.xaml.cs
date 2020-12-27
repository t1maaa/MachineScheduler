using System.Windows;
using MachineScheduler.ViewModel;

namespace MachineScheduler.UI
{
    /// <summary>
    /// Логика взаимодействия для ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {
        private ErrorViewModel ErrorViewModel { get; set; }
        public ErrorWindow(Window parent, string message)
        {
            Owner = parent;
            ErrorViewModel = new ErrorViewModel(message);
            InitializeComponent();
            DataContext = ErrorViewModel;
        }

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
