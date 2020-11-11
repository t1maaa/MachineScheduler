using System;
using System.Collections.Generic;
using System.Linq;
using KrasNonFerMet.DAL.Files.Excel.Reports;
using KrasNonFerMet.DAL.Models;

namespace KrasNonFerMet.Core
{
    public class SimpleScheduler
    {
        private List<MachineSchedule> MachineSchedules { get; set; }
        public SimpleScheduler()
        {
            MachineSchedules = new List<MachineSchedule>();
        }

        public List<MachineSchedule> MakeSchedule(List<Consignment> consignments, List<Machine> machines,
            List<Nomenclature> nomenclatures, List<Operation> operations)
        {
            if (!consignments?.Any() ?? true) throw new ArgumentException("Consignments list null or empty");
            if (!machines?.Any() ?? true) throw new ArgumentException("Machines list null or empty");
            if (!nomenclatures?.Any() ?? true) throw new ArgumentException("Nomenclatures list null or empty");
            if (!operations?.Any() ?? true) throw new ArgumentException("Operations list null or empty");
            
            foreach (var machine in machines)
            {
                MachineSchedules.Add(new MachineSchedule(machine.Id));
            }
            //TODO: try optimize var names and so on..
            foreach (var consignment in consignments)
            {
                var machineCandidates = operations //rename and refactor
                    .Where(o => o.NomenclatureId == consignment.NomenclatureId)
                    .Select(o => new {o.MachineId, o.Duration})
                    .OrderBy(o => o.Duration).ToList();

                var machineCandidatesSchedules = (from mc in machineCandidates
                    join ms in MachineSchedules on mc.MachineId equals ms.Id
                    select ms).ToList();

                var machineCandidatesLastJobs = machineCandidatesSchedules //rename
                    .Select(mcs => new { mcs.LastJob.MachineId, mcs.LastJob.CompleteAt } ).ToList();

                var newJob = (from mclj in machineCandidatesLastJobs
                    join mc in machineCandidates on mclj.MachineId equals mc.MachineId
                    select (new { mc.MachineId, StartAt = mclj.CompleteAt, CompleteAt = mc.Duration + mclj.CompleteAt })).OrderBy(s => s.CompleteAt).First();

                MachineSchedules.FirstOrDefault(ms => ms.Id == newJob.MachineId)
                    ?.AddJob(new Job
                    {
                        CompleteAt = newJob.CompleteAt,
                        ConsignmentId = consignment.Id,
                        MachineId = newJob.MachineId,
                        StartAt = newJob.StartAt
                    });
            }

            return MachineSchedules; //TODO: default order by consignment?
        }
        public void Export()
        {
            ExcelExport ee = new ExcelExport();
            
        }
    }
}