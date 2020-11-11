using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using KrasNonFerMet.Core;
using KrasNonFerMet.DAL.Extensions;
using KrasNonFerMet.DAL.Files.Excel;
using KrasNonFerMet.DAL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

namespace KrasNonFerMet.DAL.Tests
{
    [TestClass()]
    public class ExcelWriterTests
    {/*
        public static IEnumerable<object[]> GetMachineSchedule()
        {
            IExcelReader er = new ExcelReader(Data.File.Excel.Machines.Directory,
                Data.File.Excel.Machines.Filename);
            List<Machine> machines = er.GetWorksheets().FirstOrDefault().ToList<Machine>();

            er = new ExcelReader(Data.File.Excel.Consignment.Directory,
                Data.File.Excel.Consignment.Filename);
            List<Consignment> consignments = er.GetWorksheets().FirstOrDefault().ToList<Consignment>();

            er = new ExcelReader(Data.File.Excel.Operation.Directory,
                Data.File.Excel.Operation.Filename);
            List<Operation> operations = er.GetWorksheets().FirstOrDefault().ToList<Operation>();

            er = new ExcelReader(Data.File.Excel.Nomenclature.Directory,
                Data.File.Excel.Nomenclature.Filename);
            List<Nomenclature> nomenclatures = er.GetWorksheets().FirstOrDefault().ToList<Nomenclature>();

            var result = new SimpleScheduler().MakeSchedule(consignments, machines, nomenclatures, operations);

            foreach (var machineSchedule in result)
            {
                yield return new object[] { machineSchedule };
            }
        }
        */
        public static IEnumerable<object[]> GetMachineSchedulesList()
        {
            IExcelReader er = new ExcelReader(Data.File.Excel.Machines.Directory,
                Data.File.Excel.Machines.Filename);
            List<Machine> machines = er.GetWorksheets().FirstOrDefault().ToList<Machine>();

            er = new ExcelReader(Data.File.Excel.Consignment.Directory,
                Data.File.Excel.Consignment.Filename);
            List<Consignment> consignments = er.GetWorksheets().FirstOrDefault().ToList<Consignment>();

            er = new ExcelReader(Data.File.Excel.Operation.Directory,
                Data.File.Excel.Operation.Filename);
            List<Operation> operations = er.GetWorksheets().FirstOrDefault().ToList<Operation>();

            er = new ExcelReader(Data.File.Excel.Nomenclature.Directory,
                Data.File.Excel.Nomenclature.Filename);
            List<Nomenclature> nomenclatures = er.GetWorksheets().FirstOrDefault().ToList<Nomenclature>();

            var result = new SimpleScheduler().MakeSchedule(consignments, machines, nomenclatures, operations);

            yield return new object[] { result };
        }

        [DataTestMethod]
        [DynamicData(nameof(GetMachineSchedulesList), DynamicDataSourceType.Method)]
        [TestMethod()]
        public void ExcelWriterTest_MachinePerFile(List<MachineSchedule> machineSchedules)
        {
            IExcelSchedulesExport<MachineSchedule> ew = new MachineSchedulesExport(machineSchedules);
            ew.ExportMode = ExportMode.Splitted;
            ew.SaveAs(new FileInfo($"{Data.File.Excel.ExportDirectory + $"\\Schedule"}"));
        }

        [DataTestMethod]
        [DynamicData(nameof(GetMachineSchedulesList), DynamicDataSourceType.Method)]
        [TestMethod()]
        public void ExcelWriterTest_MachinePerSheet(List<MachineSchedule> machineSchedules)
        {
            IExcelSchedulesExport<MachineSchedule> ew = new MachineSchedulesExport(machineSchedules);
            ew.ExportMode = ExportMode.Paged;
            ew.SaveAs(new FileInfo($"{Data.File.Excel.ExportDirectory + $"\\SchedulePaged.xlsx"}"));
        }

        [DataTestMethod]
        [DynamicData(nameof(GetMachineSchedulesList), DynamicDataSourceType.Method)]
        [TestMethod()]
        public void ExcelWriterTest_Aggregated(List<MachineSchedule> machineSchedules)
        {
            IExcelSchedulesExport<MachineSchedule> ew = new MachineSchedulesExport(machineSchedules);
            ew.ExportMode = ExportMode.Merged;
            ew.SaveAs(new FileInfo($"{Data.File.Excel.ExportDirectory + "\\ScheduleAggregated.xlsx"}")); 
        }
    }
}