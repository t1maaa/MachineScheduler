using System.Collections.Generic;
using System.IO;
using MachineScheduler.DAL.Models;

namespace MachineScheduler.DAL.Exporters
{
    public interface IScheduleExporterFactory
    {
        IScheduleExporterStrategy Create(List<MachineSchedule> schedules, FileInfo fileInfo, object exportMode);
    }
    
}