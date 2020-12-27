using System.Collections.Generic;
using System.IO;
using MachineScheduler.DAL.Models;

namespace MachineScheduler.DAL.Exporters
{
    public abstract class ScheduleExporter : IScheduleExporterStrategy
    {
        private protected readonly FileInfo FileInfo;
        private protected readonly List<MachineSchedule> Schedules;
        public abstract void Save(); //TODO: OrderBy(prop)

        protected ScheduleExporter(List<MachineSchedule> schedules, FileInfo fileInfo)
        {
            Schedules = schedules;
            FileInfo = fileInfo;
        }
    }
}