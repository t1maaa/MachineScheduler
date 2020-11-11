using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using KrasNonFerMet.DAL.Extensions;
using KrasNonFerMet.DAL.Files.Excel;
using KrasNonFerMet.DAL.Models;
using KrasNonFerMet.DAL.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KrasNonFerMet.Core.Tests
{
    [TestClass()]
    public class SimpleSchedulerTests
    {
        [TestMethod()]
        public void MakeScheduleTest()
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

            var ss = new SimpleScheduler();
           var result = ss.MakeSchedule(consignments, machines, nomenclatures, operations);

           if (result.Count > 0) //TODO: compare with trusted sample 
           {
               foreach (var r in result)
               {
                   Trace.WriteLine($"Machine  {r.Id} - {r.Schedule.Count} jobs");
                   foreach (var job in r.Schedule)
                   {
                       Trace.WriteLine(new
                       {
                           job.MachineId,
                           job.ConsignmentId,
                           StartedAt = job.StartAt,
                           CompletedAt = job.CompleteAt
                       });
                   }
               }
                
           }
        }
    }
}