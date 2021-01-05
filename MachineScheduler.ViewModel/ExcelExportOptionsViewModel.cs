using System.Collections.Generic;
using MachineScheduler.DAL.Models;

namespace MachineScheduler.ViewModel
{
    public class ExcelExportOptionsViewModel : ExportOptionsViewModel
    {
        public Dictionary<ExcelExportMode, string> ExportModeDescription { get; set; }
        public ExcelExportOptionsViewModel(List<MachineSchedule> schedules) : base(schedules)
        {
            ExportModeDescription = new Dictionary<ExcelExportMode, string>()
            {
                { ExcelExportMode.Merged, "Single table with aggregated schedules for all machines" },
                { ExcelExportMode.Paged, "Single file but each machine's schedule on a new page " },
                { ExcelExportMode.Splitted, "Each machine's schedule in a new file" }
            };
        }
    }
}