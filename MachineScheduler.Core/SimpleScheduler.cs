using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MachineScheduler.DAL.Models;

namespace MachineScheduler.Core
{
    public class SimpleScheduler
    {
        private ObservableCollection<MachineSchedule> MachineSchedules { get; }
        public SimpleScheduler()
        {
            MachineSchedules = new ObservableCollection<MachineSchedule>();
        }

        public ObservableCollection<MachineSchedule> MakeSchedule(IEnumerable<Consignment> consignments, IEnumerable<Machine> machines,
            IEnumerable<Nomenclature> nomenclatures, IEnumerable<Operation> operations)
        {
            if (!consignments?.Any() ?? true) throw new ArgumentException("Consignments list null or empty");
            if (!machines?.Any() ?? true) throw new ArgumentException("Machines list null or empty");
            if (!nomenclatures?.Any() ?? true) throw new ArgumentException("Nomenclatures list null or empty");
            if (!operations?.Any() ?? true) throw new ArgumentException("Operations list null or empty");

            foreach (var machine in machines)
            {
                MachineSchedules.Add(new MachineSchedule(machine.Id));
            }

            foreach (var consignment in consignments)
            {
                #region Aggregated query

                /*
                 var newJob = 
                     (from m in 
                             (from suitableMachines 
                                     in (operations
                                         .Where(o => o.NomenclatureId == consignment.NomenclatureId)
                                         .Select(o => new {o.MachineId, o.Duration})
                                         .OrderBy(o => o.Duration)) 
                                 join ms in MachineSchedules on suitableMachines.MachineId equals ms.Id 
                                 select new { suitableMachines.MachineId, suitableMachines.Duration, ms.LastJob.CompleteAt }) 
                         select new Job { MachineId = m.MachineId, StartAt = m.CompleteAt, CompleteAt = m.CompleteAt + m.Duration,  ConsignmentId = consignment.Id })
                     .OrderBy(m => m.CompleteAt).First();
                     
                     MachineSchedules.FirstOrDefault(ms => ms.Id == newJob.MachineId)
                                ?.AddJob(newJob);
                 */

                #endregion

                #region Splitted query

                var machineCandidates = operations
                    .Where(o => o.NomenclatureId == consignment.NomenclatureId)
                    .Select(o => new { o.MachineId, o.Duration })
                    .OrderBy(o => o.Duration)
                    .ToList();

                if (machineCandidates.Count > 0)
                {
                    var machineCandidatesSchedules = (from mc in machineCandidates
                        join ms in MachineSchedules on mc.MachineId equals ms.Id
                        select ms).ToList();
                    
                    if (machineCandidatesSchedules.Count > 0)
                    {
                        var machineCandidatesLastJobs = machineCandidatesSchedules
                            .Select(mcs => new {mcs.LastJob.MachineId, mcs.LastJob.CompleteAt}).ToList();

                        if (machineCandidatesLastJobs.Count > 0)
                        {
                            var newJob = (from mclj in machineCandidatesLastJobs
                                    join mc in machineCandidates on mclj.MachineId equals mc.MachineId
                                    select (new Job { MachineId = mc.MachineId, StartAt = mclj.CompleteAt, CompleteAt = mc.Duration + mclj.CompleteAt, ConsignmentId = consignment.Id}))
                                .OrderBy(s => s.CompleteAt).First();

                            var nomenclature = nomenclatures.FirstOrDefault(t => t.Id == consignment.NomenclatureId)?.Name;
                            newJob.Nomenclature = nomenclature;
                            MachineSchedules.FirstOrDefault(ms => ms.Id == newJob.MachineId)
                                ?.AddJob(newJob);
                        }
                    }
                        
                }

                #endregion
            }
            return MachineSchedules;
        }
    }
}