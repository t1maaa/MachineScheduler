using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using MachineScheduler.DAL.Exporters;
using MachineScheduler.DAL.Models;
using MachineScheduler.DAL.Parsers;

namespace MachineScheduler.ViewModel
{
    public abstract class ExportOptionsViewModel
    {
        public object ExportMode { get; set; } //TODO: rewrite enum to Enumeration base class
        //public OrderBy //TODO: Export option order by
        private ICommand _exportSchedule;
        protected List<MachineSchedule> Schedules { get; set; }
        protected string Filename { get; set; }
        
        protected ExportOptionsViewModel(List<MachineSchedule> schedules)
        {
            Schedules = schedules;
        }

        public ICommand ExportSchedule
        {
            get
            {
                return _exportSchedule ??= new RelayCommand(obj =>
                {
                    if(!string.IsNullOrWhiteSpace(Filename))
                        new ScheduleExporterFactory().Create(Schedules, new FileInfo(Filename), (ExcelExportMode)ExportMode)?.Save();
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