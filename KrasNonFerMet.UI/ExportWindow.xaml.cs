using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using KrasNonFerMet.DAL.Files.Excel;
using KrasNonFerMet.DAL.Models;
using Microsoft.Win32;

namespace KrasNonFerMet.UI
{
    /// <summary>
    /// Логика взаимодействия для ExportWindow.xaml
    /// </summary>
    public partial class ExportWindow : Window
    {
        private MachineSchedulesExport _export;
        public ExportWindow(MachineSchedulesExport export)
        {
            InitializeComponent();
            _export = export;
        }

        private void BtnExportSaveAs_Click(object sender, RoutedEventArgs e)
        {
            if (RbMerged.IsChecked == true)
                _export.ExportMode = ExportMode.Merged;
            if (RbPaged.IsChecked == true)
                _export.ExportMode = ExportMode.Paged;
            if (RbSplitted.IsChecked == true)
                _export.ExportMode = ExportMode.Splitted;

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                InitialDirectory = Directory.GetCurrentDirectory(),
                Filter = "Excel Files|*.xlsx;"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                _export.SaveAs(new FileInfo(saveFileDialog.FileName));
                Close();
            }
        }
    }
}
