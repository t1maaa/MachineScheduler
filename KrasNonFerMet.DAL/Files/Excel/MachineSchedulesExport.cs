using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KrasNonFerMet.DAL.Models;
using OfficeOpenXml;

namespace KrasNonFerMet.DAL.Files.Excel
{
    public class MachineSchedulesExport : IExcelSchedulesExport<MachineSchedule>
    {
        public ExportMode ExportMode { get; set; }
        public string DirectoryPath { get; set; }
        public string Filename { get; set; }

        public List<MachineSchedule> Schedules { get; set; }

        public ExcelPackage ExcelPackage { get; set; }

        public MachineSchedulesExport(List<MachineSchedule> schedules)
        {
            Schedules = schedules;
        }

        public void SaveAs(FileInfo file)
        {
            switch (ExportMode) 
            {
                case ExportMode.Merged:
                    ExcelPackage = new ExcelPackage();
                    var flattedSchedule = Schedules.SelectMany(i => i.Schedule);//TODO: OrderBy(prop) and put this prop as first column (.Select new{}?)
                    
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
                        ExcelPackage.SaveAs(new FileInfo($"{file.FullName + $"_{machineSchedule.Id}.xlsx"}"));
                    }
                   
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }
}