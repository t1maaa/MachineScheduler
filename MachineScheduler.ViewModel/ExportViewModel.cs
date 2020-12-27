using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using MachineScheduler.DAL.Exporters;
using MachineScheduler.DAL.Models;
using MachineScheduler.DAL.Parsers;

namespace MachineScheduler.ViewModel
{
    public class ExportViewModel
    {
        private List<MachineSchedule> Schedules { get; set; }
        private string Filename { get; set; }
        public ExportMode ExportMode { get; set; }
        //public OrderBy //TODO: Export option order by
        public Dictionary<ExportMode, string> ExportModeDescription { get; set; }
        private ICommand _exportSchedule;

        public ExportViewModel(List<MachineSchedule> schedules)
        {
            Schedules = schedules;
            ExportModeDescription = new Dictionary<ExportMode, string>()
            {
                {ExportMode.Merged, "Single table with aggregated schedules for all machines"},
                {ExportMode.Paged, "Single file but each machine's schedule on a new page "},
                {ExportMode.Splitted, "Each machine's schedule in a new file"}
            };
        }
        
        public ICommand ExportSchedule
        {
            get
            {
                return _exportSchedule ??= new RelayCommand(obj =>
                {
                    if(!string.IsNullOrWhiteSpace(Filename))
                        new ScheduleExporterFactory().Create(Schedules, new FileInfo(Filename), ExportMode)?.Save();
                }, obj =>
                {
                    if (obj is not string filename) return false;
                    if (string.IsNullOrWhiteSpace(filename)) return false;
                    if (!ScheduleExporterFactory.IsSupportedFiletype(Path.GetExtension(filename))) return false;
                    Filename = filename;
                    return true;
                });
            }
        }
        
        public static string FileTypesFilter => SupportedFiletypes.GetFilterString();
    }
}