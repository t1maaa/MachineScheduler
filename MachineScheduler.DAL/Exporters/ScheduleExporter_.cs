using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MachineScheduler.DAL.Models;
using OfficeOpenXml;

namespace MachineScheduler.DAL.Exporters
{
    public class ScheduleExporter_
    {
        private readonly ExportMode _exportMode;

        public string DirectoryName { get; set; }
        public string Filename { get; set; }

        public List<MachineSchedule> Schedules { get; set; }

        public ExcelPackage ExcelPackage { get; set; }

        public ScheduleExporter_(List<MachineSchedule> schedules, ExportMode exportMode)
        {
            Schedules = schedules;
            _exportMode = exportMode;
        }

        public void SaveAs(FileInfo file)
        {
            switch (_exportMode) 
            {
                case ExportMode.Merged:
                    ExcelPackage = new ExcelPackage();
                    var flattedSchedule = Schedules.SelectMany(i => i.Schedule);
                    
                    ExcelPackage.Workbook.Worksheets.Add("Schedule")
                        .Cells["A1"].LoadFromCollection(flattedSchedule, true);
                    ExcelPackage.SaveAs(file);
                    return;

                case ExportMode.Paged:
                    ExcelPackage = new ExcelPackage();
                    foreach (var machineSchedule in Schedules)
                    {
                        ExcelPackage.Workbook.Worksheets.Add($"Machine_{machineSchedule.Id}")
                            .Cells["A1"].LoadFromCollection(machineSchedule.Schedule, true);
                    }
                    ExcelPackage.SaveAs(file);
                    return;

                case ExportMode.Splitted:
                    foreach (var machineSchedule in Schedules)
                    {
                        ExcelPackage = new ExcelPackage();
                        ExcelPackage.Workbook.Worksheets.Add($"Schedule_{machineSchedule.Id}")
                            .Cells["A1"].LoadFromCollection(machineSchedule.Schedule, true);
                        Directory.CreateDirectory(file.DirectoryName);
                        ExcelPackage.SaveAs(new FileInfo($"{ file.FullName.Substring(0, file.FullName.Length - file.Extension.Length) + $"_{machineSchedule.Id}.xlsx"}"));
                    }
                   
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}