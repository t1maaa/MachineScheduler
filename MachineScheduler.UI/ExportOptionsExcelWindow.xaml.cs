using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MachineScheduler.DAL.Models;
using MachineScheduler.ViewModel;

namespace MachineScheduler.UI
{
    /// <summary>
    /// Логика взаимодействия для ExportOptionsExcelWindow.xaml
    /// </summary>
    public partial class ExportOptionsExcelWindow : Window
    {
        private ExportViewModel ExportViewModel { get; set; }

        public ExportOptionsExcelWindow(List<MachineSchedule> schedules, string filename)
        {
            InitializeComponent();
            ExportViewModel = new ExportViewModel(schedules);
            DataContext = ExportViewModel;


            if (!ExportViewModel.ExportSchedule.CanExecute(filename))
            {
                throw new ArgumentException($"{filename} is not valid filename");
            }
        }

        private void BtnExportSaveAs_Click(object sender, RoutedEventArgs e)
        {
            /*
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                InitialDirectory = Directory.GetCurrentDirectory(),
                Filter = ExportViewModel.FileTypesFilter
            };

            if (saveFileDialog.ShowDialog() != true) return;
            */
          //  if (ExportViewModel.ExportSchedule.CanExecute(saveFileDialog.FileName))
          //  {
                ExportViewModel.ExportSchedule.Execute(null);
         //   }
            Close();
        }

        private void FrameworkElement_OnInitialized(object? sender, EventArgs e)
        {
            if (sender is RadioButton rb)
            {
                rb.ToolTip = ExportViewModel.ExportModeDescription[(ExportMode)rb.Content];
            }
            
        }
    }
}