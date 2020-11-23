using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
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
using LiveCharts;
using LiveCharts.Wpf;
using KrasNonFerMet.DAL.Models;
using LiveCharts.Configurations;
using LiveCharts.Helpers;

namespace KrasNonFerMet.UI
{
    /// <summary>
    /// Логика взаимодействия для Schedule.xaml
    /// </summary>
    public partial class Schedule : Window
    {
        private List<MachineSchedule> _schedules;
        public SeriesCollection SeriesCollection;
        public Func<double, string> Formatter { get; set; }
        public string[] Labels { get; set; }
        public Schedule(ref List<MachineSchedule> schedules)
        {
            InitializeComponent();
            _schedules = schedules;
            SeriesCollection = new SeriesCollection();

            for (int i = 0, j; ; i++)
            {
                List<int> values = new List<int>();
                SeriesCollection.Add(new StackedRowSeries
                {
                    Title = "",
                    Values = new ChartValues<int>(),
                    StackMode = StackMode.Values,
                    DataLabels = true
                });
                for (j = 0; j < _schedules.Count; j++)
                {
                    if (i < _schedules[j].Schedule.Count)
                    {
                        var value = _schedules[j].Schedule[i].CompleteAt - _schedules[j].Schedule[i].StartAt;

                        SeriesCollection[i].Values
                            .Add(value);
                        values.Add(value);
                    }
                    else
                         values.Add(0);
                }

                if (values.TrueForAll(v => v == 0))
                    break;
            }

            List<string> labels = new List<string>();
            foreach (var machineSchedule in _schedules)
            {
                labels.Add(machineSchedule.Id.ToString());
            }

            Labels = labels.ToArray();

            Formatter = val => val.ToString("G");
            DataContext = this;

            Chart.Series = SeriesCollection;
            ChartAxisY.LabelFormatter = Formatter;

        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            MachineSchedulesExport export = new MachineSchedulesExport(_schedules);

            ExportWindow exportWindow = new ExportWindow(export);
            exportWindow.Show();
        }
    }
}
