using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MachineScheduler.ViewModel;
using Microsoft.Win32;

namespace MachineScheduler.UI
{
    public partial class SettingsWindow : Window
    {
        public SettingsViewModel SettingsViewModel;
        private OpenFileDialog _openFileDialog;
        public SettingsWindow()
        {
            InitializeComponent();
            SettingsViewModel = new SettingsViewModel
            {
                DefaultConsignmentsPath = Properties.Settings.Default.DefaultConsignmentsPath,
                DefaultMachinesPath = Properties.Settings.Default.DefaultMachinesPath,
                DefaultNomenclaturesPath = Properties.Settings.Default.DefaultNomenclaturesPath,
                DefaultOperationsPath = Properties.Settings.Default.DefaultOperationsPath
            };
            DataContext = SettingsViewModel;
        }

        private void BtnSelect_Click(object sender, RoutedEventArgs e)
        {
            _openFileDialog ??= new OpenFileDialog()
            {
                InitialDirectory = Directory.GetCurrentDirectory(),
                Filter = MainWindowViewModel.FileTypesFilter
            };
            
            if (_openFileDialog.ShowDialog() != true) return;
            {
                ((StackPanel) ((Button) sender).Parent).Children.OfType<TextBox>().Single().Text =
                    _openFileDialog.FileName;
                _openFileDialog.InitialDirectory = Path.GetDirectoryName(_openFileDialog.FileName) ?? Directory.GetCurrentDirectory();
            }
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            ((Panel)(((Button)sender).Parent)).Children.OfType<TextBox>().Single().Clear();
        }
        
        // private void SettingsWindow_OnClosing(object sender, CancelEventArgs e)
        // {
        //     e.Cancel = true;
        //     ((Window)sender).Hide();
        // }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.DefaultConsignmentsPath = SettingsViewModel.DefaultConsignmentsPath;
            Properties.Settings.Default.DefaultMachinesPath = SettingsViewModel.DefaultMachinesPath;
            Properties.Settings.Default.DefaultNomenclaturesPath = SettingsViewModel.DefaultNomenclaturesPath;
            Properties.Settings.Default.DefaultOperationsPath = SettingsViewModel.DefaultOperationsPath;
            Properties.Settings.Default.Save();
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}