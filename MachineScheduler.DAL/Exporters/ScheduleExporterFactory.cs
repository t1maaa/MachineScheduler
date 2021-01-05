using System;
using System.Collections.Generic;
using System.IO;
using MachineScheduler.DAL.Models;
using MachineScheduler.DAL.Parsers;

namespace MachineScheduler.DAL.Exporters
{
    public class ScheduleExporterFactory : IScheduleExporterFactory
    {
        public IScheduleExporterStrategy Create(List<MachineSchedule> schedules, FileInfo fileInfo, object exportMode) =>
            IsSupportedFiletype(fileInfo.Extension) ?
            fileInfo.Extension switch
            {
                ".xlsx" => new ScheduleExporterExcel(schedules, fileInfo, (ExcelExportMode)exportMode),
               // ".csv" => throw new NotImplementedException(),
                _ => throw new NotImplementedException($"{fileInfo.Extension} is not supported yet")
            } : null;

        public static bool IsSupportedFiletype(string extension)
        {
            return !string.IsNullOrWhiteSpace(extension) && SupportedFiletypes.IsSupported(extension);
        }

    }
}