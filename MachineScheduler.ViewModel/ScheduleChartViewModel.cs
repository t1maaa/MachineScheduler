using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using MachineScheduler.DAL.Models;

namespace MachineScheduler.ViewModel
{
    public class ScheduleChartViewModel
    {
        public List<MachineSchedule> Schedules { get; set; }

        public SeriesCollection SeriesCollection { get; set; }

        public Func<double, string> Formatter { get; } = val => val.ToString("G");
        public string[] Labels { get; set; }

        public ScheduleChartViewModel()
        {
            SeriesCollection = new SeriesCollection();
        }
        
        private ICommand _drawStackedRow;
        public ICommand DrawStackedRow
        {
            get
            {
                return _drawStackedRow ??= new RelayCommand(data =>
                {
                   var jobs = Schedules.SelectMany(t => t.Schedule); //data as List<Job>;
                        
                        var mapper = Mappers.Xy<Job>().X((value, index) =>
                            value.CompleteAt - value.StartAt).Y((value, index) => value.MachineId);
                        Charting.For<Job>(mapper);
                        
                         SeriesCollection.Clear();
                        //  foreach (var s in SeriesCollection)
                        //  {
                        //      s.Values.Clear();
                        //      s.ActualValues.Clear();
                        //      s.Erase(true);
                        //      s.Values.CollectGarbage(s);
                        //      s.ActualValues.CollectGarbage(s);
                        //      
                        //  }

                        foreach (var job in jobs)
                        {
                            
                            SeriesCollection.Add(new StackedRowSeries
                            {
                                LabelsPosition = BarLabelPosition.Parallel,
                                StackMode = StackMode.Values,
                                MaxRowHeight = 75,
                                Values = new ChartValues<Job>
                                {
                                    job
                                },
                                Title = job.ConsignmentId.ToString(),
                                DataLabels = true,
                                LabelPoint = point => $"Cons Id: {point.SeriesView.Title}\nDuration: {point.X.ToString(CultureInfo.InvariantCulture)}\nNomenclature: {job.Nomenclature}"
                            });
                        }
                        Labels = Schedules.Select(machineSchedule => machineSchedule.Id.ToString()).ToArray();
                });
            }
        }
    }
}