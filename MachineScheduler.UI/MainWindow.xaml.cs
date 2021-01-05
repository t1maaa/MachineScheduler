using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MachineScheduler.ViewModel;
using Microsoft.Win32;

namespace MachineScheduler.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly ScheduleChartWindow _scheduleChartWindow;
        //private SettingsWindow _settingsWindow;
        private bool _isNewRow;

        public void Dispose()
        {
            DgConsignments.InitializingNewItem -= OnNewItemEventHandler;
            DgMachines.InitializingNewItem -= OnNewItemEventHandler;
            DgNomenclatures.InitializingNewItem -= OnNewItemEventHandler;
            DgOperations.InitializingNewItem -= OnNewItemEventHandler;
        }
       
        
        public MainWindow()
        {
            _mainWindowViewModel = new MainWindowViewModel(
                String.IsNullOrWhiteSpace(MachineScheduler.UI.Properties.Settings.Default.StartupConsignmentsPath)
                    ? MachineScheduler.UI.Properties.Settings.Default.DefaultConsignmentsPath
                    : MachineScheduler.UI.Properties.Settings.Default.StartupConsignmentsPath,
                
                String.IsNullOrWhiteSpace(MachineScheduler.UI.Properties.Settings.Default.StartupMachinesPath)
                    ? MachineScheduler.UI.Properties.Settings.Default.DefaultMachinesPath
                    : MachineScheduler.UI.Properties.Settings.Default.StartupMachinesPath,
                
                String.IsNullOrWhiteSpace(MachineScheduler.UI.Properties.Settings.Default.StartupNomenclaturesPath)
                    ? MachineScheduler.UI.Properties.Settings.Default.DefaultNomenclaturesPath
                    : MachineScheduler.UI.Properties.Settings.Default.StartupNomenclaturesPath,
                
                String.IsNullOrWhiteSpace(MachineScheduler.UI.Properties.Settings.Default.StartupOperationsPath)
                    ? MachineScheduler.UI.Properties.Settings.Default.DefaultOperationsPath
                    : MachineScheduler.UI.Properties.Settings.Default.StartupOperationsPath);
            
            
            DataContext = _mainWindowViewModel;
            InitializeComponent();

            _mainWindowViewModel.Schedules.CollectionChanged += (sender, args) =>
            {
                BtnExportSchedule.IsEnabled = true;
                BtnVisualizeSchedule.IsEnabled = true;
            };


            if (_mainWindowViewModel.Schedules == null)
            {
                BtnExportSchedule.IsEnabled = false;
                BtnVisualizeSchedule.IsEnabled = false;
            }
            
            DgConsignments.InitializingNewItem += OnNewItemEventHandler;
            DgMachines.InitializingNewItem += OnNewItemEventHandler;
            DgNomenclatures.InitializingNewItem += OnNewItemEventHandler;
            DgOperations.InitializingNewItem += OnNewItemEventHandler;

            _scheduleChartWindow = new ScheduleChartWindow(_mainWindowViewModel);
            //_settingsWindow = new SettingsWindow();

        }

        private void BtnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            var senderParent = ((FrameworkElement)(((Button)sender).Parent));
            var entityTypename = senderParent.Name;
            // var tbPath =  ((StackPanel) senderParent).Children.OfType<TextBox>().Single();
            // var dgItemsCount = ((DockPanel) ((GroupBox) ((StackPanel) senderParent)?.Parent)?.Parent)?.Children
            //     .OfType<DataGrid>().Single().ItemsSource.OfType<object>().Count();
            
            string path;
            ICommand openFile = null;
            switch (entityTypename)
            {
                case "Consignment":
                    openFile = _mainWindowViewModel.ConsignmentViewModel.OpenFileCommand;
                    path = _mainWindowViewModel.ConsignmentViewModel.Filename;
                    break;
                case "Nomenclature":
                    openFile = _mainWindowViewModel.NomenclatureViewModel.OpenFileCommand;
                    path = _mainWindowViewModel.NomenclatureViewModel.Filename;
                    break;
                case "Machine":
                    openFile = _mainWindowViewModel.MachineViewModel.OpenFileCommand;
                    path = _mainWindowViewModel.MachineViewModel.Filename;
                    break;
                case "Operation":
                    openFile = _mainWindowViewModel.OperationViewModel.OpenFileCommand;
                    path = _mainWindowViewModel.OperationViewModel.Filename;
                    break;
                default:
                    throw new NotSupportedException($"Type {entityTypename} is not supported yet");
            }
            
            var openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Directory.Exists(path) ? path : Directory.GetCurrentDirectory(),
                Filter = MainWindowViewModel.FileTypesFilter
            };
            
            if (openFileDialog.ShowDialog() != true) return;
            if (openFile.CanExecute(openFileDialog.FileName))
                openFile.Execute(openFileDialog.FileName);
        }

        private void BtnVisualizeSchedule_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MakeSchedule();
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, exception.Message, exception.GetType().ToString(), MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
            
            if (_scheduleChartWindow.Visibility == Visibility.Visible)
                _scheduleChartWindow.Activate();
            else
            {
                _scheduleChartWindow.Show();
            }
        }

        private void BtnExportSchedule_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MakeSchedule();
                var schedules = _mainWindowViewModel.Schedules;
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    InitialDirectory = Directory.GetCurrentDirectory(),
                    Filter = ExportOptionsViewModel.FileTypesFilter
                };
                if (saveFileDialog.ShowDialog() != true) return;

                switch (Path.GetExtension(saveFileDialog.FileName))
                {
                    case ".xlsx":
                        ExcelExportOptionsWindow excelExportOptionsWindow =
                            new ExcelExportOptionsWindow(schedules.ToList(),
                                    saveFileDialog.FileName) //TODO: base window? for export options
                                { Owner = this };
                        excelExportOptionsWindow.ShowDialog();
                        return;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, exception.Message, exception.GetType().ToString(), MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            // _settingsWindow.Owner = this;
            // if (_settingsWindow.Visibility != Visibility.Collapsed &&
            //     _settingsWindow.Visibility != Visibility.Hidden)
            //     _settingsWindow.Activate();
            // else
            // {
            //     _settingsWindow.ShowDialog();
            // }
            new SettingsWindow() { Owner = this }
                .ShowDialog();
        }

        private void MakeSchedule()
        {
            if (_mainWindowViewModel.MakeSchedule.CanExecute(null))
            {
                try
                {
                    _mainWindowViewModel.MakeSchedule.Execute(null);
                }
                catch (ArgumentException exception)
                {
                    throw;
                }
            }
        }
        
        private void DataGrid_OnBeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if (!_isNewRow)
                e.Cancel = true;
        }

        private void DataGrid_OnRowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (_isNewRow)
                _isNewRow = false;
        }

        private void OnNewItemEventHandler(object o, InitializingNewItemEventArgs e)
        {
            _isNewRow = true;
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            _scheduleChartWindow.Close();
        }
    }
}
