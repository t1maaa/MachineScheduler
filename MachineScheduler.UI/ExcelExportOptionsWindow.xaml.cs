using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MachineScheduler.DAL.Models;
using MachineScheduler.ViewModel;

namespace MachineScheduler.UI
{
    /// <summary>
    /// Логика взаимодействия для ExcelExportOptionsWindow.xaml
    /// </summary>
    public partial class ExcelExportOptionsWindow : Window
    {
        private ExcelExportOptionsViewModel ExcelExportOptionsViewModel { get; set; }

        public ExcelExportOptionsWindow(List<MachineSchedule> schedules, string filename)
        {
            InitializeComponent();
            ExcelExportOptionsViewModel = new ExcelExportOptionsViewModel(schedules);
            DataContext = ExcelExportOptionsViewModel;


            if (!ExcelExportOptionsViewModel.ExportSchedule.CanExecute(filename))
            {
                throw new ArgumentException($"{filename} is not valid filename");
            }
        }

        private void BtnExportSaveAs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ExcelExportOptionsViewModel.ExportSchedule.Execute(null);
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, exception.Message, exception.GetType().ToString(), MessageBoxButton.OK,
                    MessageBoxImage.Error);
                Close();
                return;
            }

            Close();
        }

        private void FrameworkElement_OnInitialized(object? sender, EventArgs e)
        {
            if (sender is RadioButton rb)
            {
                rb.ToolTip = ExcelExportOptionsViewModel.ExportModeDescription[(ExcelExportMode)rb.Content];
            }
            
        }
    }
}