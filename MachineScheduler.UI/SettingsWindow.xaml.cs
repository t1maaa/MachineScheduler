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
                DefaultConsignmentsPath = MachineScheduler.UI.Properties.Settings.Default.DefaultConsignmentsPath,
                DefaultMachinesPath = MachineScheduler.UI.Properties.Settings.Default.DefaultMachinesPath,
                DefaultNomenclaturesPath = MachineScheduler.UI.Properties.Settings.Default.DefaultNomenclaturesPath,
                DefaultOperationsPath = MachineScheduler.UI.Properties.Settings.Default.DefaultOperationsPath
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
            MachineScheduler.UI.Properties.Settings.Default.DefaultConsignmentsPath = SettingsViewModel.DefaultConsignmentsPath;
            MachineScheduler.UI.Properties.Settings.Default.DefaultMachinesPath = SettingsViewModel.DefaultMachinesPath;
            MachineScheduler.UI.Properties.Settings.Default.DefaultNomenclaturesPath = SettingsViewModel.DefaultNomenclaturesPath;
            MachineScheduler.UI.Properties.Settings.Default.DefaultOperationsPath = SettingsViewModel.DefaultOperationsPath;
            MachineScheduler.UI.Properties.Settings.Default.Save();
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}