using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MachineScheduler.DAL.Models;
using OfficeOpenXml;

namespace MachineScheduler.DAL.Exporters
{
    public sealed class ScheduleExporterExcel : ScheduleExporter
    {
        private readonly ExcelPackage _package;
        private readonly ExcelExportMode _excelExportMode;

        public ScheduleExporterExcel(List<MachineSchedule> schedules, FileInfo fileInfo, ExcelExportMode excelExportMode) : base(schedules, fileInfo)
        {
            _excelExportMode = excelExportMode; 
            _package = new ExcelPackage();
        }

        private void SaveMerged()
        {
            var flattedSchedule = Schedules.SelectMany(i => i.Schedule);
            
            _package.Workbook.Worksheets.Add("Schedule")
                .Cells["A1"].LoadFromCollection(flattedSchedule, true);
            _package.SaveAs(FileInfo);
        }

        private void  SavePaged()
        {
            foreach (var machineSchedule in Schedules)
            {
                _package.Workbook.Worksheets.Add($"Machine_{machineSchedule.Id}")
                    .Cells["A1"].LoadFromCollection(machineSchedule.Schedule, true);
            }
            _package.SaveAs(FileInfo);
        }

        private void SaveSplitted()
        {
            foreach (var machineSchedule in Schedules)
            {
                _package.Workbook.Worksheets.Add($"Schedule_{machineSchedule.Id}")
                    .Cells["A1"].LoadFromCollection(machineSchedule.Schedule, true);
                Directory.CreateDirectory(FileInfo.DirectoryName);
                _package.SaveAs(new FileInfo($"{ FileInfo.FullName.Substring(0, FileInfo.FullName.Length - FileInfo.Extension.Length) + $"_{machineSchedule.Id}.xlsx"}"));
            }
        }
        
        public override void Save()
        {
            switch (_excelExportMode)
            {
                case ExcelExportMode.Merged: 
                    SaveMerged();
                    return;
                case ExcelExportMode.Paged: 
                    SavePaged();
                    return;
                case ExcelExportMode.Splitted: 
                    SaveSplitted();
                    return; 
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}