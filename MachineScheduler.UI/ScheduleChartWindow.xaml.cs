using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using MachineScheduler.ViewModel;

namespace MachineScheduler.UI
{
    /// <summary>
    ///     Логика взаимодействия для ScheduleChartWindow.xaml
    /// </summary>
    public partial class ScheduleChartWindow : Window
    {
        private readonly ScheduleChartViewModel _scheduleChartViewModel;
        private readonly MainWindowViewModel _parent;
        private bool _scheduleChanged;

        public ScheduleChartWindow(MainWindowViewModel parent)
        {
            InitializeComponent();

            _parent = parent;
            _scheduleChanged = true;
            _scheduleChartViewModel = new ScheduleChartViewModel
            {
                Schedules = _parent.Schedules.ToList()
            };
            DataContext = _scheduleChartViewModel;

            Chart.Series = _scheduleChartViewModel.SeriesCollection;
            ChartAxisY.LabelFormatter = _scheduleChartViewModel.Formatter;

            var brush = new SolidColorBrush(Colors.DimGray);
            ChartAxisX.Foreground = brush;
            ChartAxisY.Foreground = brush;
            ChartAxisX.Separator.Stroke = brush;


            parent.PropertyChanged += (sender, args) =>
            {
                if (!args.PropertyName.Equals("Schedules")) return;
                _scheduleChanged = true;
                _scheduleChartViewModel.Schedules = _parent.Schedules.ToList();

                if (IsVisible) Draw();
            };
        }

        private void Draw()
        {
            if (!_scheduleChanged) return;
            
            _scheduleChartViewModel.DrawStackedRow.Execute(null);
            _scheduleChanged = false;
        }

        private void ScheduleChartWindow_OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            ((Window) sender).Hide();
        }

        private void ScheduleChartWindow_OnActivated(object? sender, EventArgs e)
        {
            //Chart.Update(true, true);
            Draw();
        }
    }
}